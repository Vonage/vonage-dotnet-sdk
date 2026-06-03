#!/usr/bin/env bash
#
# Generate one changelog file per release for the developer portal.
# git-cliff renders every release in a single stream, each prefixed with a
# "@@@FILE <version>@@@" marker; awk demuxes that stream into per-version files.
#
set -euo pipefail

CONFIG="${1:-cliff-portal.toml}"
OUTDIR="${2:-portal-changelog}"
# Extra args passed straight to git-cliff, e.g. CLIFF_ARGS="--latest" for CI.
CLIFF_ARGS="${CLIFF_ARGS:-}"

mkdir -p "$OUTDIR"

# shellcheck disable=SC2086
git-cliff --config "$CONFIG" $CLIFF_ARGS | awk -v outdir="$OUTDIR" '
    /^@@@FILE / {
        if (out != "") close(out)
        ver = $2
        sub(/@@@$/, "", ver)
        out = outdir "/" ver ".md"
        printf "" > out          # truncate/create the file
        next
    }
    out != "" { print >> out }
'

# Tidy up: trim trailing blank lines, drop empty files (e.g. the very first
# tag, which may have no preceding range).
for f in "$OUTDIR"/*.md; do
    # remove leading/trailing blank lines
    awk 'NF{p=1} p' "$f" | tac | awk 'NF{p=1} p' | tac > "$f.tmp" && mv "$f.tmp" "$f"
    # delete the file if it has no body (only frontmatter or empty)
    if ! grep -q '^### ' "$f"; then
        echo "skip (no entries): $(basename "$f")"
        rm -f "$f"
    fi
done

echo "Generated files in $OUTDIR/:"
ls -1 "$OUTDIR"
