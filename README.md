Vonage Client Library for .NET
===================================

[![](http://img.shields.io/nuget/v/Vonage.svg?style=flat-square)](https://www.nuget.org/packages/Vonage/)
[![Build Status](https://github.com/Vonage/vonage-dotnet-sdk/actions/workflows/net-build.yml/badge.svg)](https://github.com/Vonage/vonage-dotnet-sdk/actions/workflows/net-build.yml/badge.svg)
[![MultiFramework Build Status](https://github.com/Vonage/vonage-dotnet-sdk/actions/workflows/multiframework-build.yml/badge.svg)](https://github.com/Vonage/vonage-dotnet-sdk/actions/workflows/multiframework-build.yml/badge.svg)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=Vonage_vonage-dotnet-sdk&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=Vonage_vonage-dotnet-sdk)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=Vonage_vonage-dotnet-sdk&metric=coverage)](https://sonarcloud.io/summary/new_code?id=Vonage_vonage-dotnet-sdk)
[![CodeScene Code Health](https://codescene.io/projects/29782/status-badges/code-health)](https://codescene.io/projects/29782)
[![Mutation testing badge](https://img.shields.io/endpoint?style=flat&url=https%3A%2F%2Fbadge-api.stryker-mutator.io%2Fgithub.com%2FVonage%2Fvonage-dotnet-sdk%2FVonage%2Fmain)](https://dashboard.stryker-mutator.io/reports/github.com/Vonage/vonage-dotnet-sdk/Vonage/main)
[![Contributor Covenant](https://img.shields.io/badge/Contributor%20Covenant-v2.0%20adopted-ff69b4.svg)](CODE_OF_CONDUCT.md)

This library allows you to integrate features from [Vonage's APIs](#supported-apis) into your application.

Full documentation and guides: [vonage.github.io/vonage-dotnet-sdk](https://vonage.github.io/vonage-dotnet-sdk)

It requires a [Vonage account](https://dashboard.nexmo.com/sign-up?utm_source=DEV_REL&utm_medium=github&utm_campaign=csharp-client-library) first.

* [Installation](#installation)
    * [Migration guides](#migration-guides)
    * [Targeted frameworks](#targeted-frameworks)
* [Quick Start](#quick-start)
* [Supported APIs](#supported-apis)
* [Contributing](#contributing)
* [Release](#release)
* [License](#license)

## Installation

```shell
dotnet add package Vonage
```

### Migration guides

* Upcoming [v9.0.0](MIGRATION_v9.0.0.md).
* Upgrading to [v8.0.0](MIGRATION_v8.0.0.md).
* Upgrading to [v7.0.0](MIGRATION_v7.0.0.md).
* Upgrading to [v6.0.0](MIGRATION_v6.0.0.md).
* Upgrading from [OpenTok for Video API](OPENTOK_TO_VONAGE_MIGRATION.md).

### Targeted frameworks

The SDK targets `netstandard2.0` and is compatible with .NET 6, 7, and 8.

## Quick Start

```csharp
using Vonage;
using Vonage.Request;
using Vonage.VerifyV2.StartVerification;
using Vonage.VerifyV2.StartVerification.Sms;

var client = new VonageClient(
    Credentials.FromAppIdAndPrivateKeyPath("YOUR_APP_ID", "private.key"));

var result = await client.VerifyV2Client.StartVerificationAsync(
    StartVerificationRequest.Build()
        .WithBrand("MyApp")
        .WithWorkflow(SmsWorkflow.Parse("447700900000"))
        .Create());

result.Match(
    successOperation: response => Console.WriteLine($"Verification started: {response.RequestId}"),
    failureOperation: failure  => Console.WriteLine($"Error: {failure.GetFailureMessage()}"));
```

For authentication options, DI registration, configuration reference, and logging, see [Getting Started](https://vonage.github.io/vonage-dotnet-sdk/docs/getting-started.html).

## Supported APIs

The following is a list of Vonage APIs and whether the Vonage .NET SDK provides support for them:

| API                     |  API Release Status  | Supported? |
|-------------------------|:--------------------:|:----------:|
| Account API             | General Availability |     ✅      |
| Alerts API              | General Availability |     ✅      |
| Application API         | General Availability |     ✅      |
| Conversations API       | General Availability |     ✅      |
| Messages API            | General Availability |     ✅      |
| Number Insight API      | General Availability |     ✅      |
| Number Insight V2 API   | General Availability |     ✅      |
| Number Management API   | General Availability |     ✅      |
| Number Verification API | General Availability |     ✅      |
| Pricing API             | General Availability |     ✅      |
| Redact API              |  Developer Preview   |     ✅      |
| Reports API             | General Availability |     ✅      |
| SimSwap API             | General Availability |     ✅      |
| SMS API                 | General Availability |     ✅      |
| SubAccounts API         | General Availability |     ✅      |
| Users API               | General Availability |     ✅      |
| Verify API              | General Availability |     ✅      |
| Verify V2 API           | General Availability |     ✅      |
| Video API               | General Availability |     ✅      |
| Voice API               | General Availability |     ✅      |

## Contributing

Pick your preferred IDE:

- Visual Studio (Community is fine)
- Visual Studio Code
- Jetbrains Rider

1. Get the latest code either by cloning the repository or downloading a snapshot of the source.
2. Open `Vonage.sln`
3. Build — NuGet dependencies should be brought down automatically.
4. Run all tests to verify everything's fine.

Pull requests are welcome!

## Release

To publish a new version, use the release script:

```bash
./release.sh <version> [--dry-run]
```

For example:

```bash
# Preview the release without making changes
./release.sh 8.30.0 --dry-run

# Perform the release
./release.sh 8.30.0
```

The script will:

1. Update the version in `Vonage/Vonage.csproj`
2. Commit and create a git tag
3. Generate the changelog using [git-cliff](https://git-cliff.org/)
4. Push to remote
5. Create a GitHub release

Pre-release versions are also supported (e.g., `8.30.0-beta`).

## License

This library is released under [the MIT License][license].

[license]: LICENSE.md
