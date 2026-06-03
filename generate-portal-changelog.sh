#!/usr/bin/env bash
#
# Generate the developer-portal changelog files (one .md per release).
#
#   ./generate-portal-changelog.sh            # regenerate every version
#   ./generate-portal-changelog.sh --latest   # only the newest version
#
# Any extra arguments are forwarded to git-cliff as-is (e.g. --latest,
# --current, --tag vX.Y.Z), so the command stays as simple as it looks.
#
set -euo pipefail

# --- settings (edit here, not on the command line) ---
CONFIG="cliff-portal.toml"
OUTDIR="portal-changelog"
# ------------------------------------------------------

mkdir -p "$OUTDIR"

# git-cliff renders all selected releases into one stream, each prefixed with
# a "@@@FILE <version>@@@" marker; awk splits that into one file per version.
# "$@" forwards flags like --latest straight through.
git-cliff --config "$CONFIG" "$@" | awk -v outdir="$OUTDIR" '
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

# Tidy up: trim leading/trailing blank lines and drop any entry-less file
# (only the very first tag in history, which has no predecessor).
for f in "$OUTDIR"/*.md; do
    [ -e "$f" ] || continue
    awk 'NF{p=1} p' "$f" | tac | awk 'NF{p=1} p' | tac > "$f.tmp" && mv "$f.tmp" "$f"
    if ! grep -q '^### ' "$f"; then
        echo "skip (no entries): $(basename "$f")"
        rm -f "$f"
    fi
done

echo "Generated files in $OUTDIR/:"
ls -1 "$OUTDIR"