"""Compile a temporary key-input fixture, test a real training scene, restore the delivery."""
from pathlib import Path
import argparse, hashlib, json, re, shutil, subprocess, tempfile
parser = argparse.ArgumentParser()
parser.add_argument('--player', required=True)
parser.add_argument('--pwsh', default='pwsh')
args = parser.parse_args()
package = Path(__file__).resolve().parent.parent
player = Path(args.player).resolve()
managed = player / '开发项目_Data/Managed'
assembly = managed / 'Assembly-CSharp.dll'
saved = assembly.read_bytes()
def replace_body(text, declaration, body):
    start = text.index(declaration)
    brace = text.index('{', start)
    level, end = 1, brace + 1
    while level:
        level += (text[end] == '{') - (text[end] == '}')
        end += 1
    return text[:brace+1] + '\n' + body + '\n' + text[end-1:]

fixture = r'''using System;
using System.Globalization;
using Car;
using UnityEngine;
internal static class KeyboardSmoke
{
    private static float lastSample;
    private static bool Range(float lo, float hi) { return Time.time >= lo && Time.time < hi; }
    public static bool GetKey(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.F: return true;
            case KeyCode.Alpha1: return Range(0, 16);
            case KeyCode.W: return Range(2, 10) || Range(16, 24);
            case KeyCode.S: return Range(10, 16) || Range(24, 28);
            case KeyCode.A: return Range(6, 8);
            case KeyCode.D: return Range(8, 10);
            case KeyCode.R: return Range(16, 24);
            case KeyCode.N: return Range(28, 50);
            case KeyCode.Space: return Range(28, 30);
            case KeyCode.LeftShift: return Range(30, 32);
            case KeyCode.H: return Range(32, 34);
            default: return false;
        }
    }
    public static bool GetKeyDown(KeyCode key) { return GetKey(key); }
    private static string Number(float value) { return value.ToString("R", CultureInfo.InvariantCulture); }
    public static void Record()
    {
        if (Time.time - lastSample < .1f) return;
        lastSample = Time.time;
        CarSignal s = SignalSourceCtl.Instance.circuitBoardSignal;
        float force = MoNiCarDynamic.Instance.dynamicData.driveForce;
        Debug.Log("KEYBOARD_SMOKE " + Number(Time.time) + " " + Number(CarState.Instance.CurSpeed)
            + " " + Number(s.oil) + " " + Number(s.footBrake) + " " + Number(s.steerWheel)
            + " " + Number(s.cluth) + " " + Number(s.handBrake) + " " + s.gear
            + " " + s.fire + " " + s.horn + " " + Number(force));
    }
}'''
log = package / 'validation/source_keyboard_smoke.log'
log.parent.mkdir(exist_ok=True)
proc = None
try:
    with tempfile.TemporaryDirectory(prefix='keyboard-source-test-') as folder:
        stage = Path(folder)
        sources = stage / 'src'
        shutil.copytree(package / 'src/Game', sources)
        path = sources / 'Scene/MainScene.cs'
        body = '''UnityEngine.Debug.Log("KEYBOARD_SCENE_ENTER");
View.ViewJumpCtl.Instance.trainType = (View.TrainType)4;
View.ViewJumpCtl.Instance.modelType = 2;
View.ViewJumpCtl.Instance.itemType = 0;
Global.SettingInfoCtl.Instance.isManualGear = true;
NetworkManagement.Instance.IsYouKeModel = true;
View.ViewJumpCtl.Instance.EnterScene();'''
        path.write_text(replace_body(path.read_text(encoding='utf8'), 'void Start()', body), encoding='utf8')
        path = sources / 'Car/SignalSourceCtl.cs'
        path.write_text(path.read_text(encoding='utf8').replace('Input.GetKey(', 'KeyboardSmoke.GetKey(').replace('Input.GetKeyDown(', 'KeyboardSmoke.GetKeyDown('), encoding='utf8')
        path = sources / 'Car/CarUserControl.cs'
        text = path.read_text(encoding='utf8')
        start = text.index('void FixedUpdate()')
        brace = text.index('{', start)
        path.write_text(text[:brace+1] + '\nKeyboardSmoke.Record();\n' + text[brace+1:], encoding='utf8')
        (sources / 'KeyboardSmoke.cs').write_text(fixture, encoding='utf8')
        compiled = stage / 'build'
        subprocess.run([args.pwsh, '-NoProfile', '-File', str(package / 'tools/build.ps1'),
                        '-ManagedDirectory', str(managed), '-SourceDirectory', str(sources),
                        '-OutputDirectory', str(compiled)], check=True)
        raw = (compiled / 'Assembly-CSharp.dll').read_bytes()
        assembly.write_bytes(bytes((v + 0x9d) & 255 for v in raw[:1024]) + raw[1024:])
        proc = subprocess.Popen([str(player / '开发项目.exe'), '-batchmode', '-nographics', '-logFile', str(log)],
                                cwd=player, stdout=subprocess.DEVNULL, stderr=subprocess.DEVNULL)
        print('SOURCE TEST PID', proc.pid, flush=True)
        try: proc.wait(timeout=40)
        except subprocess.TimeoutExpired:
            proc.terminate()
            proc.wait(timeout=10)
finally:
    if proc and proc.poll() is None:
        proc.terminate()
        proc.wait(timeout=10)
    assembly.write_bytes(saved)

text = log.read_text(encoding='utf8', errors='replace')
samples = []
fields = ['time', 'speed', 'oil', 'brake', 'steer', 'clutch', 'handbrake', 'gear', 'fire', 'horn', 'force']
for line in text.splitlines():
    if line.startswith('KEYBOARD_SMOKE '):
        samples.append(dict(zip(fields, map(float, line.split()[1:]))))
errors = [s for s in text.splitlines() if any(v in s for v in ['Exception', 'Invalid IL', 'Error:', 'crash'])]
checks = {'scene_loaded': 'KEYBOARD_SCENE_ENTER' in text, 'car_created': bool(samples),
          'forward_movement': any(s['gear'] == 1 and s['speed'] > 5 for s in samples),
          'brake_mapping': any(s['brake'] == 1 and s['oil'] == 0 for s in samples),
          'left_turn_mapping': any(s['steer'] < 0 for s in samples),
          'right_turn_mapping': any(s['steer'] > 0 for s in samples),
          'reverse_movement': any(s['gear'] == 6 and s['force'] < 0 and s['speed'] > 2 for s in samples),
          'handbrake_mapping': any(s['handbrake'] == 1 for s in samples),
          'clutch_mapping': any(s['clutch'] == 1 for s in samples),
          'neutral_mapping': any(s['gear'] == 0 for s in samples),
          'horn_mapping': any(s['horn'] == 1 for s in samples),
          'no_runtime_errors': not errors, 'manual_build_restored': assembly.read_bytes() == saved}
summary = {'input_simulated': True, 'compiled_from_csharp': True, 'checks': checks,
           'sample_count': len(samples), 'max_speed_kmh': max((s['speed'] for s in samples), default=0),
           'delivery_sha256': hashlib.sha256(saved).hexdigest(), 'errors': errors}
(package / 'validation/source_keyboard_summary.json').write_text(json.dumps(summary, ensure_ascii=False, indent=2), encoding='utf8')
print(json.dumps(summary, ensure_ascii=False), flush=True)
assert all(checks.values()), checks
