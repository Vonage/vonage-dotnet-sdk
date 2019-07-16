workflow "New release" {
  on = "release"
  resolves = ["Add Changelog"]
}

action "Add Changelog" {
  uses = "nexmo/github-actions/nexmo-changelog@master"
  env = {
    CHANGELOG_CATEGORY = "Server SDK"
    CHANGELOG_SUBCATEGORY = "dotnet"
    CHANGELOG_RELEASE_TITLE = "nexmo-dotnet"
  }
  secrets = ["CHANGELOG_AUTH_TOKEN"]
}
