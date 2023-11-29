# Changelog
## [unreleased]

### Bug Fixes

- Mutation testing ignoring test value for private key
- Mutation testing ignoring test value for private key
- Mutation testing ignoring test value for private key

### Documentation

- Add git-cliff for changelog generation
- Generate changelog using git-cliff
- Update changelog format
- Update changelog

### Pipelines

- Add changelog workflow
- Setup changelog auto update
- Use auth token for changelog workflow
- Use PAT for changelog auto update

### Reverts

- Revert "ci: use auth token for changelog workflow"

This reverts commit 35bff74dd2c2693485ad46db99fb49babc62facd.

- Revert "fix: mutation testing ignoring test value for private key"

This reverts commit b861905a438cfe47a0c627375d170ebfdcd7cbbf.

- Revert "fix: mutation testing ignoring test value for private key"

This reverts commit 00d89a309f02dbf0ac5720dbff379a22892bb3f7.


## [v7.1.0-beta](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v7.1.0-beta) (2023-11-27)

### Releases

- Include package Vonage.Common

## [v6.12.3](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v6.12.3) (2023-11-27)

### Documentation

- Add whitesource file to solution

### Releases

- Bump Video version to v7.0.7-beta
- Remove specific assembly version for Vonage
- Reorganize dependencies in project file
- Bump video version to v7.1.0-beta
- Bump version to v6.12.2
- Bump version to v6.12.3

### Reverts

- Revert "release: reorganize dependencies in project file"

This reverts commit 5ab30fc300223c8257727c0253bca24b36502c38.


## [v6.12.1](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v6.12.1) (2023-11-24)

### Bug Fixes

