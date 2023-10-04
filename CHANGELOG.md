# v6.10.0 (2023-10-04)

* feat: add ja-jp locale to VerifyV2
* feat: add feature to verify Jwt signature
* feat: extend registration with missing clients and token generator
* refactor: Remove unused class ResponseBase
* refactor: replace TimeSpamSemaphore anf ThrottlingMessageHandler by proper dependency
* refactor: implement GetHashCode for ParsingFailure
* refactor: use a 2048 bits key for Linux and MacOs platforms
* refactor: use sonar.token instead of deprecated sonar.login
* refactor: add missing assertion in parser test
* refactor: align method signatures for async/sync methods on AccountsClient
* refactor: add missing optional parameter for VerifyClient
* refactor: make NonStateException compliant to ISerializable
* refactor: simplify ternary operator in Result
* refactor: update serialization settings
* refactor: remove obsolete method on TestBase
* refactor: enable parallelized tests on Vonage.Test.Unit
* refactor: update TestBase
* refactor: update sub clients
* refactor: replace null by optional values in ApiRequest
* refactor: simplify ApiRequest
* refactor: cover signature in query string
* ci: fix some build warnings
* ci: restore tests parallelism for Vonage.Test.Unit
* ci: remove old workflow 'publish-nuget'
* ci: mutation testing improvement
* ci: upgrade actions/checkout to v3 for mutation testing
* docs: update changelog with v6.9.0

**Full Changelog**: https://github.com/Vonage/vonage-dotnet-sdk/compare/v6.9.0...v6.10.0

---

# v6.9.0 (2023-09-07)

* feat: initialize credentials from Configuration
* feat: add versioning on Meetings API Uri
* fix: version substring in release pipeline
* refactor: update error status codes in PBT for VonageClient
* refactor: configuration improvement
* refactor: extend regex timeout
* refactor: remove InternalsVisibleTo property
* ci: pipelines permissions
* ci: release
* ci: pipeline permissions
* ci: add .editorconfig to solution
* ci: add pre-commit-config
* ci: release pipeline
* release: v6.9.0
* doc: changelog update
* doc: readme update

**Full Changelog**: https://github.com/Vonage/vonage-dotnet-sdk/compare/v6.8.1...v6.9.0

---

# v6.8.0 (2023-08-11)

* feat: users API
* refactor: async result extensions
* refactor: test refactoring
* refactor: simplify e2e tests
* refactor: video e2e refactoring
* refactor: use case helpers
* ci: increase java version to 17
* ci: pipelines permissions

**Full Changelog**: https://github.com/Vonage/vonage-dotnet-sdk/compare/v6.7.1...v6.8.0

---

# v6.7.1 (2023-07-25)

* fix: conversation StartOnEnter
* doc: changelog update
* refactor: naming update
* refactor: package update
* refactor: proactive connect e2e
* refactor: meetings Api e2e
* refactor: subaccounts e2e
* ci: improve performance
* ci: upgrade

**Full Changelog**: https://github.com/Vonage/vonage-dotnet-sdk/compare/v6.7.0...v6.7.1

---

# v6.7.0 (2023-07-05)

* feat: add specific object for ApiKey
* feat: add Type property on Failures
* feat: proactive connect
* feat: add MeetingsApi and ProactiveConnectApi clients on service injection
* refactor: failure extensions
* build: packages update
* doc: update supported apis
* release: v6.7.0

**Full Changelog**: https://github.com/Vonage/vonage-dotnet-sdk/compare/v6.6.0...v6.7.0

---

# v6.6.0 (2023-06-27)

* feat: meetings Api
* refactor: e2e testing
* doc: changelog update
* doc: add service registration in readme
* release: upgrade version

**Full Changelog**: https://github.com/Vonage/vonage-dotnet-sdk/compare/v6.5.0...v6.6.0

---

# v6.5.0 (2023-06-22)

* feat: subaccounts
* refactor: move AuthenticationHeader creation on Credentials
* refactor: use case enhancement
* release: v6.5.0
* doc: changelog update
* doc: readme update

