#!/bin/sh
set -eu

repo_root=$(CDPATH= cd -- "$(dirname -- "$0")/.." && pwd)
cd "$repo_root"

if ! command -v git >/dev/null 2>&1; then
    printf '%s\n' 'Git is not installed.' >&2
    exit 1
fi
if ! command -v make >/dev/null 2>&1; then
    printf '%s\n' 'make is not installed.' >&2
    exit 1
fi
if ! git rev-parse --is-inside-work-tree >/dev/null 2>&1; then
    printf '%s\n' 'This directory is not a Git checkout.' >&2
    exit 1
fi

upstream=$(git rev-parse --abbrev-ref --symbolic-full-name '@{upstream}' 2>/dev/null || true)
if [ -z "$upstream" ]; then
    printf '%s\n' 'The current branch has no Git upstream configured.' >&2
    exit 1
fi

if [ -n "$(git status --porcelain --untracked-files=normal)" ]; then
    printf '%s\n' 'The checkout has local changes. Commit or stash them before updating.' >&2
    git status --short
    exit 1
fi

before=$(git rev-parse HEAD)
printf 'Fetching updates for %s...\n' "$upstream"
git fetch --prune
git pull --ff-only

after=$(git rev-parse HEAD)
if [ "$before" = "$after" ]; then
    printf 'Already up to date at %s. Rebuilding and applying production anyway.\n' "$(git rev-parse --short "$after")"
else
    git update-ref refs/syncro-production/previous "$before"
    printf 'Updated %s -> %s. Previous version saved for rollback.\n' "$(git rev-parse --short "$before")" "$(git rev-parse --short "$after")"
fi

make update-production
printf '%s\n' 'Production update completed.'
