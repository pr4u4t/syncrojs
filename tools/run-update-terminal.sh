#!/bin/sh
set -eu

repo_root=$(CDPATH= cd -- "$(dirname -- "$0")/.." && pwd)
update_script="$repo_root/tools/update-production.sh"

if command -v konsole >/dev/null 2>&1; then
    exec konsole --hold -e sh "$update_script"
fi
if command -v x-terminal-emulator >/dev/null 2>&1; then
    exec x-terminal-emulator -e sh "$update_script"
fi
if command -v xterm >/dev/null 2>&1; then
    exec xterm -hold -e sh "$update_script"
fi
if command -v kdialog >/dev/null 2>&1; then
    kdialog --error 'Nie znaleziono terminala (Konsole, x-terminal-emulator ani xterm).'
fi
exit 1