- Explicit using for system.net.http to avoid conflicts with older frameworks (#553)
- Result assertion messages
- Reduce timeout for httpClient timeout tests
- Ambiguous reference with System.Net.Http
- Signed Vonage.Server.Test project
- Use test class for VideoClient tests to avoid InternalsVisibleTo
- Incorrect path for Vonage.Server release

### Documentation

- Add coverage to readme
- Update upcoming breaking changes in next major version, with links to PRs
- Update formatting in readme

### Features

- Update supported languages in Meetings API UI settings
- Implement implicit operator for VerifyV2 languages
- Add missing features on Voice capabilities
- Add privacy settings to applications
- Implement BuildVerificationRequest on StartVerificationResponse
- Redirect url on silent auth
- Use Uri instead of string for RedirectUrl

### Pipelines

- Add github action to try generating release packages
- Remove InternalsVisibleTo in Vonage.Server
- Parallelize signed and unsigned builds

### Refactoring

- Update all dependencies
- Simplify StartVerificationRequest process by removing generics
- Enable bindings redirect (#551)
- Enable bindings redirect (#552)
- Prevent exceptions in monads
- Update dependencies with net8.0 release
- Remove dependency towards Microsoft.AspNetCore.WebUtilities
- Clean VerifyV2 tests & mutants
- Simplify test setup with private key
- Remove unnecessary dependency for Vonage.Server

### Releases

- Bump version to v6.12.1
- Bump video beta to v7.0.5-beta
- Bump Vonage.Server version to v7.0.5-beta

## [v6.12.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v6.12.0) (2023-10-23)

### Bug Fixes

- Read feature on MaybeJsonConverter (#539)
- Remove stringified booleans (#544)

### Documentation

- Add v6.11.0 in changelog
- Detail upcoming major v7.0.0 release
- Update code of conduct
- Update contribution guide
- Update issue templates
- Update issue templates
- Add pull request template

### Features

- Meetings capabilties in Application API (#537)
- Support check_url on VerifyV2 silent auth (#540)
- Add missing Premium feature on talk ncco (#547)

### Merges

- Merge branch 'main' of https://github.com/Vonage/vonage-dotnet-sdk

- Merge remote-tracking branch 'origin/main'


### Other

- Adapt methods visibility (#543)

### Refactoring

- Make sync-only methods obsolete (#538)
- Remove duplicate in parametrized test
- Remove ExcludeFromCodeCoverage annotation on PemParse - the code is covered through JwtTest
- Delete unnecessary class WebhookTypeDictionaryConverter (#542)
- Fix typos and clean WebhookParser
- Reduce duplication in SubAccountsClient (#545)
- Reduce duplication in ProactiveConnect UpdateList (#546)
- Reduce duplication in ResultAssertions
- Reduce duplication in Sip InitiateCall tests
- Reduce duplication in VerifyV2 tests
- Reduce duplication in VerifyV2 tests
- Remove unused field

### Releases

- Bump version to v6.12.0

## [v6.11.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v6.11.0) (2023-10-13)

### Documentation

- Update changelog with v6.10.1
- Add documentation on RequestTimeout configuration in readme

### Features

- Enable custom timeout on HttpClient (#534)

### Pipelines

- Add .whitesource configuration file (#536)

### Refactoring

- Transform httpclient timeout into failure (#535)
- Adapt configuration key for request timeout

### Releases

- Bump version to v6.11.0

## [v6.10.1](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v6.10.1) (2023-10-10)

### Bug Fixes

- Application deserialization with Meetings custom webhooks (#532)

### Documentation

- Update changelog after v6.10.0

### Releases

- Bump version to v6.10.1

## [v6.10.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v6.10.0) (2023-10-04)

### Documentation

- Update changelog with v6.9.0 (#506)

### Features

- Extend registration with missing clients and token generator (#504)
- Add ja-jp locale to VerifyV2 (#530)
- Add feature to verify Jwt signature (#531)

### Pipelines

- Remove old worksflow 'publish-nuget' (#505)
- Mutation testing improvement (#507)
- Upgrade actions/checkout to v3 for mutation testing (#508)
- Fix some build warnings (#525)
- Restore tests parallelism for Vonage.Test.Unit (#526)

### Refactoring

- Remove unused class ResponseBase (#510)
- Replace TimeSpamSemaphore anf ThrottlingMessageHandler by proper dependency (#511)
- Implement GetHashCode for ParsingFailure (#512)
- Use a 2048 bits key for Linux and MacOs platforms (#513)
- Use sonar.token instead of deprecated sonar.login (#514)
- Add missing assertion in parser test (#515)
- Align method signatures for async/sync methods on AccountsClient (#516)
- Add missing optional parameter for VerifyClient (#517)
- Make NonStateException compliant to ISerializable (#518)
- Simplify ternary operator in Result (#519)
- Update serialization settings (#520)
- Remove obsolete method on TestBase (#521)
- Enable parallelized tests on Vonage.Test.Unit (#522)
- Update TestBase (#523)
- Update sub clients (#524)
- Replace null by optional values in ApiRequest (#527)
- Simplify ApiRequest (#529)
- Cover signature in query string (#528)

### Releases

- Bump version to v6.10.0

## [v6.9.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v6.9.0) (2023-09-06)

### Bug Fixes

- Add basic auth support for Messages (#384)
- Missing dependencies (#393)
- Exclude '{' and '}' from non-empty strings in PBT (#397)
- Exclude '{' and '}' from non-empty strings in PBT (#400)
- Hiding ProactiveConnectClient (#413)
- Change requestId to Guid for VerifyCodeRequest (#415)
- Basic auth encoding (#423)
- SubAccounts implementation (#426)
- Conversation StartOnEnter (#465)
- Version substring in release pipeline (#503)

### Documentation

- Changelog update (#402)
- Add changelog for video beta (#403)
- Update changelog
- Readme badge fix (#427)
- Changelog update (#428)
- Explaining monads in readme (#433)
- Changelog update (#435)
- Add service registration in readme (#436)
- Update supported apis (#448)
- Changelog update (#454)
- Changelog update (#466)
- Changelog update (#490)
- Readme update (#499)

### Features

- Add premium to start talk request
- Webhook classes for messages (#382)
- Verify V2 (#376)
- Hide Meetings API client until GA (#385)
- Proactive connect - lists (#395)
- Add optional claims when generating a token (#398)
- Proactive connect - items (#399)
- Proactive connect - events (#401)
- VerifyV2 BYOP (#392)
- VerifyV2 Cancel (#407)
- VerifyV2 fraud check (#409)
- Voice advanced machine detection (#412)
- Implement Match on Result with void return type (#417)
- Dependency injection extension (#418)
- Add 4.8.1 and 7.0 in targeted frameworks (#422)
- Subaccounts (#431)
- Meetings Api (#437)
- Add specific object for ApiKey (#443)
- Add Type property on Failures (#446)
- Proactive connect (#445)
- Add MeetingsApi and ProactiveConnectApi clients on service injection (#453)
- Users API (#479)
- Initialize credentials from Configuration (#491)
- Add versioning on Meetings API Uri (#497)

### Other

- Bump Vonage.Server v7.0.2-beta

- [DEVX-7140] Remove hardcoded keys (#373)

* Replace hardcoded RsaPrivateKey by environment variable

* Rename variable

* Remove hardcoded public/private keys

* Amend readme

* Update github actions with environment variable

* Update Readme

* Update Readme
- Readme update (#375)

* Fix dead links and badges

* Adapt summary

* Try updated contributors

* Remove contributors
- [DEVX-7128] NumbersAPI update (#374)

* Add possibility to exclude credentials from QueryString

* Move ApiKey & ApiSecret in query string for numbers api

* Refactor NumbersTests

* Add missing Xml Docs, refactor query parameters generation
- Unify test class names (#378)
- Bump version to v6.3.0

- Update changelog

- Bump version to v6.3.1

- Packages update (#442)
- Add editorconfig file


### Pipelines

- Bump version to 6.1.0 (#387)
- Increase version to v7.0.3-beta (#394)
- Update core release script to be usable from main (again) (#405)
- Change negation for coreSDK publish (#408)
- Fix multiframework pipeline (#425)
- Improve performance (#461)
- Upgrade & improvements (#462)
- Increase java version to 17 (#486)
- Pipelines permissions (#487)
- Release (#488)
- Pipeline permissions (#489)
- Add .editorconfig to solution (#493)
- Add pre-commit-config (#496)
- Release pipeline (#500)

### Refactoring

- Extend responses and monads capabilities (#377)
- Remove duplicate code for sync version of methods (#380)
- Warnings cleanup (#381)
- Move builder on request for VerifyV2 (#386)
- Make builders internal (#388)
- Refactor builders (#389)
- Make builders internal (#390)
- Throw failure exception on Result<>.GetSuccessUnsafe (#404)
- Add test use case interface to facilitate new tests (#406)
- Improving ApiRequest (#410)
- Make ApiRequest non-static (#411)
- Clean voice tests (#414)
- Move AuthenticationHeader creation on Credentials (#429)
- Use case enhancement (#430)
- E2e testing experiment (#438)
- Failure extensions (#447)
- Subaccounts e2e (#455)
- Naming update (#456)
- Package update (#457)
- Proactive connect e2e (#459)
- Meetings Api e2e (#460)
- Async result extensions (#470)
- Test refactoring (#469)
- Simplify e2e tests (#471)
- Simplify e2e tests (#472)
- Video e2e refactoring (#473)
- Video e2e refactoring (#476)
- Video e2e refactoring (#477)
- Use case helpers (#478)
- Update error status codes in PBT for VonageClient (#494)
- Configuration improvement (#495)
- Extend regex timeout (#498)
- Remove InternalsVisibleTo property (#501)

### Releases

- V6.3.2 (#416)
- V6.3.3 (#424)
- V6.5.0 (#434)
- Upgrade version to v6.6.0 (#441)
- V6.7.0 (#444)
- Upgrade version to v6.8.0 (#450)
- Upgrade version to v7.0.4-beta (#449)
- Revert "release: upgrade version to v6.8.0" (#452)
- V6.7.1 (#467)
- V6.9.0 (#502)

### Reverts

- Revert "Add editorconfig file"

This reverts commit 1ec8fce1053a53579a4f43974d311bac85349483.


## [v7.0.2-beta](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v7.0.2-beta) (2023-03-16)

### Other

- Updated README to show Messages was GA
- Add mutation workflow (#293)

* Add github workflows in solution

* Create mutation workflow
- Remove specific .net versioning (#294)

Stryker is not compatible with specific version
- Subaccount support (#295)

* Simple subaccount support

* fix comment

* Update interfaces

* Balance and CreditLimit could be null

* Proper auth for number transfer
- Bump Newtonsoft.Json from 9.0.1 to 13.0.1 in /Vonage (#286)

Bumps [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json) from 9.0.1 to 13.0.1.
- [Release notes](https://github.com/JamesNK/Newtonsoft.Json/releases)
- [Commits](https://github.com/JamesNK/Newtonsoft.Json/compare/9.0.1...13.0.1)

---
updated-dependencies:
- dependency-name: Newtonsoft.Json
  dependency-type: direct:production
...

Signed-off-by: dependabot[bot] <support@github.com>

Signed-off-by: dependabot[bot] <support@github.com>
Co-authored-by: dependabot[bot] <49699333+dependabot[bot]@users.noreply.github.com>
- Sonarcloud integration (#301)

* Add sonarsource analysis in main pipeline

* Remove non-supported client frameworks, add .Net6 as .net core 3.1 will get out-of-support this month

* Add client framework 4.8 back (removed by IDE)
- DEVX-6785 Framework update (#303)

* Upgrade SDK to netstandard2.0, Upgrade TestProject to everything above 4.6.2, update all libraries

* Update libraries

* Upgrade to C# 10.0
- DEVX-6545 | [Video] Sessions (#305)

* Set up Video project, implement Maybe monad

* Implement Result monad

* Fix coverage (error not extracted from monad)

* Add more client frameworks on test project, remove global usings

* Implement VideoClient / SessionClient

* Implement IpAddress and CreateSessionRequest, add monadic bind to Result

* Implement monadic bind on Maybe, factory method for ResultFailure

* Implement MaybeAssertions

* Implement custom assertions for monads (Should Be|BeSome|BeNone|BeSuccess|BeFailure)

* Implement IfFailure/IfSuccess on Result, use on ResultAssertions

* Add missing xml doc on IfFailure/IfSuccess

* Implement MapAsync, BindAsync for Result

* Implement CreateSession

* Add temporary sample test

* Test refactoring

* Add missing Xml Documentation

* Remove sample test

* Update test for token generation
- Add unsafe methods on Maybe & Result (#310)


- DEVX-6545 | [Video] GetStream / GetStreams (#307)

* Set up Video project, implement Maybe monad

* Implement Result monad

* Fix coverage (error not extracted from monad)

* Add more client frameworks on test project, remove global usings

* Implement VideoClient / SessionClient

* Implement IpAddress and CreateSessionRequest, add monadic bind to Result

* Implement monadic bind on Maybe, factory method for ResultFailure

* Implement MaybeAssertions

* Implement custom assertions for monads (Should Be|BeSome|BeNone|BeSuccess|BeFailure)

* Implement IfFailure/IfSuccess on Result, use on ResultAssertions

* Add missing xml doc on IfFailure/IfSuccess

* Implement MapAsync, BindAsync for Result

* Implement CreateSession

* Add temporary sample test

* Test refactoring

* Add missing Xml Documentation

* Remove sample test

* Update test for token generation

* Implement GetStreamRequest

* Create structure for GetStream

* Make Failure state of Result an IResultFailure. It will allow custom formatting for specific failures

* Implement HttpFailure with status codes, adapt CreateSession to use the new failure.

* Implement GetStream with error codes

* Remove custom url for WireMock

* Refactoring with extension methods

* Implement GetStreamAsync with successful state

* Implement GetStreamAsync failure when response cannot be serialized

* Remove conflicts from last merge (ResultFailure)

* Test refactoring

* Code cleanup & Xml Documentation

* Implement GetStreamsRequest

* Implement GetStreams

* Fix type change after merge

* Add factory method for failure, handle empty response differently
- Add workflow for publishing beta package for Vonage.Video (#312)


- Adding SonarCloud badge, removing unused codecov badge (#313)


- DEVX-6545 | [Video] Change stream layout & Refactoring (#314)

* Set up Video project, implement Maybe monad

* Implement Result monad

* Fix coverage (error not extracted from monad)

* Add more client frameworks on test project, remove global usings

* Implement VideoClient / SessionClient

* Implement IpAddress and CreateSessionRequest, add monadic bind to Result

* Implement monadic bind on Maybe, factory method for ResultFailure

* Implement MaybeAssertions

* Implement custom assertions for monads (Should Be|BeSome|BeNone|BeSuccess|BeFailure)

* Implement IfFailure/IfSuccess on Result, use on ResultAssertions

* Add missing xml doc on IfFailure/IfSuccess

* Implement MapAsync, BindAsync for Result

* Implement CreateSession

* Add temporary sample test

* Test refactoring

* Add missing Xml Documentation

* Remove sample test

* Update test for token generation

* Implement GetStreamRequest

* Create structure for GetStream

* Make Failure state of Result an IResultFailure. It will allow custom formatting for specific failures

* Implement HttpFailure with status codes, adapt CreateSession to use the new failure.

* Implement GetStream with error codes

* Remove custom url for WireMock

* Refactoring with extension methods

* Implement GetStreamAsync with successful state

* Implement GetStreamAsync failure when response cannot be serialized

* Remove conflicts from last merge (ResultFailure)

* Test refactoring

* Code cleanup & Xml Documentation

* Implement GetStreamsRequest

* Implement GetStreams

* Replace FluentAssertions extension .Be by .BeSome/.BeSuccess/.BeFailure to avoid confusion with base .Be method
The extension using clause wasn't discovered by the IDE.

* Rename FluentAssertion extensions

* Implement use-case approach with Screaming architecture. This will allow to comply with OCP

* Fix type change after merge

* Add factory method for failure, handle empty response differently

* Solve merge conflicts

* Remove unnecessary setter

* Simplify token generation

* Simplify http request creation

* Extract ErrorCode to higher namespace

* Remove TestRun project

* Implement ChangeStreamLayoutRequest with Parsing

* Use specific settings for camelCase serialization

* Implement ChangeStreamLayout use case

* Use 'Hollywood principle' for reducing the number of dependencies on clients & use cases (token generation using credentials)

* Remove GetStream.ErrorResponse
- DEVX-6546 | [Video] Signaling (#315)

* Set up Video project, implement Maybe monad

* Implement Result monad

* Fix coverage (error not extracted from monad)

* Add more client frameworks on test project, remove global usings

* Implement VideoClient / SessionClient

* Implement IpAddress and CreateSessionRequest, add monadic bind to Result

* Implement monadic bind on Maybe, factory method for ResultFailure

* Implement MaybeAssertions

* Implement custom assertions for monads (Should Be|BeSome|BeNone|BeSuccess|BeFailure)

* Implement IfFailure/IfSuccess on Result, use on ResultAssertions

* Add missing xml doc on IfFailure/IfSuccess

* Implement MapAsync, BindAsync for Result

* Implement CreateSession

* Add temporary sample test

* Test refactoring

* Add missing Xml Documentation

* Remove sample test

* Update test for token generation

* Implement GetStreamRequest

* Create structure for GetStream

* Make Failure state of Result an IResultFailure. It will allow custom formatting for specific failures

* Implement HttpFailure with status codes, adapt CreateSession to use the new failure.

* Implement GetStream with error codes

* Remove custom url for WireMock

* Refactoring with extension methods

* Implement GetStreamAsync with successful state

* Implement GetStreamAsync failure when response cannot be serialized

* Remove conflicts from last merge (ResultFailure)

* Test refactoring

* Code cleanup & Xml Documentation

* Implement GetStreamsRequest

* Implement GetStreams

* Replace FluentAssertions extension .Be by .BeSome/.BeSuccess/.BeFailure to avoid confusion with base .Be method
The extension using clause wasn't discovered by the IDE.

* Rename FluentAssertion extensions

* Implement use-case approach with Screaming architecture. This will allow to comply with OCP

* Fix type change after merge

* Add factory method for failure, handle empty response differently

* Solve merge conflicts

* Remove unnecessary setter

* Simplify token generation

* Simplify http request creation

* Extract ErrorCode to higher namespace

* Remove TestRun project

* Implement ChangeStreamLayoutRequest with Parsing

* Use specific settings for camelCase serialization

* Implement ChangeStreamLayout use case

* Use 'Hollywood principle' for reducing the number of dependencies on clients & use cases (token generation using credentials)

* Remove GetStream.ErrorResponse

* Setting up structure for Signaling

* Implement parsing for SendSignalsRequest

* Empty use case for SendSignals

* Implement SendSignals use case

* Implement SendSignalUseCase

* Address duplication in Signaling

* Address duplication for Sessions and Signaling

* Add test for CreateSession GetEndpointPath

* Handle null & empty bodies on responses

* Add missing Xml documentation
- DEVX-6546 | [Video] Refactoring (#316)

* Remove duplication when creating WireMock requests/responses

* Implement UseCaseHelper to reduce duplication

* Code cleanup

* Reduce duplication on property-based tests

* Use generator for FsCheck, use HttpStatusCode instead of string for ErrorResponse

* Create method for converting an ErrorReponse to HttpFailure

* Implement ValueObject and StringIdentifier

* Implement implicit operator for Identifier

* Missing constant on Identifier

* Address duplication in InputValidation
- DEVX-6548 | [Video] Moderation (#317)

* Implement DisconnectConnection, more ErrorResponse to Common namespace

* Implement MuteStream

* Implement MuteStreamsRequest

* Implement MuteStreamsUseCase

* Adapt Xml Documentation
- DEVX-6547 | [Video] Archives (#318)

* Implement DisconnectConnection, more ErrorResponse to Common namespace

* Implement MuteStream

* Implement MuteStreamsRequest

* Implement MuteStreamsUseCase

* Adapt Xml Documentation

* Implement GetArchivesRequest

* Implement GetArchives

* Implement GetArchive

* Use Archive as return type for use cases

* Implement CreateArchive

* Implement CreateArchive *

* Implement missing fields in CreateArchive

* Fix coverage on VideoClient

* Implement DeleteArchive

* Implement StopArchive

* Implement ChangeLayout

* Implement AddStream/Remove stream

* Fix body content in tests

* Fix mutants

* Refactor VideoHttpClient

* Change client parameter type from IVideoRequest to Result<IVideoRequest>

* Use Map/Bind inside VideoHttpClient

* Add test for verifying result value in each client

* Fix property name on session response

* Add tests using spec data

* Refactor serialization tests

* Implement deserialization tests for GetStream

* Fix missing Content tag on files

* Simplify deserialization tests for errors

* Refactor deserialization for errors

* Implement deserialization tests for MuteStream(s)

* Change CreatedAt to long

* Implement deserialization tests for archiving

* Fix typo on VideoClient (ModerationClient instead of IModerationClient)

* Removed conflict from merge
- DEVX-6547 | [Video] Refactoring (#320)

* Extract generic PBT in UseCaseHelper

* Reduce duplication on request verification

* Implement custom token generation for Video Client SDK

* Use TokenAdditionalClaims to generate token

* Use Result for token generation

* Fix project file

* Fix missing v2 in endpoint path

* Fix missing v2 in endpoint path

* Update xml comments for CreateSessionRequest.cs

* Use Enum for RenderResolution

* Use enums for CreateArchiveRequest, use generic enum description converter

* Applying internal access modifier on use-cases and other internal classes

* Remove interfaces for use cases
- Modify VerifyResponse to handle new information (#299)

* Add tests to verify deserialization, Add missing property on VerifyResponse

* Add missing package FluentAssertions
- MapAsync / BindAsync extension methods (#323)

* Implement chainable extension methods for MapAsync and BindAsync on Task<Result<T>>

* Implement IfFailure with default value and function to extract the success more easily
- Rebrand Vonage.Video.Beta into Vonage.Server (#325)

* Rebrand Vonage.Video.Beta into Vonage.Server

* Update project name in nuget pipeline

* Fix helper, fix property order in test
- Make nuget pipelines manual as they target different projects, mark main as default branch (#326)


- Update version to 6.0.4 (#327)


- Remove condition when configuration is not ReleaseSigned (#328)


- Nuget release automation (#329)

* Setup two automated jobs based on branch name

* Fix path name for beta

* Delete outdated releases

* Downgrade version to 7.0.0-beta

* Remove tag assembly version

* Fix Vonage.Server version
- Fix nuget workflow, update Vonage.Server config (#331)


- Create Vonage.Common project (#332)

* Create Vonage.Common library

* Remove unused changelog

* Update readme file
- 'Bumping Vonage.Server version to 7.0.1-beta' (#333)

Co-authored-by: NexmoDev <44278943+NexmoDev@users.noreply.github.com>
- [DEVX-6854] Meetings API | GetAvailableRooms (#334)

* Fix reference mismatch

* Update warnings for Vonage and Vonage.Test.Unit

* Adapt solution folders

* Create default structure and implement GetAvailableRoomRequest

* Implement GetAvailableRoomResponse and deserialization test

* Implement use case for GetAvailableRooms

* Remove IVideoRequest and VideoHttpClient from Vonage.Server, use classes from common instead

* Make exception more explicit when Credentials are null on VonageClient

* Use enums for GetAvailableRoomsResponse

* Add GetRoom endpoint

* Replacing true/false by on/off for microphone state (spec were wrong)
- Refactoring on *.Test (#336)

* Refactor Property-Based Testing to reduce duplication

* Reduce duplication when verifying response cannot be parsed

* Reduce duplication when testing the success scenario

* Remove netcoreapp3.1 from Vonage.Test.Unit
- Use builder for HttpRequestMessage (#337)


- Meetings/get sessions (#338)

* Implement GetRecording

* Implement GetRecording & GetRecordings

* Fix conflicts from last merge

* Implement GetDialNumbersRequest

* Use builder in requests

* Implement GetDialNumbers

* Implement GetApplicationThemes

* Remove unnecessary constructors for responses - Add customization to AutoFixture to generate structs without constructors

* Rename ApplicationThemes into Themes

* Implement GetTheme

* Add missing XML Doc
- Add Polysharp, update C# to latest version (#340)


- Pipeline updates (#342)

* Focus main build on .net6.0 to improve feedback loop

* Add separate pipeline to test all frameworks on push

* Forces build on .net6.0, package restore on build

* Defining build version to netstandard2.0

* Add .netstandard2.0 to test projects

* Remove specific framework on build
- Refactoring on use cases (#341)

* Refactor client & request instantiation

* Remove unnecessary parameters and fields

* Remove specific use cases, use vonage client for generic purpose
- Sets up the user-agent in HttpClient (#347)

* Add user agent from credentials to vonage client

* Fight primitive obsession on http client options
- Use configuration for Video and Meetings, refactor Configuration (#349)


- Fix multiframework build (#350)


- Meetings/rooms (#339)

* WIP - Builder fo CreateRoomRequest given the object holds many properties

* Implement CreateRoomRequestBuilder

* Implement CreateRoom

* Fix merge conflicts

* Implement DeleteRecording

* Fix merge conflicts

* Fix merge conflicts

* Implement UpdateRoomRequest

* Implement UpdateRoom

* Implement delete theme

* Improve Maybe implementation, and tests using generics

* Major refactor on serializers initialization, implement CreateTheme

* Implement GetRoomsByTheme

* Implement UpdateApplication

* Implement UpdateTheme

* Implement UpdateThemeLogo

* Fix tests for VonageRequestBuilder due to Absolute/Relative Uri

* Implement testing for UpdateThemeLogo

* Implement serialization tests for UpdateThemeLogo

* Create extension method to get the string content of a request

* Add missing body serialization tests

* Use Maybe<> on optional fields for CreateRoomRequest

* Use Maybe<> on GetAvailableRoomsRequest

* Use Maybe<> on UpdateRoomRequest

* Verify Xml Doc on entities

* Add missing Xml Doc tags

* Replace internal constructors by internal inits

* Remove dead code

* Adapt CreateRoomRequest after testing

* Improve Room response object

* Fix GetAvailableRoomsResponse layout

* Improve recordings

* Improve themes

* Improve GetRoomsByTheme

* Improve logo update

* Changes due to PR suggestion

* Use BinaryContent for file in UploadLogo (inject IFileSystem, improve declarative writing on use case)
- Video refactoring (#352)

* Create builder for AddStreamRequest

* Create builder for GetArchivesRequest

* Use Guid for UUID values instead of string

* Simplify builder tests

* Create builder for CreateArchiveRequest
- Integration testing (#353)

* Add meetings capability to Application

* Add base integration tests, modify pipelines to use environment variables and log information

* Remove appsettings from project

* Reorder Application/ApplicationCapabilities, make appsettings.json optional in integration tests

* Fix ordering in applications, use values from environment variables (with Test Runner)

* Amend Readme with integration tests configuration

* Remove logger verbosity from build
- Add missing environment variables on pipeline (#354)


- Sip/devx 6866 (#355)

* Classes setup

* Move Sip into Video beta (Vonage.Server)

* Add video capability on Application

* Implement Sip outbound call

* Implement PlayToneIntoCall

* Implement PlayToneIntoConnection

* Add missing Xml documentation

* Remove integration test for Sip

* Remove integration tests from pipelines - manual run only

* Fix based on PR suggestions

* Replace SipHeader by dictionary
- [Video] DEVX-6861 Broadcasts (#356)

* Implement GetBroadcasts

* Fix merge conflicts

* Fix merge conflicts

* Fill Xml Documentation on Broadcast

* Implement StartBroadcastRequest

* Implement StartBroadcast

* Implement GetBroadcast

* Implement StopBroadcast

* Implement AddStreamToBroadcast

* Implement AddStreamToBroadcast http content and serialization

* Implement RemoveStreamFromBroadcast

* Implement ChangeBroadcastLayout

* Use enums for BroadcastStatus and RtmpStatus

* Use Guids on most identifiers

* Remove unnecessary using

* Add missing XmlDocumentation

* Rename ArchiveLayout to Layout, given it's not specific to Archive anymore

* Convert Layout to a record

* Replace structs by records

* Apply PR suggestions

* Fix broadcast layout creation
- Package update (#358)


- Pipeline performance improvement (#360)

* Create new UseCaseHelper that uses a fake HttpMessageHandler instead of WireMock

* Add missing documentation

* Refactoring handler and use case

* Replace WireMock by a FakeHttpMessageHandler on every use case

* Use the new UseCase with handlers

* Remove WireMock dependency

* Create extension for Task<Result<T>>.IfFailure

* Fill missing XmlDocumentation

* Fix code smells

* Refactoring for CustomHttpMessageHandler and UpdateThemeLogoTest

* Csproj cleaning

* Remove unused members
- Remove duplication following code health degradation (#361)


- Split Messages tests under separate categories (#363)

* Clean code smells in MessagesTests

* Split MessagesTests into several sub-sections (SMS, MMS, WhatsApp, etc).
- Use System.Text.Json instead of Newtonsoft, use fixed ordering on serialization to allow file reordering while cleaning (#366)


- Simplify client constructors by using ClientConfiguration only (#367)


- Improve IResultFailures (#368)

* Allow failures to throw exceptions

* Use factory method to create AuthenticationException based on scenarios

* Normalize custom exceptions in 'legacy' code
- [DEVX-6796] Remove deprecated message types (wappush, val, vcar) (#362)

* Remove deprecated message types (wappush, val, vcar)

* Remove additional wappush, vcal and vcard properties

* Reorder properties
- [DEVX-7004] Messages adjustments (#369)

* Implement ViberVideoRequest following the current process (to be improved)

* Implement ViberFileMessage

* Add missing content for Viber messages

* Add action for Viber Text and Image messages

* Transform all Viber requests to struct

* Use IMessage for WhatsApp messages

* Transform all WhatsApp requests to struct

* Implement WhatsApp sticker message

* Implement builders for ProductMessages, transforming all nested entities into records

* Add missing Xml Docs

* Fix wrong property name on request

* Implement optional fields on SingleItem Product Message

* Implement validation on Product Messages

* Test refactoring

* Remove temporary comment

* Update Vonage/Messages/Viber/ViberMessageCategory.cs

Fix typo.

Co-authored-by: Karl Lingiah <karl@superchilled.co.uk>

* Update Vonage/Messages/Viber/ViberFileRequest.cs

Fix type issue.

Co-authored-by: Karl Lingiah <karl@superchilled.co.uk>

* Fix typos.

* Add missing XmlDoc on MessageType

* Fix MessageType on Video

* Add missing xml document

---------

Co-authored-by: Karl Lingiah <karl@superchilled.co.uk>
- Force Vonage.Common to be included in dotnet pack (#370)


- Remove integration testing (not applicable) (#371)


- Improve exceptions details for GetUnsafe methods on monads (#372)

* Add NoneStateException for Maybe<T>

* Use explicit exceptions for GetUnsafe methods on Result

* Fix typos in Xml Docs

* Comply to ISerializable implementation

* Add missing Serializable attribute

## [v6.0.2-rc](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v6.0.2-rc) (2022-05-31)

### Merges

- Merge pull request #280 from Vonage/dev

Adding RealTimeData option for AdvancedNumberInsights

### Other

- Adding RealTimeData option for AdvancedNumberInsights

- 'Bumping version to 6.0.2-rc'


## [v6.0.1-rc](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v6.0.1-rc) (2022-05-25)

### Merges

- Merge pull request #278 from Vonage/v6-fixes

Small fixes for v6
- Merge pull request #279 from Vonage/dev

Dev into Main for release

### Other

- Removing VersionPrefix from project file as to not confuse

- Remaning number insight methods to remove confusion

- 'Bumping version to 6.0.1-rc'


### Reverts

- Reverting the removal of .ToString on Ncco
Making Serialisation Settings public


## [v6.0.0-rc](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v6.0.0-rc) (2022-05-24)

### Merges

- Merge pull request #268 from Vonage/devx-6030

Devx 6030 - Removing old Nexmo Classes
- Merge pull request #269 from Vonage/devx-6030

Devx 6030 - Message API
- Merge pull request #270 from Vonage/devx-6030

Messenger and Viber
- Merge pull request #273 from Vonage/devx-6173

devx-6173
- Merge pull request #274 from Vonage/devx-6173

Refactoring NCCO Action to have get only property for action type
- Merge pull request #276 from Vonage/devx-6173

Renaming of enums to meet conventions
- Merge pull request #277 from Vonage/dev

Pulling dev into main ready for release

### Other

- Removing legacy nexmo classes

- Updating to version 6

- Setting serialization settings in singular place

- Adding SMS, MMS and WhatsApp messages

- Changing the auth type

- Update Vonage.Test.Unit/Data/MessagesTests/SendMmsVcardAsyncReturnsOk-request.json

Co-authored-by: Karl Lingiah <karl@superchilled.co.uk>
- Update Vonage.Test.Unit/Data/MessagesTests/SendMmsVideoAsyncReturnsOk-request.json

Co-authored-by: Karl Lingiah <karl@superchilled.co.uk>
- Update Vonage.Test.Unit/MessagesTests.cs

Co-authored-by: Karl Lingiah <karl@superchilled.co.uk>
- Update Vonage.Test.Unit/MessagesTests.cs

Co-authored-by: Karl Lingiah <karl@superchilled.co.uk>
- Changing tests to use bearer auth
Adjusting templates for array of objects not strings

- Fixing merge issues

- Messenger messages

- Adding Viber messaging to messagers API

- Typo in json test file
- Fxing badly formed xml comment
- Fixing issue with unit tests that meant urls were not being checked if there was a request body

- Refactoring NCCO Action to have get only property for action type
Removal of NccoConverter
Tests that use NCCO refactored

- Moving to use bool in code instead of strings

- Application capabilities enum refactor

- Correcting enum capitalisation for Messaging and Number Insights

- PhoneEndpoint enum rename

- 'Bumping version to 6.0.0-rc'


## [v5.10.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v5.10.0) (2022-04-20)

### Merges

- Merge pull request #264 from Vonage/DEVX-5765

Adding Auth exception
- Merge branch 'dev' into unit-tests
- Merge pull request #265 from Vonage/unit-tests

Unit tests
- Merge pull request #266 from Vonage/release-5.10

Preparing for next release and real time data
- Merge pull request #267 from Vonage/dev

Dev into Main for Release

### Other

- Update issue templates

Adding issues templates
- Adding Auth exception

- Refactoring of Messaging tests

- Adding dev as PR build trigger

- Adding msbuild setup set to fix .net framework build issue

- Removing 461 from unit tests, 462 is sufficient

- Removing 461 from nexmo tests

- Merging dev in

- Merging dev

- Preparing for next release and adding real time data to advanced number insights


## [v5.9.5](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v5.9.5) (2022-01-17)

### Other

- Bumping to version 5.9.5


## [v5.9.4](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v5.9.4) (2022-01-17)

### Merges

- Merge pull request #262 from biancaghiurutan/patch-1

Removed line that breaks the code
- Merge pull request #263 from Vonage/devx-1999

Devx 1999

### Other

- Removed line that breaks the code
- Changing biuld action to reflect new main branch name

- Moving test json to json files for maintainability

- Adding Type to NCCO Input

- CallUpdate Test refactor

- Fixing build issue

- 'Bumping version to 5.9.4'


## [v5.9.3](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v5.9.3) (2021-11-23)

### Merges

- Merge pull request #261 from geekyed/5.x

Fixing an issue caused by the usage of a non thread safe Dictionary.

### Other

- Replace Dictionary with ConcurrentDictionary

- 'Bumping version to .5.9.3'

- 'Bumping version to 5.9.3'


## [v5.9.2](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v5.9.2) (2021-11-04)

### Merges

- Merge pull request #259 from Vonage/not-romaing-bug

Fixing bug with roaming being `not_roaming` and cleaning up some tests

### Other

- Fixing bug with roaming being not_roaming and cleaning up some tests

- Adding unit test for Redact and ShortCodes

- Adding hateos link checks

- Moving redaction types to own files

- Removing PemParser from code coverage

- MEssaging and Logprovider tests changes

- Removing logger tests


## [v5.9.1](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v5.9.1) (2021-10-27)

### Merges

- Merge pull request #256 from Vonage/symbols

Source Linking, Symbols and Deterministic Building
- Merge from master

- Merge pull request #257 from Vonage/strong-name

Strong-name Package

### Other

- Creating an SNK file and strongly named build configuration

- Changing package id for singed package

- Adding switches to create symbol packages

- Changes to nuget publis action to create signed package

- Adding source link and some repo details for packaging

- Version bump

- Adding description and assembly version bump

- Package id

- Conditional inclusion of System.Web in Unit Test projects

- 'Bumping version to 5.9.1'


## [v5.9.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v5.9.0) (2021-05-27)

### Merges

- Merge pull request #253 from Vonage/random-number-pools-vapi

adding random from number feature to .NET SDK

### Other

- Adding random from number feature to .NET SDK

- 'Bumping version to 5.9.0'


## [v5.8.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v5.8.0) (2021-04-28)

### Merges

- Merge pull request #249 from Vonage/updating-5.x

moving readme/GHA to 5.x
- Merge pull request #251 from Vonage/support-ni-roaming-unknown

Support for NI Null values

### Other

- Moving readme/GHA to 5.x

- Updating instillation instructions in README
- Added support new NI responses

* Provides support for null fields
* Custom serializer for Roaming

- 'Bumping version to 5.8.0'


## [v5.7.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v5.7.0) (2021-03-09)

### Merges

- Merge pull request #248 from Vonage/feature/dlt

adding entity-id and content-id to sms request body
- Merge pull request #247 from Vonage/feature/detail-status-webhooks

adding detail to status-webhooks

### Other

- Adding detail to status-webhooks

- Adding detail enumeration and parser handling

- Adding entity-id and content-id to sms request body

- 'Bumping version to 5.7.0'


## [v5.6.5](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v5.6.5) (2021-02-22)

### Merges

- Merge pull request #246 from Vonage/bugfix/record-channels-serialization

setting Channels parameter to nullable

### Other

- Setting Channels parameter to nullable

- Adding clean/clear bit to GHA to clear out nuget cache

- 'Bumping version to 5.6.5'


## [v5.6.4](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v5.6.4) (2021-02-03)

### Bug Fixes

- Fixing unit test


### Merges

- Merge pull request #242 from Vonage/fixing_unit_test

fixing unit test
- Merge branch 'master' into syncronous_methods_blazor

- Merge pull request #243 from Vonage/syncronous_methods_blazor

Fixing issue with sync methods in blazor
- Merge pull request #245 from Vonage/github_actions_main

moving nexmo GHA -> main

### Other

- Removing GetAwaiter().GetResult() pattern from sync methods

- Adding async unit tests

- Moving nexmo GHA -> main

- 'Bumping version to 5.6.4'


## [v5.6.3](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v5.6.3) (2021-01-19)

### Merges

- Merge pull request #240 from Vonage/DLR_status_ignore

Ignoring status for Vonage.Messaging.DeliveryReceipt.

### Other

- 'Bumping version to 5.6.3'


## [v5.6.2](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v5.6.2) (2021-01-15)

### Other

- Adding output path
- 'Bumping version to 5.6.2'


## [v5.6.1](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v5.6.1) (2021-01-15)

### Other

- 'Bumping version to 5.6.1'


## [v5.6.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v5.6.0) (2021-01-15)

### Bug Fixes

- Fixing csproj name

### Merges

- Merge pull request #239 from Cereal-Killa/master

Enhancements to the ISmsClient
- Merge pull request #241 from Vonage/auto_release_nuget

adding Nuget release workflow - removing nuspec file.

### Other

- Enhancements to the ISmsClient

- Fixup for sms type.

- Simplified names.

- Ignoring status for Vonage.Messaging.DeliveryReceipt.

- Adding Nuget release workflow - removing nuspec file.


## [v5.5.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v5.5.0) (2020-11-19)

### Bug Fixes

- Fixing unit tests


### Merges

- Merge branch 'master' into feature/voice_language_style
- Merge pull request #232 from Vonage/feature/voice_language_style

Adding Language and style, marking VoiceName as obsolete

### Other

- Revving nuspec version


## [v5.4.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v5.4.0) (2020-11-12)

### Merges

- Merge pull request #220 from Vonage/readme-updates

adding compatibility list, fixing nuspec
- Merge pull request #228 from onpoc/master

query null check issue
- Merge pull request #227 from kzuri/master

Correcting enumeration of workflow
- Merge pull request #225 from perry-contribs/master

Typo fix - Update README.md
- Merge pull request #229 from Vonage/async_faq

Adding FAQ Section - one question for async at the moment.
- Merge pull request #230 from smikis/#126-async-implementation

Async calls implementation
- Merge branch 'master' of https://github.com/Vonage/vonage-dotnet-sdk into master

- Merge pull request #231 from gagandeepp/master

Issue 1221 Fix
- Merge pull request #238 from Vonage/readying_for_release

Readying for 5.4.0 release.

### Other

- Updating NuGet badge
- Adding compatibility list, fixing nuspec

- Typo fix - Update README.md

fixed spelling of word 'globally'.
- Correcting enumeration
- Correcting enumeration
- Correcting enumeration
- Correcting enumeration
- Query null check issue

query null check issue to InboundSms.cs
- Adding FAQ Section - one question for async at the moment.

- Async calls implementation

- Issue 1221 Fix

- Add additional methods for non async calls

- Adding Language and style, marking VoiceName as obsolete, also removing some unnecessary usings

- Remove async code from unit tests

- Review Pointer : Method signature fixes

- Method implmentation Added

- #126 Use async stream reading method

- Method implementation added

- Unit test needs to be fixed

- Code Review pointer added

- Code Review Pointer Implemented

- Fixing package name in readme
- Pointer Implemented

- Code Review Pointer Added

- Code Review poinrer added

- Code review pointers implemented

- Code pointer added

- Code review pointer fixed

- Build Fixes

- Readying for 5.4.0 release.


## [v5.3.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v5.3.0) (2020-09-02)

### Merges

- Merge branch 'Vonage-rename' of https://github.com/Nexmo/nexmo-dotnet into Vonage-rename

- Merge pull request #215 from Vonage/Vonage-rename

Switching over to Vonage naming
- Merge pull request #217 from Vonage/fix-action

Fix GHA

### Other

- Switching over to vonage naming

- Removing Nexmo directories, trying github action

- Trying choco to install codecov

- Moving codecov action

- Removing extra codecov step

- Moving codecov test project

- Testing with enviornment variable

- Trying alternate env var format

- Trying alternate env var format

- Trying codecov ignore

- Removing config file, removing redudant internal utility classes

- Removing legacy Call objects

- Removing RequiredIfAttribute

- Removing Nexmo directories, trying github action

removing config file, removing redudant internal utility classes

- Adding Status badge

- Removing extra ! from README

- Fixing action by changing dotnet-version

- Revving version

- Updating license

- Moving nuspec to Apache-2.0 Licence

- Fixing badge
- Fixing codecov badge
- Fixing licence nuspec tag.

## [v5.2.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v5.2.0) (2020-08-07)

### Merges

- Merge pull request #208 from Nexmo/webhook_utility

Adding utility methods for parsing webhooks
- Merge pull request #210 from Nexmo/multInputParseIssue

Fixing issue with ParseEvent for multiinput

### Other

- Adding utility methods for parsing webhooks

- Adding Url Decoding to URL parser, updating tests so that they check for multi-word inbound messages, updating readme

- Fixing issue with ParseEvent for multiinput

- Bumping version to 5.2.0


## [v5.1.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v5.1.0) (2020-07-01)

### Bug Fixes

- Fixing path for psd2, updating version in nuspec


### Merges

- Merge pull request #207 from Nexmo/psd2

Adding Psd2 functionality

### Other

- Delete codecov.yml
- Adding psd2

- Reving version

- Updating tests for correct path


## [v5.0.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v5.0.0) (2020-06-22)

### Bug Fixes

- Fixing minor bugs in voice client per spec issue


### Merges

- Merge pull request #204 from Nexmo/v5.0.0

Merging 5.0.0 PR for release.

### Other

- Adding unicode test cases for sms and voice - changing encoding for json to UTF8

- Removing sms search

- Revving version


## [v4.4.1](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v4.4.1) (2020-06-17)

### Merges

- Merge pull request #202 from Nexmo/dotnet_standard_summary_docs

.NET standard consolidation, summary docs, couple of enums
- Merge pull request #206 from Nexmo/fixing_encoding

changing payload encoding to utf8

### Other

- Merging with 4.4.0

- Updating merge to fix the test cases

- Updating readme for 5.0

- Removing unecessary BC

- Adding codecov badge
- Changing payload encoding to utf8


## [v4.4.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v4.4.0) (2020-06-05)

### Bug Fixes

- Fixing test input

- Fixing call converter, adding first Call Test

- Fixing some issues with the summary docs and adding back 452/46 - managing rearranging some depdnencies in the nuspec file

- Fixing the nuget package so it adds xml files

- Fixing typo for netcoreapp3.0


### Merges

- Merge pull request #194 from Nexmo/conform_to_naming_conventions

Conforming .NET library to standard .NET naming conventions - adding obsolete tags over old data structures
- Merge branch 'v5.0.0' into unit_test_rework

- Merge pull request #188 from Nexmo/unit_test_rework

Unit test rework
- Merge pull request #203 from Nexmo/feature/asr_nccos

adding ASR webhook and input items

### Other

- Merging naming convention stuff and doing some minor name-space cleanup

- Adding pricing client to NemxoClient, cleaning up property names in Account settings, adding pricing and Account APIs

- Adding more pricing tests in both legacy and new

- Adding new Secrets tests - adding HALLink to secret object

- Adding tests for legacy api secret api

- Removing redundant List Applications method, Deleting Legacy ApplicationV1 client as it's been tagged as obsolete through a full Major Release Cycle

- Making ApiKey a required parameter for secrets APIs

- Updates to Application structures for correctness, adding create Application test

- Adding path coverage for credentials passing

- Adding more applicaiton tests

- Adding credential testing paths to legacy tests

- Adding Conversion tests

- Adding MessagesSearch tests, updating MessagesSearch request, and ApiRequest to accomodate multiple search ids

- Adding send SMS tests

- Adding some missing stuff to messaging structs, adding inbound/dlr tests

- Signature generation/validation test

- Adding webhook tests

- Adding notification test, unanswered test, and null test

- Adding Ncco Serializations tests

- Adding legacy create call test

- Adding list calls test

- Adding streaming tests

- Adding get recordings test

- Removing superfolous voice class

- Removing signature helper and unused request in ApiRequest.cs

- Renaming ApiRequest method 'DoGetRequestWithUrlContent' to 'DoGetRequestWithQueryContent' adding NumberInsights exceptions, adding numberinsights tests

- Adding response to NexmoNumberResponseException, changing name of NumberInsightsResponseException to NexmoNumberInsightResponseException adding Number tests fixing minor issues with Number class structure

- Adding codecov file

- Updates to codecov file

- Changing path for codecov ignore

- Adding error handling to verify, adding Verify tests

- Adding summary docs as well as some new enumerated types

- Making unit tests compiliant to the new enums

- Dropping core 3.0 tests

- Adding some extra path testing for missing status

- Adding ASR webhook and input items

- Rolling back input updates adding new multiInput class and tests for it

- Apparently first run at switching out ASR with multi-input tests didn't take

- Adding error field to speech webhook struct

- Tearing out weird hard-coded path

- Updating release notes in nuspec


## [v4.3.2](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v4.3.2) (2020-04-08)

### Bug Fixes

- Fixing build issue

- Fixing broken tests - setting optional parameters in application list call to nullable


### Merges

- Merge branch 'v5.0.0' into unit_test_rework

- Merge pull request #193 from Nexmo/add-code-of-conduct

Create CODE_OF_CONDUCT.md
- Merge branch 'conform_to_naming_conventions' of https://github.com/Nexmo/nexmo-dotnet into conform_to_naming_conventions

- Merge pull request #195 from Nexmo/vonage-wordmark

Update branding in README
- Merge pull request #197 from taylus/master

Serialize streamUrl as array per docs to avoid bad request
- Merge pull request #199 from Nexmo/bugfix/websocket_header_serialization

changing headers type to object to allow it to serialize cleanly.

### Other

- Merging with master

- Merging

- Create CODE_OF_CONDUCT.md
- Adding badge

- Reforming voice client

- Pushing up

- Some preliminary changes to accounts API

- Account / secrets names fixed

- Account renaming

- Application renaming

- Pricing numbers and redact

- Adding back client

- Finishing off the actual renaming part

- Adding obsolete tags

- Adding factory methods for creating Credentials

- Add Vonage wordmark to Nexmo repo

- Various updates made while creating code snippets

- Serialize streamUrl as array per docs to avoid bad request

- Adding Taylus to the contributors section of the readme
- Updating naming stuff to make sure they work and work cleanly

- Adding logger to nuspec

- Update Release GH Action workflow
- Changing headers type to object to allow it to serialize cleanly.

- Updating for 4.3.2 release


## [v4.3.1](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v4.3.1) (2020-02-18)

### Bug Fixes

- Fixing search numbers test


### Merges

- Merge branch 'unit_test_rework' of https://github.com/Nexmo/nexmo-dotnet into unit_test_rework

- Merge branch 'add_logging_extensions' of https://github.com/Nexmo/nexmo-dotnet into add_logging_extensions

- Merge pull request #189 from Nexmo/add_logging_extensions

Add logging extensions
- Merge branch 'v5.0.0' into unit_test_rework

- Merge pull request #191 from Nexmo/bugfix/malformed_user_agents

Fixing malformed user agents
- Merge branch 'master' into bugfix/loop_param_ignored_when_zero

- Merge pull request #190 from Nexmo/bugfix/loop_param_ignored_when_zero

Fixing default serialization of zero's in loop
- Merge pull request #187 from Nexmo/exception_handling

Fix error handling so that well formed exceptions are thrown explicity.

### Other

- Adding nullable contingencies

- Changing exception handling

- Further cleanup and exception throwing

- Adding VerifyResponseException, moving common response stuff to VerifyResponseBase class, making Verify Request, Check, and Control throw VerifyResponseExceptions when a failure is detected

- Moving all requesting logic to ApiRequest - deleting VersionedApiRequest class pointing everything at the more common methods

- Cleaning up stuff for unit tests

- Creating branch

- Switching to xUnit

- Adding signature and inbound test

- Adding full set of signing tests and dlr test

- Adding verify test

- Committing unit test work

- Adding logging extension

- Adding Microsoft.Extensions.Logging to cs file - synchonizing extensions at 1.1.2

- Adding nullable contingencies

- Removing old LibLog

- Switching to xUnit

- Adding signature and inbound test

- Adding full set of signing tests and dlr test

- Adding verify test

- Committing unit test work

- Switching to xUnit

- Adding signature and inbound test

- Adding full set of signing tests and dlr test

- Resolving merge conflicts

- Axing merge flags

- Merging with the v5.0.0 branch

- Adding some summary documentation, changing names of the tons of DoRequest calls

- Adding summary docs to all the client methods indicating the new exception throws

- Allowing loop for talk and stream and making the seralizer ignore if null and include if zero

- Removing OS description from User agent - making sure to scrub the runtimeVersion of any parentheses that could cause an error to propogate from the runtime.

- Updating for error handling PR

- Revving version for release

- Updating release notes for nuget package


## [v4.3.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v4.3.0) (2020-01-17)

### Bug Fixes

- Fixing private key formattign in readme


### Merges

- Merge pull request #160 from Nexmo/contributing

Add simple contributing file
- Merge pull request #177 from Nexmo/adding_appveyor

Adding badge for appveyor
- Merge branch 'master' into feature/add_list_own_numbers

- Merge branch 'feature/add_list_own_numbers' of https://github.com/Nexmo/nexmo-dotnet into feature/add_list_own_numbers

- Merge pull request #180 from Nexmo/feature/add_list_own_numbers

Feature/add list own numbers make JWT generator public
- Merge pull request #183 from Nexmo/list-owned-numbers-patch

making has_application nullable

### Other

- Fixing Nexmo.Application.Key description

- Adding badge for appveyor

- Removing extra nuget badge

- Switching to xUnit

- Adding signature and inbound test

- Adding full set of signing tests and dlr test

- Adding verify test

- Making jwt generation public, reving nuspec to 4.2.2

- Making jwt generation public, reving nuspec to 4.3.0

- Making jwt generation public, reving nuspec to 4.3.0

- Removing newline

- Updating assembly version

- Update README.md
- Making has_application nullable
- Committing unit test work

- Adding logging extension

- Adding Microsoft.Extensions.Logging to cs file - synchonizing extensions at 1.1.2

- Update README.md

## [v4.2.1](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v4.2.1) (2019-11-11)

### Merges

- Merge branch 'bugfix/rsa-on-osx' of https://github.com/Nexmo/nexmo-dotnet into bugfix/rsa-on-osx

- Merge pull request #155 from Nexmo/bugfix/rsa-on-osx

Avoiding creating RSACng on non-windows platforms
- Merge pull request #174 from Nexmo/bugfix/ConfigurationAbstractionDepdencyBug

Fixing Unit tests, fixing Application flow, fixing possible NRE
- Merge pull request #169 from Nexmo/refactor

Fixing Ncco serialization bug

### Other

- Fixing Ncco serialization bug

- Avoiding creating RSACng on non-windows platforms

- Adding Fauna5 to readme

- Fixing Unit tests, breaking out Application List get call to work the same way everything else doees, fixing Null Reference Exception Bugs, fixing dependency nuget issue

- Adding other unit testing frameworks

- Adding list own number request

- Removing unecessary class

- Updates

- Changing payload to an object

- Adding auth string encoding for application gets


## [v4.2.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v4.2.0) (2019-10-21)

### Merges

- Mergeing with master

- Merge pull request #167 from Nexmo/refactor

Adding type-safe webhooks and NCCOs. Adding application_id and has_application to Numbers API

### Other

- Adding NccoObj field to CallCommand, creating CallCommandConverter to explicitly handle callCommand json serialization, fixing nuspec, creating Ncco converter to serialize NCCOs decorating Ncco field in CallCommand to obsolete


## [v4.1.2](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v4.1.2) (2019-10-11)

### Bug Fixes

- Fixing url encoding issue with signed SMS


### Merges

- Merge branch 'redact_fixes' into application_fixes

- Merge pull request #163 from Nexmo/application_fixes

Application Update / List, Redact, GetRecording fixed or added
- Merge pull request #166 from Nexmo/4_1_1

Harmonizing dependencies 
- Merge branch 'refactor' of https://github.com/Nexmo/nexmo-dotnet into refactor

- Merge branch 'master' into refactor
- Merge pull request #168 from Nexmo/add_sms_signing

Adding sms signing

### Other

- Add auto-changelog on Release

This PR adds a Github action which is triggered when a release is published. The action adds a new entry to the public Nexmo changelog with the contents of the release notes
- Migrate Github Actions to YAML format

- Add simple contributing file

- Adding workflow_id to VerifyRequest

- Adding workflow_id to VerifyRequest

- Fixing application update structures

- Fixing redact api

- Removing api_driver

- Adding getRecordingRequest

- Adding getRecording

- Cleanup

- Reving version

- Revving version

- Adding NCCO And Input Classes

- Fixing Configuration.Abstractions incorrect assembly loading issue

- Harmonizing Dependencies

- Removing aspnetcore from the regular .NET packages

- Fixing indentation

- Fixing application update structures

- Fixing redact api

- Removing api_driver

- Adding getRecordingRequest

- Adding getRecording

- Cleanup

- Reving version

- Revving version

- Adding NCCO And Input Classes

- Adding has_application and application_id to query for Numbers

- Forcing ordinal values to prevent nulled serialization

- Fixing SMS Signing for Hash and adding HMAC SMS signing

- Cleanup

- Enhancing so user doesn't have to generate their own signature string

- Changing data type to IDictionary and adding release notes to nuspec


## [v4.0.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v4.0.0) (2019-06-11)

### Bug Fixes

- Fixes to use the right authorization

- Fixed method signature


### Merges

- Merge pull request #156 from Nexmo/ApplicationV2

Application v2 - READY TO MERGE

### Other

- [WIP] : Application V2 first commit

- Hacky fix for GET Application

- Hacky fix for GET List until the API is properly fixed

- Fixing all the typos from coding at 1AM

- Fixed capabilities objects, we can only have one WebHook per type

- Added tests for Application V2

- API was fixed


## [v3.4.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v3.4.0) (2019-05-10)

### Bug Fixes

- Fixed conflict


### Merges

- Merge pull request #151 from Nexmo/Feature_completion

Feature completion

### Other

- GetRecording
- Avoiding creating RSACng on non-windows platforms

- Added package properties

- More merge conflicts


### Reverts

- Revert "WIP: getRecording"

This reverts commit 6546e31afad4fc190cdc2ab442fee2de25654818.


## [v3.3.1](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v3.3.1) (2019-04-23)

### Bug Fixes

- Fixing code and renaming creds to credentials


### Merges

- Merge pull request #128 from MrEpiX/patch-1

Updated readme small text fixes
- Merge pull request #121 from cirojr/master

Removed unused App.config from project
- Merge pull request #141 from Nexmo/CallEditCommandFix

Fixed Destination in call command
- Merge pull request #148 from Nexmo/bug_124_fix

Fixed bug 124
- Merge pull request #153 from Nexmo/NCCOImplementation

Implemented NCCO param for creating a call

### Other

- Updated changelog.md

- Update README.md (#118)

Fixing Link for Redacting a message
- Fix mismatch on casing on the name of the folder Nexmo.Api (#119)


- Update README.md (#120)


- Removed unused App.config from project

- Updated readme small text fixes

Added a dot to the end of the License-section and removed an unused Create Account-link.
- Fixed Destination in call command

- Updated test dependencies

- Fixed bug 124

- Added GetPrefixPricing method

- Added submitConversion method and tests

- Changed creds to credentials based on MAnik's review

- Implemented NCCO param for creating a call


### Reverts

- Revert "changed creds to credentials based on MAnik's review"

This reverts commit 5582b1b26c84362243fdbc598e313646997f9048.


## [v3.2.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v3.2.0) (2018-09-28)

### Merges

- Merge pull request #115 from Nexmo/secretapi

Implement API Secret calls

### Other

- Implement API Secret calls

- Renamed methods to meet specs


## [v3.1.1](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v3.1.1) (2018-08-30)

### Merges

- Merge pull request #106 from Nexmo/Credentials_fix

adding default constructor to credentials class

### Other

- Adding default constructor to credentials class

- Updated CHANGELOG and Client Lib version for release


## [v3.1.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v3.1.0) (2018-08-17)

### Merges

- Merge pull request #103 from Nexmo/RedactImplementation

Redact implementation

### Other

- Adding constructors for basic and full auth (#92)

* Adding constructors for basic and full auth
- VS added services for test running

- Switch to liblog (#101)

Switch to LibLog
- Update liblog example
- Added redact transaction functionnality

- Add issue template
- Added Redact

- Updated README

- Updated CHANGELOG

- Renaming some methods


## [v3.0.1](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v3.0.1) (2018-03-05)

### Bug Fixes

- Fixed missed NumberInsight instanciation (#90)



### Other

- Move integration tests to mstest; update mstest version

- 3.0.1


## [v3.0.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v3.0.0) (2018-02-24)

### Bug Fixes

- Fixed 'roaming' property to match the json (#74)



### Merges

- Merge branch 'master' into 3.0

(v2.2.2 to v2.3.1)

- Merge remote-tracking branch 'origin/master' into 3.0

- Merge remote-tracking branch 'origin/master' into 3.0

- Merge remote-tracking branch 'origin/master' into 3.0


### Other

- No samples in 3.0

- Fix silly copy & paste error with request.ip
- Ni refresh (#62)

* Work in progress: updating Number insight

* Number Insight : updated Endpoints + fixed naming

* converting all properties name to follow c# convention + mapping to Json properties

- 2018!

- Fix #78 : event_url returns the url and other meta data thus it is a string[] (#81)


- #36 Updated README (#84)

* Updated README

* fixed typo

- Remove deprecated PackageTargetFallback

- Remove IO dependency; expect contents of private key and not path to key

- Document credentials fields

- Call static methods instead of copying code; add missing doc; add missing VersionedApi Call calls

- #82 fixed message_timestamp deserialization issue by changing the property to DateTime instead of string (#86)

* #82 fixed message_timestamp deserialization issue by changing the property to DateTime instead of string
- Minor doc updates for v3.0

- Add missing using

- Upgrade System.Security.Cryptography as that seems to stop #83 from happening

- Ensure netstandard2.0 uses correct crypto

- Update a few deps; explicitly call out target frameworks as nuget likes that better

- Correct v3.0 date

## [v2.3.1](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v2.3.1) (2017-11-24)

### Other

- Clarify NumberInsight changes

- Put the NI changes under the correct item...
- V2.3.1; Set Json serialization DefaultValueHandling to ignore (addresses Voice API usage regression)

- V2.3.1 changelog

## [v2.3.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v2.3.0) (2017-11-09)

### Other

- Move unit tests to mstest

- Instance client

- Nuget prerelease 1

- 3.0 pre-release badge
- Updated ReadMe to contain more building blocks (#39)

* updated ReadMe to contain more building blocks

* updated list of examples and links to point to the new NDP + formatting

- Minor formatting
- Update jose-jwt to 2.3.0

- Move to .NET Standard 2.0, update deps, release pre2. Closes #37

- Bring back netstandard1.6 support; Remove unused System.Xml.* dependencies; netstandard2.0 should use Microsoft.Extensions.DependencyInjection v2.0

- Pass credentials into SetUserAgent so when file config isn't being used the option to send app version is still available

- Back to keeping standard1.6

- Bring back 1.6
- V3.0 note
- Specify .NET Core SDK via global.json; fixes #69

- Start/end time can return as null

- API and doc refresh; GetBalance and NI breaking changes

- Support additional call endpoint types; closes #70

- Add Nexmo.Api.EnsureSuccessStatusCode configuration option

- Address ShortCode.RequestAlert request bug

- Test updates for 2.3.0

- Prep samples for v2.3.0

- Expose the configuration ILoggerFactory for use with external logging implementations.

- Prep for 2.3.0

- Attribution to @RabebOthmani for NI!


## [v2.2.2](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v2.2.2) (2017-06-19)

### Other

- Switch to RSACng for .NET 4.6+

- Tag 3.0.0

- Remove support for configuration via web.config.

- Removing examples as they will be pushed to the community samples repo

- Move to VS2017

- Style improvements (#31)

Changed Call API to Voice API and other stylistic improvements
- V2.2.2; Updated jose-jwt to 2.3.0 which is reported to address key loading issues.


## [v2.2.1](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v2.2.1) (2017-03-21)

### Other

- Dependency updates

- Add link to separate examples repo in README

- Fixed NuGet dependencies


## [v2.2.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v2.2.0) (2017-03-10)

### Other

- Syntax

- 2.2.0


## [v2.2.0-rc2](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v2.2.0-rc2) (2017-01-28)

### Other

- Allow PKCS#8 formatted private keys; auth key parser logging

- 2.2.0-rc2


## [v2.2.0-rc1](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v2.2.0-rc1) (2017-01-13)

### Other

- Support optional throttling (#19)


- Prep for 2.2.0

- Add nuget shield
- Initial support for signing requests; quick example of verifying signature

- Sig verify refactoring

- Config and request logging. Addresses the majority of #9

- Cleanup

- Make account calls conform internally to the rest of the API

- Allow override of credentials per request. Closes #18

- 🎉 2017! 🎉

- Expose API request methods to allow custom API calls from library consumers

- Update documentation

- Push 2.2.0-rc1


## [v2.1.2](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v2.1.2) (2016-12-07)

### Other

- * Look for appsettings.json (netcore webapp convention)
* Ensure XML config parser only looks for keyvalues inside appSettings and connectionStrings elements.
* Gracefully ignore elements with key attribute but not value attribute.


## [v2.1.1](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v2.1.1) (2016-12-07)

### Other

- Remove accidental writeline

- 2.1.1 - look for legacy app.config convention of [exec process].exe.config


## [v2.1.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v2.1.0) (2016-11-18)

### Other

- Read settings from web.config; fixes #14

- Implement user-agent support. Fixes #10

- .NET Core web example

- 2.1.0 version bump and doc changes


## [v2.0.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v2.0.0) (2016-10-24)

### Merges

- Merge pull request #13 from Nexmo/netcore

.NET Core/Standard and v2.0 release

### Other

- Update gitignore

- NumberInsight basic + standard

- Implement Verify Control call

- Update README

- Update README

- Application API

- Initial JWT support

- Cleanup tests a bit; remove deprecated voice api; implement remainder of call-specific api calls

- Implement remainder of voice calls

- Working on core port

- Minor changes to tts sample

- Reworking tests, nuspec, project

- Update readmes

- Add note re: config file change

- Fix JWT generation (key import fail) on OSX/Linux

- Dependency marking for netstandard1.6; dep cleanup


## [v1.0.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v1.0.0) (2016-03-19)

### Merges

- Merge pull request #1 from jonferreira/master

Implement SMS send

### Other

- Initial public release.

- Update SMS.cs
- Update NumberVerify.cs
- Update ApiRequest.cs
- Minor cleanup

- Add SMS type and response codes (thanks @DJFliX)

- Starting unit and integration tests, bit of refactoring

- Move to 4.5.2

- Implement Number calls

- Cleanup; sms test

- Update sample configs

- Implement SMS inbound and receipt

- Implement search

- Implement remainder of account calls

- Implement short code calls

- Update packages - mainly for JSON.net 8

- Implement voice call and TTS

- Prep for nuget release

- Minor API updates

- Rename SMS.SendSMS to SMS.Send

- Update packages

- Minor nuget spec change

- Support for 4.5.2-4.6.1; housekeeping


<!-- generated by git-cliff -->