**Full Changelog**: https://github.com/Vonage/vonage-dotnet-sdk/compare/v6.4.0...v6.5.0

---

# v6.4.0 (2023-06-09)

* fix: SubAccounts implementation
* feat: implement Match on Result with void return type
* feat: dependency injection extension
* feat: add 4.8.1 and 7.0 in targeted frameworks
* ci: fix multiframework pipeline
* doc: readme badge fix

**Full Changelog**: https://github.com/Vonage/vonage-dotnet-sdk/compare/v6.3.3...v6.4.0

---

# v6.3.3 (2023-06-01)

* fix: basic auth encoding

**Full Changelog**: https://github.com/Vonage/vonage-dotnet-sdk/compare/v6.3.2...v6.3.3

---

# v6.3.2 (2023-05-22)

* fix: change RequestId to Guid for VerifyCodeRequest

**Full Changelog**: https://github.com/Vonage/vonage-dotnet-sdk/compare/v6.3.1...v6.3.2

---

# v6.3.1 (2023-05-19)

* feat: proactive connect
* feat: add optional claims when generating a token
* feat: verifyV2 BYOP
* feat: verifyV2 Cancel
* feat: verifyV2 fraud check
* feat: voice advanced machine detection

**Full Changelog**: https://github.com/Vonage/vonage-dotnet-sdk/compare/v6.2.0...v6.3.0

---

# v6.2.0 (2023-04-19)

* fix: missing dependencies
* refactor: make builders internal

**Full Changelog**: https://github.com/Vonage/vonage-dotnet-sdk/compare/v6.1.0...v6.2.0

---

# v6.1.0 (2023-04-17)

* feat: add premium to start talk request
* feat: webhook classes for messages
* refactor: remove duplicate code for sync version of methods
* refactor: extend responses and monads capabilities
* style: unify test class names
* fix: add basic auth support for Messages

**Full Changelog**: https://github.com/Vonage/vonage-dotnet-sdk/compare/v6.0.5...v6.1.0

---

# v6.0.5 (2023-03-27)

* Fix Numbers Api authentication to match Api specs
* Add latest Messages features
* Remove hardcoded keys from the repository
* Use System.Text.Json instead of Newtonsoft for Messages Api
* New Vonage.Common project
* Readme update

**Full Changelog**: https://github.com/Vonage/vonage-dotnet-sdk/compare/v6.0.4...v6.0.5

---

# v6.0.4 (2023-01-13)

* Align frameworks on netstandard2.0
* Subaccount support

**Full Changelog**: https://github.com/Vonage/vonage-dotnet-sdk/compare/v6.0.3...v6.0.4

---

# v6.0.3 (2022-08-04)

- First v6 general release.

---

# v6.0.2-rc (2022-05-31)

- Adding Real Time Data for Advanced Number Insights

---

# v6.0.1-rc (2022-05-25)

- Reinstating .ToString method on Ncco class
- Making Vonage serialization settings public
- Removing `VersionPrefix` from project file as to not confuse
- Renaming Number Insights methods so not confusing between `async` and `Asynchronous`

---

# v6.0.0-rc (2022-05-24)

- Removing legacy Nexmo classes that have been marked as obsolete in previous versions
- Renaming enums to use Pascal Case as is accepted practice
- Moving serialisation settings to a single location
- Adding methods for new Messages API (SMS, MMS, WhatsApp, Messenger, Viber)
- Refactoring NCCO class to use List as it's base class
- Misc. refactoring

---

# v5.10.0 (2022-04-20)

- Real-Time data for advanced number insights
- Unit Test refactoring
- Authentication exceptions to give more information if incorrect authentication credentials are supplied

---

# v5.9.5 (2022-01-19)

- NCCO Input "Type" property added to align with documentation

---

# v5.9.3 (2021-11-23)

- Fixing an issue caused by the usage of a non thread safe Dictionary.

---

# v5.9.2 (2021-11-04)

- Fixing issue with Advance Number Insights throwing an exception when status = `not_roaming`

---