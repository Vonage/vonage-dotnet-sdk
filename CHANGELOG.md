# 3.2.0 (2018-09-28)

* Added Secret Management API implementation.

# 3.1.1 (2018-08-30)

* Fixed missed Credentials default constructor.

# 3.1.0 (2018-08-16)

* Added Redact API implementation.

# 3.0.1 (2018-03-05)

* Fixed missed NumberInsight instanciation (#90)

# 3.0.0 (2018-02-23)

* __[BREAKING]__ Deprioritize support for configuration via configuration files.
* __[BREAKING]__ ApplicationKey must now be the actual private key and not a path to the key.
* Instance-based methods are available to use.
* Support .NET Standard 2.0.

# 2.3.1 (2017-11-23)

* Set Json serialization DefaultValueHandling to ignore (Voice API defaults no longer required to be explicitly sent)

# 2.3.0 (2017-11-09)

* __[BREAKING]__ `Account.GetBalance` returns (instead of a `decimal`) a `Balance` object that includes your account
  balance and other properties.
* __[BREAKING]__ `NumberInsight` calls, request classes, and response classes have changed slightly. The underlying
  calls to Nexmo's API have been updated as well. Thanks to @RabebOthmani for the PR!
  * CHANGE: NumberInsightBasicResponse RequestBasic(NumberInsightBasicRequest, ...) => NumberInsightBasicResponse
    RequestBasic(NumberInsightRequest, ...)
  * CHANGE: NumberInsightStandardResponse RequestStandard(NumberInsightBasicRequest, ...) =>
    NumberInsightStandardResponse RequestStandard(NumberInsightRequest, ...)
  * ADD: NumberInsightAdvancedResponse RequestAdvanced(NumberInsightRequest, ...)
  * CHANGE: NumberInsightRequestResponse Request(NumberInsightRequest, ...) => NumberInsightAsyncRequestResponse
    RequestAsync(NumberInsightAsyncRequest, ...)
* API and documentation refresh: Added missing JSON properties, updated summaries from the official docs.
* Support additional call endpoint types.
* Introduced `Nexmo.Api.EnsureSuccessStatusCode` configuration option. You may instruct the library to throw an
  exception if a request results in an unsuccessful HTTP status code.
* Address `ShortCode.RequestAlert` request bug.
* Expose the configuration `ILoggerFactory` for use with external logging implementations.

# 2.2.2 (2017-06-19)

* Updated jose-jwt to 2.3.0 which is reported to address key loading issues.

# 2.2.1 (2017-03-21)

* Fixed NuGet dependencies; testing shows they are now being correctly included when performing `install-package`.

# 2.2.0 (2017-03-10)

* Promoted to release, no changes from rc2.

# 2.2.0-rc2 (2017-01-12)

* Allow PKCS#8 formatted private keys; auth key parser logging.

# 2.2.0-rc1 (2017-01-12)

* Expose internal API request methods to allow custom API calls from library consumers as some new Nexmo API endpoints
  may not be immediately supported.
* Allow override of request credentials per API call.
* Optional configuration and request logging.
* Support signed requests via security key.
* Optional API request rate limiting.

# 2.1.2 (2016-12-07)

* Look for `appsettings.json` (netcore webapp convention)
* Ensure XML config parser only looks for keyvalues inside `<appSettings>` and `<connectionStrings>` elements.
* Gracefully ignore elements with key attribute but not value attribute.

# 2.1.1 (2016-12-06)

* Look for `<executing process>.exe.config` file for XML configuration.

# 2.1.0 (2016-11-18)

* User-Agent reporting. You may also append an application-specific ID via settings.json.
* __[BREAKING]__ Support web.config XML. This has changed the settings.json structure slightly. Please check the README
  for details.

# 2.0.0 (2016-10-24)

* Dependency marking for netstandard1.6

# 2.0.0-rc2 (2016-10-22)

* Fix JWT generation (key import fail) on OSX/Linux

# 2.0.0-rc1 (2016-10-16)

* NumberInsight basic + standard support
* NumberVerify control call
* JWT token generation
* Application API support
* Application-based call API support
* .NET Standard 1.6 support
* __[BREAKING]__ Moved configuration from app.config to settings.json
* __[BREAKING]__ Nexmo.Api.Voice static class has been deprecated - you must move to the new Voice calls inside the new
  Nexmo.Api.Voice namespace. See [the Nexmo docs](https://docs.nexmo.com/voice/voice-api) for details.

JWT notes:

* When registering a new application, make sure you save the private key. This library does not (currently) take care of
  this for you.
* Make sure your saved private key is ASCII (not UTF-8, no
  BOM) - http://stackoverflow.com/questions/1068650/using-awk-to-remove-the-byte-order-mark

# 1.0.0 (2016-03-19)

* Initial release with nuget package
