# Breaking changes in version 9.0.0

This document covers all upcoming breaking changes that will be introduced with v9.0.0.

If you migrate from v8.X.X to v9.X.X (or above), you will have to handle the following points in your codebase.

* Applications API has been reworked using builders and results for consistency and better DevEx (branch `breaking-applications`)
* Accounts API has been reworked using builders and results for consistency and better DevEx (branch `breaking-accounts`)
* `ParseEvent()` on `EventWebhooks.Event` will be removed. This method is now unnecessary as any webhook can be
  deserialized using either `Newtonsoft.Json` or `System.Text.Json`
* `RealTimedata` on `AdvancedNumberInsightRequest` will be removed.
* `FraudScore` on `FraudCheck` will be removed.
* `TimeOut` on `MultiInput` will be removed. Please use the `Timeout` of the underlying `Speech` of `DTMF` object
  instead.