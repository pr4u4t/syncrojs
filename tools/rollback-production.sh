#!/bin/sh
set -eu

repo_root=$(CDPATH= cd -- "$(dirname -- "$0")/.." && pwd)
cd "$repo_root"

if ! git rev-parse --is-inside-work-tree >/dev/null 2>&1; then
    printf '%s\n' 'This directory is not a Git checkout.' >&2
    exit 1
fi
if ! command -v make >/dev/null 2>&1; then
    printf '%s\n' 'make is not installed.' >&2
    exit 1
fi
if [ -n "$(git status --porcelain --untracked-files=normal)" ]; then
    printf '%s\n' 'The checkout has local changes. Commit or stash them before rollback.' >&2
    git status --short
    exit 1
fi

previous=$(git rev-parse --verify refs/syncro-production/previous 2>/dev/null || true)
if [ -z "$previous" ]; then
    printf '%s\n' 'No previous version is recorded. Run an update through this updater first.' >&2
    exit 1
fi
current=$(git rev-parse HEAD)
if [ "$current" = "$previous" ]; then
    printf 'Already on the recorded previous version (%s).\n' "$(git rev-parse --short "$current")"
    exit 0
fi

printf 'Current version:  %s\n' "$(git rev-parse --short "$current")"
printf 'Restore version:  %s\n' "$(git rev-parse --short "$previous")"
printf '%s\n' 'This restores application code only. PostgreSQL migrations and data are not rolled back.'
if [ "${1:-}" != '--confirmed' ]; then
    if [ "$#" -gt 0 ]; then
        printf '%s\n' 'Usage: rollback-production.sh [--confirmed]' >&2
        exit 2
    fi
    printf 'Type ROLLBACK to continue: '
    read -r confirmation
    if [ "$confirmation" != 'ROLLBACK' ]; then
        printf '%s\n' 'Rollback cancelled.'
        exit 1
    fi
fi

git update-ref refs/syncro-production/rollback-from "$current"
git reset --hard "$previous"
make update-production
printf '%s\n' 'Rollback deployment completed.'
