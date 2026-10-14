"""Install the rebuilt assembly into an explicitly selected local player copy."""
from pathlib import Path
import argparse, hashlib
parser = argparse.ArgumentParser()
parser.add_argument('--player', required=True, help='A separate copy of the Unity player')
parser.add_argument('--assembly', default=str(Path(__file__).resolve().parent.parent / 'build/Assembly-CSharp.dll'))
args = parser.parse_args()
source = Path(args.assembly).resolve()
player = Path(args.player).resolve()
target = player / '开发项目_Data/Managed/Assembly-CSharp.dll'
assert target.is_file(), 'Player assembly not found'
data = source.read_bytes()
assert data.startswith(b'MZ') and len(data) > 1024, 'Expected a decoded managed PE assembly'
backup = player / ('Assembly-CSharp.before-source-build.' + hashlib.sha256(target.read_bytes()).hexdigest()[:12] + '.bak')
if not backup.exists():
    backup.write_bytes(target.read_bytes())
encoded = bytes((v + 0x9d) & 255 for v in data[:1024]) + data[1024:]
target.write_bytes(encoded)
print('Installed:', target)
print('SHA256:', hashlib.sha256(encoded).hexdigest())
