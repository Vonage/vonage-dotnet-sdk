#!/bin/bash
set -e

VERSION=""
DRY_RUN=false

# Parse arguments
while [[ $# -gt 0 ]]; do
    case $1 in
        --dry-run)
            DRY_RUN=true
            shift
            ;;
        *)
            VERSION=$1
            shift
            ;;
    esac
done

# Validate version
if [[ ! $VERSION =~ ^[0-9]+\.[0-9]+\.[0-9]+(-[a-zA-Z0-9]+)?$ ]]; then
    echo "Usage: ./release.sh <version> [--dry-run]"
    echo ""
    echo "Arguments:"
    echo "  version     Version number (e.g., 8.30.0 or 8.30.0-beta)"
    echo "  --dry-run   Preview changes without executing"
    exit 1
fi

TAG="v$VERSION"
CSPROJ="Vonage/Vonage.csproj"

# Check GitHub CLI authentication before doing anything
echo "Checking GitHub CLI authentication..."
if ! gh auth status &>/dev/null; then
    echo "Error: Not authenticated with GitHub CLI."
    echo "Please run: gh auth login"
    exit 1
fi
echo "GitHub CLI authenticated."
echo ""

# Dry run mode
if [ "$DRY_RUN" = true ]; then
    echo "=== DRY RUN MODE ==="
    echo ""
    echo "Version: $VERSION"
    echo "Tag: $TAG"
    echo ""
    echo "Changes to $CSPROJ:"
    echo "  - Version -> $VERSION"
    echo "  - PackageReleaseNotes -> https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/$TAG"
    echo ""
    echo "Commands that would run:"
    echo "  1. git add $CSPROJ"
    echo "  2. git commit -m \"docs: bump version to $TAG\""
    echo "  3. git tag -f $TAG"
    echo "  4. git cliff -o CHANGELOG.md"
    echo "  5. git add CHANGELOG.md"
    echo "  6. git commit -m \"docs: generate changelog for $TAG\""
    echo "  7. git push"
    echo "  8. git push origin $TAG --force"
    echo "  9. gh release create $TAG --notes-file RELEASE_NOTES.md --title $TAG"
    echo ""
    echo "Changelog preview for this release:"
    echo "---"
    git cliff --unreleased --strip header 2>/dev/null || echo "(no unreleased changes detected)"
    echo "---"
    exit 0
fi

# Show summary and ask for confirmation
echo "=== Release Summary ==="
echo ""
echo "Version: $VERSION"
echo "Tag: $TAG"
echo ""
echo "This will:"
echo "  1. Update version in $CSPROJ"
echo "  2. Commit and tag"
echo "  3. Generate changelog"
echo "  4. Push to remote"
echo "  5. Create GitHub release"
echo ""
read -p "Proceed with release? [y/N] " -n 1 -r
echo ""

if [[ ! $REPLY =~ ^[Yy]$ ]]; then
    echo "Aborted."
    exit 1
fi

echo ""
echo "Starting release..."
echo ""

# Update version in .csproj
echo "Updating $CSPROJ..."
sed -i "s|<Version>.*</Version>|<Version>$VERSION</Version>|" "$CSPROJ"
sed -i "s|<PackageReleaseNotes>.*</PackageReleaseNotes>|<PackageReleaseNotes>https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/$TAG</PackageReleaseNotes>|" "$CSPROJ"

# Commit and tag
echo "Committing version bump..."
git add "$CSPROJ"
git commit -m "docs: bump version to $TAG"

echo "Creating tag $TAG..."
git tag -f "$TAG"

# Generate changelog
echo "Generating changelog..."
git cliff -o CHANGELOG.md
git add CHANGELOG.md
git commit -m "docs: generate changelog for $TAG"

# Push
echo "Pushing to remote..."
git push
git push origin "$TAG" --force

# Create GitHub release
echo "Creating GitHub release..."
git cliff --latest --strip header -o RELEASE_NOTES.md
gh release create "$TAG" --notes-file RELEASE_NOTES.md --title "$TAG"
rm RELEASE_NOTES.md

echo ""
echo "Done! Released $TAG"
echo "https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/$TAG"
