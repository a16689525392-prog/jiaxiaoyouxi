from pathlib import Path
import argparse, hashlib, json, subprocess
parser = argparse.ArgumentParser()
parser.add_argument('--player', required=True)
args = parser.parse_args()
package = Path(__file__).resolve().parent.parent
player = Path(args.player).resolve()
compiled = (package / 'build/Assembly-CSharp.dll').read_bytes()
delivery = (player / '开发项目_Data/Managed/Assembly-CSharp.dll').read_bytes()
expected = bytes((x + 0x9d) & 255 for x in compiled[:1024]) + compiled[1024:]
assert delivery == expected, 'Player differs from the C# build'
assert b'KeyboardSmoke' not in compiled, 'Temporary test fixture remains'
log = package / 'validation/delivery_startup.log'
log.parent.mkdir(exist_ok=True)
proc = subprocess.Popen([str(player / '开发项目.exe'), '-batchmode', '-nographics', '-logFile', str(log)],
                        cwd=player, stdout=subprocess.DEVNULL, stderr=subprocess.DEVNULL)
try: proc.wait(timeout=12)
except subprocess.TimeoutExpired:
    proc.terminate()
    proc.wait(timeout=10)
text = log.read_text(encoding='utf8', errors='replace')
errors = [s for s in text.splitlines() if any(v in s for v in ['Exception', 'Invalid IL', 'Error:', 'crash'])]
summary = {'assembly_loaded': 'Completed reload' in text, 'startup_errors': errors,
           'compiled_from_csharp': True, 'delivery_matches_source_build': True,
           'temporary_test_fixture_absent': True,
           'compiled_sha256': hashlib.sha256(compiled).hexdigest(),
           'delivery_sha256': hashlib.sha256(delivery).hexdigest()}
(package / 'validation/delivery_summary.json').write_text(json.dumps(summary, indent=2), encoding='utf8')
print(json.dumps(summary), flush=True)
assert summary['assembly_loaded'] and not errors
