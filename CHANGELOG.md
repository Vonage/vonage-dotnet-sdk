# Changelog
## [v7.21.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v7.21.0) (2025-06-02)

### Documentation

- Generate changelog for v7.20.1 ([2af7759](https://github.com/Vonage/vonage-dotnet-sdk/commit/2af7759a2285d488bdd0c203c255cb7ef1dae5d4))

- Bump version to v7.21.0 ([b5bcc8f](https://github.com/Vonage/vonage-dotnet-sdk/commit/b5bcc8f4dd2a03a45a436b9763899dfc691c8d6b))


### Features

- Add missing video webhook types in common webhooks ([f22d30a](https://github.com/Vonage/vonage-dotnet-sdk/commit/f22d30a05bb8a7aed72dbb2b1f8e5f86c7220c78))


## [v7.20.1](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v7.20.1) (2025-05-30)

### Bug Fixes

- Ignore ttl for RCS Messages when default ([1a7f6fe](https://github.com/Vonage/vonage-dotnet-sdk/commit/1a7f6fed46f030e5cd198db6d749df283fc0427c))


### Build updates

- Add slack notifications (#608) ([87576ff](https://github.com/Vonage/vonage-dotnet-sdk/commit/87576ffadfaa6b35825d6568c069e9b4a459390d))


### Documentation

- Generate changelog for v7.20.0 ([456ba7c](https://github.com/Vonage/vonage-dotnet-sdk/commit/456ba7ca26e95eb8dbda798089121a9288a0f515))

- Bump version to v7.20.1 ([9ccbddc](https://github.com/Vonage/vonage-dotnet-sdk/commit/9ccbddce37979ec29588c6e8516e2f256f4d8206))

- Generate changelog for v7.20.1 ([fb0976a](https://github.com/Vonage/vonage-dotnet-sdk/commit/fb0976a85357f78ae424b7349bdb7ae84cd4fe6b))


### Refactoring

- Reduce duplication in Result with Try ([cc0fc62](https://github.com/Vonage/vonage-dotnet-sdk/commit/cc0fc6293f2f875c96b65266378166588d6b5bad))

- Reduce duplication in RequestBuilderTest for Start LiveCaptions ([9580a83](https://github.com/Vonage/vonage-dotnet-sdk/commit/9580a838116010d9b8bc60aedfd8c62c1874c640))

- Reduce duplication in RequestBuilderTest for Start AudioConnector ([584ee21](https://github.com/Vonage/vonage-dotnet-sdk/commit/584ee21160420decc28a0b80055c1abe6f30441d))

- Reduce duplication in RequestBuilderTest for Start ExperienceComposer ([ced6c62](https://github.com/Vonage/vonage-dotnet-sdk/commit/ced6c62d8832ceb2f5ea5ebe856b13e3e9a764cf))

- Reduce duplication in RequestBuilderTest for GetEvents Conversations ([86eb3cb](https://github.com/Vonage/vonage-dotnet-sdk/commit/86eb3cbdbb1a24e8d6c206da8a58b9847975c353))

- Clean tests in Messages ([0b9d774](https://github.com/Vonage/vonage-dotnet-sdk/commit/0b9d774ec92d6512a0f8f274898741fd32bc9dd0))

- Clean Messenger Messages ([abb5323](https://github.com/Vonage/vonage-dotnet-sdk/commit/abb5323eb68bbfc0bc591ab8d2081ca18ad97e46))

- Clear warnings in MMS Messages ([4ca7a6b](https://github.com/Vonage/vonage-dotnet-sdk/commit/4ca7a6bc9d5b83e7bc5b1648068ee9a514eb2980))


### Reverts

- Revert "build: add slack notifications (#608)"

This reverts commit 87576ffadfaa6b35825d6568c069e9b4a459390d.
 ([bc73867](https://github.com/Vonage/vonage-dotnet-sdk/commit/bc7386706a23401d32311d2ccb4a520743f4887a))


## [v7.20.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v7.20.0) (2025-05-26)

### Documentation

- Generate changelog for v7.19.0 ([b18b8e1](https://github.com/Vonage/vonage-dotnet-sdk/commit/b18b8e156d330fd617716e9fe15e6cf4be9ec67a))

- Update documentation for voice maximum length_timer ([553ab6a](https://github.com/Vonage/vonage-dotnet-sdk/commit/553ab6a24952363c9cccc98d465cf5c4259460f7))

- Bump version to v7.20.0 ([c5e0530](https://github.com/Vonage/vonage-dotnet-sdk/commit/c5e0530420a88e459e89d52257bf9eafbb452774))


### Features

- Support explicit constructor on generated builders ([5fe6c42](https://github.com/Vonage/vonage-dotnet-sdk/commit/5fe6c42c19affa2db4c58daeca58ebc017c151f4))

- Support multiple validation rules for mandatory builder attribute ([4d63adf](https://github.com/Vonage/vonage-dotnet-sdk/commit/4d63adf595e4706fc9cf3f527fc9f39c56633f62))

- Support optional with default attribute for source generators ([061b1f3](https://github.com/Vonage/vonage-dotnet-sdk/commit/061b1f3f5f2231bf8b6ac16f24ee3455610a033b))

- Support validation rules for optional attributes ([e4d8ffc](https://github.com/Vonage/vonage-dotnet-sdk/commit/e4d8ffc54621ef765705fc6583c4818cd834339e))

- Add User and Domain to SipEndpoint ([37d486e](https://github.com/Vonage/vonage-dotnet-sdk/commit/37d486e848876b4ea0d01c3d909488860f4f6257))

- Update NumberInsight with header authentication ([998e99e](https://github.com/Vonage/vonage-dotnet-sdk/commit/998e99ef6d662b9143dd616238bd63515a860ae9))


### Refactoring

- Use generated builders for SubAccounts ([e358564](https://github.com/Vonage/vonage-dotnet-sdk/commit/e3585640713ea97cf3102db2cf6524b6659d7f37))

- Use generated builders for SubAccounts ([883791e](https://github.com/Vonage/vonage-dotnet-sdk/commit/883791eba432d0c0bca143aa64fa586f282f4d44))

- Use generated builders for Video ([4512b00](https://github.com/Vonage/vonage-dotnet-sdk/commit/4512b00a929ebecbdf37e3943e643c52f5973b40))

- Fix incorrect filename ([6a02dbc](https://github.com/Vonage/vonage-dotnet-sdk/commit/6a02dbceb6666a953c786a2e7e9b537937d1c79e))

- Wrap optionals in Maybe<> for builders ([e12355e](https://github.com/Vonage/vonage-dotnet-sdk/commit/e12355e608938de03da1bf7f7c1fdda21289f60c))

- Use generated builders for Video ([fdaba15](https://github.com/Vonage/vonage-dotnet-sdk/commit/fdaba1554498d13afe37eba01cc4bbbf87293a02))

- Use generated builders for Video ([37e9ab7](https://github.com/Vonage/vonage-dotnet-sdk/commit/37e9ab7f6b797cce13e2a6ef8e61f5d227085544))

- Use generated builders for Video ([91b3493](https://github.com/Vonage/vonage-dotnet-sdk/commit/91b3493b20b7d06725ee2ce2694802276a4398ca))

- Use generated builders for Video ([a4abbaa](https://github.com/Vonage/vonage-dotnet-sdk/commit/a4abbaa0ce280e9bfbc18fbbc5642168e0f64d3e))

- Clean and reorganize source generation ([ee352fd](https://github.com/Vonage/vonage-dotnet-sdk/commit/ee352fd0638d986f730bec8e0b7067bb5f2410bc))


### Reverts

- Revert "refactor: use generated builders for SubAccounts"

This reverts commit e3585640713ea97cf3102db2cf6524b6659d7f37.
 ([1dde6ef](https://github.com/Vonage/vonage-dotnet-sdk/commit/1dde6ef5291a7294f8fd1ce6024fa3a637db549a))


## [v7.19.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v7.19.0) (2025-05-21)

### Documentation

- Generate changelog for v7.18.0 ([cb9a893](https://github.com/Vonage/vonage-dotnet-sdk/commit/cb9a893cc5706c9547ac8f26fcdd80569ebbb019))

- Bump version to v7.19.0 ([4619508](https://github.com/Vonage/vonage-dotnet-sdk/commit/46195083c2f16162346abf589ae8aa2f6af2a315))


### Features

- Support MMS File type in Messages ([4e3b540](https://github.com/Vonage/vonage-dotnet-sdk/commit/4e3b5404fdda85fc09ca93d47910f8b64edbfefa))

- Support MMS Content type in Messages ([017c755](https://github.com/Vonage/vonage-dotnet-sdk/commit/017c755c17c1c1ecf5a363b53918a04bb9f1622c))


## [v7.18.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v7.18.0) (2025-05-19)

### Documentation

- Generate changelog for v7.17.4 ([a316c90](https://github.com/Vonage/vonage-dotnet-sdk/commit/a316c907334c54534779a90e675eabcee17511a6))

- Bump version to v7.18.0 ([d521288](https://github.com/Vonage/vonage-dotnet-sdk/commit/d52128849d4a90dabe15e50b2cbdcbd946b5257d))


### Features

- Add QuantizationParameter when creating archives ([0a2c65f](https://github.com/Vonage/vonage-dotnet-sdk/commit/0a2c65f7b271bea0c873442425bb4ff26445f098))

- Add QuantizationParameter on Archive ([241a8de](https://github.com/Vonage/vonage-dotnet-sdk/commit/241a8de52bc2497a101102a233a7a90f66603fd7))


## [v7.17.4](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v7.17.4) (2025-05-16)

### Bug Fixes

- Add missing text message type for mms ([b56a5e8](https://github.com/Vonage/vonage-dotnet-sdk/commit/b56a5e8805f6a9ee54d4a380ee6e9979337f41d1))


### Documentation

- Generate changelog for v7.17.3 ([e1e4e44](https://github.com/Vonage/vonage-dotnet-sdk/commit/e1e4e449523e334767426f9e79f171c7d0b09588))

- Bump version to v7.17.4 ([39520ea](https://github.com/Vonage/vonage-dotnet-sdk/commit/39520eabd1dda18d397c84747063cfc8723e341f))


## [v7.17.3](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v7.17.3) (2025-05-15)

### Bug Fixes

- Transform EventUrl on transcription settings to an array ([6c2d38e](https://github.com/Vonage/vonage-dotnet-sdk/commit/6c2d38e6e0b4cd58445ea2f0b8e2780bda2aae4d))


### Documentation

- Generate changelog for v7.17.2 ([c98fc6d](https://github.com/Vonage/vonage-dotnet-sdk/commit/c98fc6d3e2f85062a08c19d38d9e57debae5d35a))

- Bump version to v7.17.3 ([e04a435](https://github.com/Vonage/vonage-dotnet-sdk/commit/e04a435f021b9ea09c33d954d130ad0e48821e6f))


## [v7.17.2](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v7.17.2) (2025-05-09)

### Bug Fixes

- Use TestableIO prefix for System.IO.Abstractions ([f7c1310](https://github.com/Vonage/vonage-dotnet-sdk/commit/f7c131055590c6354b10c811526a6183a0b692c9))


### Documentation

- Generate changelog for v7.17.1 ([b4c1306](https://github.com/Vonage/vonage-dotnet-sdk/commit/b4c130665a9ded2eb46b2c28ad94efbb8eed40fa))

- Bump version to v7.17.2 ([4c1184f](https://github.com/Vonage/vonage-dotnet-sdk/commit/4c1184ffd73b0e718931d24afa9e6109ba42461b))


### Refactoring

- Add category for SourceGeneration tests ([e704367](https://github.com/Vonage/vonage-dotnet-sdk/commit/e7043675dd02d08e41746df6b0a816e803c7fa3f))

- Add stryker-config file for Vonage.SourceGenerator.Test ([c7eed02](https://github.com/Vonage/vonage-dotnet-sdk/commit/c7eed028f6a70a7f4cdb1b0790e6ee982afd255b))


## [v7.17.1](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v7.17.1) (2025-05-02)

### Bug Fixes

- Downgrade Microsoft.CodeAnalysis.Csharp to v4.8.0 ([0d75b91](https://github.com/Vonage/vonage-dotnet-sdk/commit/0d75b914f6fd8d3e84d1a7294e46577bd286a744))

- Remove JsonIgnore annotation on AdvanceMachineDetection BeepTimeout ([890933b](https://github.com/Vonage/vonage-dotnet-sdk/commit/890933b69fa95505de496dd23578e9cbdcb5de64))


### Dependencies upgrade

- Update packages ([303f5cb](https://github.com/Vonage/vonage-dotnet-sdk/commit/303f5cbe2859da3abd9e5f197e93e142141c3b9e))

- Update packages ([ba072a8](https://github.com/Vonage/vonage-dotnet-sdk/commit/ba072a8274755e0c46e1e89de1c184e19e440dc1))


### Documentation

- Generate changelog for v7.17.0 ([beb63dc](https://github.com/Vonage/vonage-dotnet-sdk/commit/beb63dc7c885c562bfb77f1018ad116a5b825537))

- Update readme about v8.0.0 breaking changes ([e57b924](https://github.com/Vonage/vonage-dotnet-sdk/commit/e57b92443451ca741e6e33c0c680bb5f4ddb59cc))

- Bump version to v7.17.1 ([7ded98d](https://github.com/Vonage/vonage-dotnet-sdk/commit/7ded98de984e160f6ef3043369016d139215ce74))


### Features

- Rely on source generators to generate builders (#604) ([cd4419d](https://github.com/Vonage/vonage-dotnet-sdk/commit/cd4419d3b680c2a3e90b1a4161ad626663ee9e01))

- Add test project for source generation ([33e7391](https://github.com/Vonage/vonage-dotnet-sdk/commit/33e7391d8354ac7b05af107581db499e77a9cfc7))

- Support OptionalBoolean attribute for source generators ([8564c34](https://github.com/Vonage/vonage-dotnet-sdk/commit/8564c34058165cc2662ef9d190bc3e81a0fc6ef7))

- Update OptionalBoolean attribute ([56b8413](https://github.com/Vonage/vonage-dotnet-sdk/commit/56b8413f3ffc43010e168e1302241ef56f151510))


## [v7.17.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v7.17.0) (2025-03-06)

### Documentation

- Generate changelog for v7.16.0 ([8629ef5](https://github.com/Vonage/vonage-dotnet-sdk/commit/8629ef5f06c0dad8c23386b0c4bbdf0842e4ef20))

- Bump version to v7.17.0 ([b57f5bf](https://github.com/Vonage/vonage-dotnet-sdk/commit/b57f5bf2ecb06058e1b632ec46983dff70255436))


### Features

- Add 'mode' to Voice MultiInputAction ([1457317](https://github.com/Vonage/vonage-dotnet-sdk/commit/14573178ec08554a214871f84295aa7fdb047c5b))

- Implement subscribe and unsubscribe to real-time dtmf events ([b5d4154](https://github.com/Vonage/vonage-dotnet-sdk/commit/b5d4154edbf1f6d310e385b3d6991d80a03f9ea8))

- Add transcription settings on Record action ([0ce645c](https://github.com/Vonage/vonage-dotnet-sdk/commit/0ce645cff7efa25f9856aa5ad2f97faba5886c2c))


## [v7.16.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v7.16.0) (2025-02-17)

### Dependencies upgrade

- Remove unnecessary dependencies (#600) ([f4ece67](https://github.com/Vonage/vonage-dotnet-sdk/commit/f4ece670500dc5311bb94e6a59eedc90b063ce41))


### Documentation

- Generate changelog for v7.15.0 ([d0fde03](https://github.com/Vonage/vonage-dotnet-sdk/commit/d0fde0372bbde08748a1086bb4251fbec46e167b))

- Update readme for v8.0.0 changes ([95c5c4d](https://github.com/Vonage/vonage-dotnet-sdk/commit/95c5c4d65b5e5b0e8db903520f68b6a0a6de8338))

- Bump version to v7.16.0 ([d6ecd99](https://github.com/Vonage/vonage-dotnet-sdk/commit/d6ecd99430f66f5335763db994089ac6266fff85))


### Features

- Remove fraud score from NIv2 ([47591b0](https://github.com/Vonage/vonage-dotnet-sdk/commit/47591b0016c319ab5d9530e7162ee7534c9ce8ca))


### Merges

- Merge remote-tracking branch 'origin/main'
 ([80a94ff](https://github.com/Vonage/vonage-dotnet-sdk/commit/80a94ffa6bb923611165add66524cb1913f1bd3f))


## [v7.15.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v7.15.0) (2025-02-05)

### Bug Fixes

- Address mend violation (non-critical: fake token in test case) ([5ac1718](https://github.com/Vonage/vonage-dotnet-sdk/commit/5ac1718bb71c8115266e20f8bf09c0c119c88384))

- Address mend violation (non-critical: fake token in test case) ([71ba6b1](https://github.com/Vonage/vonage-dotnet-sdk/commit/71ba6b1907c205dc614f5387cf17e405ddced5ca))

- Update whitesource configuration to exclude test projects ([e9f95e6](https://github.com/Vonage/vonage-dotnet-sdk/commit/e9f95e626b417843993c8c341a8812471aa01956))

- Disable IaC in whitesource (mend) ([d1fd3d9](https://github.com/Vonage/vonage-dotnet-sdk/commit/d1fd3d9448bce91b89109771b578f7e37dd908ba))


### Documentation

- Generate changelog for v7.14.1 ([bd7d2b5](https://github.com/Vonage/vonage-dotnet-sdk/commit/bd7d2b587b8cc8ee0537fb3fe6bfa474c06990de))

- Bump version to v7.15.0 ([9903bd5](https://github.com/Vonage/vonage-dotnet-sdk/commit/9903bd5a26b9abca2160e19d5252daf7ca116928))


### Features

- Block FluentAssertions version to v7.0.0 to prevent upgrade to v8.0.0 (paid version) ([b0a7ec7](https://github.com/Vonage/vonage-dotnet-sdk/commit/b0a7ec76d5434912cd7ebb209aab338f265cc3cf))

- Add uri validation on Voice GetRecording ([240cf85](https://github.com/Vonage/vonage-dotnet-sdk/commit/240cf85c5b3b682aa9e991f061c50af0418c950c))


### Reverts

- Revert "fix: update whitesource configuration to exclude test projects"

This reverts commit e9f95e626b417843993c8c341a8812471aa01956.
 ([f664137](https://github.com/Vonage/vonage-dotnet-sdk/commit/f664137197b22e6d4465acce49251aa211a4c741))

- Revert "fix: address mend violation (non-critical: fake token in test case)"

This reverts commit 71ba6b1907c205dc614f5387cf17e405ddced5ca.
 ([92b95a2](https://github.com/Vonage/vonage-dotnet-sdk/commit/92b95a21cea0d7b477c25a1bb167a2c72959a126))

- Revert "fix: address mend violation (non-critical: fake token in test case)"

This reverts commit 5ac1718bb71c8115266e20f8bf09c0c119c88384.
 ([0f403ca](https://github.com/Vonage/vonage-dotnet-sdk/commit/0f403ca1ad796dfa94c43bf96cd65723be7a40f6))


## [v7.14.1](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v7.14.1) (2025-01-14)

### Documentation

- Generate changelog for v7.14.0 ([cf5f12c](https://github.com/Vonage/vonage-dotnet-sdk/commit/cf5f12cc64ca5abe49780fd35d07aa827bac4717))

- Bump version to v7.14.1 ([607f099](https://github.com/Vonage/vonage-dotnet-sdk/commit/607f0997a54a59f516ca5e39bc11e0d3b4c8fae5))


### Features

- Make RealTimeData obsolete in NI ([7f6c25f](https://github.com/Vonage/vonage-dotnet-sdk/commit/7f6c25ff63fe559e963f2e9594e79e7c17ef7458))


## [v7.14.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v7.14.0) (2024-12-18)

### Documentation

- Generate changelog for v7.13.1 ([76f0e20](https://github.com/Vonage/vonage-dotnet-sdk/commit/76f0e2037e17d2a8e29ddc1eafd69d2d4e3e61d0))

- Bump version to v7.14.0 ([d79afae](https://github.com/Vonage/vonage-dotnet-sdk/commit/d79afae3aee79f92f50357eaaa1d2814a8f329b8))


### Features

- Add Do, DoWhenSuccess and DoWhenFailure on Result ([2af8a0b](https://github.com/Vonage/vonage-dotnet-sdk/commit/2af8a0befa5089a92a76c8e63814b3af7333cd92))

- Add async extensions for Do, DoWhenSuccess and DoWhenFailure on Result ([60e14a1](https://github.com/Vonage/vonage-dotnet-sdk/commit/60e14a1f5a08451deca99ebefdd351a6b5d4a1f2))

- Add Do, DoWhenSome and DoWhenNone for Maybe, as well as async extensions ([4c1040c](https://github.com/Vonage/vonage-dotnet-sdk/commit/4c1040c9a7fdee62ec4856b397d3a412795e4b34))


### Refactoring

- Reorganize tests for Monads for better navigation ([d298f50](https://github.com/Vonage/vonage-dotnet-sdk/commit/d298f507a71f177831c39667adac3a97c550743c))


## [v7.13.1](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v7.13.1) (2024-12-16)

### Documentation

- Generate changelog for v7.13.0 ([276eb21](https://github.com/Vonage/vonage-dotnet-sdk/commit/276eb21275b2aea859805dfd547cb18796b54d51))

- Bump version to v7.13.1 ([f2190da](https://github.com/Vonage/vonage-dotnet-sdk/commit/f2190daf4f9420a994926a39bc1187590f25d91f))


### Features

- Allow Vonage Urls to include a relative path ([81ab4da](https://github.com/Vonage/vonage-dotnet-sdk/commit/81ab4da7952012e2604fc0ad289ba5d3b1a4d928))


### Refactoring

- Make SendRequest pure ([dbab981](https://github.com/Vonage/vonage-dotnet-sdk/commit/dbab981deb3a2e6772e5d4d454d9a55fba033107))


## [v7.13.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v7.13.0) (2024-12-05)

### Dependencies upgrade

- Update packages ([92688a8](https://github.com/Vonage/vonage-dotnet-sdk/commit/92688a87b6cb6208ab2c284aa6dcf3f1973ab9ba))


### Documentation

- Generate changelog for v7.12.0 ([9881c09](https://github.com/Vonage/vonage-dotnet-sdk/commit/9881c0929775b34065236ea4df29477f2172946f))

- Bump version to v7.13.0 ([671131f](https://github.com/Vonage/vonage-dotnet-sdk/commit/671131f059fc33f420296abc86d81d2a6fd019d8))


### Features

- Add max bitrate on Video archives ([e8acea0](https://github.com/Vonage/vonage-dotnet-sdk/commit/e8acea085fad0cb2018ff94c8f94daddf4cfcadb))


### Merges

- Merge remote-tracking branch 'origin/main'
 ([f417bdd](https://github.com/Vonage/vonage-dotnet-sdk/commit/f417bdd5aa1399909ac494e3bb26a9e6904e383c))


## [v7.12.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v7.12.0) (2024-11-21)

### Documentation

- Generate changelog for v7.11.0 ([f5a9442](https://github.com/Vonage/vonage-dotnet-sdk/commit/f5a9442949593e132dd65ee77d939b9b1529ea8f))

- Bump version to v7.12.0 ([29b411f](https://github.com/Vonage/vonage-dotnet-sdk/commit/29b411f49a92fd0202cdd1a6eac58521185c1a3a))


### Features

- Update auth for Accounts ([165ab9a](https://github.com/Vonage/vonage-dotnet-sdk/commit/165ab9ad71216c2bc88d5c492239d323be7eb1fb))

- Update auth for Numbers ([0d18216](https://github.com/Vonage/vonage-dotnet-sdk/commit/0d18216691a638abe7cafb36e150d1c65c94558a))

- Update auth for Pricing ([eed0bcb](https://github.com/Vonage/vonage-dotnet-sdk/commit/eed0bcb89b7bb4d523bfc4f587be3cca2a73e797))

- Update auth for ShortCodes ([b695c1f](https://github.com/Vonage/vonage-dotnet-sdk/commit/b695c1fb5daca7c7ddaed7356a7f4c3cfc34071e))

- Add support for custom proxies ([99faaec](https://github.com/Vonage/vonage-dotnet-sdk/commit/99faaece16402d32551ad21d625b679251d5b745))


## [v7.11.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v7.11.0) (2024-11-14)

### Documentation

- Generate changelog for v7.10.0 ([3cb94c3](https://github.com/Vonage/vonage-dotnet-sdk/commit/3cb94c380d392ef5a2b28570006d737ef556dcf5))

- Bump version to v7.11.0 ([8592e18](https://github.com/Vonage/vonage-dotnet-sdk/commit/8592e18d3ede31b30f6150486180030ee2228a68))


### Features

- Add optional WithTemplateId on StartVerificationRequest ([4965c80](https://github.com/Vonage/vonage-dotnet-sdk/commit/4965c80942228892c544217edaf528a9b8f08e6e))


### Merges

- Merge remote-tracking branch 'origin/main'
 ([8c66087](https://github.com/Vonage/vonage-dotnet-sdk/commit/8c660870962e21001ce3957bb45e7203bf29063b))


### Other

- Update dependency system.text.json to v8.0.5 (#596) ([c8a048b](https://github.com/Vonage/vonage-dotnet-sdk/commit/c8a048b707971eb85e1e173692e76a4574e24a34))


### Refactoring

- Make ErrorResponse an internal record ([c875b62](https://github.com/Vonage/vonage-dotnet-sdk/commit/c875b62706eb0a910c9ad2bbad770ca7c19d4e5e))

- Improve error information with tailored format ([f11e417](https://github.com/Vonage/vonage-dotnet-sdk/commit/f11e417735d73e1c68aadebbd42df4c44b6e7e4c))


## [v7.10.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v7.10.0) (2024-10-04)

### Bug Fixes

- Change TemplateId to Guid for VerifyV2 templates ([7a459d3](https://github.com/Vonage/vonage-dotnet-sdk/commit/7a459d3822ebe9fba8768c1bab105ef26bade319))

- Fix typo in IBuilderForName interface ([fd96bc2](https://github.com/Vonage/vonage-dotnet-sdk/commit/fd96bc249bde6ddfd1323882e6fbd0bcdc7ae55e))

- Typo in filename ([b817854](https://github.com/Vonage/vonage-dotnet-sdk/commit/b8178543ea3ce186edb581a4e8d7116437c41ee6))


### Documentation

- Generate changelog for v7.9.0 ([667fc6d](https://github.com/Vonage/vonage-dotnet-sdk/commit/667fc6d580c2bc7dd5dddd5131cee24e08634397))

- Update supported APIs in readme ([254e7a7](https://github.com/Vonage/vonage-dotnet-sdk/commit/254e7a765c803e9c965ac8c50e5d5e04ff37cd41))

- Bump version to v7.10.0 ([ef90ec3](https://github.com/Vonage/vonage-dotnet-sdk/commit/ef90ec3c814529af35d6f4178ccb007b81fd5127))


### Features

- Implement CreateTemplate for VerifyV2 ([9f4e0bf](https://github.com/Vonage/vonage-dotnet-sdk/commit/9f4e0bfa8873ad6438ae41e80a65bc390fe53499))

- Implement DeleteTemplate for VerifyV2 ([5d8f806](https://github.com/Vonage/vonage-dotnet-sdk/commit/5d8f8064f5d0d613f2b07f08215d90bcf3c9ceea))

- Implement UpdateTemplate for VerifyV2 ([a591a15](https://github.com/Vonage/vonage-dotnet-sdk/commit/a591a156e1e5545ac789641f4136a8167dd525bd))

- Implement GetTemplate for VerifyV2 ([ce23ccd](https://github.com/Vonage/vonage-dotnet-sdk/commit/ce23ccdbdcc7bb18b9eb94f7cc020237320e6864))

- Implement GetTemplates for VerifyV2 ([ccf80dd](https://github.com/Vonage/vonage-dotnet-sdk/commit/ccf80ddf77e071728e267001eb5000c633091944))

- Implement request creation from response for GetTemplates in VerifyV2 ([0eca99a](https://github.com/Vonage/vonage-dotnet-sdk/commit/0eca99a82ac00357a0140c66b8e7bc44bd722a6e))

- Implement DeleteTemplateFragment for VerifyV2 ([6dae0f9](https://github.com/Vonage/vonage-dotnet-sdk/commit/6dae0f9b21a3d91851ac556be0fdb7a2bbaf64be))

- Implement CreateTemplateFragment for VerifyV2 ([31ebebb](https://github.com/Vonage/vonage-dotnet-sdk/commit/31ebebb56afc85e3c7594d67c30ce5351ede809f))

- Implement UpdateTemplateFragmentRequest for VerifyV2 ([427435f](https://github.com/Vonage/vonage-dotnet-sdk/commit/427435fd1d54628122f2e60d1aacbabb3ec5107e))

- Implement GetTemplateFragment for VerifyV2 ([cfb1b8e](https://github.com/Vonage/vonage-dotnet-sdk/commit/cfb1b8ef0310b65b1bd622f4d03984a1053f74e0))

- Implement GetTemplateFragments for VerifyV2 ([016cbb2](https://github.com/Vonage/vonage-dotnet-sdk/commit/016cbb21c4790af92d20db0db5328df3b66ae034))

- Update allowed channels for VerifyV2 template fragments ([0bc9705](https://github.com/Vonage/vonage-dotnet-sdk/commit/0bc97051b95d70fe3ead0a948adf35c39a178b09))


### Refactoring

- Make request builder readonly for CreateTemplate ([8116473](https://github.com/Vonage/vonage-dotnet-sdk/commit/8116473d90371e27bb115555ba8e01230e624aee))

- Add verification channel enum for VerifyV2 ([af7cd63](https://github.com/Vonage/vonage-dotnet-sdk/commit/af7cd630279d8ca6ca374c36759fbe5f8d948810))


## [v7.9.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v7.9.0) (2024-09-04)

### Documentation

- Generate changelog for v7.8.2 ([1abcee1](https://github.com/Vonage/vonage-dotnet-sdk/commit/1abcee17986953d527195cc17f9dca9b1768548e))

- Bump version to v7.9.0 ([3c63fef](https://github.com/Vonage/vonage-dotnet-sdk/commit/3c63fefa28c6483f9c7f811108a75b3dcb457ec3))


### Features

- Implement new RCS request in Messages ([0327522](https://github.com/Vonage/vonage-dotnet-sdk/commit/0327522463a8a0af34d10fcbda06107a02b1925d))

- Implement UpdateMessage in Messages ([a46c451](https://github.com/Vonage/vonage-dotnet-sdk/commit/a46c451d5bd842440c161892e44bdf896dbed725))

- Setup uri for update messages ([a4f69b8](https://github.com/Vonage/vonage-dotnet-sdk/commit/a4f69b8aecad0d55bb412d5c1edc8cb7d5a354c3))


### Refactoring

- Clean messages tests ([e5218d6](https://github.com/Vonage/vonage-dotnet-sdk/commit/e5218d60b08a8fc1020238fc833adca349d07cec))


## [v7.8.2](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v7.8.2) (2024-08-29)

### Documentation

- Generate changelog for v7.8.1 ([80440ee](https://github.com/Vonage/vonage-dotnet-sdk/commit/80440ee85072838624b095f45b68d6927f647715))

- Bump version to v7.8.2 ([163bb43](https://github.com/Vonage/vonage-dotnet-sdk/commit/163bb432ea5ea2319777a23ab08284bd8625b81d))


### Features

- Flag Meetings API and Proactive Connect API as obsolete ([b4a3239](https://github.com/Vonage/vonage-dotnet-sdk/commit/b4a3239e0f030201aa7af9369817fb95ef08b031))


## [v7.8.1](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v7.8.1) (2024-08-28)

### Bug Fixes

- Remove default object for DTMF in MultiInputAction ([f832baf](https://github.com/Vonage/vonage-dotnet-sdk/commit/f832bafb68055a48352110e8fdb081c744c0db5b))


### Documentation

- Generate changelog for v7.8.0 ([8f09ff3](https://github.com/Vonage/vonage-dotnet-sdk/commit/8f09ff395e7d9f28f267e7d93c1190ffbfbda6d7))

- Bump version to v7.8.1 ([71a9986](https://github.com/Vonage/vonage-dotnet-sdk/commit/71a9986a40f5b59678b5ecc6c672d81d8de5134c))


### Features

- Add async IfNone extension for Maybe ([b2bb7cc](https://github.com/Vonage/vonage-dotnet-sdk/commit/b2bb7cc17610675931789b46853f5dc3d6e99d25))


## [v7.8.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v7.8.0) (2024-08-26)

### Documentation

- Generate changelog for v7.7.4 ([33c4eb7](https://github.com/Vonage/vonage-dotnet-sdk/commit/33c4eb794f50abcc6df2ad11335c5c4ced0cbb2d))

- Bump version to v7.8.0 ([c3ce7ee](https://github.com/Vonage/vonage-dotnet-sdk/commit/c3ce7ee4e0e77488460c2406dabf320617a61ae6))


### Features

- Add ValidateSignature on DeliveryReceipt ([b0d4b64](https://github.com/Vonage/vonage-dotnet-sdk/commit/b0d4b64743a6028af1e2b68a022b87966359c950))


### Refactoring

- Regroup InboundSms signature validation tests into a parametrized one ([177c78d](https://github.com/Vonage/vonage-dotnet-sdk/commit/177c78dbf51e6be0bc04e67b21d340200b8d8825))

- Move InboundSms signature validation tests in their own file ([aa1f816](https://github.com/Vonage/vonage-dotnet-sdk/commit/aa1f8163987327dbf262f2bb405218b8bed44a2a))

- Clean signature validation tests ([40c60f3](https://github.com/Vonage/vonage-dotnet-sdk/commit/40c60f38d2bf727fc14d63d35de0b7e69b4ad08e))

- Clean SignatureValidation ([c30416c](https://github.com/Vonage/vonage-dotnet-sdk/commit/c30416c903705b5fd03551f6bf5d82d3434a368a))


## [v7.7.4](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v7.7.4) (2024-08-08)

### Documentation

- Generate changelog for v7.7.3 ([0c57c3f](https://github.com/Vonage/vonage-dotnet-sdk/commit/0c57c3f87af1194ae3d622146fb4ef28e958dfda))

- Bump version to v7.7.4 ([23dec91](https://github.com/Vonage/vonage-dotnet-sdk/commit/23dec91a0d5eb663cfbf80bc8b4cd85bbade2ed8))


### Features

- Add caption on MMS attachment ([f388e83](https://github.com/Vonage/vonage-dotnet-sdk/commit/f388e83931b5dd6b1ea791c08668685afdf85d88))

- Add name to WhatsApp file attachment ([b04ac43](https://github.com/Vonage/vonage-dotnet-sdk/commit/b04ac43f0bc1cfd9cd7583a8cddc9e0e034936f6))

- Add StandardHeaders to SipEndpoint ([b858c59](https://github.com/Vonage/vonage-dotnet-sdk/commit/b858c595d8da70d5c63b5fb11b997cad1bcadbbf))


## [v7.7.3](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v7.7.3) (2024-08-07)

### Bug Fixes

- Ignore TimeToLive during serialization when default ([f206f50](https://github.com/Vonage/vonage-dotnet-sdk/commit/f206f50c58c9918bb84e3409bcecf40ab6afb979))

- Add missing properties on Number ([7062559](https://github.com/Vonage/vonage-dotnet-sdk/commit/7062559aea95fef2cd9fcecc191a4cf3eada1d47))


### Documentation

- Generate changelog for v7.7.2 ([2100a43](https://github.com/Vonage/vonage-dotnet-sdk/commit/2100a432101efa8ae4755b641ed6c50bd68ee8ac))

- Bump version to v7.7.3 ([e3a9fdc](https://github.com/Vonage/vonage-dotnet-sdk/commit/e3a9fdc71cbeaa36469bfc9bbbcc61c2e2cc4cd5))

- Generate changelog for v7.7.3 ([bd0e9d8](https://github.com/Vonage/vonage-dotnet-sdk/commit/bd0e9d8a16e9cff99bb5e3ec0858066fedf1f614))


### Features

- Add Webhook url and version for Messages requests ([feb5153](https://github.com/Vonage/vonage-dotnet-sdk/commit/feb5153948fa91987aea45be4d7caf65f5419a27))

- Add TimeToLive and OptionalSettings on SmsMessageRequest ([5848ec8](https://github.com/Vonage/vonage-dotnet-sdk/commit/5848ec8d8f4e8fae3662ede529829dc1e336c403))


## [v7.7.2](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v7.7.2) (2024-07-30)

### Bug Fixes

- Add missing ApplicationId property on NumberSearchRequest ([7ea2677](https://github.com/Vonage/vonage-dotnet-sdk/commit/7ea26770d35129571d7a7497acf1fe618bdf0a80))

- Add missing AppId property on Number ([d9193aa](https://github.com/Vonage/vonage-dotnet-sdk/commit/d9193aa15d66ef3d34d44c201baeb0a378919f14))


### Documentation

- Generate changelog for v7.7.1 ([8538de3](https://github.com/Vonage/vonage-dotnet-sdk/commit/8538de341bc8c4afdc41c0d8abc42ccf307e0e79))

- Bump version to v7.7.2 ([47f0e65](https://github.com/Vonage/vonage-dotnet-sdk/commit/47f0e65b6304ed1a3e53ae0d90ce36be1aaa0191))


## [v7.7.1](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v7.7.1) (2024-07-26)

### Bug Fixes

- Empty configuration when using the services extension ([57ad45d](https://github.com/Vonage/vonage-dotnet-sdk/commit/57ad45df313761b43905d0558c417a7fe293a07c))

- Vulnerability for System.Formats.Asn1 ([62b8580](https://github.com/Vonage/vonage-dotnet-sdk/commit/62b8580146c6c8d9c0fbb5117a3f5109a61dad96))


### Documentation

- Generate changelog for v7.7.0 ([5c7393c](https://github.com/Vonage/vonage-dotnet-sdk/commit/5c7393cf3d4d93b0b6028469f457f5b827d25099))

- Bump version to v7.7.1 ([9b642a3](https://github.com/Vonage/vonage-dotnet-sdk/commit/9b642a32c21bccc6cab8a617251bc7797dd41ec5))


## [v7.7.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v7.7.0) (2024-07-19)

### Build updates

- Add regex to validate bump script input ([52e424d](https://github.com/Vonage/vonage-dotnet-sdk/commit/52e424d58ce4d7c1e4c1d33b1c31b9974ad4b9a9))


### Dependencies upgrade

- Update packages ([677e434](https://github.com/Vonage/vonage-dotnet-sdk/commit/677e4341b05e2299365c0c8ac937c665ca6c4955))


### Documentation

- Generate changelog for v7.6.1 ([ad340e9](https://github.com/Vonage/vonage-dotnet-sdk/commit/ad340e951fed9af07c150840bc61a6d0b2cbcf93))

- Fix typo in XML documentation ([f48d9da](https://github.com/Vonage/vonage-dotnet-sdk/commit/f48d9daa1a0bbd6a90716250e0b01d90fcd83819))

- Bump version to v7.6.2 ([d03d593](https://github.com/Vonage/vonage-dotnet-sdk/commit/d03d5933d89eb8ef3a59f2ec128ac216e15b08a4))

- Generate changelog for v7.6.2 ([df1572a](https://github.com/Vonage/vonage-dotnet-sdk/commit/df1572ad1d46452cb06351b7e9221442785b9aa5))

- Bump version to v7.7.0 ([256178a](https://github.com/Vonage/vonage-dotnet-sdk/commit/256178ac75ce8e99c8c28b60f4e24ddab0cbbdf9))

- Generate changelog for v7.7.0 ([99c2312](https://github.com/Vonage/vonage-dotnet-sdk/commit/99c2312ef29a29ce482a660c9dbaed814e7426d7))


### Features

- Support E2EE for Video sessions ([550df07](https://github.com/Vonage/vonage-dotnet-sdk/commit/550df076ebb4182d521260cafec9ab0dc6a31345))

- Implement request creation for audio connector ([1ecb51e](https://github.com/Vonage/vonage-dotnet-sdk/commit/1ecb51e40ef164dd6d6d88b3a44e9e2cf734e94b))

- Implement Start feature for AudioConnector ([63d84ba](https://github.com/Vonage/vonage-dotnet-sdk/commit/63d84ba1c65bfd855ab3134ae50ac0a70f795761))

- Implement Stop for LiveCaptions ([01cc92a](https://github.com/Vonage/vonage-dotnet-sdk/commit/01cc92a06a02345ab2304d15a39318995ca1398a))

- Implement Start for LiveCaptions ([358e2cf](https://github.com/Vonage/vonage-dotnet-sdk/commit/358e2cf97484acecc7833e88ad24d8ad68cd050c))

- Change minimum channel timeout from 60s to 15s for VerifyV2 ([f1766a2](https://github.com/Vonage/vonage-dotnet-sdk/commit/f1766a212ad8150d663fe47bb8f80029f1891610))


## [v7.6.1](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v7.6.1) (2024-06-28)

### Bug Fixes

- Edge case importing RSA key (#580) ([6436947](https://github.com/Vonage/vonage-dotnet-sdk/commit/6436947fdb5e56e358a82370fb3125fb81275e02))


### Build updates

- Make multi-framework workflow the default one for PRs as it doesn't rely on any secrets (SONAR). ([a69164d](https://github.com/Vonage/vonage-dotnet-sdk/commit/a69164d51b3965200578af2b0bec041edd667430))


### Documentation

- Generate changelog for v7.6.0 ([7968faf](https://github.com/Vonage/vonage-dotnet-sdk/commit/7968fafe14b8be5cdbbfeadb3c4c54686af22da0))

- Bump version to v7.6.1 ([793c8ea](https://github.com/Vonage/vonage-dotnet-sdk/commit/793c8eaabd86e2f1ce1f553974a6cc01482c1ab2))


## [v7.6.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v7.6.0) (2024-06-27)

### Documentation

- Generate changelog for v7.5.0 ([5f04107](https://github.com/Vonage/vonage-dotnet-sdk/commit/5f041075d647274c8830b7660053068f27da726a))

- Bump version to v7.6.0 ([57904e6](https://github.com/Vonage/vonage-dotnet-sdk/commit/57904e64b201b89cd2616b20b1ac03682c329ed8))


### Features

- Implement authentication mechanism for NumberVerification API ([dfa1c39](https://github.com/Vonage/vonage-dotnet-sdk/commit/dfa1c39a78b79ce781b0979471d68331cb8b7832))

- Implement Verify feature for NumberVerification ([9c95a13](https://github.com/Vonage/vonage-dotnet-sdk/commit/9c95a1310861ecc299932527235623e90912c31d))

- Add NumberVerificationClient in services collection extensions ([01311c4](https://github.com/Vonage/vonage-dotnet-sdk/commit/01311c4e8990ea2aa09779df33157e8e5c99f439))

- Introduce configurable url for OIDC requests ([3172720](https://github.com/Vonage/vonage-dotnet-sdk/commit/3172720a0470114ad9f9c23942cf40554d2723a3))

- Use specific clients for OIDC requests in NumberVerification ([c468a60](https://github.com/Vonage/vonage-dotnet-sdk/commit/c468a607d8bd7c7c285f0ac2540348f8496d60fc))

- Implement GetSession for ExperienceComposer ([ea3ec87](https://github.com/Vonage/vonage-dotnet-sdk/commit/ea3ec8751f43e2d840aa46417543dc5d7994da8f))

- Implement GetSessions on ExperienceComposer ([53d26c0](https://github.com/Vonage/vonage-dotnet-sdk/commit/53d26c0799a298e37fedd8fc6f372a473065e621))

- Implement Stop on ExperienceComposer ([1710977](https://github.com/Vonage/vonage-dotnet-sdk/commit/17109779aa41f2a9e1d9a2a7c41c8cec7a3b612f))

- Implement request validation for Start in ExperienceComposer ([6d6f1bf](https://github.com/Vonage/vonage-dotnet-sdk/commit/6d6f1bf4f11d27eeefd4d3bc385ade1a8fd09a5b))

- Implement request path for Start in ExperienceComposer ([ab9490e](https://github.com/Vonage/vonage-dotnet-sdk/commit/ab9490e22234a6bdbd8ab9c8b14b4ce87b990d86))

- Implement serialization for Start on ExperienceComposer ([5b91318](https://github.com/Vonage/vonage-dotnet-sdk/commit/5b9131886f65521eb2fe667b9e5f32170ec80f21))

- Implement E2E for Start on ExperienceComposer ([03f9e7a](https://github.com/Vonage/vonage-dotnet-sdk/commit/03f9e7a3a4efac3f06037719739f5500348446d5))

- Add TimeToLive property on MMS in Messages ([79ebe28](https://github.com/Vonage/vonage-dotnet-sdk/commit/79ebe28a6a2e8c0a1a63ada1aa8ed4f62a36f1be))

- Add Context on WhatsApp messages ([426ef47](https://github.com/Vonage/vonage-dotnet-sdk/commit/426ef4721306da25dcab144cc8a4b560756bd71d))

- Support new properties on Messages Webhooks ([02044e5](https://github.com/Vonage/vonage-dotnet-sdk/commit/02044e56792fb86e37ddb5c6ff06042c8ea2c597))


### Refactoring

- Clean PBT test ([c3615eb](https://github.com/Vonage/vonage-dotnet-sdk/commit/c3615eb435e7e3392f205ecb371f0c55c0eec4b5))

- Remove unnecessary parameters in E2ETests ([8b39bc1](https://github.com/Vonage/vonage-dotnet-sdk/commit/8b39bc149c1c8439b3d65366974ab1e668849662))

- Use different wiremock instances to differentiate Vonage requests and OIDC requests ([ccbe6f4](https://github.com/Vonage/vonage-dotnet-sdk/commit/ccbe6f4f59fee3ab0f5cf22a414c6cdbaa4259d0))

- Hide video client constructors ([fc15060](https://github.com/Vonage/vonage-dotnet-sdk/commit/fc150605029f6c309968dccab607e5f38e42442f))

- Use PBT to assert ranges of values for Start in ExperienceComposer ([3eedf52](https://github.com/Vonage/vonage-dotnet-sdk/commit/3eedf52248c8e9ad7e9ade3c1c99d0ee5b7bd5c3))

- Extract serialization test for ExperienceComposer Session ([c989a09](https://github.com/Vonage/vonage-dotnet-sdk/commit/c989a094f1973acbe89eede02c50d36083d53516))


### Reverts

- Revert "refactor: [breaking] remove EventUrl and EventMethod from ConversationAction, with a disclaimer in the Readme"

This reverts commit fd02cd530c59ffa32eb111e545c7d6da44d83a7a.
 ([ac8c666](https://github.com/Vonage/vonage-dotnet-sdk/commit/ac8c66648ddf55797d4b65dfa60386e77422e439))


## [v7.5.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v7.5.0) (2024-06-14)

### Bug Fixes

- Coverage not recognized from SonarSource ([ccef03b](https://github.com/Vonage/vonage-dotnet-sdk/commit/ccef03b02723a7d88c8cc3c83f3c98980c40dbeb))


### Documentation

- Generate changelog for v7.4.0 ([5474055](https://github.com/Vonage/vonage-dotnet-sdk/commit/5474055b62aa3222bede6ee3083d42092c14a9f2))

- Fix xml comment on SimSwap ([5c7f4c1](https://github.com/Vonage/vonage-dotnet-sdk/commit/5c7f4c174be3daa856a1f3181b3733acfc24f122))

- Bump version to v7.5.0 ([9e72fbc](https://github.com/Vonage/vonage-dotnet-sdk/commit/9e72fbc4ba319e74e477550ba49dec15e2f254b9))


### Features

- Implement SimSwap authentication mechanism ([fe131a2](https://github.com/Vonage/vonage-dotnet-sdk/commit/fe131a22f3e7d1eee5ada45b0b9be45b4e2b0233))

- Add SimSwap client to ServiceCollection extensions ([443f9ac](https://github.com/Vonage/vonage-dotnet-sdk/commit/443f9ace9c0135c86af876f966f7323f8c0032d1))

- Implement SimSwap Check ([db8c97d](https://github.com/Vonage/vonage-dotnet-sdk/commit/db8c97d8366eebadfecd3aa58bad7c612434e56e))

- Expose token scope on AuthenticateRequest ([1b3c756](https://github.com/Vonage/vonage-dotnet-sdk/commit/1b3c7565c898672c00b9224ec40a623406435d58))

- Implement SimSwap date retrieval ([a0d8d27](https://github.com/Vonage/vonage-dotnet-sdk/commit/a0d8d276f4bbf9933d6d14883c89d33fd153d82a))

- Implement GetMember in Conversations ([881ef15](https://github.com/Vonage/vonage-dotnet-sdk/commit/881ef156ddc7d7c026137f25f62e923240f926fc))

- Update SimSwap authentication based on documentation changes ([521e975](https://github.com/Vonage/vonage-dotnet-sdk/commit/521e9757b14af30c051b513ba19ca4779a878802))

- Update SimSwap authentication based on documentation changes ([d0b98ec](https://github.com/Vonage/vonage-dotnet-sdk/commit/d0b98ec18ab871235b35f0782058123eee29681a))

- Implement GetMembers in Conversations ([ffe0c0a](https://github.com/Vonage/vonage-dotnet-sdk/commit/ffe0c0a713fbefb59d134d02465ba191be17a783))

- Implement CreateMember in Conversations ([5e01bf3](https://github.com/Vonage/vonage-dotnet-sdk/commit/5e01bf38f0e4e5d55f69ece5870644ca1fe7a74b))

- Implement UpdateMember in Conversations ([271bfcc](https://github.com/Vonage/vonage-dotnet-sdk/commit/271bfcc0d5622c1fea174fe147bed9baa2779e02))

- Implement DeleteEvent in Conversations ([0ddedaa](https://github.com/Vonage/vonage-dotnet-sdk/commit/0ddedaa767c5cf55ae5fb26a3f94da4b78b90a9b))

- Refactor builders for Conversations ([62467b2](https://github.com/Vonage/vonage-dotnet-sdk/commit/62467b2250a5a7545de938ab77a1441b9a344d01))

- Implement GetEvents responses serialization in Conversations ([05fed72](https://github.com/Vonage/vonage-dotnet-sdk/commit/05fed72163bbd9c3e1c8afe7bbc41e9aec01a271))

- Implement RandomMessage for GetEvents ([39d7732](https://github.com/Vonage/vonage-dotnet-sdk/commit/39d77324aa07c350e1aebd5ff70e296c37b062e0))

- Implement FluentAssertion extension for JsonElement ([c0aae8b](https://github.com/Vonage/vonage-dotnet-sdk/commit/c0aae8bc58bdf4caab1e89d37dd87c55e65b6479))

- Finalize E2E flow for GetEvent ([d969926](https://github.com/Vonage/vonage-dotnet-sdk/commit/d9699260b23d01a969187accb31ac570317550c6))

- Implement GetEvents in Conversations ([b43cf1f](https://github.com/Vonage/vonage-dotnet-sdk/commit/b43cf1fe3cae271c160bf2e80a95a7158cd7eefe))

- Feat; implement CreateEvent in Conversations
 ([199c37f](https://github.com/Vonage/vonage-dotnet-sdk/commit/199c37fcca47899ca0e91c93d20fe2b30d1c3cdc))


### Merges

- Merge remote-tracking branch 'origin/main'
 ([13e1477](https://github.com/Vonage/vonage-dotnet-sdk/commit/13e1477d3a36aea072491992f984e26359c65612))


### Refactoring

- Clean SimSwap ([e92db28](https://github.com/Vonage/vonage-dotnet-sdk/commit/e92db2801fc18bdf16682621b73928f6ab206ef0))

- Make InputEvaluation internal ([9ddba24](https://github.com/Vonage/vonage-dotnet-sdk/commit/9ddba24b6fbd5a9e28734cd289a5662554be9f57))

- Remove converter for ChannelType ([da4c568](https://github.com/Vonage/vonage-dotnet-sdk/commit/da4c568e4a6bf66cc9bcf314ccfd8f7980df3c5c))

- Remove temporary naming on CreateMember ([3986550](https://github.com/Vonage/vonage-dotnet-sdk/commit/39865504bf409564c1974b9c2ff050df932e274a))

- Clean CreateMember tests ([5b75177](https://github.com/Vonage/vonage-dotnet-sdk/commit/5b7517720d15b820a2777348d499f7d4535cc25f))

- Builders for NumberInsightsV2 ([991dca1](https://github.com/Vonage/vonage-dotnet-sdk/commit/991dca15d456919709cb7b1f7996cff4923ce248))

- Refactor builders for SimSwap ([a081eec](https://github.com/Vonage/vonage-dotnet-sdk/commit/a081eec3290670ebda0dc446df5ff9e8c9c8888e))

- Rely on JsonElement for extensive structure of Events in Conversations ([3b23c7c](https://github.com/Vonage/vonage-dotnet-sdk/commit/3b23c7ca947fc23a32396b351107f425fb1e7d67))

- Clean responses in Conversations ([5202a45](https://github.com/Vonage/vonage-dotnet-sdk/commit/5202a453ac4f39548cdcb21012a5fd83e117b710))


## [v7.4.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v7.4.0) (2024-05-13)

### Documentation

- Generate changelog for v7.2.2 ([942ebdc](https://github.com/Vonage/vonage-dotnet-sdk/commit/942ebdcfdb837af1d8e89e278494cdc2a0b97351))

- Bump version to v7.3.0 ([3ea78ae](https://github.com/Vonage/vonage-dotnet-sdk/commit/3ea78ae784ab55418c1809f6c91c7f9613c92bdf))

- Generate changelog for v7.3.0 ([e6403a7](https://github.com/Vonage/vonage-dotnet-sdk/commit/e6403a70b915738f1c3358338a371249c35cc60a))

- Bump version to v7.4.0 ([763a3f1](https://github.com/Vonage/vonage-dotnet-sdk/commit/763a3f15de42d925b0c4d50b8c0c85691367c51a))


### Features

- Add PublisherOnly role for Video ([52e5988](https://github.com/Vonage/vonage-dotnet-sdk/commit/52e5988e90e430587049edaf8a2eb2b07271fa28))


## [v7.2.2](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v7.2.2) (2024-05-10)

### Bug Fixes

- Add missing ClientRef property on SmsResponseMesage ([3051a53](https://github.com/Vonage/vonage-dotnet-sdk/commit/3051a53e008b983d4ac5278aedb9d340528fff70))

- Set StartOnEnter default value to true for ConversationAction ([80a39b1](https://github.com/Vonage/vonage-dotnet-sdk/commit/80a39b12fddb979f2031389355730a3c2dcc5880))

- Make StartTime and EndTime nullable on Completed webhook. ([e8a9b82](https://github.com/Vonage/vonage-dotnet-sdk/commit/e8a9b82be63585446a37f6f619e851e12ed549e5))


### Documentation

- Generate changelog for v7.2.1 ([1252147](https://github.com/Vonage/vonage-dotnet-sdk/commit/1252147047cae2e917a5231a52b6c6e9f250fd30))

- Bump version to v7.2.2 ([8e92f13](https://github.com/Vonage/vonage-dotnet-sdk/commit/8e92f13722fe2b3099cb3a382702c4909c335e9b))


## [v7.2.1](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v7.2.1) (2024-05-06)

### Bug Fixes

- Add ConfigureAwait on async code to avoid potential deadlocks ([a97f908](https://github.com/Vonage/vonage-dotnet-sdk/commit/a97f908a80454c39470048c32580a37450941217))


### Build updates

- Remove dotnet7.0 from targeted frameworks given that version reached end-of-support on May 5th ([67ab30f](https://github.com/Vonage/vonage-dotnet-sdk/commit/67ab30fc4809813867ed2153a44b18d156f3d676))

- Remove net7.0 from pipelines ([e458347](https://github.com/Vonage/vonage-dotnet-sdk/commit/e4583478300522d4fcf37ab7ba751222a751c01a))


### Documentation

- Generate changelog for v7.2.0 ([a404398](https://github.com/Vonage/vonage-dotnet-sdk/commit/a404398fb38d2808c60b9dcc9545b8a1c812e348))

- Bump version to v7.2.1 ([fd75d49](https://github.com/Vonage/vonage-dotnet-sdk/commit/fd75d496c5d0a44e1fccd59c6d37e4b44d88aa54))


## [v7.2.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v7.2.0) (2024-04-15)

### Documentation

- Generate changelog for v7.1.0 ([f15e7a4](https://github.com/Vonage/vonage-dotnet-sdk/commit/f15e7a485bc7d09cd01c494f0994be8a06090245))

- Bump version to v7.2.0 ([ad37c8d](https://github.com/Vonage/vonage-dotnet-sdk/commit/ad37c8dc4fb9b6855387879a0e864249f9995b37))

- Generate changelog for v7.2.0 ([73d9366](https://github.com/Vonage/vonage-dotnet-sdk/commit/73d9366ead3f970585bfd514271b6d472b7dddf9))

- Bump version to v7.3.0 ([b7aad8f](https://github.com/Vonage/vonage-dotnet-sdk/commit/b7aad8f86805287d4e88d89fdb0b252abd650369))

- Generate changelog for v7.3.0 ([cb06018](https://github.com/Vonage/vonage-dotnet-sdk/commit/cb06018e155f5440a8da44d72f7f94746542c623))

- Bump version to v7.2.0 ([1e8dfff](https://github.com/Vonage/vonage-dotnet-sdk/commit/1e8dfffd23d358a704837c38133f85506b202ea9))

- Generate changelog for v7.2.0 ([2517e9f](https://github.com/Vonage/vonage-dotnet-sdk/commit/2517e9f9f86e62f0771242a25fddb0061497c871))


### Features

- Brand is now limiter to 16 characters in VerifyV2 ([11bb4ef](https://github.com/Vonage/vonage-dotnet-sdk/commit/11bb4ef8dfd18c8a8c27cdf9af7349ca07561bdf))

- NextWorkflow for VerifyV2 ([46cf5f0](https://github.com/Vonage/vonage-dotnet-sdk/commit/46cf5f02765cb491265f6cbbad530326a989c777))


### Merges

- Merge remote-tracking branch 'origin/main'
 ([2d9aad7](https://github.com/Vonage/vonage-dotnet-sdk/commit/2d9aad7233f7ce12fce34950c658236c572d9bf8))


### Refactoring

- [breaking] remove EventUrl and EventMethod from ConversationAction, with a disclaimer in the Readme ([fd02cd5](https://github.com/Vonage/vonage-dotnet-sdk/commit/fd02cd530c59ffa32eb111e545c7d6da44d83a7a))


## [v7.1.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v7.1.0) (2024-04-08)

### Bug Fixes

- Dispose event listener ([f0a99d8](https://github.com/Vonage/vonage-dotnet-sdk/commit/f0a99d81575066f1f1c8d63896e4502f98d3a2a1))

- Disable parallelization for connection lifetime tests ([38e7bec](https://github.com/Vonage/vonage-dotnet-sdk/commit/38e7becf9c429b78e6ea717e71538137db62ce8e))

- Disable parallelization for connection lifetime tests using NonThreadSafeCollection ([7f9f556](https://github.com/Vonage/vonage-dotnet-sdk/commit/7f9f55674582c8a3f0d5bd70ac06295827e5cd2d))


### Dependencies upgrade

- Update packages ([7f6c7a0](https://github.com/Vonage/vonage-dotnet-sdk/commit/7f6c7a00c585a89c262dff50639c9052bfcf21c1))


### Documentation

- Generate changelog for v7.0.0 ([df6aa0d](https://github.com/Vonage/vonage-dotnet-sdk/commit/df6aa0d95df60f2f340f890a497be42e73fe4f8a))

- Update readme with connection lifetime configuration ([2699b05](https://github.com/Vonage/vonage-dotnet-sdk/commit/2699b0576e5669df8c23e40859b600c6b5cd2a12))

- Bump version to v7.1.0 ([fefbccf](https://github.com/Vonage/vonage-dotnet-sdk/commit/fefbccfd71ec9bdd1f975262e53d091ac4350230))


### Features

- Add SocketsHttpHandler in Configuration to override http connection pool lifetime and idle timeout. ([607b9d7](https://github.com/Vonage/vonage-dotnet-sdk/commit/607b9d766606d4b30b097a25213831cb231a2c31))


### Refactoring

- Remove TODOs ([d4ee5e4](https://github.com/Vonage/vonage-dotnet-sdk/commit/d4ee5e4e7f0ad9ad6bf02725c67b22adf66d1814))

- Use single instance for HttpMessageHandler ([ca96110](https://github.com/Vonage/vonage-dotnet-sdk/commit/ca9611042da7b85823f565941b9b3319ebc5a3a2))

- Simplify client initialization ([5779958](https://github.com/Vonage/vonage-dotnet-sdk/commit/5779958816823689c901fedd1da603e87eecbe12))


## [v7.0.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v7.0.0) (2024-03-25)

### Build updates

- Add action to evaluate deployment frequency ([edac971](https://github.com/Vonage/vonage-dotnet-sdk/commit/edac971e77b38f7cdbb621bad513a20b9562a360))


### Documentation

- Generate changelog for v7.0.0-alpha.1 ([2cdca34](https://github.com/Vonage/vonage-dotnet-sdk/commit/2cdca3447fed2d49096ef7126a3f79b79281cd3f))

- Declutter readme ([40ac0fc](https://github.com/Vonage/vonage-dotnet-sdk/commit/40ac0fc046363423fd1d68ef4cbad57886f82ce3))

- Bump version to v7.0.0 ([7e0b5da](https://github.com/Vonage/vonage-dotnet-sdk/commit/7e0b5dad1989553c639d9c3e68f81e19947e51fc))


### Refactoring

- Remove unused 'EnsureSuccessStatusCode' key in appSettings ([cb8b670](https://github.com/Vonage/vonage-dotnet-sdk/commit/cb8b6703da823f10e4345456ceab742207d12373))


### Reverts

- Revert "build: add action to evaluate deployment frequency"

This reverts commit edac971e77b38f7cdbb621bad513a20b9562a360.
 ([b82638e](https://github.com/Vonage/vonage-dotnet-sdk/commit/b82638e3670e1da849a7a1de9e4cb9cd87856a9a))


## [v7.0.0-alpha.1](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v7.0.0-alpha.1) (2024-03-20)

### Documentation

- Generate changelog for v7.0.0-alpha ([2dfa40d](https://github.com/Vonage/vonage-dotnet-sdk/commit/2dfa40dcea2b68dbcfffd50f252d79ee28e908a3))

- Update v7.0.0 migration guide ([e12a11b](https://github.com/Vonage/vonage-dotnet-sdk/commit/e12a11b8567fba81e329aea1834f50bceac44d7d))

- Update v7.0.0 migration guide ([70ca933](https://github.com/Vonage/vonage-dotnet-sdk/commit/70ca933d54254fbeb5f5625e4655795132e053f5))

- Bump version to v7.0.0-alpha.1 ([7b12df0](https://github.com/Vonage/vonage-dotnet-sdk/commit/7b12df0097ef3df916b924473d26e1cf3eade5d4))


### Refactoring

- [breaking] remove URLs on Configuration, in favor or VonageUrls ([8d42b09](https://github.com/Vonage/vonage-dotnet-sdk/commit/8d42b0977f53e48833743e3382c52eccdb79de22))

- [breaking] remove obsolete Credentials constructors, making it read-only. ([e701f21](https://github.com/Vonage/vonage-dotnet-sdk/commit/e701f21a9ee158fa4536b0ee0de2123c56f613f5))


## [v7.0.0-alpha](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v7.0.0-alpha) (2024-03-15)

### Bug Fixes

- [breaking] make StartTime nullable on Answered webhook (#568) ([426d91f](https://github.com/Vonage/vonage-dotnet-sdk/commit/426d91f8832adbc9c9f420776cb4e495cc1af3ef))


### Documentation

- Generate changelog for v6.16.0 ([2494760](https://github.com/Vonage/vonage-dotnet-sdk/commit/249476030c0fa8a1200ac88ee1912c75451b08e5))

- Update v7.0.0 migration guides with latest breaking changes ([f09a93c](https://github.com/Vonage/vonage-dotnet-sdk/commit/f09a93c0b1d2c100c9034f507cb50eeb27a378a6))

- Update git-cliff configuration ([df6b870](https://github.com/Vonage/vonage-dotnet-sdk/commit/df6b8701aab7a6d44bdafa89d4504cca2453534a))

- Update v7.0.0 migration ([125d2cb](https://github.com/Vonage/vonage-dotnet-sdk/commit/125d2cb904b918a83ebcd4351a5533edd4b1823c))

- Update v7.0.0 migration guide ([9d3715b](https://github.com/Vonage/vonage-dotnet-sdk/commit/9d3715bced06a25ddb4fa684204a692e22901967))

- Remove migration steps from Readme to favor dedicated file ([0cfc6fe](https://github.com/Vonage/vonage-dotnet-sdk/commit/0cfc6fea34483a3c5eac7b3eabaa525f461f6c63))

- Bump version to v7.0.0-alpha ([39cf6ea](https://github.com/Vonage/vonage-dotnet-sdk/commit/39cf6ea1cd7ae22dc6ad62685e2971b44cc5c658))


### Features

- [breaking] add connection and socket timeouts on voice application (#548) ([8948ead](https://github.com/Vonage/vonage-dotnet-sdk/commit/8948ead4dba5ec17b7c4ff6d3d706fc3d87a880a))

- [breaking] make WhatsApp 'from' mandatory (#572) ([e473554](https://github.com/Vonage/vonage-dotnet-sdk/commit/e473554732e6106061d6a892687c7efe3a2846f1))


### Refactoring

- [breaking] replace 'appSettings' key by 'vonage' (#550) ([d1f245e](https://github.com/Vonage/vonage-dotnet-sdk/commit/d1f245ef99de31b290e2b1e8dc8b2c42037465c9))

- [breaking] remove sync methods from AccountClient, as well as subaccount feature ([aabbb67](https://github.com/Vonage/vonage-dotnet-sdk/commit/aabbb67b2b51b3ed80c7115e5f5985fe82aef9fc))

- Replace "async void" by "async Task" ([09062db](https://github.com/Vonage/vonage-dotnet-sdk/commit/09062db0e7e042a8876f3f7ac9bee2f65be74df1))

- [breaking] remove sync methods from ApplicationClient ([e8f90f6](https://github.com/Vonage/vonage-dotnet-sdk/commit/e8f90f642eb0833e2c8fef4ffe00447e6b4aee8c))

- [breaking] remove sync methods from ConversionClient ([7b5732a](https://github.com/Vonage/vonage-dotnet-sdk/commit/7b5732a09d66b4383dcfb001db7a1fe898671698))

- [breaking] remove sync methods from SmsClient ([52f4822](https://github.com/Vonage/vonage-dotnet-sdk/commit/52f48225aaf3951d0063ba3f47e4912388b41885))

- [breaking] remove sync methods from NumberInsightClient ([a4ba984](https://github.com/Vonage/vonage-dotnet-sdk/commit/a4ba9842d72ec95b565ffff8f0d4c90f4f8f3227))

- [breaking] remove sync methods from NumbersClient ([7779669](https://github.com/Vonage/vonage-dotnet-sdk/commit/77796699cbb5c3588f24de936f31148005fc6be0))

- [breaking] remove sync methods from PricingClient ([fe7a4c1](https://github.com/Vonage/vonage-dotnet-sdk/commit/fe7a4c1723c4ee7796f0f9f047c322f94e900344))

- [breaking] remove sync methods from RedactClient ([6e07991](https://github.com/Vonage/vonage-dotnet-sdk/commit/6e07991e2186905ae93ae33b4f952f0ebc3108e4))

- [breaking] remove sync methods from ShortCodesClient ([d3d7627](https://github.com/Vonage/vonage-dotnet-sdk/commit/d3d7627dfa6c1acbb234cbfb80d14a88c92fa25c))

- [breaking] remove sync methods from VerifyClient ([36fb4f0](https://github.com/Vonage/vonage-dotnet-sdk/commit/36fb4f0167da5a929b56d10e97e74e4c61715bcf))

- [breaking] remove sync methods from VoiceClient ([70428e7](https://github.com/Vonage/vonage-dotnet-sdk/commit/70428e7879311e66ad908e42b6b19f8ed060f813))

- [breaking] remove obsolete Input from webhooks ([d166b59](https://github.com/Vonage/vonage-dotnet-sdk/commit/d166b59297f6837ed52aeb71be062fc4a5c78bcc))

- [breaking] remove obsolete portuguese language in Meetings ([2b64f3c](https://github.com/Vonage/vonage-dotnet-sdk/commit/2b64f3c4dbea43a6d842a1bff8363d55975cc56e))

- [breaking] remove obsolete VoiceName from TalkCommand ([a832a3d](https://github.com/Vonage/vonage-dotnet-sdk/commit/a832a3d0d2774a18c154e7f3162781f62179881f))

- [breaking] remove obsolete VoiceName from TalkAction ([d8a00f9](https://github.com/Vonage/vonage-dotnet-sdk/commit/d8a00f904847647f4d4d3445ca5a4abfcf84759f))


## [v6.16.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v6.16.0) (2024-03-07)

### Bug Fixes

- Wrong setup in test ([e497cf0](https://github.com/Vonage/vonage-dotnet-sdk/commit/e497cf007c4d2d1a5c432ccff78940a100a4db32))


### Build updates

- Remove module from stryker config ([64bbd1e](https://github.com/Vonage/vonage-dotnet-sdk/commit/64bbd1e71fd378d37b7347eb9e80e546a4d9b400))

- Upgrade stryker to v4 ([8d1bd74](https://github.com/Vonage/vonage-dotnet-sdk/commit/8d1bd74a2fef0a48770265e2c349b00ef86cb311))

- Add bump_version script to increase the version and generate a changelog ([a902c76](https://github.com/Vonage/vonage-dotnet-sdk/commit/a902c76a603baeffe109a8907a06fbe0110e4df3))

- Fix spacing in bump script ([758aa8d](https://github.com/Vonage/vonage-dotnet-sdk/commit/758aa8d4a5e08f365097a225fb4f79cb7f9fd2ab))

- Add missing command in bump version script ([f920c2d](https://github.com/Vonage/vonage-dotnet-sdk/commit/f920c2d12976805abad55200e41d389e9ec5e430))

- Fix commands in bump version script ([5a3d1cd](https://github.com/Vonage/vonage-dotnet-sdk/commit/5a3d1cdcb493817153d513b42f9d98dfc36e939d))


### Documentation

- Generate changelog for v6.15.5 ([9b83a1d](https://github.com/Vonage/vonage-dotnet-sdk/commit/9b83a1dff97c3c459eb6fb6f53269d9d037fb631))

- Create migration guide for v7.0.0 ([d9d65f5](https://github.com/Vonage/vonage-dotnet-sdk/commit/d9d65f577d2c84d5bb0b271986707a43302bb221))


### Features

- Add async capabilities on Maybe ([9446d9c](https://github.com/Vonage/vonage-dotnet-sdk/commit/9446d9c43e8d3eeec2f0ca9aa1ebd26b1bbff500))

- Implement async extensions on Maybe ([c09d7c0](https://github.com/Vonage/vonage-dotnet-sdk/commit/c09d7c0b26d58b6b6223458da8190307c0fbb0db))

- Support Match extension on async Result ([c6574f8](https://github.com/Vonage/vonage-dotnet-sdk/commit/c6574f828c7f3c3c60324fc99d4bcd9689d23701))

- Support optional From in VerifyV2 Sms worflow ([e1e7900](https://github.com/Vonage/vonage-dotnet-sdk/commit/e1e7900f4acffd04a98a440638febdf042aca129))


### Other

- Add gitignore to solution ([3a4f0d5](https://github.com/Vonage/vonage-dotnet-sdk/commit/3a4f0d539f2c719b8475cfe56d29b1f7f67490b0))

- Bump version to v6.16.0' ([b1dcd1a](https://github.com/Vonage/vonage-dotnet-sdk/commit/b1dcd1aeb4d6176beaf11fe08c657cff3f7523c7))


### Refactoring

- Move Result extensions tests in a specific file ([713c0be](https://github.com/Vonage/vonage-dotnet-sdk/commit/713c0be4bda3e925062edf76974f042ddcb14dde))

- Move tests behaviors for Monads in a specific collection ([f6954b6](https://github.com/Vonage/vonage-dotnet-sdk/commit/f6954b64703c5f66d1bfadd9e929759ee452b1a3))

- Clean duplicates in ResultTest ([6ee846f](https://github.com/Vonage/vonage-dotnet-sdk/commit/6ee846f448ac5042f4be59a5ccf04714a536cf05))


## [v6.15.5](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v6.15.5) (2024-02-29)

### Dependencies upgrade

- Update packages ([28701c2](https://github.com/Vonage/vonage-dotnet-sdk/commit/28701c201ca6f8cfc7034d6efdb91623d862276a))


### Documentation

- Generate changelog for v6.15.4 ([f9c7b06](https://github.com/Vonage/vonage-dotnet-sdk/commit/f9c7b066e4f5112fa5bab4aff06dba6ae5d048b1))

- Add new category in cliff configuration ([5380bd3](https://github.com/Vonage/vonage-dotnet-sdk/commit/5380bd3a379849004f37bf04a930364ae879c1c9))


### Features

- Add EntityId and ContentId on SmsWorkflow ([197513c](https://github.com/Vonage/vonage-dotnet-sdk/commit/197513c837f2b31792c1f52aba6d71e07896a61a))

- Support multiArchiveTag when creating an archive ([142c988](https://github.com/Vonage/vonage-dotnet-sdk/commit/142c988e44721a3f370edf11626bd334fd13a153))

- Support multiArchiveTag on archive responses ([d9b38c2](https://github.com/Vonage/vonage-dotnet-sdk/commit/d9b38c24420a39e40c441222e19bb5d8620c3eb3))


### Releases

- Bump version to v6.15.5 ([0b6decb](https://github.com/Vonage/vonage-dotnet-sdk/commit/0b6decbc8110a2fa4a2200fa9dd84822c703358f))


## [v6.15.4](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v6.15.4) (2024-02-26)

### Bug Fixes

- Remove default resolution when creating an archive ([6570363](https://github.com/Vonage/vonage-dotnet-sdk/commit/657036338792e37bcb0fa1a11fb20510c6700d4e))


### Documentation

- Generate changelog for v6.15.3 ([dad79a5](https://github.com/Vonage/vonage-dotnet-sdk/commit/dad79a5659ea0577b2ec1195d71130ec7ed5cd7d))


### Releases

- Bump version to v6.15.4 ([080e68f](https://github.com/Vonage/vonage-dotnet-sdk/commit/080e68f35478453b94a86271992af1388247fdea))


## [v6.15.3](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v6.15.3) (2024-02-23)

### Documentation

- Generate changelog for v6.15.2 ([40b6792](https://github.com/Vonage/vonage-dotnet-sdk/commit/40b6792553caa94e914dccf8f9e8b8c9bf212a99))


### Features

- Register Credentials in service collection extensions ([7d7d7a9](https://github.com/Vonage/vonage-dotnet-sdk/commit/7d7d7a96d59c6b0edbdf597eeab97c8735514e63))

- Enable custom claims for Video token ([a6cbb5c](https://github.com/Vonage/vonage-dotnet-sdk/commit/a6cbb5c6c819f010915b8abeef69162ca07c27aa))


### Releases

- Bump version to v6.15.3 ([ad94cbe](https://github.com/Vonage/vonage-dotnet-sdk/commit/ad94cbed2966b30c4bb534efce4ad53d3866ca51))


## [v6.15.2](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v6.15.2) (2024-02-22)

### Bug Fixes

- Pass claims for VideoToken serialization ([1a42173](https://github.com/Vonage/vonage-dotnet-sdk/commit/1a421738336226f0a9c3e19c583323b24d7f3e31))


### Documentation

- Update readme about next major version ([d619f2c](https://github.com/Vonage/vonage-dotnet-sdk/commit/d619f2ce1a6a9d9c58b03e30d5c09da0118bac27))

- Update readme with latest breaking changes ([331938c](https://github.com/Vonage/vonage-dotnet-sdk/commit/331938c1b2b34eeb1ba69e36362e119c87c39d6c))


### Releases

- Bump version to v6.15.2 ([145d545](https://github.com/Vonage/vonage-dotnet-sdk/commit/145d5456748c0eb8d23e0a4a2b17a4c10f5dbae0))


## [v6.15.1](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v6.15.1) (2024-02-21)

### Bug Fixes

- Downgrade Stryker to v3.12.0 until v3.13.x is fixed ([e89ee70](https://github.com/Vonage/vonage-dotnet-sdk/commit/e89ee70b34ea1442c7440dfc6f905a3128546b71))

- Downgrade Stryker to v3.12.0 until v3.13.x is fixed ([d978992](https://github.com/Vonage/vonage-dotnet-sdk/commit/d978992c0385472c20f727e664d08f0b5b7f2dd1))

- Use configuration instance instead of singleton in ApiRequest ([779bae0](https://github.com/Vonage/vonage-dotnet-sdk/commit/779bae09e22151247631f29a9e7ea6cabca330c0))


### Documentation

- Generate changelog for v6.15.0 ([46187e1](https://github.com/Vonage/vonage-dotnet-sdk/commit/46187e1848a5f04b957a69f23adc924c2fe9ec64))

- Generate changelog for v6.15.1 ([7484324](https://github.com/Vonage/vonage-dotnet-sdk/commit/748432400fa7833c436cdd9435fdd0b28cff8552))


### Features

- Implement request for GetUserConversations ([d315f61](https://github.com/Vonage/vonage-dotnet-sdk/commit/d315f617c546f04656760da24003a1a88db246b2))

- Implement endpoint construction for GetUserConversations ([91a6132](https://github.com/Vonage/vonage-dotnet-sdk/commit/91a6132865fca15aa1307daeeb3e6b9b2655bbc4))

- Implement request creation from HalLink for GetUserConversations ([68fbbd5](https://github.com/Vonage/vonage-dotnet-sdk/commit/68fbbd5ebf480b7362ac39659de4a6619582ff70))

- Implement GetUserConversations ([fee8589](https://github.com/Vonage/vonage-dotnet-sdk/commit/fee85899403007d1868c31e5c426ec0a2ccebc31))

- Implement new object VonageUrls to handle multi-region urls ([6ccc249](https://github.com/Vonage/vonage-dotnet-sdk/commit/6ccc249e86beb4407fcf8fd780212921f23ceea3))


### Merges

- Merge remote-tracking branch 'origin/main'
 ([be02e1c](https://github.com/Vonage/vonage-dotnet-sdk/commit/be02e1cb330a348645bf315d1030e9efeb78f633))


### Other

- Refactor on Meetings Client ([a37a78f](https://github.com/Vonage/vonage-dotnet-sdk/commit/a37a78f2005a6b684561f50c6a7acd9aae6fbe42))


### Refactoring

- Exclude .NET Frameworks from target frameworks ([a0cdb4f](https://github.com/Vonage/vonage-dotnet-sdk/commit/a0cdb4ff9138c7af235f646a4bcfa04809a84dce))

- Use builder when creating request from a HalLink ([78d2b71](https://github.com/Vonage/vonage-dotnet-sdk/commit/78d2b717e786d52b5e73a9ac60841f072301ec5c))

- Use builder when creating request from a HalLink ([f880710](https://github.com/Vonage/vonage-dotnet-sdk/commit/f8807101110fb063d26f0cad3c88a3e05a1246ab))

- Enable test parallelization (#566) ([d87e0e1](https://github.com/Vonage/vonage-dotnet-sdk/commit/d87e0e1c1e7ae6b2a74a0686ad6b9f1ef8cac4e5))

- Make builders immutable for Conversations ([25a7648](https://github.com/Vonage/vonage-dotnet-sdk/commit/25a7648100f1d88c56a229f2988d47af7fa3bba1))

- Code cleanup on tests ([e3b8506](https://github.com/Vonage/vonage-dotnet-sdk/commit/e3b8506dd476154c617396167c95c09dfe517708))

- Clean HalLink builders ([f52d115](https://github.com/Vonage/vonage-dotnet-sdk/commit/f52d1151a9aee76c655ae4ce4dd721d505a5a5ec))

- Clean empty folders ([79ab4d1](https://github.com/Vonage/vonage-dotnet-sdk/commit/79ab4d15d9f1a039f41b26ba759dcc90cc680f7f))

- Add trait on serialization tests ([5d048a6](https://github.com/Vonage/vonage-dotnet-sdk/commit/5d048a6c69e8b98b037b48a7ae312c3e829878e8))

- Add missing traits ([72aff33](https://github.com/Vonage/vonage-dotnet-sdk/commit/72aff33ac46dc7797bd5f7abfa817a09699967b9))

- Add legacy traits on tests ([7a56337](https://github.com/Vonage/vonage-dotnet-sdk/commit/7a5633774f9094346535a01f03b12a812562bd98))

- Remove url information from Configuration ([00197f5](https://github.com/Vonage/vonage-dotnet-sdk/commit/00197f5cba5f84f537cc366bd7f647f8f3f754b3))

- Mark urls obsolete in Configuration, in favor of the VonageUrls property ([55c138a](https://github.com/Vonage/vonage-dotnet-sdk/commit/55c138a06731ff475738f7cec72cdb83f15198a6))

- Remove uses of Configuration.VideoApiUrl ([f02fc6f](https://github.com/Vonage/vonage-dotnet-sdk/commit/f02fc6f757da33cdcf53ba605342202acf2ffadb))

- Remove uses of Configuration.RestApiUrl ([34b5d40](https://github.com/Vonage/vonage-dotnet-sdk/commit/34b5d40cc7f4b71c43aa222f0936b966613c086b))

- Remove uses of Configuration.NexmoApiUrl ([d19fb8d](https://github.com/Vonage/vonage-dotnet-sdk/commit/d19fb8d0836f06edbe5e633fe722c48c2e74c26a))


### Releases

- Bump version to v6.15.1 ([25c5d09](https://github.com/Vonage/vonage-dotnet-sdk/commit/25c5d09984a621bc9e8827d2bf5049beed40d9d6))


## [v6.15.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v6.15.0) (2023-12-20)

### Bug Fixes

- Remove Vonage.Common from workflows ([45b1d70](https://github.com/Vonage/vonage-dotnet-sdk/commit/45b1d70c8717095701444b2731d32b7a12d42357))

- Broken test due to incompatible target-typed new ([5ba9500](https://github.com/Vonage/vonage-dotnet-sdk/commit/5ba9500a92b254dc34f1dea1e0e7302697849ee1))


### Documentation

- Update changelog ([b8d4a5f](https://github.com/Vonage/vonage-dotnet-sdk/commit/b8d4a5ff01747f889d44da1ffd2a72355e7bbe4f))

- Add migration guide for Video API ([1b2bc1f](https://github.com/Vonage/vonage-dotnet-sdk/commit/1b2bc1f7dcedbbed92d4b282333258f41f807140))

- Add missing header link in migrations ([8af1d00](https://github.com/Vonage/vonage-dotnet-sdk/commit/8af1d0063cad4812e5ac40cbce9f8392563908d7))


### Features

- Extend support for VideoTokenGenerator ([8716a7a](https://github.com/Vonage/vonage-dotnet-sdk/commit/8716a7a5ff97b9caa719646d8025d615ce7af1df))

- Implement serialization for CreateConversation ([a871912](https://github.com/Vonage/vonage-dotnet-sdk/commit/a8719129fe28d19ddb4ddc50e3d92aae2fc8391e))

- Implement CreateConversation ([d1e2d9e](https://github.com/Vonage/vonage-dotnet-sdk/commit/d1e2d9ee97d7d92b8e2132ef4a573140c895681a))

- Add net8.0 as target-framework for test projects (#560) ([3da8df5](https://github.com/Vonage/vonage-dotnet-sdk/commit/3da8df5a75492ad7be17118cda15545214fbbfa7))

- Implement parsing for DeleteConversation ([e36996a](https://github.com/Vonage/vonage-dotnet-sdk/commit/e36996ae4919f39b5d4263bf8c8c738d2f1dff41))

- Implement DeleteConversationRequest ([77cf0ce](https://github.com/Vonage/vonage-dotnet-sdk/commit/77cf0ce9d6543eff40386247e0d6cd29d7f85933))

- Implement DeleteConversation ([f4e1396](https://github.com/Vonage/vonage-dotnet-sdk/commit/f4e1396004c9b1c016166e9c85300074362c8208))

- Implement GetConversationRequest ([90cd652](https://github.com/Vonage/vonage-dotnet-sdk/commit/90cd652d1b71c5a021768ab8d404ac1ac5b285ca))

- Implement serialization for GetConversation ([2b29193](https://github.com/Vonage/vonage-dotnet-sdk/commit/2b29193544c054fe9dd974882b6a9c5080cd4c03))

- Implement GetConversation ([f890405](https://github.com/Vonage/vonage-dotnet-sdk/commit/f8904051dfdfab9805c6c354688ed9554e0191e9))

- Implement builder for GetConversations ([cee1ff6](https://github.com/Vonage/vonage-dotnet-sdk/commit/cee1ff6fc9a331904d82d23ce31069db6ebc91db))

- Implement GetEndpointPath for GetConversations ([1edd9e9](https://github.com/Vonage/vonage-dotnet-sdk/commit/1edd9e998b2ff9572a48a41ac60d61ac082beb88))

- Implement serialization for GetConversations ([e519ca9](https://github.com/Vonage/vonage-dotnet-sdk/commit/e519ca98338a9c8ec6e94d69be551257acdbc43a))

- Implement GetConversations ([d53529f](https://github.com/Vonage/vonage-dotnet-sdk/commit/d53529ff9734052873529be0a3bd5f6ea14041d3))

- Implement BuildRequest on hal link for GetConversations ([5008bd0](https://github.com/Vonage/vonage-dotnet-sdk/commit/5008bd0b69a05e6bf933db6757c3b299f1b85f87))

- Implement request builder for UpdateConversation ([46ff578](https://github.com/Vonage/vonage-dotnet-sdk/commit/46ff578d816df0a82eb2c0062eb28a40a961efe7))

- Implement serialization for UpdateConversation ([5c683e2](https://github.com/Vonage/vonage-dotnet-sdk/commit/5c683e251604fce6f002d4a13d0377f5a55b666e))

- Implement request for UpdateConversation ([53a7ee9](https://github.com/Vonage/vonage-dotnet-sdk/commit/53a7ee9d087f003c7fc86f66713eeb927b5e976a))

- Implement UpdateConversation ([e60d8de](https://github.com/Vonage/vonage-dotnet-sdk/commit/e60d8de6a978a598acb16b03ebe0365c53cac3c5))


### Refactoring

- Move Conversation to enable reusability ([d6740d7](https://github.com/Vonage/vonage-dotnet-sdk/commit/d6740d7fcadc18e7ff3f6941725615052d4ded7d))

- Remove Vonage.Common (#562) ([93bba4b](https://github.com/Vonage/vonage-dotnet-sdk/commit/93bba4b35973e345638761d71ec46b2bfa435401))

- Rename Vonage.Test.Unit into Vonage.Test (#563) ([b9c565d](https://github.com/Vonage/vonage-dotnet-sdk/commit/b9c565df6cd288e931cb802dc33d9f68061c8259))


### Releases

- Bump version to v6.15.0 ([e311974](https://github.com/Vonage/vonage-dotnet-sdk/commit/e31197412a2b203e62a4dbe5370bbbb229f33efd))


## [v6.14.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v6.14.0) (2023-12-11)

### Bug Fixes

- Add specific System.Net.Http for Vonage.Common.Test to avoid mismatch when building with .NetFramework ([aa6a2d1](https://github.com/Vonage/vonage-dotnet-sdk/commit/aa6a2d1bd9b2b58bee0bc1833142f9978993df1f))

- Add explicit reference to System.Net.Http for .NetFramework4.6.2 ([5d151b6](https://github.com/Vonage/vonage-dotnet-sdk/commit/5d151b6170723bbf337d79773fe6ec85a7753111))


### Documentation

- Update changelog after v6.13.0 tag ([a89ee57](https://github.com/Vonage/vonage-dotnet-sdk/commit/a89ee57cb323db0b28b73c93e1292b4b3ac84429))

- Update changelog ([ea21a0c](https://github.com/Vonage/vonage-dotnet-sdk/commit/ea21a0c838a9226c148f774b67c49e5493ea0080))

- Update readme with available APIs ([13875a8](https://github.com/Vonage/vonage-dotnet-sdk/commit/13875a835a51519edc78b4c3682e5ca060871b5d))


### Features

- Base structure for CreateConversation in Conversations API ([b401dc7](https://github.com/Vonage/vonage-dotnet-sdk/commit/b401dc7220775f7b965f83b93ceec7ef42bc142d))

- Implement CreateConversationResponse deserialization ([98b6539](https://github.com/Vonage/vonage-dotnet-sdk/commit/98b6539125f774a2e58114aea2ea7c701590761a))

- Override .ToString() on Maybe<T> ([09133a0](https://github.com/Vonage/vonage-dotnet-sdk/commit/09133a05fe517e0dddaf21b0bf50a7733535e06a))

- Implement Merge capability on Maybe<T> ([f961208](https://github.com/Vonage/vonage-dotnet-sdk/commit/f96120835c58d657f3cc722f6ee0d92da9a5db70))

- Implement GetFailureUnsafe on Result<T> ([038c24e](https://github.com/Vonage/vonage-dotnet-sdk/commit/038c24e3b3c6a00b94181dde28a08ce6fb637250))

- Add BeEquivalentTo assertions to Maybe<T> and Result<T> ([364d43d](https://github.com/Vonage/vonage-dotnet-sdk/commit/364d43ddf75b19c5ac55740fe918b0fa60017314))

- Implement builder for Name & DisplayName in CreateConversation ([5b1b575](https://github.com/Vonage/vonage-dotnet-sdk/commit/5b1b57502f77e1b0fdebcbf734cc284c9e052de9))

- Implement builder for Uri in CreateConversation ([4d012dd](https://github.com/Vonage/vonage-dotnet-sdk/commit/4d012dd8774f3f304fee04af5c5ba97d8bf14a54))

- Implement builder for Properties in CreateConversation ([7a8c4c1](https://github.com/Vonage/vonage-dotnet-sdk/commit/7a8c4c13d0adcfe2b79b716b41e6e074cd720c8c))

- Implement builder for Callback in CreateConversation ([61511f3](https://github.com/Vonage/vonage-dotnet-sdk/commit/61511f3320887f2423ad7589ebaea9c32f29efe5))

- Implement request validation for CreateConversation ([4250e09](https://github.com/Vonage/vonage-dotnet-sdk/commit/4250e09a11b6e4210ae92f3a5264a89854420d7d))

- Implement default serialization for CreateConversation ([5d92598](https://github.com/Vonage/vonage-dotnet-sdk/commit/5d92598504d1dc0c9022be7c76bc310497fec6b5))

- Implement builder for Numbers in CreateConversation ([51cddd7](https://github.com/Vonage/vonage-dotnet-sdk/commit/51cddd79f2a553db5cde504bf03488d06ea77123))

- Support serialization for CreateConversation ([6e6cc78](https://github.com/Vonage/vonage-dotnet-sdk/commit/6e6cc78536589549e19b961acdde2806e421deda))

- Video integration (#558) ([2e5c46e](https://github.com/Vonage/vonage-dotnet-sdk/commit/2e5c46e18c22be8eb73c1daa9614feb0a36e5df6))


### Merges

- Merge branch 'main' of https://github.com/Vonage/vonage-dotnet-sdk
 ([0573876](https://github.com/Vonage/vonage-dotnet-sdk/commit/057387692544222bc958c558c45bf44d4c0367d4))


### Pipelines

- Make changelog workflow dispatch only ([f9466fa](https://github.com/Vonage/vonage-dotnet-sdk/commit/f9466fa14383181a7b6f9386e24fbf72634a23c9))


### Refactoring

- Use value equality when asserting properties ([68ff008](https://github.com/Vonage/vonage-dotnet-sdk/commit/68ff008312e7f5e488a31accb7afc79d12956912))

- Add invariant culture when parsing DateTimeOffset ([16bcd1b](https://github.com/Vonage/vonage-dotnet-sdk/commit/16bcd1b9114720510551535d613a3f1211112010))

- Update dependencies ([128712c](https://github.com/Vonage/vonage-dotnet-sdk/commit/128712c5c9c9290f491ec17c27f00c23b9b9cf9e))


### Releases

- Bump version to v6.14.0 ([50cca55](https://github.com/Vonage/vonage-dotnet-sdk/commit/50cca55c7495f6fc8bdc5d62126cf62412fd8587))


### Reverts

- Revert "fix: add specific System.Net.Http for Vonage.Common.Test to avoid mismatch when building with .NetFramework"

This reverts commit aa6a2d1bd9b2b58bee0bc1833142f9978993df1f.
 ([3a39d06](https://github.com/Vonage/vonage-dotnet-sdk/commit/3a39d06200340c7a71623a872e576841c04a327d))


## [v6.13.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v6.13.0) (2023-12-01)

### Bug Fixes

- Mutation testing ignoring test value for private key ([b861905](https://github.com/Vonage/vonage-dotnet-sdk/commit/b861905a438cfe47a0c627375d170ebfdcd7cbbf))

- Mutation testing ignoring test value for private key ([00d89a3](https://github.com/Vonage/vonage-dotnet-sdk/commit/00d89a309f02dbf0ac5720dbff379a22892bb3f7))

- Mutation testing ignoring test value for private key ([e0552d0](https://github.com/Vonage/vonage-dotnet-sdk/commit/e0552d0e294220a32f65a1dcf67649a2fe406836))

- IDisposable implementation on TestingContext ([56ebfb1](https://github.com/Vonage/vonage-dotnet-sdk/commit/56ebfb11732e87c22ba296f765f0c32ca5e03c01))


### Documentation

- Add git-cliff for changelog generation ([0fbfbd8](https://github.com/Vonage/vonage-dotnet-sdk/commit/0fbfbd8ed987dc31b9d66e64289c3eaea60033cf))

- Generate changelog using git-cliff ([757e4a8](https://github.com/Vonage/vonage-dotnet-sdk/commit/757e4a8dabbe30b5e377f5828e5b97272cf79cc4))

- Update changelog format ([dc61d19](https://github.com/Vonage/vonage-dotnet-sdk/commit/dc61d19d53a82d31813682a0cc30923fa3fb787b))

- Update changelog ([296b723](https://github.com/Vonage/vonage-dotnet-sdk/commit/296b723ee876d6c334e6e2444811f282cf07a183))

- Update changelog ([4aaa658](https://github.com/Vonage/vonage-dotnet-sdk/commit/4aaa658388b411366074df2bde03c244e24ea393))

- Update changelog ([8f7d33d](https://github.com/Vonage/vonage-dotnet-sdk/commit/8f7d33dd8e419d34140d311d26355ccab72cdf70))

- Update changelog ([4ea6ad3](https://github.com/Vonage/vonage-dotnet-sdk/commit/4ea6ad3c488e20aa8bbfaefc37b25e7d6fa7e172))

- Add commit hash in changelog ([505329c](https://github.com/Vonage/vonage-dotnet-sdk/commit/505329c62bc80e3d582e4df601f64ff064f2790e))

- Update changelog ([88c2112](https://github.com/Vonage/vonage-dotnet-sdk/commit/88c211248e48d0b6f5662642dcb179ffb97e3aab))


### Features

- Setup base structure for Number Insight V2 ([f906166](https://github.com/Vonage/vonage-dotnet-sdk/commit/f90616648a43237090b250a77c2d6dc89aaa15e3))

- Write main use case for Fraud Check in Number Insight V2 ([cf425f3](https://github.com/Vonage/vonage-dotnet-sdk/commit/cf425f3bbbcde2b71e447d780122d07852cdcad6))

- Implement builder for FraudCheck ([2409d10](https://github.com/Vonage/vonage-dotnet-sdk/commit/2409d10e63f909a64d30f4ea078eb1bc08141015))

- Use PhoneNumber on FraudCheckRequest ([8770ef7](https://github.com/Vonage/vonage-dotnet-sdk/commit/8770ef773484f40d9d4ffc67fa5739be1e2f6a26))

- Add BiMap functionality to Result ([5e3945c](https://github.com/Vonage/vonage-dotnet-sdk/commit/5e3945c80055796c88b1d60eab53d054b50c546d))

- Implement serialization for FraudCheckRequest ([c927b87](https://github.com/Vonage/vonage-dotnet-sdk/commit/c927b8726e958425125b5b5d3024f6983276d6a5))

- Implement FraudCheckResponse deserialization ([dfc9249](https://github.com/Vonage/vonage-dotnet-sdk/commit/dfc924957258f9a0a4a3f6229e4815e510ecfcf7))

- Implement PerformFraudCheck capability on Number Insight client ([a65986e](https://github.com/Vonage/vonage-dotnet-sdk/commit/a65986ee20d253b5434481ae3d15610f0cfb467c))

- Add NumberInsights V2 client in services registration ([abf4af3](https://github.com/Vonage/vonage-dotnet-sdk/commit/abf4af36642af3bb25c884efa5be73ea2d51bcb6))

- Change FraudScoreLabel into an enum ([f45bf94](https://github.com/Vonage/vonage-dotnet-sdk/commit/f45bf94d5eaafe71c15770660e21830c27c58066))

- Change RiskRecommendation to an enum ([b0da643](https://github.com/Vonage/vonage-dotnet-sdk/commit/b0da6435896ea2560eb4a9c10a1ec055e8e854a0))

- Change SimSwap status to an enum ([e82ac73](https://github.com/Vonage/vonage-dotnet-sdk/commit/e82ac73f80837d71efcd288241a68af671118256))

- FraudCheck without FraudScore or SimSwap ([4309d11](https://github.com/Vonage/vonage-dotnet-sdk/commit/4309d110dc9eed4c911d76f74a8839df9873b373))


### Merges

- Merge remote-tracking branch 'origin/main'
 ([0e25394](https://github.com/Vonage/vonage-dotnet-sdk/commit/0e253943f74fdc1034e881d32e027be84fec9c0c))

- Merge remote-tracking branch 'origin/main'
 ([4ae4dda](https://github.com/Vonage/vonage-dotnet-sdk/commit/4ae4ddacdf995168539f50396201cef00d16c93d))


### Pipelines

- Add changelog workflow ([83f91a6](https://github.com/Vonage/vonage-dotnet-sdk/commit/83f91a6c124ffb7357ff8735dc0f621d7ceaabc0))

- Setup changelog auto update ([8a8057e](https://github.com/Vonage/vonage-dotnet-sdk/commit/8a8057eb568f8a5b03bbc6b6adafd53c07bb883f))

- Use auth token for changelog workflow ([35bff74](https://github.com/Vonage/vonage-dotnet-sdk/commit/35bff74dd2c2693485ad46db99fb49babc62facd))

- Use PAT for changelog auto update ([67d05d4](https://github.com/Vonage/vonage-dotnet-sdk/commit/67d05d4b6eabcf61b980461ef976dece07ced0c2))

- Make changelog update on release ([2ea9192](https://github.com/Vonage/vonage-dotnet-sdk/commit/2ea91921071ace858229bc5087a1fa3dcabc71f9))


### Refactoring

- Use BiMap in FraudCheckRequestBuilder ([747bfb9](https://github.com/Vonage/vonage-dotnet-sdk/commit/747bfb90150a319462e7f03d24f570820effe8f5))

- Test FraudCheck with both Basic and Bearer auth ([bbd8a4b](https://github.com/Vonage/vonage-dotnet-sdk/commit/bbd8a4beea669f8ea24853e1648da35b386de409))

- Reuse authorization value from E2EHelper ([4e58812](https://github.com/Vonage/vonage-dotnet-sdk/commit/4e588120e15dcf1d2cb37566aa01d16ff859f252))

- E2E tests for FraudCheck ([9a27e8f](https://github.com/Vonage/vonage-dotnet-sdk/commit/9a27e8f1aa0139d7d0b77bda51ae6416294e19ce))

- Use Maybe on both optionals SimSwap and FraudScore ([7767886](https://github.com/Vonage/vonage-dotnet-sdk/commit/776788680ef7d6a8d30e9018d5fd929234272b4e))


### Releases

- Include package Vonage.Common ([bce66db](https://github.com/Vonage/vonage-dotnet-sdk/commit/bce66db2288a72cf0ff9350acb82dae33722f2d0))

- Bump version to v6.13.0 ([fc17ed9](https://github.com/Vonage/vonage-dotnet-sdk/commit/fc17ed90a8de7c02d500e85ad2f0a5363ea41363))


### Reverts

- Revert "ci: use auth token for changelog workflow"

This reverts commit 35bff74dd2c2693485ad46db99fb49babc62facd.
 ([23084e2](https://github.com/Vonage/vonage-dotnet-sdk/commit/23084e2140552c3024a2f52e6ebc4ba0f51ab199))

- Revert "fix: mutation testing ignoring test value for private key"

This reverts commit b861905a438cfe47a0c627375d170ebfdcd7cbbf.
 ([646d460](https://github.com/Vonage/vonage-dotnet-sdk/commit/646d460276286b8557be527713372c580f547aa4))

- Revert "fix: mutation testing ignoring test value for private key"

This reverts commit 00d89a309f02dbf0ac5720dbff379a22892bb3f7.
 ([f48fa0a](https://github.com/Vonage/vonage-dotnet-sdk/commit/f48fa0a5097cd9634defcf23ef1c0d4203370249))


## [v6.12.3](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v6.12.3) (2023-11-27)

### Documentation

- Add whitesource file to solution ([9bd1637](https://github.com/Vonage/vonage-dotnet-sdk/commit/9bd16376bed09ca24bf5001658d9dd9b074f7ad6))


### Releases

- Bump Video version to v7.0.7-beta ([9431e53](https://github.com/Vonage/vonage-dotnet-sdk/commit/9431e53706d4edc202b48dfe8e652516f432ccf4))

- Remove specific assembly version for Vonage ([8ebb8e6](https://github.com/Vonage/vonage-dotnet-sdk/commit/8ebb8e6f8930985527f4e2b269d84797b25b0a29))

- Reorganize dependencies in project file ([5ab30fc](https://github.com/Vonage/vonage-dotnet-sdk/commit/5ab30fc300223c8257727c0253bca24b36502c38))

- Bump video version to v7.1.0-beta ([29c256e](https://github.com/Vonage/vonage-dotnet-sdk/commit/29c256e4a64a01d37c67bdf8073443d8fbb6548c))

- Bump version to v6.12.2 ([45b3610](https://github.com/Vonage/vonage-dotnet-sdk/commit/45b36104cc43ec7b1b8da0d64899a7805459620e))

- Bump version to v6.12.3 ([e3894b2](https://github.com/Vonage/vonage-dotnet-sdk/commit/e3894b2b0c5c95823ab0cfdfe164e201eb90d7b2))


### Reverts

- Revert "release: reorganize dependencies in project file"

This reverts commit 5ab30fc300223c8257727c0253bca24b36502c38.
 ([7c66b3d](https://github.com/Vonage/vonage-dotnet-sdk/commit/7c66b3d48f71eb4fb6a6c15cc70d05c710ee1e72))


## [v6.12.1](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v6.12.1) (2023-11-24)

### Bug Fixes

- Explicit using for system.net.http to avoid conflicts with older frameworks (#553) ([55a09bc](https://github.com/Vonage/vonage-dotnet-sdk/commit/55a09bc9d6e36f41f13c2dfce5d2c53d26a1c26e))

- Result assertion messages ([41d83c9](https://github.com/Vonage/vonage-dotnet-sdk/commit/41d83c9eda09c566a7b29bcb34413ed3d2cca0a3))

- Reduce timeout for httpClient timeout tests ([a68912a](https://github.com/Vonage/vonage-dotnet-sdk/commit/a68912a81424c787184f1ed49b556acc4651efa0))

- Ambiguous reference with System.Net.Http ([2cb16e3](https://github.com/Vonage/vonage-dotnet-sdk/commit/2cb16e3805aa6b0d988f6970efc8e2f215898884))

- Signed Vonage.Server.Test project ([727393a](https://github.com/Vonage/vonage-dotnet-sdk/commit/727393add0baf25a1ad9369a2105eeffe08b9f4b))

- Use test class for VideoClient tests to avoid InternalsVisibleTo ([70a866c](https://github.com/Vonage/vonage-dotnet-sdk/commit/70a866cf0aee222f59c9957c58bd791181ccc096))

- Incorrect path for Vonage.Server release ([e9eef58](https://github.com/Vonage/vonage-dotnet-sdk/commit/e9eef5878ea6206c0a3162d7bc9ccdea333ffc19))


### Documentation

- Add coverage to readme ([bff5803](https://github.com/Vonage/vonage-dotnet-sdk/commit/bff580359a0405049be0bc6c9ee303737a1ae1d2))

- Update upcoming breaking changes in next major version, with links to PRs ([898c266](https://github.com/Vonage/vonage-dotnet-sdk/commit/898c266febb85819a7a7d55c915fb56e64044e84))

- Update formatting in readme ([ad6faa1](https://github.com/Vonage/vonage-dotnet-sdk/commit/ad6faa17aa279f128cd4b9bf1101f2920303dd3b))


### Features

- Update supported languages in Meetings API UI settings ([85139e0](https://github.com/Vonage/vonage-dotnet-sdk/commit/85139e0973db7813e2c2285f7667618f5f0a8289))

- Implement implicit operator for VerifyV2 languages ([eec73ee](https://github.com/Vonage/vonage-dotnet-sdk/commit/eec73eec8a2d37b4219e162964bc14fd5a449e91))

- Add missing features on Voice capabilities ([d14ec5b](https://github.com/Vonage/vonage-dotnet-sdk/commit/d14ec5b05d409b2bf4cf75a9fe41d1d377d8cd85))

- Add privacy settings to applications ([f2d2ce0](https://github.com/Vonage/vonage-dotnet-sdk/commit/f2d2ce0aa71439a38833a51f039311c72faf3f46))

- Implement BuildVerificationRequest on StartVerificationResponse ([27dfe16](https://github.com/Vonage/vonage-dotnet-sdk/commit/27dfe161e73fb0912934ef42ec7dac385d9a18e7))

- Redirect url on silent auth ([d87b602](https://github.com/Vonage/vonage-dotnet-sdk/commit/d87b6021c6d36fc7aeda484ccba9df6e79a0aa96))

- Use Uri instead of string for RedirectUrl ([b15aac9](https://github.com/Vonage/vonage-dotnet-sdk/commit/b15aac93d48a9305453f96921f3f69bab730971f))


### Pipelines

- Add github action to try generating release packages ([2494e40](https://github.com/Vonage/vonage-dotnet-sdk/commit/2494e40184f50a5c6be490abb0eafd3806b8bd63))

- Remove InternalsVisibleTo in Vonage.Server ([02b2c9e](https://github.com/Vonage/vonage-dotnet-sdk/commit/02b2c9e3f917bec1ed35404574fcb0d20562b4e0))

- Parallelize signed and unsigned builds ([5f08d58](https://github.com/Vonage/vonage-dotnet-sdk/commit/5f08d58305b2389923f28c82788a3f32bd9b2ebe))


### Refactoring

- Update all dependencies ([1e49817](https://github.com/Vonage/vonage-dotnet-sdk/commit/1e498178eab825b99be48a731e58b74bf40d2991))

- Simplify StartVerificationRequest process by removing generics ([c0f10a8](https://github.com/Vonage/vonage-dotnet-sdk/commit/c0f10a87fc6f42a77fb63f81be56df9b556df87b))

- Enable bindings redirect (#551) ([a5366bf](https://github.com/Vonage/vonage-dotnet-sdk/commit/a5366bf418133691d5f86b8d35d5ab56080b53fc))

- Enable bindings redirect (#552) ([feb99ee](https://github.com/Vonage/vonage-dotnet-sdk/commit/feb99ee091f1afa331721cd9228ae325a4ec08a3))

- Prevent exceptions in monads ([c26cdbb](https://github.com/Vonage/vonage-dotnet-sdk/commit/c26cdbb7ac6751f2fe251c4cdcc12782435306bb))

- Update dependencies with net8.0 release ([a7763cf](https://github.com/Vonage/vonage-dotnet-sdk/commit/a7763cf83fcb31d64cb389324431c0e7c816c40d))

- Remove dependency towards Microsoft.AspNetCore.WebUtilities ([0e38b86](https://github.com/Vonage/vonage-dotnet-sdk/commit/0e38b86109f60e6bcdfb7084714ef6ee97248c36))

- Clean VerifyV2 tests & mutants ([e8407e6](https://github.com/Vonage/vonage-dotnet-sdk/commit/e8407e615a7f5b9e69090e2a2be048311ba4cf75))

- Simplify test setup with private key ([e080884](https://github.com/Vonage/vonage-dotnet-sdk/commit/e080884ca45bb7f516c270a566bd97fc857131ec))

- Remove unnecessary dependency for Vonage.Server ([ec921cb](https://github.com/Vonage/vonage-dotnet-sdk/commit/ec921cb7e3c6da88298ad8dd0ef5dd8992c14af2))


### Releases

- Bump version to v6.12.1 ([b271f1c](https://github.com/Vonage/vonage-dotnet-sdk/commit/b271f1cfcd2515f7d7b1676a9dc886e313fff682))

- Bump video beta to v7.0.5-beta ([2600ceb](https://github.com/Vonage/vonage-dotnet-sdk/commit/2600cebd08911f3b1146e3fed5ad69fefeee47dc))

- Bump Vonage.Server version to v7.0.5-beta ([b3c44a6](https://github.com/Vonage/vonage-dotnet-sdk/commit/b3c44a6c5465269308a51b54b885b2e571fdc31d))


## [v6.12.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v6.12.0) (2023-10-23)

### Bug Fixes

- Read feature on MaybeJsonConverter (#539) ([e0f4b43](https://github.com/Vonage/vonage-dotnet-sdk/commit/e0f4b43da41db3e9cee571d31279b03d7e52b1b7))

- Remove stringified booleans (#544) ([972c624](https://github.com/Vonage/vonage-dotnet-sdk/commit/972c624ee4e6949dd7d14a6933279f6fab9854c1))


### Documentation

- Detail upcoming major v7.0.0 release ([ba41a7e](https://github.com/Vonage/vonage-dotnet-sdk/commit/ba41a7ed42da5567c51011ef1ffec64980fdfcbe))

- Update code of conduct ([7ca9a2a](https://github.com/Vonage/vonage-dotnet-sdk/commit/7ca9a2a99f298b55181bf16c7362c9862b04817a))

- Update contribution guide ([b8b2524](https://github.com/Vonage/vonage-dotnet-sdk/commit/b8b252432e6210e2694f8c6e28fcb644f100e01b))

- Update issue templates ([59fe7c2](https://github.com/Vonage/vonage-dotnet-sdk/commit/59fe7c2ece3bf2f353d0e9b28749aed9471a17fc))

- Update issue templates ([a2be030](https://github.com/Vonage/vonage-dotnet-sdk/commit/a2be030ff47e402c4d428eca6f5c066f1c52dcfc))

- Add pull request template ([07ed037](https://github.com/Vonage/vonage-dotnet-sdk/commit/07ed037cfa7cc6c4be2934ae42748a13ec9940c2))


### Features

- Meetings capabilties in Application API (#537) ([efb342e](https://github.com/Vonage/vonage-dotnet-sdk/commit/efb342e4b9f2338c95eec91193893f76aeb4381e))

- Support check_url on VerifyV2 silent auth (#540) ([8b2d3f9](https://github.com/Vonage/vonage-dotnet-sdk/commit/8b2d3f9c41db059f6db15acb12e244afcf3a5797))

- Add missing Premium feature on talk ncco (#547) ([1cd33bb](https://github.com/Vonage/vonage-dotnet-sdk/commit/1cd33bb73ba8751aea6d63c053dd685f8037a978))


### Merges

- Merge branch 'main' of https://github.com/Vonage/vonage-dotnet-sdk
 ([74453d9](https://github.com/Vonage/vonage-dotnet-sdk/commit/74453d9098f7d07f9c1faa26ec50fac7907559cb))

- Merge remote-tracking branch 'origin/main'
 ([4a6648d](https://github.com/Vonage/vonage-dotnet-sdk/commit/4a6648dec6f9c5f32cb6052c05cfb9889e474c16))


### Other

- Adapt methods visibility (#543) ([102682f](https://github.com/Vonage/vonage-dotnet-sdk/commit/102682f2288e08af1c738088f19b1d793f988440))


### Refactoring

- Make sync-only methods obsolete (#538) ([1b96b6b](https://github.com/Vonage/vonage-dotnet-sdk/commit/1b96b6b5b1119cfb8995d2b84cc34fc15ef794d4))

- Remove duplicate in parametrized test ([5a946f1](https://github.com/Vonage/vonage-dotnet-sdk/commit/5a946f165de58b3d13fa52780cdbd49362bad336))

- Remove ExcludeFromCodeCoverage annotation on PemParse - the code is covered through JwtTest ([b896c4d](https://github.com/Vonage/vonage-dotnet-sdk/commit/b896c4de68a4e51fe34036460eb06b927d70a829))

- Delete unnecessary class WebhookTypeDictionaryConverter (#542) ([4dda43f](https://github.com/Vonage/vonage-dotnet-sdk/commit/4dda43fe8d04bab934d64a8ed84dc365500b7959))

- Fix typos and clean WebhookParser ([024ea99](https://github.com/Vonage/vonage-dotnet-sdk/commit/024ea9954630f9acfec2a5299256a7d8c6bd1bbb))

- Reduce duplication in SubAccountsClient (#545) ([929b331](https://github.com/Vonage/vonage-dotnet-sdk/commit/929b331fec563c6d764efef31f81558a0aad9bea))

- Reduce duplication in ResultAssertions ([75bb036](https://github.com/Vonage/vonage-dotnet-sdk/commit/75bb036bd7e6c51ca7b68f96091e8f43f1f77eaa))

- Reduce duplication in ProactiveConnect UpdateList (#546) ([9e51219](https://github.com/Vonage/vonage-dotnet-sdk/commit/9e5121929ac9a584e2db3c2f28932e9bda590ac0))

- Reduce duplication in Sip InitiateCall tests ([b4acd2c](https://github.com/Vonage/vonage-dotnet-sdk/commit/b4acd2ce04faed470a6888427fe901760c37e064))

- Reduce duplication in VerifyV2 tests ([29b5a84](https://github.com/Vonage/vonage-dotnet-sdk/commit/29b5a84ffd28a2c9dbcc8273ea89264cd5947bff))

- Reduce duplication in VerifyV2 tests ([dcf0ae9](https://github.com/Vonage/vonage-dotnet-sdk/commit/dcf0ae96758845dfa6fb500ad134212b0e3d1a3e))

- Remove unused field ([4f6d0d3](https://github.com/Vonage/vonage-dotnet-sdk/commit/4f6d0d3112edfbc91d62a651b8fcc53064ef99e0))


### Releases

- Bump version to v6.12.0 ([f8f9bbd](https://github.com/Vonage/vonage-dotnet-sdk/commit/f8f9bbd5f903af59fda7a58d1a6f4a6a145020ee))


## [v6.11.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v6.11.0) (2023-10-13)

### Documentation

- Update changelog with v6.10.1 ([a550a7b](https://github.com/Vonage/vonage-dotnet-sdk/commit/a550a7b516cc4b88231a82db5786fc7e79550527))

- Add documentation on RequestTimeout configuration in readme ([bba0442](https://github.com/Vonage/vonage-dotnet-sdk/commit/bba04427d8b4b7bd64298e87ba4c6fd9909b1d05))

- Add v6.11.0 in changelog ([c496cb1](https://github.com/Vonage/vonage-dotnet-sdk/commit/c496cb1f6883d1f2cbf0cde46c06a2e297cedf38))


### Features

- Enable custom timeout on HttpClient (#534) ([e9f75d8](https://github.com/Vonage/vonage-dotnet-sdk/commit/e9f75d8507aaf8cd3e706173db8878c087357533))


### Pipelines

- Add .whitesource configuration file (#536) ([4c295bd](https://github.com/Vonage/vonage-dotnet-sdk/commit/4c295bd1014487a82e5f121ebcc9b2284a68067c))


### Refactoring

- Transform httpclient timeout into failure (#535) ([4856bb0](https://github.com/Vonage/vonage-dotnet-sdk/commit/4856bb0627a8e282dfb311cdb9e24bba31cd8a2e))

- Adapt configuration key for request timeout ([975e732](https://github.com/Vonage/vonage-dotnet-sdk/commit/975e732954f8586daa17204ea73df9daec33a0cc))


### Releases

- Bump version to v6.11.0 ([987eebc](https://github.com/Vonage/vonage-dotnet-sdk/commit/987eebc1d356edf8487cd394c1d0542560c08dfa))


## [v6.10.1](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v6.10.1) (2023-10-10)

### Bug Fixes

- Application deserialization with Meetings custom webhooks (#532) ([9166a02](https://github.com/Vonage/vonage-dotnet-sdk/commit/9166a02314e0c96175bc139d7095bcde8bef30d5))


### Documentation

- Update changelog after v6.10.0 ([37c429b](https://github.com/Vonage/vonage-dotnet-sdk/commit/37c429bc4c90c7c4093ed938d7097352251bbda8))


### Releases

- Bump version to v6.10.1 ([305a1a9](https://github.com/Vonage/vonage-dotnet-sdk/commit/305a1a99da4c7f8553d4bab3d8d994e058ee07aa))


## [v6.10.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v6.10.0) (2023-10-04)

### Documentation

- Update changelog with v6.9.0 (#506) ([1efac61](https://github.com/Vonage/vonage-dotnet-sdk/commit/1efac61f1dd8e344373d39f2833898684ffdaba2))


### Features

- Extend registration with missing clients and token generator (#504) ([9d2a936](https://github.com/Vonage/vonage-dotnet-sdk/commit/9d2a93652003a5b6e7667e8ec11ef00dd7b82442))

- Add ja-jp locale to VerifyV2 (#530) ([c35c11a](https://github.com/Vonage/vonage-dotnet-sdk/commit/c35c11ad2511d9f19666f772a3859dc5e570b99b))

- Add feature to verify Jwt signature (#531) ([bc4cb4a](https://github.com/Vonage/vonage-dotnet-sdk/commit/bc4cb4a3c9d94d2c191795cba0bfba1a1cddc4bd))


### Pipelines

- Remove old worksflow 'publish-nuget' (#505) ([8d7452f](https://github.com/Vonage/vonage-dotnet-sdk/commit/8d7452fd79aaed603cdde66a80997266b834c315))

- Mutation testing improvement (#507) ([9f4dd1e](https://github.com/Vonage/vonage-dotnet-sdk/commit/9f4dd1ef0ac158f74c30fa1dc4a5a9270b777b11))

- Upgrade actions/checkout to v3 for mutation testing (#508) ([4620ad4](https://github.com/Vonage/vonage-dotnet-sdk/commit/4620ad4aa6255b267a4baf5b1da1462331cbb7eb))

- Fix some build warnings (#525) ([ab9e28d](https://github.com/Vonage/vonage-dotnet-sdk/commit/ab9e28d7c49c4d417c3bae4500ef06147694e919))

- Restore tests parallelism for Vonage.Test.Unit (#526) ([9d5239e](https://github.com/Vonage/vonage-dotnet-sdk/commit/9d5239e40fb7b7d20d91047a3ea1bbc7d3f01b11))


### Refactoring

- Remove unused class ResponseBase (#510) ([6649450](https://github.com/Vonage/vonage-dotnet-sdk/commit/664945078a8eacf980dde7aebb6447b56d83c182))

- Replace TimeSpamSemaphore anf ThrottlingMessageHandler by proper dependency (#511) ([8372d9f](https://github.com/Vonage/vonage-dotnet-sdk/commit/8372d9f8d52e08616caa49f18dcc5ada2315a423))

- Implement GetHashCode for ParsingFailure (#512) ([2bfc4e1](https://github.com/Vonage/vonage-dotnet-sdk/commit/2bfc4e10c6079ec06ec97fe149194b5fabf1c3a6))

- Use a 2048 bits key for Linux and MacOs platforms (#513) ([a89f6b4](https://github.com/Vonage/vonage-dotnet-sdk/commit/a89f6b4259fc73cd8c4020deff9f2d8e474f281c))

- Use sonar.token instead of deprecated sonar.login (#514) ([743f4de](https://github.com/Vonage/vonage-dotnet-sdk/commit/743f4de3ea7b28a21bcf25cff1930d650b61f379))

- Add missing assertion in parser test (#515) ([8a067f6](https://github.com/Vonage/vonage-dotnet-sdk/commit/8a067f6b2d2f287674e11531c3d81d501af5aa2a))

- Align method signatures for async/sync methods on AccountsClient (#516) ([3a6ebcc](https://github.com/Vonage/vonage-dotnet-sdk/commit/3a6ebcc558623076fdef9effa39119d4667891ec))

- Add missing optional parameter for VerifyClient (#517) ([84b5978](https://github.com/Vonage/vonage-dotnet-sdk/commit/84b59785db7ba0c517c575bb5f2d87eac4f328d5))

- Make NonStateException compliant to ISerializable (#518) ([a81aeca](https://github.com/Vonage/vonage-dotnet-sdk/commit/a81aeca170600f6f03fd63be10ffcf6cebe9eeed))

- Simplify ternary operator in Result (#519) ([057071a](https://github.com/Vonage/vonage-dotnet-sdk/commit/057071a7db4e5eb67e55d5c950c3b7987293deb5))

- Update serialization settings (#520) ([f8c18fd](https://github.com/Vonage/vonage-dotnet-sdk/commit/f8c18fd789f19d3732658aef1b5bf640af743b75))

- Remove obsolete method on TestBase (#521) ([5fa633d](https://github.com/Vonage/vonage-dotnet-sdk/commit/5fa633de67cc97e184abea1f59688ee8de497c82))

- Enable parallelized tests on Vonage.Test.Unit (#522) ([0ed7896](https://github.com/Vonage/vonage-dotnet-sdk/commit/0ed7896ffc3c00c109e39ced4e741451cfdf2fb2))

- Update TestBase (#523) ([88a198d](https://github.com/Vonage/vonage-dotnet-sdk/commit/88a198deed03822261efb2e76d49f17f53188730))

- Update sub clients (#524) ([2fb7db6](https://github.com/Vonage/vonage-dotnet-sdk/commit/2fb7db64e9d52b0b1c512214ffc9b323c5df7339))

- Replace null by optional values in ApiRequest (#527) ([dc3498e](https://github.com/Vonage/vonage-dotnet-sdk/commit/dc3498e4bd6b6d1052e4f791aeab8f65487f2fa6))

- Simplify ApiRequest (#529) ([7c5bb7f](https://github.com/Vonage/vonage-dotnet-sdk/commit/7c5bb7f842eb5124c17f0e737984cc6d763349f6))

- Cover signature in query string (#528) ([5d7a481](https://github.com/Vonage/vonage-dotnet-sdk/commit/5d7a481db78166831f5917600ebf22475b3a8250))


### Releases

- Bump version to v6.10.0 ([32f3170](https://github.com/Vonage/vonage-dotnet-sdk/commit/32f31706f23b4f3c0eba5a255dc976fc17df67c0))


## [v6.9.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v6.9.0) (2023-09-06)

### Bug Fixes

- Add basic auth support for Messages (#384) ([8fae4fb](https://github.com/Vonage/vonage-dotnet-sdk/commit/8fae4fbd119159664c930048e3166c0c583c8597))

- Missing dependencies (#393) ([d81a7be](https://github.com/Vonage/vonage-dotnet-sdk/commit/d81a7be180739a75a952ab3fe335273b19ab6c8e))

- Exclude '{' and '}' from non-empty strings in PBT (#397) ([b448195](https://github.com/Vonage/vonage-dotnet-sdk/commit/b448195b54cf285164544b258b55599756d0f888))

- Exclude '{' and '}' from non-empty strings in PBT (#400) ([c437f72](https://github.com/Vonage/vonage-dotnet-sdk/commit/c437f726b83b5d58e60c2d30e143b5ac8a49b701))

- Hiding ProactiveConnectClient (#413) ([95bf903](https://github.com/Vonage/vonage-dotnet-sdk/commit/95bf9033cf0051b5243bcae914edc48e7293f2b0))

- Change requestId to Guid for VerifyCodeRequest (#415) ([647f69b](https://github.com/Vonage/vonage-dotnet-sdk/commit/647f69be0fc354f32191eaaa7935d9024b39d9c3))

- Basic auth encoding (#423) ([c844682](https://github.com/Vonage/vonage-dotnet-sdk/commit/c8446828ee7904aa7734755cb2e0675bef0d29d7))

- SubAccounts implementation (#426) ([1a3644b](https://github.com/Vonage/vonage-dotnet-sdk/commit/1a3644be5327cf5faaff5dfa9b2853806ddf85f5))

- Conversation StartOnEnter (#465) ([808a03b](https://github.com/Vonage/vonage-dotnet-sdk/commit/808a03b7d23fd97650cfd7a0060437212b687648))

- Version substring in release pipeline (#503) ([c11e660](https://github.com/Vonage/vonage-dotnet-sdk/commit/c11e66095c9ccdec51ec710e71c484fac89eb044))


### Build updates

- Packages update (#442) ([94dfcc2](https://github.com/Vonage/vonage-dotnet-sdk/commit/94dfcc2a82256399e95967a805eb02690aa19585))


### Documentation

- Changelog update (#402) ([03d3c23](https://github.com/Vonage/vonage-dotnet-sdk/commit/03d3c230736bdbeb3d987c12ddb1095a7c16c5cc))

- Add changelog for video beta (#403) ([e68eeeb](https://github.com/Vonage/vonage-dotnet-sdk/commit/e68eeeb8b22d86b98caf08d2a4f6638ac978ac42))

- Update changelog ([a26d276](https://github.com/Vonage/vonage-dotnet-sdk/commit/a26d2763ae1c99e1d00344453ec63f64eb529fe5))

- Readme badge fix (#427) ([b0103d9](https://github.com/Vonage/vonage-dotnet-sdk/commit/b0103d9426d19ec9665fb213e3a261f71b5dba78))

- Changelog update (#428) ([1f5277c](https://github.com/Vonage/vonage-dotnet-sdk/commit/1f5277c1ca9695178418ea48184ab81755960eb3))

- Explaining monads in readme (#433) ([cd33b97](https://github.com/Vonage/vonage-dotnet-sdk/commit/cd33b975f94d90bb9a32b27ccbd724ce79da72dd))

- Changelog update (#435) ([8b6bf0b](https://github.com/Vonage/vonage-dotnet-sdk/commit/8b6bf0be0c8f0d6b389dfec6b0fb4338380b0a1b))

- Add service registration in readme (#436) ([434041c](https://github.com/Vonage/vonage-dotnet-sdk/commit/434041c9f38c8b1b6f799f2d9be9c9f2767771c8))

- Update supported apis (#448) ([99ec512](https://github.com/Vonage/vonage-dotnet-sdk/commit/99ec5128d337ec9833eb905bab83cfaa45f0b564))

- Changelog update (#454) ([dc52c71](https://github.com/Vonage/vonage-dotnet-sdk/commit/dc52c714a563237b035437dce206596f76e021ea))

- Changelog update (#466) ([a7bcf50](https://github.com/Vonage/vonage-dotnet-sdk/commit/a7bcf5039534091c74266f455a94e57331a00885))

- Changelog update (#490) ([f1c963c](https://github.com/Vonage/vonage-dotnet-sdk/commit/f1c963cc5ab681a30b94c4ef2f09cace07d03cae))

- Readme update (#499) ([53c906d](https://github.com/Vonage/vonage-dotnet-sdk/commit/53c906d4bbc8b28cbde87eab21bf42292b0c2eba))


### Features

- Add premium to start talk request ([86257e3](https://github.com/Vonage/vonage-dotnet-sdk/commit/86257e33e20235f17cad816ea2b95eb61520872e))

- Webhook classes for messages (#382) ([5e4d741](https://github.com/Vonage/vonage-dotnet-sdk/commit/5e4d741e560e3c702042e57393dd2b13d9e5e0a9))

- Verify V2 (#376) ([5a3828a](https://github.com/Vonage/vonage-dotnet-sdk/commit/5a3828a1c6d8027d199dba329b54219ff5b1ea96))

- Hide Meetings API client until GA (#385) ([8df4179](https://github.com/Vonage/vonage-dotnet-sdk/commit/8df4179ac33b0c377018c547236b93ea1f3bc982))

- Proactive connect - lists (#395) ([18490c2](https://github.com/Vonage/vonage-dotnet-sdk/commit/18490c29cf25654ad69601c2354cb50bd77af88a))

- Add optional claims when generating a token (#398) ([9dc1f4a](https://github.com/Vonage/vonage-dotnet-sdk/commit/9dc1f4a92343db9018c37259e3c8500a7bbc63cc))

- Proactive connect - items (#399) ([4cfae6d](https://github.com/Vonage/vonage-dotnet-sdk/commit/4cfae6d13ced8525ad04c06ece2c4a79b2068b75))

- Proactive connect - events (#401) ([8f97bfe](https://github.com/Vonage/vonage-dotnet-sdk/commit/8f97bfe33dac4260f8c3b2fde3411fcc63d4258b))

- VerifyV2 BYOP (#392) ([692f0bc](https://github.com/Vonage/vonage-dotnet-sdk/commit/692f0bc79f8ddb7bc6d12565f35f60dc25d5089e))

- VerifyV2 Cancel (#407) ([abdd721](https://github.com/Vonage/vonage-dotnet-sdk/commit/abdd721a10860886c3744b15a4d156ba51940273))

- VerifyV2 fraud check (#409) ([0d9e0d4](https://github.com/Vonage/vonage-dotnet-sdk/commit/0d9e0d4460dabc649f670c7457df9b7c338bd61f))

- Voice advanced machine detection (#412) ([95c55be](https://github.com/Vonage/vonage-dotnet-sdk/commit/95c55be7fa61342022877446bb8c444fb09cf25a))

- Implement Match on Result with void return type (#417) ([58975cc](https://github.com/Vonage/vonage-dotnet-sdk/commit/58975cc427547c3146ab5bb06f4cd8fc9a16a58c))

- Dependency injection extension (#418) ([ca9618e](https://github.com/Vonage/vonage-dotnet-sdk/commit/ca9618e6529468ff61bee5a5f05a6849faf525bb))

- Add 4.8.1 and 7.0 in targeted frameworks (#422) ([be77a7b](https://github.com/Vonage/vonage-dotnet-sdk/commit/be77a7b014e0ae116b1f8f644a490fbecf21a168))

- Subaccounts (#431) ([29b03c2](https://github.com/Vonage/vonage-dotnet-sdk/commit/29b03c27cc21f9c3af5b29fff5795110d73a450c))

- Meetings Api (#437) ([e067392](https://github.com/Vonage/vonage-dotnet-sdk/commit/e067392d2ce6aa6fd1fbae1f5678267ca613bf24))

- Add specific object for ApiKey (#443) ([9f908ef](https://github.com/Vonage/vonage-dotnet-sdk/commit/9f908efe1dc32f9b9b0560c13ec0604b96679de4))

- Add Type property on Failures (#446) ([58355a5](https://github.com/Vonage/vonage-dotnet-sdk/commit/58355a58b0c0ecca6bb0d95ffbf3c19fec0bf605))

- Proactive connect (#445) ([44f4431](https://github.com/Vonage/vonage-dotnet-sdk/commit/44f4431eca19ecb8fe19508b66a09da1558957f1))

- Add MeetingsApi and ProactiveConnectApi clients on service injection (#453) ([7b691d5](https://github.com/Vonage/vonage-dotnet-sdk/commit/7b691d59b17e0ce9c268d4d7cb3e7f60dd97b0a7))

- Users API (#479) ([5dd3061](https://github.com/Vonage/vonage-dotnet-sdk/commit/5dd3061f8444fb7653a370d581aaf46bc3c4c019))

- Initialize credentials from Configuration (#491) ([8afc506](https://github.com/Vonage/vonage-dotnet-sdk/commit/8afc506b90faa70459f899b2a13e3c4251b8c04b))

- Add versioning on Meetings API Uri (#497) ([a6a64b7](https://github.com/Vonage/vonage-dotnet-sdk/commit/a6a64b70e3dc61a9cc0fa021a5f15bf27089e2aa))


### Other

- Updated README to show Messages was GA ([33a2417](https://github.com/Vonage/vonage-dotnet-sdk/commit/33a24177ad3ae1f1e2f57444a2a230d0d47fb1cc))

- Add mutation workflow (#293)

* Add github workflows in solution

* Create mutation workflow ([c9cabbf](https://github.com/Vonage/vonage-dotnet-sdk/commit/c9cabbff786fc1df5fbad582bb862d4d06779906))

- Remove specific .net versioning (#294)

Stryker is not compatible with specific version ([0a5a9bf](https://github.com/Vonage/vonage-dotnet-sdk/commit/0a5a9bf7e96763d7ba356ea7572396d2a49194f8))

- Subaccount support (#295)

* Simple subaccount support

* fix comment

* Update interfaces

* Balance and CreditLimit could be null

* Proper auth for number transfer ([47c264c](https://github.com/Vonage/vonage-dotnet-sdk/commit/47c264ce1ae82ca728c83f3a73e3a01a1d9d30d7))

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
Co-authored-by: dependabot[bot] <49699333+dependabot[bot]@users.noreply.github.com> ([f7d64e7](https://github.com/Vonage/vonage-dotnet-sdk/commit/f7d64e712664467c95c59d2cc1c9bd4abc6c66f0))

- Sonarcloud integration (#301)

* Add sonarsource analysis in main pipeline

* Remove non-supported client frameworks, add .Net6 as .net core 3.1 will get out-of-support this month

* Add client framework 4.8 back (removed by IDE) ([7ddbe96](https://github.com/Vonage/vonage-dotnet-sdk/commit/7ddbe96d7228a3338363debf93f8fd4156a30a77))

- DEVX-6785 Framework update (#303)

* Upgrade SDK to netstandard2.0, Upgrade TestProject to everything above 4.6.2, update all libraries

* Update libraries

* Upgrade to C# 10.0 ([00c022b](https://github.com/Vonage/vonage-dotnet-sdk/commit/00c022b14b80cccfe10a785dce2076165e1306c0))

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

* Update test for token generation ([82b65ee](https://github.com/Vonage/vonage-dotnet-sdk/commit/82b65eea57f1eafb56b01243c47f94ac924ab612))

- Add unsafe methods on Maybe & Result (#310)

 ([692a0d0](https://github.com/Vonage/vonage-dotnet-sdk/commit/692a0d0c026ca3f65afbd9eec0fc770344e557bc))

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

* Add factory method for failure, handle empty response differently ([d774754](https://github.com/Vonage/vonage-dotnet-sdk/commit/d77475437a52ebafbda662cac73afe721d11d7d5))

- Add workflow for publishing beta package for Vonage.Video (#312)

 ([cc4fc7a](https://github.com/Vonage/vonage-dotnet-sdk/commit/cc4fc7a3500f159d60be14c2a21860b5b3f072bf))

- Adding SonarCloud badge, removing unused codecov badge (#313)

 ([fd573d7](https://github.com/Vonage/vonage-dotnet-sdk/commit/fd573d7baed4485af93bd6e30b559c6ea5a8fd32))

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

* Remove GetStream.ErrorResponse ([27090f7](https://github.com/Vonage/vonage-dotnet-sdk/commit/27090f7aaf9726b61c9cd3777f5b5ecb3217536b))

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

* Add missing Xml documentation ([071eeca](https://github.com/Vonage/vonage-dotnet-sdk/commit/071eeca50ab360fc58602db0aaea1bd87ff132b7))

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

* Address duplication in InputValidation ([30201ad](https://github.com/Vonage/vonage-dotnet-sdk/commit/30201ad0fdc505ca4418e8db63a7a12ed5b3660a))

- DEVX-6548 | [Video] Moderation (#317)

* Implement DisconnectConnection, more ErrorResponse to Common namespace

* Implement MuteStream

* Implement MuteStreamsRequest

* Implement MuteStreamsUseCase

* Adapt Xml Documentation ([ed03b9f](https://github.com/Vonage/vonage-dotnet-sdk/commit/ed03b9fb259dc637482ca840e50ddadf38f635f4))

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

* Removed conflict from merge ([987f0b5](https://github.com/Vonage/vonage-dotnet-sdk/commit/987f0b58989556b6b6418f3bd9932b4dd4ac7352))

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

* Remove interfaces for use cases ([f63f8c6](https://github.com/Vonage/vonage-dotnet-sdk/commit/f63f8c6dad65973e66dca7ade2a128125ec4348d))

- Modify VerifyResponse to handle new information (#299)

* Add tests to verify deserialization, Add missing property on VerifyResponse

* Add missing package FluentAssertions ([2a85fac](https://github.com/Vonage/vonage-dotnet-sdk/commit/2a85fac7a00f132421613a6587a669d78175928b))

- MapAsync / BindAsync extension methods (#323)

* Implement chainable extension methods for MapAsync and BindAsync on Task<Result<T>>

* Implement IfFailure with default value and function to extract the success more easily ([a04a620](https://github.com/Vonage/vonage-dotnet-sdk/commit/a04a620cb7b11c35f5ff20076f30c759247474ea))

- Rebrand Vonage.Video.Beta into Vonage.Server (#325)

* Rebrand Vonage.Video.Beta into Vonage.Server

* Update project name in nuget pipeline

* Fix helper, fix property order in test ([a9c87ff](https://github.com/Vonage/vonage-dotnet-sdk/commit/a9c87ff2db0e161be36cfe29d438dd067ec38a9d))

- Make nuget pipelines manual as they target different projects, mark main as default branch (#326)

 ([2c3ab4f](https://github.com/Vonage/vonage-dotnet-sdk/commit/2c3ab4f6a76fa5826b114b8f387af668c2d5cc76))

- Update version to 6.0.4 (#327)

 ([98bda78](https://github.com/Vonage/vonage-dotnet-sdk/commit/98bda78eb4f5de2e6843b44bcdd73940ceafa755))

- Remove condition when configuration is not ReleaseSigned (#328)

 ([07c9321](https://github.com/Vonage/vonage-dotnet-sdk/commit/07c9321654ba9fabf9af571c190c83885655087d))

- Nuget release automation (#329)

* Setup two automated jobs based on branch name

* Fix path name for beta

* Delete outdated releases

* Downgrade version to 7.0.0-beta

* Remove tag assembly version

* Fix Vonage.Server version ([26b0cbc](https://github.com/Vonage/vonage-dotnet-sdk/commit/26b0cbc07f2ac63764320401e68df534d0d2cab3))

- Fix nuget workflow, update Vonage.Server config (#331)

 ([9933936](https://github.com/Vonage/vonage-dotnet-sdk/commit/993393659aa175ca6732df112a15374acac79176))

- Create Vonage.Common project (#332)

* Create Vonage.Common library

* Remove unused changelog

* Update readme file ([bfcc929](https://github.com/Vonage/vonage-dotnet-sdk/commit/bfcc92914811fb41ad73fce598f66bab51f4ad57))

- 'Bumping Vonage.Server version to 7.0.1-beta' (#333)

Co-authored-by: NexmoDev <44278943+NexmoDev@users.noreply.github.com> ([87296e3](https://github.com/Vonage/vonage-dotnet-sdk/commit/87296e3ca5b02b0f6a2647f3ca82967dfddcd28f))

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

* Replacing true/false by on/off for microphone state (spec were wrong) ([ac5ea50](https://github.com/Vonage/vonage-dotnet-sdk/commit/ac5ea50b8b344f19575330f6518e7f2637e62750))

- Refactoring on *.Test (#336)

* Refactor Property-Based Testing to reduce duplication

* Reduce duplication when verifying response cannot be parsed

* Reduce duplication when testing the success scenario

* Remove netcoreapp3.1 from Vonage.Test.Unit ([dcb6ce9](https://github.com/Vonage/vonage-dotnet-sdk/commit/dcb6ce9078fb89c1093f409ef7b6e0c959dc9bbf))

- Use builder for HttpRequestMessage (#337)

 ([b92d2b7](https://github.com/Vonage/vonage-dotnet-sdk/commit/b92d2b7a89b59a4b446d9bb531754688a76adb74))

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

* Add missing XML Doc ([98c9800](https://github.com/Vonage/vonage-dotnet-sdk/commit/98c98002455c7c889fabc28718fe28234cda48d8))

- Add Polysharp, update C# to latest version (#340)

 ([dc03b7d](https://github.com/Vonage/vonage-dotnet-sdk/commit/dc03b7de823501632d855525c59d8dfc1272d01b))

- Pipeline updates (#342)

* Focus main build on .net6.0 to improve feedback loop

* Add separate pipeline to test all frameworks on push

* Forces build on .net6.0, package restore on build

* Defining build version to netstandard2.0

* Add .netstandard2.0 to test projects

* Remove specific framework on build ([d56e0ee](https://github.com/Vonage/vonage-dotnet-sdk/commit/d56e0ee68974e697005e103632cd8b572273c578))

- Refactoring on use cases (#341)

* Refactor client & request instantiation

* Remove unnecessary parameters and fields

* Remove specific use cases, use vonage client for generic purpose ([d950232](https://github.com/Vonage/vonage-dotnet-sdk/commit/d950232e54ed8ca98ec07f7432df3a9a6b060271))

- Sets up the user-agent in HttpClient (#347)

* Add user agent from credentials to vonage client

* Fight primitive obsession on http client options ([d8de61a](https://github.com/Vonage/vonage-dotnet-sdk/commit/d8de61a68c6ee18641b4be6b1da1293defdf4321))

- Use configuration for Video and Meetings, refactor Configuration (#349)

 ([4bd1dd3](https://github.com/Vonage/vonage-dotnet-sdk/commit/4bd1dd3178f53f9074702c8e9040a11de27cef91))

- Fix multiframework build (#350)

 ([4b4d215](https://github.com/Vonage/vonage-dotnet-sdk/commit/4b4d2152c155ddd7b6fb236c10366d30ae67b09f))

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

* Use BinaryContent for file in UploadLogo (inject IFileSystem, improve declarative writing on use case) ([fc7b373](https://github.com/Vonage/vonage-dotnet-sdk/commit/fc7b373266532d00ce39d6c9b584748188a7b027))

- Video refactoring (#352)

* Create builder for AddStreamRequest

* Create builder for GetArchivesRequest

* Use Guid for UUID values instead of string

* Simplify builder tests

* Create builder for CreateArchiveRequest ([49c4175](https://github.com/Vonage/vonage-dotnet-sdk/commit/49c41753b2da4799f7374d19385c8ee7ecd9df26))

- Integration testing (#353)

* Add meetings capability to Application

* Add base integration tests, modify pipelines to use environment variables and log information

* Remove appsettings from project

* Reorder Application/ApplicationCapabilities, make appsettings.json optional in integration tests

* Fix ordering in applications, use values from environment variables (with Test Runner)

* Amend Readme with integration tests configuration

* Remove logger verbosity from build ([86d020f](https://github.com/Vonage/vonage-dotnet-sdk/commit/86d020fcd40467021d96d03bbcdacf0f8e6904a4))

- Add missing environment variables on pipeline (#354)

 ([615029d](https://github.com/Vonage/vonage-dotnet-sdk/commit/615029de3f285eb93c257b5c59cb84d4cb869301))

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

* Replace SipHeader by dictionary ([6e5506e](https://github.com/Vonage/vonage-dotnet-sdk/commit/6e5506e947f110fa0f369a002529e7e88f6f15ac))

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

* Fix broadcast layout creation ([e203bfc](https://github.com/Vonage/vonage-dotnet-sdk/commit/e203bfc4ee8f2e1d9bcff6acfc190d4281226958))

- Package update (#358)

 ([824edcc](https://github.com/Vonage/vonage-dotnet-sdk/commit/824edcc22f7eb268216381eb6164f4e77e3f130d))

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

* Remove unused members ([ef9928a](https://github.com/Vonage/vonage-dotnet-sdk/commit/ef9928a304af3459bcb99c3ad7f0d7c3caa520a4))

- Remove duplication following code health degradation (#361)

 ([96f63a6](https://github.com/Vonage/vonage-dotnet-sdk/commit/96f63a625b5ec554a1948c01e0810a7560cd5a16))

- Split Messages tests under separate categories (#363)

* Clean code smells in MessagesTests

* Split MessagesTests into several sub-sections (SMS, MMS, WhatsApp, etc). ([51792d5](https://github.com/Vonage/vonage-dotnet-sdk/commit/51792d570e4338e369f8addb9d0a579a61f978b6))

- Use System.Text.Json instead of Newtonsoft, use fixed ordering on serialization to allow file reordering while cleaning (#366)

 ([0825fb0](https://github.com/Vonage/vonage-dotnet-sdk/commit/0825fb0509e92cf428ac0e2ebdb8e9bad56dcbcb))

- Simplify client constructors by using ClientConfiguration only (#367)

 ([5fb3c51](https://github.com/Vonage/vonage-dotnet-sdk/commit/5fb3c5162c39af040a481c8acc5d5d7b66faf3f1))

- Improve IResultFailures (#368)

* Allow failures to throw exceptions

* Use factory method to create AuthenticationException based on scenarios

* Normalize custom exceptions in 'legacy' code ([22c614b](https://github.com/Vonage/vonage-dotnet-sdk/commit/22c614b693a52af92d8073d92c4e077f3cbef4d2))

- [DEVX-6796] Remove deprecated message types (wappush, val, vcar) (#362)

* Remove deprecated message types (wappush, val, vcar)

* Remove additional wappush, vcal and vcard properties

* Reorder properties ([fed26db](https://github.com/Vonage/vonage-dotnet-sdk/commit/fed26dbca3f357797b1ae2eeceb93416c571e900))

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

Co-authored-by: Karl Lingiah <karl@superchilled.co.uk> ([8edb64b](https://github.com/Vonage/vonage-dotnet-sdk/commit/8edb64b9c4f7bd7e1c3a8f2c895e3c44a92fe1b9))

- Force Vonage.Common to be included in dotnet pack (#370)

 ([15ed790](https://github.com/Vonage/vonage-dotnet-sdk/commit/15ed7903d56590419ecdf0a4bfdb8b2a7bae4a97))

- Remove integration testing (not applicable) (#371)

 ([a7fff14](https://github.com/Vonage/vonage-dotnet-sdk/commit/a7fff148d57a4d10a2a2c090c186a355a46e2f5b))

- Improve exceptions details for GetUnsafe methods on monads (#372)

* Add NoneStateException for Maybe<T>

* Use explicit exceptions for GetUnsafe methods on Result

* Fix typos in Xml Docs

* Comply to ISerializable implementation

* Add missing Serializable attribute ([b9e8b38](https://github.com/Vonage/vonage-dotnet-sdk/commit/b9e8b3857ac7f03dd67f3b351a71ba7ce0b2f78d))

- Bump Vonage.Server v7.0.2-beta
 ([bed1b4b](https://github.com/Vonage/vonage-dotnet-sdk/commit/bed1b4b7e08c0d3953160f2e79165b52f0162797))

- [DEVX-7140] Remove hardcoded keys (#373)

* Replace hardcoded RsaPrivateKey by environment variable

* Rename variable

* Remove hardcoded public/private keys

* Amend readme

* Update github actions with environment variable

* Update Readme

* Update Readme ([3c54086](https://github.com/Vonage/vonage-dotnet-sdk/commit/3c54086064050d68418cc9f81a262fdf757b27ce))

- Readme update (#375)

* Fix dead links and badges

* Adapt summary

* Try updated contributors

* Remove contributors ([2fd2256](https://github.com/Vonage/vonage-dotnet-sdk/commit/2fd2256aa536f055b33b6dd5d32844f4376410e6))

- [DEVX-7128] NumbersAPI update (#374)

* Add possibility to exclude credentials from QueryString

* Move ApiKey & ApiSecret in query string for numbers api

* Refactor NumbersTests

* Add missing Xml Docs, refactor query parameters generation ([ea57833](https://github.com/Vonage/vonage-dotnet-sdk/commit/ea57833580b03cf77ffb1164bed6918c817dcf55))

- Unify test class names (#378) ([e31700e](https://github.com/Vonage/vonage-dotnet-sdk/commit/e31700e9fe88d94c643797439eed27f605ca57ae))

- Bump version to v6.3.0
 ([1fb8362](https://github.com/Vonage/vonage-dotnet-sdk/commit/1fb8362a5c8a4474ab846bb283433ba56a266db8))

- Update changelog
 ([fb6c6cc](https://github.com/Vonage/vonage-dotnet-sdk/commit/fb6c6cc30de0c0e2eefe95c324bc1a2a2a3eb810))

- Bump version to v6.3.1
 ([1135897](https://github.com/Vonage/vonage-dotnet-sdk/commit/113589739c5d7b283853151f6aca591e817c3a5f))

- Add editorconfig file
 ([1ec8fce](https://github.com/Vonage/vonage-dotnet-sdk/commit/1ec8fce1053a53579a4f43974d311bac85349483))


### Pipelines

- Bump version to 6.1.0 (#387) ([0d6e98a](https://github.com/Vonage/vonage-dotnet-sdk/commit/0d6e98a8d1117fa78bd1e4c414c9dac55fb11bc4))

- Increase version to v7.0.3-beta (#394) ([980bff4](https://github.com/Vonage/vonage-dotnet-sdk/commit/980bff40a7e514366e79aceb0cc765835b981fb1))

- Update core release script to be usable from main (again) (#405) ([85aaa76](https://github.com/Vonage/vonage-dotnet-sdk/commit/85aaa76e4dca3e7f69d132b4fdd7c12e8b6cf5f7))

- Change negation for coreSDK publish (#408) ([7841ede](https://github.com/Vonage/vonage-dotnet-sdk/commit/7841ede7c49bd9358189e56cebf9a2f8311edec6))

- Fix multiframework pipeline (#425) ([3b56879](https://github.com/Vonage/vonage-dotnet-sdk/commit/3b56879fdd3da8b75c905c3c9e12263d99b821e5))

- Improve performance (#461) ([4e45da2](https://github.com/Vonage/vonage-dotnet-sdk/commit/4e45da2355746c3b4b970cd8c4fd897713e2196b))

- Upgrade & improvements (#462) ([2b5fad7](https://github.com/Vonage/vonage-dotnet-sdk/commit/2b5fad7864c398b703dd796bfe3f5962a7ebaa48))

- Increase java version to 17 (#486) ([ad51973](https://github.com/Vonage/vonage-dotnet-sdk/commit/ad519737239d42b22385f2b01120e5d9a684e8c2))

- Pipelines permissions (#487) ([d37ecc0](https://github.com/Vonage/vonage-dotnet-sdk/commit/d37ecc0ce2622da3cd24e6f37b9789bca07b860f))

- Release (#488) ([97d4503](https://github.com/Vonage/vonage-dotnet-sdk/commit/97d450366077403e3bf63b7bcb59047d5669f975))

- Pipeline permissions (#489) ([556bfba](https://github.com/Vonage/vonage-dotnet-sdk/commit/556bfba8031c92fa134ebe15a0e792a173c189c2))

- Add .editorconfig to solution (#493) ([b7a03c0](https://github.com/Vonage/vonage-dotnet-sdk/commit/b7a03c0821c1947931d695ffc6e69403403f1060))

- Add pre-commit-config (#496) ([5aa6768](https://github.com/Vonage/vonage-dotnet-sdk/commit/5aa676891ea0d46e7484c20f4cd96793c0be739c))

- Release pipeline (#500) ([c91edb3](https://github.com/Vonage/vonage-dotnet-sdk/commit/c91edb3ea397d33cb7451a8e94efcf25432d346f))


### Refactoring

- Extend responses and monads capabilities (#377) ([259aba4](https://github.com/Vonage/vonage-dotnet-sdk/commit/259aba4f4fb1365be26495523019852168ba0e7e))

- Remove duplicate code for sync version of methods (#380) ([96c496c](https://github.com/Vonage/vonage-dotnet-sdk/commit/96c496c20d9f184fc4938f73d10ce30a8f2e0419))

- Warnings cleanup (#381) ([fd3d448](https://github.com/Vonage/vonage-dotnet-sdk/commit/fd3d448ec5d72ef0c8c517883f97aa4277d9272a))

- Move builder on request for VerifyV2 (#386) ([99482c8](https://github.com/Vonage/vonage-dotnet-sdk/commit/99482c8e73eb90bd8be280bda6a1535036a2f3ae))

- Make builders internal (#388) ([a3784a8](https://github.com/Vonage/vonage-dotnet-sdk/commit/a3784a8a71efb9dd5e26688032dc6ecf3e75c2b6))

- Refactor builders (#389) ([a56abd3](https://github.com/Vonage/vonage-dotnet-sdk/commit/a56abd301e090512533d4abbb6a769597dd7623c))

- Make builders internal (#390) ([cf4cadf](https://github.com/Vonage/vonage-dotnet-sdk/commit/cf4cadf24495fdc59448d49e7d2ad9e7487c96b0))

- Throw failure exception on Result<>.GetSuccessUnsafe (#404) ([cf5b654](https://github.com/Vonage/vonage-dotnet-sdk/commit/cf5b6540e15893c9bf5de5ceb31f7807ec7705ef))

- Add test use case interface to facilitate new tests (#406) ([c41414d](https://github.com/Vonage/vonage-dotnet-sdk/commit/c41414d223534164a7c6095e421085719f18835a))

- Improving ApiRequest (#410) ([08dd9de](https://github.com/Vonage/vonage-dotnet-sdk/commit/08dd9dee901186671f09159450ad82ebc0722643))

- Make ApiRequest non-static (#411) ([d4bb72f](https://github.com/Vonage/vonage-dotnet-sdk/commit/d4bb72f8cfd4115051ef56f85fae48169c3974c5))

- Clean voice tests (#414) ([d4d0f86](https://github.com/Vonage/vonage-dotnet-sdk/commit/d4d0f860e60263196de986ff4461506c541b89e9))

- Move AuthenticationHeader creation on Credentials (#429) ([7ba8fd1](https://github.com/Vonage/vonage-dotnet-sdk/commit/7ba8fd1c05b67f1412aea858a271be6d24ef298e))

- Use case enhancement (#430) ([851ceac](https://github.com/Vonage/vonage-dotnet-sdk/commit/851ceac2793e5bfe2408799f26731092e9c98290))

- E2e testing experiment (#438) ([6de5370](https://github.com/Vonage/vonage-dotnet-sdk/commit/6de5370f013bbdf6a5489e89446d154d12da705c))

- Failure extensions (#447) ([bd7828c](https://github.com/Vonage/vonage-dotnet-sdk/commit/bd7828cfedd20654699d9386476dece09f50958b))

- Subaccounts e2e (#455) ([aa2a72f](https://github.com/Vonage/vonage-dotnet-sdk/commit/aa2a72fe744e7b25d0f93740623dd825c5f2d7a8))

- Naming update (#456) ([ce14d49](https://github.com/Vonage/vonage-dotnet-sdk/commit/ce14d4976bcd035bc9cc3b04f217b28765415486))

- Package update (#457) ([a90429a](https://github.com/Vonage/vonage-dotnet-sdk/commit/a90429a356d5aef6c6e8e682761f7976cb4df3d6))

- Proactive connect e2e (#459) ([55c977a](https://github.com/Vonage/vonage-dotnet-sdk/commit/55c977a906380bd8d2247fd1e9ba7e1da42e73dd))

- Meetings Api e2e (#460) ([f202f00](https://github.com/Vonage/vonage-dotnet-sdk/commit/f202f009240714dc4efea5bfca0f5710da0098fa))

- Async result extensions (#470) ([edcd78c](https://github.com/Vonage/vonage-dotnet-sdk/commit/edcd78cee330f7f99cdd69af43327d009a4c1942))

- Test refactoring (#469) ([f2e13a2](https://github.com/Vonage/vonage-dotnet-sdk/commit/f2e13a247e8e4823e03ff08c46f2d9c8c1375dc8))

- Simplify e2e tests (#471) ([3ece2e9](https://github.com/Vonage/vonage-dotnet-sdk/commit/3ece2e957544636d03a47d1e98f9767ea21cbb18))

- Simplify e2e tests (#472) ([0358fe5](https://github.com/Vonage/vonage-dotnet-sdk/commit/0358fe5fe7f148787afe3588aec4e8f4c58be2fb))

- Video e2e refactoring (#473) ([2338cf6](https://github.com/Vonage/vonage-dotnet-sdk/commit/2338cf6c6d810bceb91f04cca80090f991e9c65c))

- Video e2e refactoring (#476) ([460ce9e](https://github.com/Vonage/vonage-dotnet-sdk/commit/460ce9e375430b60dfa5bbd8bd4b5870e450ca43))

- Video e2e refactoring (#477) ([5ece90a](https://github.com/Vonage/vonage-dotnet-sdk/commit/5ece90a2ef5ece544e618967b2f72b7a9809aa46))

- Use case helpers (#478) ([bd22c5e](https://github.com/Vonage/vonage-dotnet-sdk/commit/bd22c5e80bc94302bb4bee346fb1ff420595d63b))

- Update error status codes in PBT for VonageClient (#494) ([3be8f8d](https://github.com/Vonage/vonage-dotnet-sdk/commit/3be8f8db710b3bcd27dc4c55141699903f3ceed2))

- Configuration improvement (#495) ([f42def0](https://github.com/Vonage/vonage-dotnet-sdk/commit/f42def0bec653891bb110dce3101ce1e1593f967))

- Extend regex timeout (#498) ([e40a432](https://github.com/Vonage/vonage-dotnet-sdk/commit/e40a4320d4d8a050424135d6a7446de9a4783713))

- Remove InternalsVisibleTo property (#501) ([8b1eb77](https://github.com/Vonage/vonage-dotnet-sdk/commit/8b1eb7715e55cfd9942483c3526c3f9a6d106f88))


### Releases

- V6.3.2 (#416) ([fa9482e](https://github.com/Vonage/vonage-dotnet-sdk/commit/fa9482efddf769f74f8c1a7ced69cdf0111e0c3b))

- V6.3.3 (#424) ([da9075a](https://github.com/Vonage/vonage-dotnet-sdk/commit/da9075af12da2f6953e3fb46bcb7f65cc7eb7616))

- V6.5.0 (#434) ([8fd419a](https://github.com/Vonage/vonage-dotnet-sdk/commit/8fd419a30d236553acf0381838bef91c8dd95cf9))

- Upgrade version to v6.6.0 (#441) ([c0a6acf](https://github.com/Vonage/vonage-dotnet-sdk/commit/c0a6acf60602cbf16f5008ce57547a8539e958dc))

- V6.7.0 (#444) ([13bf2c3](https://github.com/Vonage/vonage-dotnet-sdk/commit/13bf2c3c3a2a8024d385d60a165b710e4e659770))

- Upgrade version to v6.8.0 (#450) ([702766b](https://github.com/Vonage/vonage-dotnet-sdk/commit/702766bc76089cddeb0373872053f1d7e0ce8650))

- Upgrade version to v7.0.4-beta (#449) ([d786398](https://github.com/Vonage/vonage-dotnet-sdk/commit/d786398f7b9b2c86fc134340f94ac7fc526e3a91))

- Revert "release: upgrade version to v6.8.0" (#452) ([684362d](https://github.com/Vonage/vonage-dotnet-sdk/commit/684362d3f085ebea21efb8b531f170674ca4b85a))

- V6.7.1 (#467) ([b9e925a](https://github.com/Vonage/vonage-dotnet-sdk/commit/b9e925ad3b0fd5b0a1592755099052b45de2f3ba))

- V6.9.0 (#502) ([f5e03e2](https://github.com/Vonage/vonage-dotnet-sdk/commit/f5e03e24012f56fb2e09c6dcc2c263ffeb0f690d))


### Reverts

- Revert "Add editorconfig file"

This reverts commit 1ec8fce1053a53579a4f43974d311bac85349483.
 ([4c56e47](https://github.com/Vonage/vonage-dotnet-sdk/commit/4c56e472eff2d2b9d2fff4669c8ec5404c21f22a))


## [v6.0.2-rc](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v6.0.2-rc) (2022-05-31)

### Merges

- Merge pull request #280 from Vonage/dev

Adding RealTimeData option for AdvancedNumberInsights ([7d56853](https://github.com/Vonage/vonage-dotnet-sdk/commit/7d56853de7492354edcf35d9feef70f0f2c6b4de))


### Other

- Adding RealTimeData option for AdvancedNumberInsights
 ([92bbd88](https://github.com/Vonage/vonage-dotnet-sdk/commit/92bbd8808ae2ecd30ed79cc85e244d355f349aca))

- 'Bumping version to 6.0.2-rc'
 ([4189dab](https://github.com/Vonage/vonage-dotnet-sdk/commit/4189dab236817de8afdf73098cc8842b3cb25908))


## [v6.0.1-rc](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v6.0.1-rc) (2022-05-25)

### Merges

- Merge pull request #278 from Vonage/v6-fixes

Small fixes for v6 ([2579b68](https://github.com/Vonage/vonage-dotnet-sdk/commit/2579b6894217414ebc9d84958fa0fb3f8e1c4f28))

- Merge pull request #279 from Vonage/dev

Dev into Main for release ([5f929a7](https://github.com/Vonage/vonage-dotnet-sdk/commit/5f929a79c09651298905a5cef6b2ddb13cd7cf97))


### Other

- Removing VersionPrefix from project file as to not confuse
 ([c7c3299](https://github.com/Vonage/vonage-dotnet-sdk/commit/c7c329958536081bd43c9bcc1765f515a80049a4))

- Remaning number insight methods to remove confusion
 ([018153e](https://github.com/Vonage/vonage-dotnet-sdk/commit/018153e6c99b96296a6b7efdc610f4d4b0398f43))

- 'Bumping version to 6.0.1-rc'
 ([1239541](https://github.com/Vonage/vonage-dotnet-sdk/commit/1239541cf3a9288c59d6e289f003a3b27ec7e85a))


### Reverts

- Reverting the removal of .ToString on Ncco
Making Serialisation Settings public
 ([c2ceb5f](https://github.com/Vonage/vonage-dotnet-sdk/commit/c2ceb5f8899976d632f76f9bbe614ccdb90f00fb))


## [v6.0.0-rc](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v6.0.0-rc) (2022-05-24)

### Merges

- Merge pull request #268 from Vonage/devx-6030

Devx 6030 - Removing old Nexmo Classes ([f635aa9](https://github.com/Vonage/vonage-dotnet-sdk/commit/f635aa9785e262ac312550517ad76790f73d8991))

- Merge pull request #269 from Vonage/devx-6030

Devx 6030 - Message API ([46e0b9d](https://github.com/Vonage/vonage-dotnet-sdk/commit/46e0b9db845991e451dd95cdbbb033e50bee36f4))

- Merge pull request #270 from Vonage/devx-6030

Messenger and Viber ([9f7d64f](https://github.com/Vonage/vonage-dotnet-sdk/commit/9f7d64fe9492d248a80bcd00522049ac362df7f6))

- Merge pull request #273 from Vonage/devx-6173

devx-6173 ([4633892](https://github.com/Vonage/vonage-dotnet-sdk/commit/4633892d7c80b404a7f4e2abd0a55338f1eebd03))

- Merge pull request #274 from Vonage/devx-6173

Refactoring NCCO Action to have get only property for action type ([90cce0a](https://github.com/Vonage/vonage-dotnet-sdk/commit/90cce0ac82d62db87ce9bbdbb01b77b4c2d852f5))

- Merge pull request #276 from Vonage/devx-6173

Renaming of enums to meet conventions ([1a5ba44](https://github.com/Vonage/vonage-dotnet-sdk/commit/1a5ba44225d045f56776756a04d5cecf6ebfff47))

- Merge pull request #277 from Vonage/dev

Pulling dev into main ready for release ([0ead3a3](https://github.com/Vonage/vonage-dotnet-sdk/commit/0ead3a3a988e57e5409975a42ac8a809fee98a01))


### Other

- Removing legacy nexmo classes
 ([311ce6f](https://github.com/Vonage/vonage-dotnet-sdk/commit/311ce6f478a08bc675e16ae5fe46d7f302cdd4e4))

- Updating to version 6
 ([848e504](https://github.com/Vonage/vonage-dotnet-sdk/commit/848e504b8d8f4c3ec9d20a2595c54babd4b1d74e))

- Setting serialization settings in singular place
 ([93829cc](https://github.com/Vonage/vonage-dotnet-sdk/commit/93829cc05af1a193b5699b54539c0dbf95ba8518))

- Adding SMS, MMS and WhatsApp messages
 ([7befd35](https://github.com/Vonage/vonage-dotnet-sdk/commit/7befd35fab6f697ac67615bd7f7ff273a3cae5de))

- Changing the auth type
 ([8924588](https://github.com/Vonage/vonage-dotnet-sdk/commit/8924588c41d451a52d0aac0fa7da800fcf9e7e7c))

- Changing tests to use bearer auth
Adjusting templates for array of objects not strings
 ([604f7f9](https://github.com/Vonage/vonage-dotnet-sdk/commit/604f7f9e373a6220106913ef1678559bec083fa8))

- Update Vonage.Test.Unit/Data/MessagesTests/SendMmsVcardAsyncReturnsOk-request.json

Co-authored-by: Karl Lingiah <karl@superchilled.co.uk> ([4264700](https://github.com/Vonage/vonage-dotnet-sdk/commit/42647005e9a66ff0510be4a906254d529094e260))

- Update Vonage.Test.Unit/Data/MessagesTests/SendMmsVideoAsyncReturnsOk-request.json

Co-authored-by: Karl Lingiah <karl@superchilled.co.uk> ([c72d822](https://github.com/Vonage/vonage-dotnet-sdk/commit/c72d822d8d9439d6b09a26fe0a6bc17011b1e783))

- Update Vonage.Test.Unit/MessagesTests.cs

Co-authored-by: Karl Lingiah <karl@superchilled.co.uk> ([780fc80](https://github.com/Vonage/vonage-dotnet-sdk/commit/780fc807110120bd70f4dd3d3788948f73574ad0))

- Update Vonage.Test.Unit/MessagesTests.cs

Co-authored-by: Karl Lingiah <karl@superchilled.co.uk> ([3f6c7f4](https://github.com/Vonage/vonage-dotnet-sdk/commit/3f6c7f4ba0a3db1cffa3747f485a9f883d11fa05))

- Fixing merge issues
 ([f12136a](https://github.com/Vonage/vonage-dotnet-sdk/commit/f12136a0e41e791bdd45a4afc74ee4a185290b25))

- Messenger messages
 ([668d5dc](https://github.com/Vonage/vonage-dotnet-sdk/commit/668d5dcfd43d7470b3f4bc2a1e5c871ce566f72b))

- Adding Viber messaging to messagers API
 ([068d986](https://github.com/Vonage/vonage-dotnet-sdk/commit/068d986c904e77836da17184f01a3864f0f0346c))

- Typo in json test file ([e5186a8](https://github.com/Vonage/vonage-dotnet-sdk/commit/e5186a8d533f44b5d3e2725cd8b518bcf1116f72))

- Fxing badly formed xml comment ([80e6bc2](https://github.com/Vonage/vonage-dotnet-sdk/commit/80e6bc2375d439f8fdc2e5ce22b428e8ab377862))

- Fixing issue with unit tests that meant urls were not being checked if there was a request body
 ([70fdc16](https://github.com/Vonage/vonage-dotnet-sdk/commit/70fdc16c8a350b36f3ee6b4f6376b2f69d2e6cf3))

- Refactoring NCCO Action to have get only property for action type
Removal of NccoConverter
Tests that use NCCO refactored
 ([2e56d85](https://github.com/Vonage/vonage-dotnet-sdk/commit/2e56d85c54475636df28c69a73ca329de3161ae4))

- Moving to use bool in code instead of strings
 ([f151ea4](https://github.com/Vonage/vonage-dotnet-sdk/commit/f151ea4ab4f54506ae2ed598d2c59b5d11fbccbb))

- Application capabilities enum refactor
 ([0a8bd54](https://github.com/Vonage/vonage-dotnet-sdk/commit/0a8bd547e3c7b72e33b571983503ccb34dfe5d3e))

- Correcting enum capitalisation for Messaging and Number Insights
 ([7bd3a17](https://github.com/Vonage/vonage-dotnet-sdk/commit/7bd3a17c4dd6ea9f423c246bc48f759726f26963))

- PhoneEndpoint enum rename
 ([a868485](https://github.com/Vonage/vonage-dotnet-sdk/commit/a8684857badb8c8b35b96846508dc2ee70aa1385))

- 'Bumping version to 6.0.0-rc'
 ([71cf431](https://github.com/Vonage/vonage-dotnet-sdk/commit/71cf43114e835e7a5b0da98df28bbed79be4c2bf))


## [v5.10.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v5.10.0) (2022-04-20)

### Merges

- Merge pull request #264 from Vonage/DEVX-5765

Adding Auth exception ([f45294f](https://github.com/Vonage/vonage-dotnet-sdk/commit/f45294fb8335bf2806970b5950d3b3f4403a05a0))

- Merge branch 'dev' into unit-tests ([9cc0bad](https://github.com/Vonage/vonage-dotnet-sdk/commit/9cc0badd3a518d08b032f2306567d2c054452980))

- Merge pull request #265 from Vonage/unit-tests

Unit tests ([eb0bec0](https://github.com/Vonage/vonage-dotnet-sdk/commit/eb0bec055280aeccccf28d5edcfdfed897a8e3e9))

- Merge pull request #266 from Vonage/release-5.10

Preparing for next release and real time data ([857c132](https://github.com/Vonage/vonage-dotnet-sdk/commit/857c132e7c8d3b4ad9229bd1bc41cec68318871d))

- Merge pull request #267 from Vonage/dev

Dev into Main for Release ([cd3c53a](https://github.com/Vonage/vonage-dotnet-sdk/commit/cd3c53aecbd7685f129143be66fedb186a09a14f))


### Other

- Update issue templates

Adding issues templates ([ef3761a](https://github.com/Vonage/vonage-dotnet-sdk/commit/ef3761a69b05f140db9666ae3ba3858abc39891a))

- Merging dev in
 ([a979d42](https://github.com/Vonage/vonage-dotnet-sdk/commit/a979d42ee2d4a9392e1e971d0df36551897ce630))

- Merging dev
 ([2f7fc0c](https://github.com/Vonage/vonage-dotnet-sdk/commit/2f7fc0c087a2f8e7ca2d3f30311525fa83fb2586))

- Adding Auth exception
 ([0dd59ae](https://github.com/Vonage/vonage-dotnet-sdk/commit/0dd59aeb0600b01c4ac40cd8aa69a2b818819bea))

- Refactoring of Messaging tests
 ([ef15833](https://github.com/Vonage/vonage-dotnet-sdk/commit/ef15833363434e850b72f25bd571033365c57f8b))

- Adding dev as PR build trigger
 ([82ef986](https://github.com/Vonage/vonage-dotnet-sdk/commit/82ef986e56cbb4db9bea553c942d7a87097fab2d))

- Adding msbuild setup set to fix .net framework build issue
 ([5241795](https://github.com/Vonage/vonage-dotnet-sdk/commit/524179570062d9e3e9d977975b4696438597b22c))

- Removing 461 from unit tests, 462 is sufficient
 ([eede53c](https://github.com/Vonage/vonage-dotnet-sdk/commit/eede53c70f30274cc7c001d9c963862a6a794d53))

- Removing 461 from nexmo tests
 ([8ebe881](https://github.com/Vonage/vonage-dotnet-sdk/commit/8ebe8817d8bb643e35b9ee2ff08c2e57f731983f))

- Preparing for next release and adding real time data to advanced number insights
 ([3db1a68](https://github.com/Vonage/vonage-dotnet-sdk/commit/3db1a68e1ac956a497b02c17c7cf117993be687c))


## [v5.9.5](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v5.9.5) (2022-01-17)

### Other

- Bumping to version 5.9.5
 ([91b0f5e](https://github.com/Vonage/vonage-dotnet-sdk/commit/91b0f5ec13786c9259e79c9d4dcd95b50609246d))


## [v5.9.4](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v5.9.4) (2022-01-17)

### Merges

- Merge pull request #262 from biancaghiurutan/patch-1

Removed line that breaks the code ([e9623de](https://github.com/Vonage/vonage-dotnet-sdk/commit/e9623de34fb71cb0072d533b3d4b6855082c5d8c))

- Merge pull request #263 from Vonage/devx-1999

Devx 1999 ([189a7fe](https://github.com/Vonage/vonage-dotnet-sdk/commit/189a7fec020d4b0bb687ea23c109eb6c9365256a))


### Other

- Removed line that breaks the code ([13d8d0d](https://github.com/Vonage/vonage-dotnet-sdk/commit/13d8d0d5f599cf3e7d85b5583056af0294eeeaf0))

- Changing biuld action to reflect new main branch name
 ([f7d2199](https://github.com/Vonage/vonage-dotnet-sdk/commit/f7d219981248568b566332342578a945be1ef541))

- Moving test json to json files for maintainability
 ([1734147](https://github.com/Vonage/vonage-dotnet-sdk/commit/1734147785adb9c4d2d801ae76f9b813e2785fa9))

- Adding Type to NCCO Input
 ([4bb9d31](https://github.com/Vonage/vonage-dotnet-sdk/commit/4bb9d31789c30265901425ed68c54925a5921e59))

- CallUpdate Test refactor
 ([b24a252](https://github.com/Vonage/vonage-dotnet-sdk/commit/b24a2525a194c843ed9fdca16ddefc2f7419ec94))

- Fixing build issue
 ([bf2c303](https://github.com/Vonage/vonage-dotnet-sdk/commit/bf2c3034a3b23620cbaa7c3cd5151fe1cd8f223a))

- 'Bumping version to 5.9.4'
 ([d1d9e81](https://github.com/Vonage/vonage-dotnet-sdk/commit/d1d9e819d123abd5b78e8f78306be8e003e35c66))


## [v5.9.3](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v5.9.3) (2021-11-23)

### Merges

- Merge pull request #261 from geekyed/5.x

Fixing an issue caused by the usage of a non thread safe Dictionary. ([da8a1f0](https://github.com/Vonage/vonage-dotnet-sdk/commit/da8a1f040e842d91ab261c0f27ea2fae9494d483))


### Other

- Replace Dictionary with ConcurrentDictionary
 ([2558b4b](https://github.com/Vonage/vonage-dotnet-sdk/commit/2558b4ba9d84b4200cc368e7a8fa670f6f2723c4))

- 'Bumping version to .5.9.3'
 ([8234083](https://github.com/Vonage/vonage-dotnet-sdk/commit/8234083a84ba1699e00e4b8c0cc0d5b9afb4084b))

- 'Bumping version to 5.9.3'
 ([7ab3784](https://github.com/Vonage/vonage-dotnet-sdk/commit/7ab3784a83f12cddae9f1d110993bcd58b847953))


## [v5.9.2](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v5.9.2) (2021-11-04)

### Merges

- Merge pull request #259 from Vonage/not-romaing-bug

Fixing bug with roaming being `not_roaming` and cleaning up some tests ([dfa8be7](https://github.com/Vonage/vonage-dotnet-sdk/commit/dfa8be7fd51af5bf9d7c2b859c5b6b95e5eb3e0d))


### Other

- Adding unit test for Redact and ShortCodes
 ([cb2e0d8](https://github.com/Vonage/vonage-dotnet-sdk/commit/cb2e0d8b63873bf77b8cc24f7b60bab477775574))

- Adding hateos link checks
 ([7ccf0d1](https://github.com/Vonage/vonage-dotnet-sdk/commit/7ccf0d1a6dedce13fbbe11f1c808321c5316709a))

- Moving redaction types to own files
 ([9ecb974](https://github.com/Vonage/vonage-dotnet-sdk/commit/9ecb974af96b1c313031175b99c3c3c4a1774de4))

- Removing PemParser from code coverage
 ([5b6b252](https://github.com/Vonage/vonage-dotnet-sdk/commit/5b6b2523e522742640cd5a1a4ca4d59aa4b2a151))

- MEssaging and Logprovider tests changes
 ([26dd393](https://github.com/Vonage/vonage-dotnet-sdk/commit/26dd393605924f0760df8b846b9c7ade32509091))

- Removing logger tests
 ([5dc09c4](https://github.com/Vonage/vonage-dotnet-sdk/commit/5dc09c467750726929bba9057e7bf48bf1759cc8))

- Fixing bug with roaming being not_roaming and cleaning up some tests
 ([1e73d21](https://github.com/Vonage/vonage-dotnet-sdk/commit/1e73d213d4ee8a6726fa8b4206452e8f5bc63dbf))


## [v5.9.1](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v5.9.1) (2021-10-27)

### Merges

- Merge pull request #256 from Vonage/symbols

Source Linking, Symbols and Deterministic Building ([f010418](https://github.com/Vonage/vonage-dotnet-sdk/commit/f0104183e67e3f7f87675a238c818e46cc35fed0))

- Merge from master
 ([0b08b63](https://github.com/Vonage/vonage-dotnet-sdk/commit/0b08b63270c050b051db4df1ec49a063d927da51))

- Merge pull request #257 from Vonage/strong-name

Strong-name Package ([7a4c3e0](https://github.com/Vonage/vonage-dotnet-sdk/commit/7a4c3e0bb49fcfcb2db58de857c18fdb6a1e20c4))


### Other

- Creating an SNK file and strongly named build configuration
 ([8e937c5](https://github.com/Vonage/vonage-dotnet-sdk/commit/8e937c5ddce8291d6b2f5e27df64e14b5bd6a7f1))

- Changing package id for singed package
 ([a37758c](https://github.com/Vonage/vonage-dotnet-sdk/commit/a37758c84be58f957b984dde19afa7f3f86266f1))

- Changes to nuget publis action to create signed package
 ([f27aed8](https://github.com/Vonage/vonage-dotnet-sdk/commit/f27aed890054cb0369b9e97fc9656334cead7f39))

- Adding switches to create symbol packages
 ([373abf2](https://github.com/Vonage/vonage-dotnet-sdk/commit/373abf266ccb0ba7276999208a4df0c34b7fc3d1))

- Adding source link and some repo details for packaging
 ([e6dbd2b](https://github.com/Vonage/vonage-dotnet-sdk/commit/e6dbd2b88c398e4046b4d72ec166086264d6f076))

- Version bump
 ([afc4e28](https://github.com/Vonage/vonage-dotnet-sdk/commit/afc4e28b6a3bfd2833a6fe8877174158af9d0e78))

- Adding description and assembly version bump
 ([338636e](https://github.com/Vonage/vonage-dotnet-sdk/commit/338636eeb50486aeec05eaaf94f835a7b5480311))

- Package id
 ([d10ae1f](https://github.com/Vonage/vonage-dotnet-sdk/commit/d10ae1f332dcfc698b7248a72645cb4a7a29e328))

- Conditional inclusion of System.Web in Unit Test projects
 ([041e8d6](https://github.com/Vonage/vonage-dotnet-sdk/commit/041e8d665539182bdc7bf2af12d1c6aa0944ef43))

- 'Bumping version to 5.9.1'
 ([bef1e70](https://github.com/Vonage/vonage-dotnet-sdk/commit/bef1e709f7f334eb0cd2a2c524d1899655dd7ef8))


## [v5.9.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v5.9.0) (2021-05-27)

### Merges

- Merge pull request #253 from Vonage/random-number-pools-vapi

adding random from number feature to .NET SDK ([9af7ddf](https://github.com/Vonage/vonage-dotnet-sdk/commit/9af7ddf8e490e16132374f3977761176baa755c7))


### Other

- Adding random from number feature to .NET SDK
 ([bb39173](https://github.com/Vonage/vonage-dotnet-sdk/commit/bb391734ef10f7ceac69f9358882f4b20f444937))

- 'Bumping version to 5.9.0'
 ([6d27ade](https://github.com/Vonage/vonage-dotnet-sdk/commit/6d27ade0079059508a43d2b08d5f7b00867ca661))


## [v5.8.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v5.8.0) (2021-04-28)

### Merges

- Merge pull request #249 from Vonage/updating-5.x

moving readme/GHA to 5.x ([ff9a324](https://github.com/Vonage/vonage-dotnet-sdk/commit/ff9a32489176c34928b37ed12e8910b6de98518d))

- Merge pull request #251 from Vonage/support-ni-roaming-unknown

Support for NI Null values ([4385f37](https://github.com/Vonage/vonage-dotnet-sdk/commit/4385f376267d09df27b183c241ec129546904b23))


### Other

- Moving readme/GHA to 5.x
 ([fbf653e](https://github.com/Vonage/vonage-dotnet-sdk/commit/fbf653e873fa99c0c839e751ea5a6471ccb1e0b6))

- Updating instillation instructions in README ([65ca22f](https://github.com/Vonage/vonage-dotnet-sdk/commit/65ca22f0310f783657e906b6a2b1293ec4bc7a21))

- Added support new NI responses

* Provides support for null fields
* Custom serializer for Roaming
 ([7d7b65e](https://github.com/Vonage/vonage-dotnet-sdk/commit/7d7b65e8e2389c7d456fbf3dfbf82e5c321024a4))

- 'Bumping version to 5.8.0'
 ([75dd2ad](https://github.com/Vonage/vonage-dotnet-sdk/commit/75dd2adc29402bbf105fe53fac6366937c5b01f9))


## [v5.7.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v5.7.0) (2021-03-09)

### Merges

- Merge pull request #248 from Vonage/feature/dlt

adding entity-id and content-id to sms request body ([71440cb](https://github.com/Vonage/vonage-dotnet-sdk/commit/71440cb17ac8cd5f6664c508b2d22cddfa8f716a))

- Merge pull request #247 from Vonage/feature/detail-status-webhooks

adding detail to status-webhooks ([9266a31](https://github.com/Vonage/vonage-dotnet-sdk/commit/9266a31d917094bc75fa9d1b219312d7a98c362d))


### Other

- Adding entity-id and content-id to sms request body
 ([8c3518d](https://github.com/Vonage/vonage-dotnet-sdk/commit/8c3518dc851d7125ecdad6c9fae471b42cc969e7))

- Adding detail to status-webhooks
 ([b7bc802](https://github.com/Vonage/vonage-dotnet-sdk/commit/b7bc80247b149f5ac1d2ed1d11f236552eb11ff9))

- Adding detail enumeration and parser handling
 ([a4e3c48](https://github.com/Vonage/vonage-dotnet-sdk/commit/a4e3c48cfcdbadf1feed6ce799c6630a22919035))

- 'Bumping version to 5.7.0'
 ([da30e6b](https://github.com/Vonage/vonage-dotnet-sdk/commit/da30e6b56dbfa2f5de7f67244a06486d138c6033))


## [v5.6.5](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v5.6.5) (2021-02-22)

### Merges

- Merge pull request #246 from Vonage/bugfix/record-channels-serialization

setting Channels parameter to nullable ([9a59f73](https://github.com/Vonage/vonage-dotnet-sdk/commit/9a59f738da552ade3f9df829bb7e55d6d0a9b217))


### Other

- Setting Channels parameter to nullable
 ([8ad19ca](https://github.com/Vonage/vonage-dotnet-sdk/commit/8ad19ca4744bd7ae8f49b73c326976025fbd537d))

- Adding clean/clear bit to GHA to clear out nuget cache
 ([c887576](https://github.com/Vonage/vonage-dotnet-sdk/commit/c887576a1532837cd9deceb2491dbf81405f299b))

- 'Bumping version to 5.6.5'
 ([35ce2a8](https://github.com/Vonage/vonage-dotnet-sdk/commit/35ce2a8a1a4252093a07fb9d7e293e56d01278e0))


## [v5.6.4](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v5.6.4) (2021-02-03)

### Bug Fixes

- Fixing unit test
 ([fd79e0d](https://github.com/Vonage/vonage-dotnet-sdk/commit/fd79e0d5e76d7506c6424dd0968504fac57c6943))


### Merges

- Merge pull request #242 from Vonage/fixing_unit_test

fixing unit test ([159dcd9](https://github.com/Vonage/vonage-dotnet-sdk/commit/159dcd95afd953aa05d2bb0e1d7e4793ca59e97f))

- Merge branch 'master' into syncronous_methods_blazor
 ([bed0e67](https://github.com/Vonage/vonage-dotnet-sdk/commit/bed0e672ff335d7fc3ae8450234b03311da8e16d))

- Merge pull request #243 from Vonage/syncronous_methods_blazor

Fixing issue with sync methods in blazor ([4abe18a](https://github.com/Vonage/vonage-dotnet-sdk/commit/4abe18a4e886f76f68e254f00e235e5d9b08739e))

- Merge pull request #245 from Vonage/github_actions_main

moving nexmo GHA -> main ([3ca85a9](https://github.com/Vonage/vonage-dotnet-sdk/commit/3ca85a9147ed6fe0607881b5be47d5ae9f6844de))


### Other

- Removing GetAwaiter().GetResult() pattern from sync methods
 ([1831275](https://github.com/Vonage/vonage-dotnet-sdk/commit/18312754c0fb9200b53aa86218dd02e145eeb273))

- Adding async unit tests
 ([131bd58](https://github.com/Vonage/vonage-dotnet-sdk/commit/131bd58879d3fbbe5fc2d55ef720897a4e6a58d3))

- Moving nexmo GHA -> main
 ([eab0095](https://github.com/Vonage/vonage-dotnet-sdk/commit/eab0095153b1b06994a8dfb90fa8f7ad238fce03))

- 'Bumping version to 5.6.4'
 ([91b58a7](https://github.com/Vonage/vonage-dotnet-sdk/commit/91b58a784939c0cf96164647f06990389f96f83a))


## [v5.6.3](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v5.6.3) (2021-01-19)

### Merges

- Merge pull request #240 from Vonage/DLR_status_ignore

Ignoring status for Vonage.Messaging.DeliveryReceipt. ([d0a1fe1](https://github.com/Vonage/vonage-dotnet-sdk/commit/d0a1fe1d0e7680ed952221f5414ebc540ff24e0c))


### Other

- Ignoring status for Vonage.Messaging.DeliveryReceipt.
 ([54f1bcf](https://github.com/Vonage/vonage-dotnet-sdk/commit/54f1bcf0a9f313d4c2fbbcdeff7f5b647e5e9ac1))

- 'Bumping version to 5.6.3'
 ([ae5816b](https://github.com/Vonage/vonage-dotnet-sdk/commit/ae5816baf746a4054d28cac78e1ee5cdf5cf5b20))


## [v5.6.2](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v5.6.2) (2021-01-15)

### Other

- Adding output path ([34c0f81](https://github.com/Vonage/vonage-dotnet-sdk/commit/34c0f8174596fbd657ded8b4dde2a23ec69975c5))

- 'Bumping version to 5.6.2'
 ([515ae01](https://github.com/Vonage/vonage-dotnet-sdk/commit/515ae0175df62c61af93b969f2b4e9e59a886dfe))


## [v5.6.1](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v5.6.1) (2021-01-15)

### Other

- 'Bumping version to 5.6.1'
 ([2787abf](https://github.com/Vonage/vonage-dotnet-sdk/commit/2787abf6db04b7c953b519631a008d7e8985f481))


## [v5.6.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v5.6.0) (2021-01-15)

### Bug Fixes

- Fixing csproj name ([30810d5](https://github.com/Vonage/vonage-dotnet-sdk/commit/30810d50add6b81ab00d0b13df86176d4b880a73))


### Merges

- Merge pull request #239 from Cereal-Killa/master

Enhancements to the ISmsClient ([7b42193](https://github.com/Vonage/vonage-dotnet-sdk/commit/7b4219304425661eda0ad551af1b422a53b754a7))

- Merge pull request #241 from Vonage/auto_release_nuget

adding Nuget release workflow - removing nuspec file. ([58e89aa](https://github.com/Vonage/vonage-dotnet-sdk/commit/58e89aa024a5c18ccde4885fcd09d0a50187add1))


### Other

- Enhancements to the ISmsClient
 ([d7d1c2e](https://github.com/Vonage/vonage-dotnet-sdk/commit/d7d1c2e8af69d285723b44752c5b29a82807fa07))

- Fixup for sms type.
 ([d0143e5](https://github.com/Vonage/vonage-dotnet-sdk/commit/d0143e53b9318e92480ed24b9205ed7e9550292a))

- Simplified names.
 ([e72a406](https://github.com/Vonage/vonage-dotnet-sdk/commit/e72a40690ba37a1c234bdf993d60193bfd0a3054))

- Adding Nuget release workflow - removing nuspec file.
 ([5d66cc7](https://github.com/Vonage/vonage-dotnet-sdk/commit/5d66cc794489d267c0228062d4a05083dc7f1ff2))


## [v5.5.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v5.5.0) (2020-11-19)

### Bug Fixes

- Fixing unit tests
 ([c487097](https://github.com/Vonage/vonage-dotnet-sdk/commit/c487097bb76f7dc5dfb8288798f950b965786cec))


### Merges

- Merge branch 'master' into feature/voice_language_style ([9eea3a7](https://github.com/Vonage/vonage-dotnet-sdk/commit/9eea3a79b4b13e4b67efed9cd033103f5725ab67))

- Merge pull request #232 from Vonage/feature/voice_language_style

Adding Language and style, marking VoiceName as obsolete ([9e006b5](https://github.com/Vonage/vonage-dotnet-sdk/commit/9e006b5330e47adddd10b60b949e56c70518b004))


### Other

- Revving nuspec version
 ([8f6bdb3](https://github.com/Vonage/vonage-dotnet-sdk/commit/8f6bdb3ce90f76c46f769c3f1b6d84cd4dc08681))


## [v5.4.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v5.4.0) (2020-11-12)

### Merges

- Merge pull request #220 from Vonage/readme-updates

adding compatibility list, fixing nuspec ([69a6e1b](https://github.com/Vonage/vonage-dotnet-sdk/commit/69a6e1bc021b88d0fc9a7b4b7c78972d35e0932f))

- Merge pull request #228 from onpoc/master

query null check issue ([3a33a88](https://github.com/Vonage/vonage-dotnet-sdk/commit/3a33a885c51229fef3204253f79c59a1cf8034be))

- Merge pull request #227 from kzuri/master

Correcting enumeration of workflow ([b1952bb](https://github.com/Vonage/vonage-dotnet-sdk/commit/b1952bbdcdbcc48c830b86eca2e8c266ebf1573e))

- Merge pull request #225 from perry-contribs/master

Typo fix - Update README.md ([15722e9](https://github.com/Vonage/vonage-dotnet-sdk/commit/15722e97697631f02e42e74d791c604f2114f48c))

- Merge pull request #229 from Vonage/async_faq

Adding FAQ Section - one question for async at the moment. ([b445e0c](https://github.com/Vonage/vonage-dotnet-sdk/commit/b445e0cc40d7d79fac7c605b2bcb7544067a52c3))

- Merge pull request #230 from smikis/#126-async-implementation

Async calls implementation ([c0b4f7c](https://github.com/Vonage/vonage-dotnet-sdk/commit/c0b4f7c5a42af2b61b1c10f23d947c1f97800ef8))

- Merge branch 'master' of https://github.com/Vonage/vonage-dotnet-sdk into master
 ([71b2d8f](https://github.com/Vonage/vonage-dotnet-sdk/commit/71b2d8fd536db5c18a19ac2caf901baabda77c85))

- Merge pull request #231 from gagandeepp/master

Issue 1221 Fix ([21d8974](https://github.com/Vonage/vonage-dotnet-sdk/commit/21d897470a2bbcb4658856556a6fcea3cd8f7d36))

- Merge pull request #238 from Vonage/readying_for_release

Readying for 5.4.0 release. ([1d75720](https://github.com/Vonage/vonage-dotnet-sdk/commit/1d75720d09a5394410f788564501ea09413b9d24))


### Other

- Updating NuGet badge ([af1d91e](https://github.com/Vonage/vonage-dotnet-sdk/commit/af1d91e31c0f4736c0c2a063ef78a101882f45a1))

- Adding compatibility list, fixing nuspec
 ([788dd1c](https://github.com/Vonage/vonage-dotnet-sdk/commit/788dd1c9115c02af62171aed87a8e09fb631be2e))

- Query null check issue

query null check issue to InboundSms.cs ([38f2a79](https://github.com/Vonage/vonage-dotnet-sdk/commit/38f2a79c38721741f6bf5537a1fec19c3b112ff7))

- Correcting enumeration ([d3f3870](https://github.com/Vonage/vonage-dotnet-sdk/commit/d3f3870abf1bc63898fe7d3724a5b46ec642fe49))

- Correcting enumeration ([7b2c688](https://github.com/Vonage/vonage-dotnet-sdk/commit/7b2c688c8500c7cef3bb7ac8328a6c70726ec209))

- Correcting enumeration ([053613b](https://github.com/Vonage/vonage-dotnet-sdk/commit/053613b45bac55418dd8071a8fc5f591be492c90))

- Correcting enumeration ([861b8e8](https://github.com/Vonage/vonage-dotnet-sdk/commit/861b8e8191f8ead573d2ff844d340d5cdb84673e))

- Typo fix - Update README.md

fixed spelling of word 'globally'. ([ebe45b3](https://github.com/Vonage/vonage-dotnet-sdk/commit/ebe45b323e4d4d0c2eba52c87d28939166313930))

- Adding FAQ Section - one question for async at the moment.
 ([24d64c2](https://github.com/Vonage/vonage-dotnet-sdk/commit/24d64c210b72dc29bed1d4be95b84c6826d3893e))

- Adding Language and style, marking VoiceName as obsolete, also removing some unnecessary usings
 ([6070d98](https://github.com/Vonage/vonage-dotnet-sdk/commit/6070d986a4a1a00d245ff06124df21fff6026da8))

- Issue 1221 Fix
 ([32537f7](https://github.com/Vonage/vonage-dotnet-sdk/commit/32537f70ca4c2a20cf63afa344e023bd1d5f5efa))

- Review Pointer : Method signature fixes
 ([bced564](https://github.com/Vonage/vonage-dotnet-sdk/commit/bced564eb2fd3d0579da0b4646eebb53d5d1331c))

- Method implmentation Added
 ([cc21629](https://github.com/Vonage/vonage-dotnet-sdk/commit/cc21629fe2b2bfed33cff464b85e1cc8d2264bf7))

- Method implementation added
 ([dd7a51d](https://github.com/Vonage/vonage-dotnet-sdk/commit/dd7a51d5162f20dc8850232be02f6f397f4a04f2))

- Unit test needs to be fixed
 ([30a0c5a](https://github.com/Vonage/vonage-dotnet-sdk/commit/30a0c5ad8f2a1dab0ae812f12bea976ee00f8d41))

- Code Review pointer added
 ([535b219](https://github.com/Vonage/vonage-dotnet-sdk/commit/535b2195eaa81b4efc74b01ab4145dcc9f5fbda3))

- Async calls implementation
 ([cbf2e85](https://github.com/Vonage/vonage-dotnet-sdk/commit/cbf2e850fd4a701d680e9fa375f26671e5676196))

- Add additional methods for non async calls
 ([eeb4d26](https://github.com/Vonage/vonage-dotnet-sdk/commit/eeb4d2672e7678b7bf8fd1590b291071f944f0ca))

- Remove async code from unit tests
 ([ac19ef1](https://github.com/Vonage/vonage-dotnet-sdk/commit/ac19ef1e4eb44dc9156c71276126fc83c1ea92c6))

- #126 Use async stream reading method
 ([cc2d180](https://github.com/Vonage/vonage-dotnet-sdk/commit/cc2d18002c713459c83fdd5c5a1988c29f71c7a5))

- Code Review Pointer Implemented
 ([61fbc63](https://github.com/Vonage/vonage-dotnet-sdk/commit/61fbc63e91a371ac52f8adf65f72922f273c7a8c))

- Pointer Implemented
 ([dde9aa4](https://github.com/Vonage/vonage-dotnet-sdk/commit/dde9aa447ec1329662c839a37aa9df23c9f25abb))

- Fixing package name in readme ([491e57f](https://github.com/Vonage/vonage-dotnet-sdk/commit/491e57f24808217460b9c0deae4ddaa54664f0d6))

- Code Review Pointer Added
 ([5e4356b](https://github.com/Vonage/vonage-dotnet-sdk/commit/5e4356bfa97e19b37d06e0639c344aa3a4ff692c))

- Code Review poinrer added
 ([ad0fe18](https://github.com/Vonage/vonage-dotnet-sdk/commit/ad0fe18a9c138c1b34cdcd753ae8219a93b1b6e0))

- Code review pointers implemented
 ([968f901](https://github.com/Vonage/vonage-dotnet-sdk/commit/968f90138e635a8cf923ad536eac0c65bd198886))

- Code pointer added
 ([dcad93e](https://github.com/Vonage/vonage-dotnet-sdk/commit/dcad93e4fd313c4383f5fa329e7c8a19ab6069a3))

- Code review pointer fixed
 ([219bc62](https://github.com/Vonage/vonage-dotnet-sdk/commit/219bc62433bb72f9c137181937c981aed6a44bb5))

- Build Fixes
 ([92488f4](https://github.com/Vonage/vonage-dotnet-sdk/commit/92488f443d5c2abf202fb3345b73fc0373413771))

- Readying for 5.4.0 release.
 ([c9f402b](https://github.com/Vonage/vonage-dotnet-sdk/commit/c9f402bb3b710cba5941af86e91addbd3516afa8))


## [v5.3.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v5.3.0) (2020-09-02)

### Merges

- Merge branch 'Vonage-rename' of https://github.com/Nexmo/nexmo-dotnet into Vonage-rename
 ([98619b2](https://github.com/Vonage/vonage-dotnet-sdk/commit/98619b2df82829b7c8ca6231d1873bf96f5989b6))

- Merge pull request #215 from Vonage/Vonage-rename

Switching over to Vonage naming ([62c8f1b](https://github.com/Vonage/vonage-dotnet-sdk/commit/62c8f1b7d58f18fa64a296ecf529f6c9586e63e1))

- Merge pull request #217 from Vonage/fix-action

Fix GHA ([9857b8a](https://github.com/Vonage/vonage-dotnet-sdk/commit/9857b8a564c0f103e7199b6c4bfb634e4cca9ce0))


### Other

- Switching over to vonage naming
 ([0702bf1](https://github.com/Vonage/vonage-dotnet-sdk/commit/0702bf19afdd90fa3faf482a4dda5e8c10fd1069))

- Removing Nexmo directories, trying github action

removing config file, removing redudant internal utility classes
 ([98fc815](https://github.com/Vonage/vonage-dotnet-sdk/commit/98fc815e8998699282e20315eae84f2d7bad534f))

- Removing Nexmo directories, trying github action
 ([ab08add](https://github.com/Vonage/vonage-dotnet-sdk/commit/ab08addfd54465682f258bc4b2ecc86beacd8c82))

- Trying choco to install codecov
 ([fa1efed](https://github.com/Vonage/vonage-dotnet-sdk/commit/fa1efed18bcac6b8876708869d765a95c481f2a9))

- Moving codecov action
 ([fbcbe52](https://github.com/Vonage/vonage-dotnet-sdk/commit/fbcbe527d136277cca3b6c4eb9ae41c62b9b2f90))

- Removing extra codecov step
 ([d362cb4](https://github.com/Vonage/vonage-dotnet-sdk/commit/d362cb4e3a993964b1f87d82c5576eac20ae616f))

- Moving codecov test project
 ([fe85bf7](https://github.com/Vonage/vonage-dotnet-sdk/commit/fe85bf737f6d5b9bb582e354620ef65d52bfbb8d))

- Testing with enviornment variable
 ([92baf0f](https://github.com/Vonage/vonage-dotnet-sdk/commit/92baf0fccfc5ca5af0af118063aa6e35b67d9802))

- Trying alternate env var format
 ([10915fb](https://github.com/Vonage/vonage-dotnet-sdk/commit/10915fb3412a837a5fba54121f259e2426e09f2a))

- Trying alternate env var format
 ([6672d41](https://github.com/Vonage/vonage-dotnet-sdk/commit/6672d4145e7aebef68821b665f4614377d57e55e))

- Trying codecov ignore
 ([28597e7](https://github.com/Vonage/vonage-dotnet-sdk/commit/28597e7cbd90ad6309eabbc1a55ec0939c80fa41))

- Removing config file, removing redudant internal utility classes
 ([44cecb8](https://github.com/Vonage/vonage-dotnet-sdk/commit/44cecb842ca5da761ecf686afed4015ca1391214))

- Removing legacy Call objects
 ([744b45c](https://github.com/Vonage/vonage-dotnet-sdk/commit/744b45cd89202312211788f6f69658caacd65b6f))

- Removing RequiredIfAttribute
 ([c4efc98](https://github.com/Vonage/vonage-dotnet-sdk/commit/c4efc98b4be7e91d85a90efab4e90b114685a32c))

- Adding Status badge
 ([7ff2222](https://github.com/Vonage/vonage-dotnet-sdk/commit/7ff2222f82cea27916ee9d0d579a6fc138088b83))

- Removing extra ! from README
 ([148935a](https://github.com/Vonage/vonage-dotnet-sdk/commit/148935a610c3ec58e19799aa710c64f94cf4c13d))

- Fixing action by changing dotnet-version
 ([881ba83](https://github.com/Vonage/vonage-dotnet-sdk/commit/881ba8302b07e70dba34f919c9f87acfc79ebcab))

- Revving version
 ([f06ef91](https://github.com/Vonage/vonage-dotnet-sdk/commit/f06ef918d0c319e46717cbf337a698364557f748))

- Updating license
 ([5b5dcc8](https://github.com/Vonage/vonage-dotnet-sdk/commit/5b5dcc834fef62e7eb7748e0cdaffe07f534591e))

- Moving nuspec to Apache-2.0 Licence
 ([8431f4d](https://github.com/Vonage/vonage-dotnet-sdk/commit/8431f4d94991efa6fd16584a14e00433850fcc0f))

- Fixing badge ([1e92fa1](https://github.com/Vonage/vonage-dotnet-sdk/commit/1e92fa186a42d653c39d5400a73265b47897f9b1))

- Fixing codecov badge ([acbe7f3](https://github.com/Vonage/vonage-dotnet-sdk/commit/acbe7f3173856b0767b193d6aa8a372422b5a974))

- Fixing licence nuspec tag. ([3e441ec](https://github.com/Vonage/vonage-dotnet-sdk/commit/3e441ec1dda60faab3655298ae26cfd74ca5cab9))


## [v5.2.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v5.2.0) (2020-08-07)

### Merges

- Merge pull request #208 from Nexmo/webhook_utility

Adding utility methods for parsing webhooks ([5462d81](https://github.com/Vonage/vonage-dotnet-sdk/commit/5462d8176648423bd3c07cbab63359247589111b))

- Merge pull request #210 from Nexmo/multInputParseIssue

Fixing issue with ParseEvent for multiinput ([62f4cbb](https://github.com/Vonage/vonage-dotnet-sdk/commit/62f4cbb69cf12f70c1cc0f6302dd17712ae2fab3))


### Other

- Adding utility methods for parsing webhooks
 ([7af6361](https://github.com/Vonage/vonage-dotnet-sdk/commit/7af63610d8e5ab2c5bb252e94588832a8a61fae5))

- Adding Url Decoding to URL parser, updating tests so that they check for multi-word inbound messages, updating readme
 ([ce00e8e](https://github.com/Vonage/vonage-dotnet-sdk/commit/ce00e8ecf14b840a7e9edd6b1f29b64931b90c8c))

- Fixing issue with ParseEvent for multiinput
 ([9fa3001](https://github.com/Vonage/vonage-dotnet-sdk/commit/9fa300130f314ef663bbf1c0748b0d93ee59ec8f))

- Bumping version to 5.2.0
 ([df91e20](https://github.com/Vonage/vonage-dotnet-sdk/commit/df91e206e2767a2ac566024e6df493a3d281b7e1))


## [v5.1.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v5.1.0) (2020-07-01)

### Bug Fixes

- Fixing path for psd2, updating version in nuspec
 ([adeab69](https://github.com/Vonage/vonage-dotnet-sdk/commit/adeab69c894d4672f88c9dff341b9d8511923341))


### Merges

- Merge pull request #207 from Nexmo/psd2

Adding Psd2 functionality ([e506f2f](https://github.com/Vonage/vonage-dotnet-sdk/commit/e506f2fc92cd4becd43692e18577d3c9f22cf70f))


### Other

- Delete codecov.yml ([e70b763](https://github.com/Vonage/vonage-dotnet-sdk/commit/e70b76341a97ceb2e60cc7a4bc79b8fed354ec4b))

- Adding psd2
 ([4d1eba3](https://github.com/Vonage/vonage-dotnet-sdk/commit/4d1eba39513b8e54a3ea7531141bb85cf5aa4038))

- Reving version
 ([59518df](https://github.com/Vonage/vonage-dotnet-sdk/commit/59518dfe15221d0807f8ae938c0ad9902a1c7e17))

- Updating tests for correct path
 ([f53bc15](https://github.com/Vonage/vonage-dotnet-sdk/commit/f53bc15f3dabf61cb2fff1d395b4be25b82a4156))


## [v5.0.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v5.0.0) (2020-06-22)

### Bug Fixes

- Fixing minor bugs in voice client per spec issue
 ([4f0a3e0](https://github.com/Vonage/vonage-dotnet-sdk/commit/4f0a3e0fe47e11203c032390e75e036ccd43d7d9))


### Merges

- Merge pull request #204 from Nexmo/v5.0.0

Merging 5.0.0 PR for release. ([66b393a](https://github.com/Vonage/vonage-dotnet-sdk/commit/66b393a0b5a88ebe57e2013d1d45f2498bd38dd9))


### Other

- Adding unicode test cases for sms and voice - changing encoding for json to UTF8
 ([f2c43d3](https://github.com/Vonage/vonage-dotnet-sdk/commit/f2c43d3e5c0f4cf53a250c2220312a157796251a))

- Removing sms search
 ([9f26e6c](https://github.com/Vonage/vonage-dotnet-sdk/commit/9f26e6c2e5a2cb352b0f9d94542a37bc2f051749))

- Revving version
 ([0c704c0](https://github.com/Vonage/vonage-dotnet-sdk/commit/0c704c07aee541a8b85eab770ebed1f51bd2cbc7))


## [v4.4.1](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v4.4.1) (2020-06-17)

### Merges

- Merge pull request #206 from Nexmo/fixing_encoding

changing payload encoding to utf8 ([27ec412](https://github.com/Vonage/vonage-dotnet-sdk/commit/27ec412309e921b64b24df059552191a02893954))


### Other

- Merging with 4.4.0
 ([9dcbf94](https://github.com/Vonage/vonage-dotnet-sdk/commit/9dcbf9459e4942aaf678b3693ed852dec19cb89e))

- Updating merge to fix the test cases
 ([3c160c1](https://github.com/Vonage/vonage-dotnet-sdk/commit/3c160c107d50cac1593a908126502c31212abf3b))

- Updating readme for 5.0
 ([ad45f66](https://github.com/Vonage/vonage-dotnet-sdk/commit/ad45f669f728a9d7bc1f21b7d1dfa615ac0e9abc))

- Removing unecessary BC
 ([aa56b6d](https://github.com/Vonage/vonage-dotnet-sdk/commit/aa56b6d62d98f5fd0a14098125b5ce69be64e235))

- Adding codecov badge ([b1b738b](https://github.com/Vonage/vonage-dotnet-sdk/commit/b1b738b04575de1279564122a84554e9f8379234))

- Changing payload encoding to utf8
 ([14de643](https://github.com/Vonage/vonage-dotnet-sdk/commit/14de643e309fdaefe081e87120883c489d837fe4))


## [v4.4.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v4.4.0) (2020-06-05)

### Merges

- Merge pull request #203 from Nexmo/feature/asr_nccos

adding ASR webhook and input items ([71037fd](https://github.com/Vonage/vonage-dotnet-sdk/commit/71037fdab292934d40e2b28ee4c5605924825dc9))


### Other

- Adding ASR webhook and input items
 ([3821b63](https://github.com/Vonage/vonage-dotnet-sdk/commit/3821b63e8da1311567aa58d3821ba2ae7bf020b1))

- Rolling back input updates adding new multiInput class and tests for it
 ([9f4b30b](https://github.com/Vonage/vonage-dotnet-sdk/commit/9f4b30b27394a0fc089cd33d2e4f276b99a07fa3))

- Apparently first run at switching out ASR with multi-input tests didn't take
 ([e39127d](https://github.com/Vonage/vonage-dotnet-sdk/commit/e39127d73612d74c2254557746639daf338d7742))

- Adding error field to speech webhook struct
 ([d9810af](https://github.com/Vonage/vonage-dotnet-sdk/commit/d9810aff5f3ac178e4fa9697721e6cac8faf4b11))

- Updating release notes in nuspec
 ([59ff4b6](https://github.com/Vonage/vonage-dotnet-sdk/commit/59ff4b6fcf0b8c02f87d1d99273712442110fd2c))


## [v4.3.2](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v4.3.2) (2020-04-08)

### Bug Fixes

- Fixing build issue
 ([02d38fc](https://github.com/Vonage/vonage-dotnet-sdk/commit/02d38fcf8cf673d834daea1b473adbd3a6fc691c))

- Fixing broken tests - setting optional parameters in application list call to nullable
 ([a3964e5](https://github.com/Vonage/vonage-dotnet-sdk/commit/a3964e5859923ddd90506bdaded2a99b4241e433))

- Fixing test input
 ([8902e58](https://github.com/Vonage/vonage-dotnet-sdk/commit/8902e58a1ec6621b8a996c8ed5b5c606691103e5))

- Fixing call converter, adding first Call Test
 ([9011bdc](https://github.com/Vonage/vonage-dotnet-sdk/commit/9011bdc5c09c0e66ce3abe2151f770d228783944))

- Fixing some issues with the summary docs and adding back 452/46 - managing rearranging some depdnencies in the nuspec file
 ([4d7b36e](https://github.com/Vonage/vonage-dotnet-sdk/commit/4d7b36e2a06e49c80ea5ff27665299fe00520dd6))

- Fixing the nuget package so it adds xml files
 ([b504122](https://github.com/Vonage/vonage-dotnet-sdk/commit/b504122d5ec9ad18830a3cb80723e6418bca1b43))

- Fixing typo for netcoreapp3.0
 ([0495c00](https://github.com/Vonage/vonage-dotnet-sdk/commit/0495c00efa40e9de73ff930aec3eeaddc997511c))


### Merges

- Merge branch 'v5.0.0' into unit_test_rework
 ([c23a015](https://github.com/Vonage/vonage-dotnet-sdk/commit/c23a0156d6386db96abd73c23b0cf8d0b44dae55))

- Merge branch 'conform_to_naming_conventions' of https://github.com/Nexmo/nexmo-dotnet into conform_to_naming_conventions
 ([27bd158](https://github.com/Vonage/vonage-dotnet-sdk/commit/27bd158e7244873d179e1cf454d1c7677f6298ab))

- Merge pull request #194 from Nexmo/conform_to_naming_conventions

Conforming .NET library to standard .NET naming conventions - adding obsolete tags over old data structures ([38d74b7](https://github.com/Vonage/vonage-dotnet-sdk/commit/38d74b7530003dd7280e8af737514a1fa5e9c458))

- Merge branch 'v5.0.0' into unit_test_rework
 ([21bd708](https://github.com/Vonage/vonage-dotnet-sdk/commit/21bd708626711a93827f1267dc6c3a551bdaec47))

- Merge pull request #188 from Nexmo/unit_test_rework

Unit test rework ([b8e476a](https://github.com/Vonage/vonage-dotnet-sdk/commit/b8e476a88d2a9e922d427bc7ee1f75edefdc46d0))

- Merge pull request #202 from Nexmo/dotnet_standard_summary_docs

.NET standard consolidation, summary docs, couple of enums ([12599da](https://github.com/Vonage/vonage-dotnet-sdk/commit/12599daecdf119845493c468a71e02e47de4a783))

- Merge pull request #193 from Nexmo/add-code-of-conduct

Create CODE_OF_CONDUCT.md ([7849a3f](https://github.com/Vonage/vonage-dotnet-sdk/commit/7849a3f19f4e48bc2e88d29e42056e86aa611d46))

- Merge pull request #195 from Nexmo/vonage-wordmark

Update branding in README ([b3bd241](https://github.com/Vonage/vonage-dotnet-sdk/commit/b3bd241d68c881fe0b290200ab5297779a80b891))

- Merge pull request #197 from taylus/master

Serialize streamUrl as array per docs to avoid bad request ([b1013b1](https://github.com/Vonage/vonage-dotnet-sdk/commit/b1013b1c769a4e42047b3150b069dc50902361a8))

- Merge pull request #199 from Nexmo/bugfix/websocket_header_serialization

changing headers type to object to allow it to serialize cleanly. ([b8013c6](https://github.com/Vonage/vonage-dotnet-sdk/commit/b8013c6c4ef297ff62dfde8f7e326845e0e3383a))


### Other

- Merging with master
 ([086d985](https://github.com/Vonage/vonage-dotnet-sdk/commit/086d9852bf76fb8eea52b11556603bbb4ad8b71e))

- Merging
 ([d174401](https://github.com/Vonage/vonage-dotnet-sdk/commit/d174401c26f65ce6636afe64fd39bf99ee042d37))

- Reforming voice client
 ([25cabdc](https://github.com/Vonage/vonage-dotnet-sdk/commit/25cabdc9357771f49c8814c673a452b4134386ca))

- Pushing up
 ([c2d2818](https://github.com/Vonage/vonage-dotnet-sdk/commit/c2d281806c87ab7d7ea037f0c710bb8a5c0b2f44))

- Some preliminary changes to accounts API
 ([7cca5bf](https://github.com/Vonage/vonage-dotnet-sdk/commit/7cca5bf6fdbb34ec1ca98308439cdde592d97db7))

- Account / secrets names fixed
 ([8a39abd](https://github.com/Vonage/vonage-dotnet-sdk/commit/8a39abd2128a7e8cfc98a9e6759cdc3b2feb60c9))

- Application renaming
 ([fbaa6ec](https://github.com/Vonage/vonage-dotnet-sdk/commit/fbaa6ec85dea1a099105a2c805c62347e68a72ec))

- Account renaming
 ([be09252](https://github.com/Vonage/vonage-dotnet-sdk/commit/be09252ee2bcec343c559f2b9937ee6114fe75ad))

- Pricing numbers and redact
 ([4d4930d](https://github.com/Vonage/vonage-dotnet-sdk/commit/4d4930d287fe8f38e64793f02e343d07cd61404c))

- Adding back client
 ([413db19](https://github.com/Vonage/vonage-dotnet-sdk/commit/413db19fb008b8db55a637e17cefc9188731851c))

- Finishing off the actual renaming part
 ([8fb70fd](https://github.com/Vonage/vonage-dotnet-sdk/commit/8fb70fd73a41a497182ad51a288c807e9e5adfe3))

- Adding obsolete tags
 ([2b953df](https://github.com/Vonage/vonage-dotnet-sdk/commit/2b953df1d326a7dd6d7be7d44589209c383d8d32))

- Adding factory methods for creating Credentials
 ([fd97e0a](https://github.com/Vonage/vonage-dotnet-sdk/commit/fd97e0a2f82060d2e6cef110b44c20af9350b84b))

- Various updates made while creating code snippets
 ([9091d5a](https://github.com/Vonage/vonage-dotnet-sdk/commit/9091d5a7118d74c91542fba08f7294f47e35e5bb))

- Updating naming stuff to make sure they work and work cleanly
 ([e2b0414](https://github.com/Vonage/vonage-dotnet-sdk/commit/e2b0414352f05ade6ba7d6f888212cbc3b6dc6e4))

- Adding logger to nuspec
 ([0ba05c4](https://github.com/Vonage/vonage-dotnet-sdk/commit/0ba05c4e07027cb2799b501221597ddc388f13aa))

- Merging naming convention stuff and doing some minor name-space cleanup
 ([251de99](https://github.com/Vonage/vonage-dotnet-sdk/commit/251de99d9aeb166ea8fa0922beb6f519a69ad5e2))

- Adding pricing client to NemxoClient, cleaning up property names in Account settings, adding pricing and Account APIs
 ([dcd006b](https://github.com/Vonage/vonage-dotnet-sdk/commit/dcd006b787260756b075f783640260f68edf86dc))

- Adding more pricing tests in both legacy and new
 ([5cc5d84](https://github.com/Vonage/vonage-dotnet-sdk/commit/5cc5d84b4c246ee5e22a5dd2f4bb2a5263d66b06))

- Adding new Secrets tests - adding HALLink to secret object
 ([0a1b5e5](https://github.com/Vonage/vonage-dotnet-sdk/commit/0a1b5e538cc633e25d33bab7c73edd73552636c5))

- Adding tests for legacy api secret api
 ([5286c1c](https://github.com/Vonage/vonage-dotnet-sdk/commit/5286c1c37a6ef93e8db80477df32d2021e020326))

- Removing redundant List Applications method, Deleting Legacy ApplicationV1 client as it's been tagged as obsolete through a full Major Release Cycle
 ([1bc5b06](https://github.com/Vonage/vonage-dotnet-sdk/commit/1bc5b06dce61816c05653a1141213b29e277f688))

- Making ApiKey a required parameter for secrets APIs
 ([593c28e](https://github.com/Vonage/vonage-dotnet-sdk/commit/593c28eb2c18997d83557ceb7b22ef1894f47dd2))

- Updates to Application structures for correctness, adding create Application test
 ([d864b12](https://github.com/Vonage/vonage-dotnet-sdk/commit/d864b12f3611bff652512c01b397e563dd3f01e2))

- Adding path coverage for credentials passing
 ([2d8de3b](https://github.com/Vonage/vonage-dotnet-sdk/commit/2d8de3b14b5bcc3deab60117adee5d839224cbb4))

- Adding more applicaiton tests
 ([7eb5192](https://github.com/Vonage/vonage-dotnet-sdk/commit/7eb5192102be6715cb5a55181355a56ef086107e))

- Adding credential testing paths to legacy tests
 ([1ef1042](https://github.com/Vonage/vonage-dotnet-sdk/commit/1ef10424a2d298cae0c355c996c624af35dc9fd6))

- Adding Conversion tests
 ([062ebf0](https://github.com/Vonage/vonage-dotnet-sdk/commit/062ebf0db7ec8260a7e6a375405a49b364a004d8))

- Adding MessagesSearch tests, updating MessagesSearch request, and ApiRequest to accomodate multiple search ids
 ([ec07ed5](https://github.com/Vonage/vonage-dotnet-sdk/commit/ec07ed53f742571dadf5eeff0b4db1f553338db0))

- Adding send SMS tests
 ([e1bf8e0](https://github.com/Vonage/vonage-dotnet-sdk/commit/e1bf8e066b3cad06fc0775d9822c7de5119653b4))

- Adding some missing stuff to messaging structs, adding inbound/dlr tests
 ([860b190](https://github.com/Vonage/vonage-dotnet-sdk/commit/860b1906a1fa8d3fe57a616bdb223dd361b7ea0f))

- Signature generation/validation test
 ([7beb202](https://github.com/Vonage/vonage-dotnet-sdk/commit/7beb202a69c84f0d332e915dffa288422c7d5cc4))

- Adding webhook tests
 ([77010c9](https://github.com/Vonage/vonage-dotnet-sdk/commit/77010c9ecf3fa2210ced8c36204d5c43e6303df7))

- Adding notification test, unanswered test, and null test
 ([c0a27e2](https://github.com/Vonage/vonage-dotnet-sdk/commit/c0a27e2ed2b4f95c7bbcb07517f8b2ad7614dcee))

- Adding Ncco Serializations tests
 ([67b66df](https://github.com/Vonage/vonage-dotnet-sdk/commit/67b66df5cfea2f4981cd535ea7834f74fbc22278))

- Adding legacy create call test
 ([372d25d](https://github.com/Vonage/vonage-dotnet-sdk/commit/372d25d399bbf4f00d00c661899a8c32b8f766d2))

- Adding list calls test
 ([7d459d3](https://github.com/Vonage/vonage-dotnet-sdk/commit/7d459d34d834d465cd7161f272379e1b5e7e472f))

- Adding streaming tests
 ([c46abad](https://github.com/Vonage/vonage-dotnet-sdk/commit/c46abadf5d4f9abed6f06c0c48f7838eec877aa4))

- Adding get recordings test
 ([69fb233](https://github.com/Vonage/vonage-dotnet-sdk/commit/69fb23374b523c378ff4c587131fcf3454169459))

- Removing superfolous voice class
 ([756879d](https://github.com/Vonage/vonage-dotnet-sdk/commit/756879deb818a6eb25f0709d0ce89ad3e5a13a32))

- Removing signature helper and unused request in ApiRequest.cs
 ([eafafe4](https://github.com/Vonage/vonage-dotnet-sdk/commit/eafafe45e9bbf9a5ed37f11db0b44f9a9ee1a893))

- Renaming ApiRequest method 'DoGetRequestWithUrlContent' to 'DoGetRequestWithQueryContent' adding NumberInsights exceptions, adding numberinsights tests
 ([ace90e7](https://github.com/Vonage/vonage-dotnet-sdk/commit/ace90e7f7089b37b0b1eac81568ddb960a8dbae6))

- Adding response to NexmoNumberResponseException, changing name of NumberInsightsResponseException to NexmoNumberInsightResponseException adding Number tests fixing minor issues with Number class structure
 ([6b7607b](https://github.com/Vonage/vonage-dotnet-sdk/commit/6b7607b66cb03265762f822ba6d47c5c243805db))

- Adding codecov file
 ([7f58921](https://github.com/Vonage/vonage-dotnet-sdk/commit/7f58921a6959e5aad72d2aa47af27facbd173c56))

- Updates to codecov file
 ([9da4020](https://github.com/Vonage/vonage-dotnet-sdk/commit/9da4020b3aded7953b9d0956c25aff1ad599ff6c))

- Changing path for codecov ignore
 ([a76fcc3](https://github.com/Vonage/vonage-dotnet-sdk/commit/a76fcc3d203103f3194d8f0b98538dce1dc1fd94))

- Adding error handling to verify, adding Verify tests
 ([9a23461](https://github.com/Vonage/vonage-dotnet-sdk/commit/9a2346189c7a284ac3d86a80c6a48f6e4c5f73fb))

- Adding summary docs as well as some new enumerated types
 ([13bdc66](https://github.com/Vonage/vonage-dotnet-sdk/commit/13bdc6628d5aa5e35a8c6fd80eb887e0673e0685))

- Making unit tests compiliant to the new enums
 ([b577430](https://github.com/Vonage/vonage-dotnet-sdk/commit/b57743005489c8825e9b919e3db361919ceecdcf))

- Dropping core 3.0 tests
 ([27e7b83](https://github.com/Vonage/vonage-dotnet-sdk/commit/27e7b8308c50348b534e23ecad635f1f240ff9e6))

- Adding some extra path testing for missing status
 ([8a56ff6](https://github.com/Vonage/vonage-dotnet-sdk/commit/8a56ff6c3c5220147cfc82696d2e5ad2ce33e891))

- Tearing out weird hard-coded path
 ([849d9e5](https://github.com/Vonage/vonage-dotnet-sdk/commit/849d9e5c301b31de11b07653fb4238c16cfd4c21))

- Create CODE_OF_CONDUCT.md ([14f0085](https://github.com/Vonage/vonage-dotnet-sdk/commit/14f00857ac5afdb04e262ee24e13c23b915c64dc))

- Adding badge
 ([a1df6c9](https://github.com/Vonage/vonage-dotnet-sdk/commit/a1df6c9bbc2a28cc5b9f9d3c5fc6fdcd51b9b80f))

- Add Vonage wordmark to Nexmo repo
 ([e7b542c](https://github.com/Vonage/vonage-dotnet-sdk/commit/e7b542c0bac43e8795bb177897b5d55f12568666))

- Serialize streamUrl as array per docs to avoid bad request
 ([0d804c9](https://github.com/Vonage/vonage-dotnet-sdk/commit/0d804c91bceee0f92470fd9cc26b968f006c22dc))

- Adding Taylus to the contributors section of the readme ([fc03f32](https://github.com/Vonage/vonage-dotnet-sdk/commit/fc03f32085cd866a828b55fe2da4356d105c47df))

- Update Release GH Action workflow ([f8ce237](https://github.com/Vonage/vonage-dotnet-sdk/commit/f8ce2379ac388c41a6745842824213db1e6f2568))

- Changing headers type to object to allow it to serialize cleanly.
 ([1e38527](https://github.com/Vonage/vonage-dotnet-sdk/commit/1e385276fbc1cb40e2a6b7419504ae85f4b9b04a))

- Updating for 4.3.2 release
 ([ce90660](https://github.com/Vonage/vonage-dotnet-sdk/commit/ce9066088dd058eb6967b19a23a8438c02d56121))


## [v4.3.1](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v4.3.1) (2020-02-18)

### Bug Fixes

- Fixing search numbers test
 ([28db3ec](https://github.com/Vonage/vonage-dotnet-sdk/commit/28db3ecf5166c45a149538c39ee678064e115ae1))


### Merges

- Merge branch 'unit_test_rework' of https://github.com/Nexmo/nexmo-dotnet into unit_test_rework
 ([bac43e9](https://github.com/Vonage/vonage-dotnet-sdk/commit/bac43e98a1af25f04e0911f73c8de3f0c52700a8))

- Merge branch 'add_logging_extensions' of https://github.com/Nexmo/nexmo-dotnet into add_logging_extensions
 ([b5e6ed6](https://github.com/Vonage/vonage-dotnet-sdk/commit/b5e6ed663c99931f31b378700e2ad78621afbf55))

- Merge pull request #189 from Nexmo/add_logging_extensions

Add logging extensions ([d733b62](https://github.com/Vonage/vonage-dotnet-sdk/commit/d733b62ce5d8371a7731866e27d1a6be9f131829))

- Merge branch 'v5.0.0' into unit_test_rework
 ([d511b07](https://github.com/Vonage/vonage-dotnet-sdk/commit/d511b07b438d7451d575e199bab3484d9e8c5403))

- Merge pull request #187 from Nexmo/exception_handling

Fix error handling so that well formed exceptions are thrown explicity. ([5972445](https://github.com/Vonage/vonage-dotnet-sdk/commit/59724450bb16fac59433c92ff2f4a76e90300525))

- Merge pull request #191 from Nexmo/bugfix/malformed_user_agents

Fixing malformed user agents ([211055b](https://github.com/Vonage/vonage-dotnet-sdk/commit/211055b8f365b61f6cf6f48e2ad99574276210a1))

- Merge branch 'master' into bugfix/loop_param_ignored_when_zero
 ([be8c4fd](https://github.com/Vonage/vonage-dotnet-sdk/commit/be8c4fde827ca3ae3e7b3afde73ef6fa8f53c246))

- Merge pull request #190 from Nexmo/bugfix/loop_param_ignored_when_zero

Fixing default serialization of zero's in loop ([a51d9ec](https://github.com/Vonage/vonage-dotnet-sdk/commit/a51d9ec11475c645fbbd7bdd4ffedbb33421d0ba))


### Other

- Creating branch
 ([f74a027](https://github.com/Vonage/vonage-dotnet-sdk/commit/f74a0275d9008cbc0d5dc8f8dbd8186ed0757a15))

- Switching to xUnit
 ([956cbbe](https://github.com/Vonage/vonage-dotnet-sdk/commit/956cbbe1462842eacaeaa910b45cc242ccf1f7f1))

- Adding signature and inbound test
 ([9aa2ee4](https://github.com/Vonage/vonage-dotnet-sdk/commit/9aa2ee43828595e6cf83146f76ecac9b70d2dabe))

- Adding full set of signing tests and dlr test
 ([0ffe46d](https://github.com/Vonage/vonage-dotnet-sdk/commit/0ffe46d07ac7aacf80e28ea0f92303d4dc0f5a28))

- Adding verify test
 ([7b6e013](https://github.com/Vonage/vonage-dotnet-sdk/commit/7b6e013369c6437f1cc26356e54bb173a75e1bd6))

- Committing unit test work
 ([a3e75cb](https://github.com/Vonage/vonage-dotnet-sdk/commit/a3e75cb618b4943d80d38659c8e323c507f90421))

- Switching to xUnit
 ([30d6e44](https://github.com/Vonage/vonage-dotnet-sdk/commit/30d6e445c90707985f6912960d72d03c0cfe7cf9))

- Adding signature and inbound test
 ([847583c](https://github.com/Vonage/vonage-dotnet-sdk/commit/847583c26029854acdacb34a7872addd49678bd2))

- Adding full set of signing tests and dlr test
 ([e86442c](https://github.com/Vonage/vonage-dotnet-sdk/commit/e86442cd30b11e13418d6258d2e4c7417a15ec6a))

- Switching to xUnit
 ([3c4cc89](https://github.com/Vonage/vonage-dotnet-sdk/commit/3c4cc8928d9cb268fd054668c136bbfc2039aaf5))

- Adding signature and inbound test
 ([9aab50f](https://github.com/Vonage/vonage-dotnet-sdk/commit/9aab50f1843e0721543616426989b04415cc44cf))

- Adding full set of signing tests and dlr test
 ([15462b1](https://github.com/Vonage/vonage-dotnet-sdk/commit/15462b1e66f8c951e17a56a3bf8c115a17d6dc13))

- Adding verify test
 ([8b43fec](https://github.com/Vonage/vonage-dotnet-sdk/commit/8b43fecca2c642fa1e833be55433d16677891e3d))

- Committing unit test work
 ([1aaf0ab](https://github.com/Vonage/vonage-dotnet-sdk/commit/1aaf0abd7b1b1ac95bded5f917c8e6f4b98df8a0))

- Switching to xUnit
 ([5f3c383](https://github.com/Vonage/vonage-dotnet-sdk/commit/5f3c38395eddf26127bbf118a938d26662627809))

- Adding signature and inbound test
 ([da3f3a1](https://github.com/Vonage/vonage-dotnet-sdk/commit/da3f3a1f5c738bbd08b450cbbbec120806f60d65))

- Adding full set of signing tests and dlr test
 ([6f02a5e](https://github.com/Vonage/vonage-dotnet-sdk/commit/6f02a5e24f73497cae1a48db3eea33b422ffbf0a))

- Adding verify test
 ([eea5e41](https://github.com/Vonage/vonage-dotnet-sdk/commit/eea5e418ff9ecddb060113a4aeac5f7da2b98c09))

- Committing unit test work
 ([028b22a](https://github.com/Vonage/vonage-dotnet-sdk/commit/028b22aabe77604756a654b6f1431f50360698fb))

- Resolving merge conflicts
 ([fb1f64a](https://github.com/Vonage/vonage-dotnet-sdk/commit/fb1f64af2ce1706caee2f2b7135b4a4ef23fed7c))

- Adding logging extension
 ([5ddddbe](https://github.com/Vonage/vonage-dotnet-sdk/commit/5ddddbe6974d24ffadfb935ad80224ce1772f88b))

- Adding Microsoft.Extensions.Logging to cs file - synchonizing extensions at 1.1.2
 ([3c07b8c](https://github.com/Vonage/vonage-dotnet-sdk/commit/3c07b8c9186b31e3917251053c1904c921e6d007))

- Adding nullable contingencies
 ([f7fc6a0](https://github.com/Vonage/vonage-dotnet-sdk/commit/f7fc6a09404f36465156089bc39bfecd50a4a750))

- Adding logging extension
 ([a0ede24](https://github.com/Vonage/vonage-dotnet-sdk/commit/a0ede2490d31e6f9c1a459470184f79293bbf437))

- Adding Microsoft.Extensions.Logging to cs file - synchonizing extensions at 1.1.2
 ([132ff4a](https://github.com/Vonage/vonage-dotnet-sdk/commit/132ff4a2ff4fe3faf47baeacdd50e473173c9841))

- Adding nullable contingencies
 ([00c9583](https://github.com/Vonage/vonage-dotnet-sdk/commit/00c95833099b209b28788b529172fdf7403f8c29))

- Removing old LibLog
 ([7bdd7da](https://github.com/Vonage/vonage-dotnet-sdk/commit/7bdd7da8b7ab771f6ad0312d0056bc5cd4c88ab6))

- Axing merge flags
 ([07b1644](https://github.com/Vonage/vonage-dotnet-sdk/commit/07b1644a7e1be9fbd4068975b6dd210ed1eeec0e))

- Changing exception handling
 ([d0568fb](https://github.com/Vonage/vonage-dotnet-sdk/commit/d0568fb4706e30828b50b1e7f6b1f64d1869be22))

- Further cleanup and exception throwing
 ([c6e458c](https://github.com/Vonage/vonage-dotnet-sdk/commit/c6e458c52e56bcfc92396cf5e9fc5172951845e8))

- Adding VerifyResponseException, moving common response stuff to VerifyResponseBase class, making Verify Request, Check, and Control throw VerifyResponseExceptions when a failure is detected
 ([731f53e](https://github.com/Vonage/vonage-dotnet-sdk/commit/731f53e0d4046b4063b4e7a4754815ee77c9a129))

- Moving all requesting logic to ApiRequest - deleting VersionedApiRequest class pointing everything at the more common methods
 ([9e0e233](https://github.com/Vonage/vonage-dotnet-sdk/commit/9e0e2331193433c3175cd69a324ca2194deb2d83))

- Cleaning up stuff for unit tests
 ([8242b17](https://github.com/Vonage/vonage-dotnet-sdk/commit/8242b1764c2f9378cdee67b6ec3521186146f752))

- Merging with the v5.0.0 branch
 ([40a6481](https://github.com/Vonage/vonage-dotnet-sdk/commit/40a64819659b3958adf7065b975627a634c2dec2))

- Adding some summary documentation, changing names of the tons of DoRequest calls
 ([8084e8b](https://github.com/Vonage/vonage-dotnet-sdk/commit/8084e8b431d671e63e3c82bb92c95a4b1c3887b0))

- Adding summary docs to all the client methods indicating the new exception throws
 ([21a9a81](https://github.com/Vonage/vonage-dotnet-sdk/commit/21a9a81dfc3bb608ea3abc5b2ce1b4dfaf190b99))

- Updating for error handling PR
 ([aea57a6](https://github.com/Vonage/vonage-dotnet-sdk/commit/aea57a6c3060f1dcea8b463160fa62e8da69fc6b))

- Allowing loop for talk and stream and making the seralizer ignore if null and include if zero
 ([51c5019](https://github.com/Vonage/vonage-dotnet-sdk/commit/51c50198b2634a71948fa02bd571c2ae133031b4))

- Removing OS description from User agent - making sure to scrub the runtimeVersion of any parentheses that could cause an error to propogate from the runtime.
 ([597873a](https://github.com/Vonage/vonage-dotnet-sdk/commit/597873a0641c6be8569350bb3465fd14fd4c0b7a))

- Revving version for release
 ([e899ae6](https://github.com/Vonage/vonage-dotnet-sdk/commit/e899ae612a744682d2e54ab1b9dd33e095d63150))

- Updating release notes for nuget package
 ([6171cee](https://github.com/Vonage/vonage-dotnet-sdk/commit/6171ceecd0e989225031c9b0f73cff45919243c2))


## [v4.3.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v4.3.0) (2020-01-17)

### Bug Fixes

- Fixing private key formattign in readme
 ([e425bcb](https://github.com/Vonage/vonage-dotnet-sdk/commit/e425bcbcbea15662bfb4f648baeb74a117869901))


### Merges

- Merge pull request #160 from Nexmo/contributing

Add simple contributing file ([7fa539f](https://github.com/Vonage/vonage-dotnet-sdk/commit/7fa539ff1aedfabfbf38ac4aeb767873f4b83c4d))

- Merge pull request #177 from Nexmo/adding_appveyor

Adding badge for appveyor ([f70a7fa](https://github.com/Vonage/vonage-dotnet-sdk/commit/f70a7fa8ed036d167691837f3e41275377378f8f))

- Merge branch 'master' into feature/add_list_own_numbers
 ([c10b536](https://github.com/Vonage/vonage-dotnet-sdk/commit/c10b536d4a840e264780e0f9e86c77c8f1e06195))

- Merge branch 'feature/add_list_own_numbers' of https://github.com/Nexmo/nexmo-dotnet into feature/add_list_own_numbers
 ([bba34f2](https://github.com/Vonage/vonage-dotnet-sdk/commit/bba34f2b3daf2ef98f839531db3194b35f64eb1b))

- Merge pull request #180 from Nexmo/feature/add_list_own_numbers

Feature/add list own numbers make JWT generator public ([716b2e8](https://github.com/Vonage/vonage-dotnet-sdk/commit/716b2e89af4a1ad8493ace560bb20d2f95973c5e))

- Merge pull request #183 from Nexmo/list-owned-numbers-patch

making has_application nullable ([3b0d3c7](https://github.com/Vonage/vonage-dotnet-sdk/commit/3b0d3c7c465d840b6262825938fb191693c9cb05))


### Other

- Add simple contributing file
 ([e7bb590](https://github.com/Vonage/vonage-dotnet-sdk/commit/e7bb590ea885b2410b518b35f50fe6e035eeffa4))

- Fixing Nexmo.Application.Key description
 ([c741f4b](https://github.com/Vonage/vonage-dotnet-sdk/commit/c741f4be8e75c909cdf26aa7cc49da8a4cdf84ee))

- Adding badge for appveyor
 ([275190a](https://github.com/Vonage/vonage-dotnet-sdk/commit/275190a1ef370eef7f65ddd4273609265d4639ad))

- Removing extra nuget badge
 ([d7968de](https://github.com/Vonage/vonage-dotnet-sdk/commit/d7968de5c1805d55d2fc9c1069ac8f880296ca5b))

- Update README.md ([e952b56](https://github.com/Vonage/vonage-dotnet-sdk/commit/e952b56e3c081c264dbeb8fadc992ccd559df877))

- Adding list own number request
 ([bb63bbb](https://github.com/Vonage/vonage-dotnet-sdk/commit/bb63bbbf0854fde0cffad58bd7b3cf85e5fc6529))

- Removing unecessary class
 ([50f2d5f](https://github.com/Vonage/vonage-dotnet-sdk/commit/50f2d5f0f597c35f8a9eb25b78b61c6e5450894f))

- Making jwt generation public, reving nuspec to 4.3.0
 ([e1564be](https://github.com/Vonage/vonage-dotnet-sdk/commit/e1564be68c8ee3c5acf72699acbc826764f7807d))

- Making jwt generation public, reving nuspec to 4.2.2
 ([fbf94f2](https://github.com/Vonage/vonage-dotnet-sdk/commit/fbf94f21fef57e8920e5329a3990016cc0b399e2))

- Making jwt generation public, reving nuspec to 4.3.0
 ([b43c897](https://github.com/Vonage/vonage-dotnet-sdk/commit/b43c897f8a5aeacc2dbf876d8f3315c11c8a7cdc))

- Removing newline
 ([7a1611d](https://github.com/Vonage/vonage-dotnet-sdk/commit/7a1611d09120b750a0a5db14a998cdb1dc2ab6bf))

- Updating assembly version
 ([0d0d8aa](https://github.com/Vonage/vonage-dotnet-sdk/commit/0d0d8aae9a7cb57d2289adf6b9949b1adf608fb1))

- Making has_application nullable ([aa5b38c](https://github.com/Vonage/vonage-dotnet-sdk/commit/aa5b38c06aec5496ade8f18ea4cdc75ed8b4f9da))

- Update README.md ([dae26d6](https://github.com/Vonage/vonage-dotnet-sdk/commit/dae26d6d52c294f0f481ca3faed4253f402751e9))


## [v4.2.1](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v4.2.1) (2019-11-11)

### Merges

- Merge branch 'bugfix/rsa-on-osx' of https://github.com/Nexmo/nexmo-dotnet into bugfix/rsa-on-osx
 ([b57dc8c](https://github.com/Vonage/vonage-dotnet-sdk/commit/b57dc8ca3fa7b355a46a07a23dab745c4085763b))

- Merge pull request #155 from Nexmo/bugfix/rsa-on-osx

Avoiding creating RSACng on non-windows platforms ([4c651e2](https://github.com/Vonage/vonage-dotnet-sdk/commit/4c651e2bcfe88fb364359b9c161cf117a7aeeba5))

- Merge pull request #174 from Nexmo/bugfix/ConfigurationAbstractionDepdencyBug

Fixing Unit tests, fixing Application flow, fixing possible NRE ([ea42f4a](https://github.com/Vonage/vonage-dotnet-sdk/commit/ea42f4a10083eade0b6cdccb8f04d4c6aa5e2cfa))

- Merge pull request #169 from Nexmo/refactor

Fixing Ncco serialization bug ([f5683be](https://github.com/Vonage/vonage-dotnet-sdk/commit/f5683be838117b78f2f7c932633356c5307f6894))


### Other

- Fixing Ncco serialization bug
 ([39b3a7a](https://github.com/Vonage/vonage-dotnet-sdk/commit/39b3a7aaf2bd8d2d8b16f0e9dc77d04868ec4481))

- Avoiding creating RSACng on non-windows platforms
 ([5ac6d62](https://github.com/Vonage/vonage-dotnet-sdk/commit/5ac6d622001eb18ef8881ae8c32cf0db28c1e1cd))

- Avoiding creating RSACng on non-windows platforms
 ([2a9f319](https://github.com/Vonage/vonage-dotnet-sdk/commit/2a9f319912e1f053e6dfbf9bde4a927ff9116af9))

- Adding Fauna5 to readme
 ([0b5e93f](https://github.com/Vonage/vonage-dotnet-sdk/commit/0b5e93fff5594fb2f0df72fe8eb47996c9a6c8b8))

- Fixing Unit tests, breaking out Application List get call to work the same way everything else doees, fixing Null Reference Exception Bugs, fixing dependency nuget issue
 ([f409c72](https://github.com/Vonage/vonage-dotnet-sdk/commit/f409c72acf9bf1fed535131042daeffcea5eacc9))

- Adding other unit testing frameworks
 ([06efc55](https://github.com/Vonage/vonage-dotnet-sdk/commit/06efc550ebaa69974ec8803ddbe146233e2777ab))

- Updates
 ([69fea1c](https://github.com/Vonage/vonage-dotnet-sdk/commit/69fea1c46ace5d4cd54bc33dc63c79738eade60d))

- Changing payload to an object
 ([3b049f9](https://github.com/Vonage/vonage-dotnet-sdk/commit/3b049f978b70eec884bca73ccab0c60a3455be0b))

- Adding auth string encoding for application gets
 ([ac3be56](https://github.com/Vonage/vonage-dotnet-sdk/commit/ac3be56a8c744e9d7793604e62ce0d228af42a5d))


## [v4.2.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v4.2.0) (2019-10-21)

### Merges

- Mergeing with master
 ([dce8d83](https://github.com/Vonage/vonage-dotnet-sdk/commit/dce8d83a43c19f4b562686ab6029aa0138646277))

- Merge pull request #167 from Nexmo/refactor

Adding type-safe webhooks and NCCOs. Adding application_id and has_application to Numbers API ([568b570](https://github.com/Vonage/vonage-dotnet-sdk/commit/568b5706e5bf0a26fc06df4d70ccc1ef3f9b0d7a))


### Other

- Adding NccoObj field to CallCommand, creating CallCommandConverter to explicitly handle callCommand json serialization, fixing nuspec, creating Ncco converter to serialize NCCOs decorating Ncco field in CallCommand to obsolete
 ([7514230](https://github.com/Vonage/vonage-dotnet-sdk/commit/751423066dbc02240496d4224eba4ef3b0949d2b))


## [v4.1.2](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v4.1.2) (2019-10-11)

### Bug Fixes

- Fixing url encoding issue with signed SMS
 ([32265d4](https://github.com/Vonage/vonage-dotnet-sdk/commit/32265d4e1e387022f9507b9acb3d38353a19c7ce))


### Merges

- Merge branch 'redact_fixes' into application_fixes
 ([74f4c55](https://github.com/Vonage/vonage-dotnet-sdk/commit/74f4c5555cf5242b13927aefe7aa95ec1a7aa576))

- Merge branch 'refactor' of https://github.com/Nexmo/nexmo-dotnet into refactor
 ([e84edf7](https://github.com/Vonage/vonage-dotnet-sdk/commit/e84edf74e1d84117c040c6dccd91a1dac4365c51))

- Merge pull request #163 from Nexmo/application_fixes

Application Update / List, Redact, GetRecording fixed or added ([c961129](https://github.com/Vonage/vonage-dotnet-sdk/commit/c961129ed364412305675dc2761c771d1476b9ba))

- Merge pull request #166 from Nexmo/4_1_1

Harmonizing dependencies  ([79b5551](https://github.com/Vonage/vonage-dotnet-sdk/commit/79b55510e5ce18d57424aab9b7670690596d3877))

- Merge branch 'master' into refactor ([c8a517f](https://github.com/Vonage/vonage-dotnet-sdk/commit/c8a517f44553ea5c22d0ae152c459808a61a3687))

- Merge pull request #168 from Nexmo/add_sms_signing

Adding sms signing ([629e5e7](https://github.com/Vonage/vonage-dotnet-sdk/commit/629e5e72b93ee97255c24ef88ca69a9496286d10))


### Other

- Add auto-changelog on Release

This PR adds a Github action which is triggered when a release is published. The action adds a new entry to the public Nexmo changelog with the contents of the release notes ([f4db79e](https://github.com/Vonage/vonage-dotnet-sdk/commit/f4db79e99731df36154c36211b56dbc6bcfd250c))

- Migrate Github Actions to YAML format
 ([cfb965d](https://github.com/Vonage/vonage-dotnet-sdk/commit/cfb965de4e80c420bbf3fa5955fee8cebe7c9f0d))

- Adding workflow_id to VerifyRequest
 ([f50aaff](https://github.com/Vonage/vonage-dotnet-sdk/commit/f50aaffc032e0f45a9fb5b6cb0d73a4a4b695c72))

- Fixing application update structures
 ([8b16fe2](https://github.com/Vonage/vonage-dotnet-sdk/commit/8b16fe2457e549741e8d07343f9788da8e5e3a2e))

- Fixing redact api
 ([39ed469](https://github.com/Vonage/vonage-dotnet-sdk/commit/39ed469054178c6189c560430652e90540c10234))

- Removing api_driver
 ([b23c703](https://github.com/Vonage/vonage-dotnet-sdk/commit/b23c7038bad7a8743eddbe0e7cb9fc4f2ff60eef))

- Adding getRecordingRequest
 ([da6dd9c](https://github.com/Vonage/vonage-dotnet-sdk/commit/da6dd9c25e8991809d4a92877070be91b8e8b2ad))

- Adding getRecording
 ([31e712e](https://github.com/Vonage/vonage-dotnet-sdk/commit/31e712ed812f5eddc6a89c19595957c319be70dd))

- Cleanup
 ([423067c](https://github.com/Vonage/vonage-dotnet-sdk/commit/423067c3dda1bbba493fc8dc5ccb78ccecf6cc0a))

- Reving version
 ([bce6c15](https://github.com/Vonage/vonage-dotnet-sdk/commit/bce6c15b31a0227dc1519f555f3dc14592bf5e60))

- Revving version
 ([6dae360](https://github.com/Vonage/vonage-dotnet-sdk/commit/6dae360cf1633379f8804f9f408aa93c481db20e))

- Adding NCCO And Input Classes
 ([90a8c07](https://github.com/Vonage/vonage-dotnet-sdk/commit/90a8c07326218d34652c6614a0ca176ec7a77316))

- Adding workflow_id to VerifyRequest
 ([fff625e](https://github.com/Vonage/vonage-dotnet-sdk/commit/fff625e8e2970f1fcfca0e1c3a5474decbb70c08))

- Fixing application update structures
 ([dca63d4](https://github.com/Vonage/vonage-dotnet-sdk/commit/dca63d43de50af15ab0250dae60192cc8ab51514))

- Fixing redact api
 ([bdcb161](https://github.com/Vonage/vonage-dotnet-sdk/commit/bdcb16118767d8f9c1042a3ce6b4c8c53e081794))

- Removing api_driver
 ([002d189](https://github.com/Vonage/vonage-dotnet-sdk/commit/002d189d89039e778eae8bfbd6e275d7eba11982))

- Adding getRecordingRequest
 ([e2e1ebe](https://github.com/Vonage/vonage-dotnet-sdk/commit/e2e1ebe8e9fba714bcacd147ec3ab3d6045ae7a8))

- Adding getRecording
 ([9470836](https://github.com/Vonage/vonage-dotnet-sdk/commit/9470836107f09c489c6cbd516fa6afc65b030652))

- Cleanup
 ([1dad5d3](https://github.com/Vonage/vonage-dotnet-sdk/commit/1dad5d3c087f70be9a75186715dd18581f080635))

- Reving version
 ([11be7a9](https://github.com/Vonage/vonage-dotnet-sdk/commit/11be7a94d1416f395e4dc4157f6330213d7d4345))

- Revving version
 ([32b39cb](https://github.com/Vonage/vonage-dotnet-sdk/commit/32b39cb6e3e0ee15c7b372b62d6f6760dcb9ffb5))

- Adding NCCO And Input Classes
 ([acb780f](https://github.com/Vonage/vonage-dotnet-sdk/commit/acb780f466daf4f62ef8a6ea4c5d759c726d5b62))

- Adding has_application and application_id to query for Numbers
 ([1dfc341](https://github.com/Vonage/vonage-dotnet-sdk/commit/1dfc341b6cd44d57117def5a7bd6a6e5ddab51e0))

- Forcing ordinal values to prevent nulled serialization
 ([d851640](https://github.com/Vonage/vonage-dotnet-sdk/commit/d8516406114febcdf2e084d168a4ed99fd50f90d))

- Fixing Configuration.Abstractions incorrect assembly loading issue
 ([93eb62e](https://github.com/Vonage/vonage-dotnet-sdk/commit/93eb62ee031eb4bd5e4d8a7dff960799babb9519))

- Harmonizing Dependencies
 ([fb6833a](https://github.com/Vonage/vonage-dotnet-sdk/commit/fb6833afeaa914e7219449ee95930f4f3b358a97))

- Removing aspnetcore from the regular .NET packages
 ([7928779](https://github.com/Vonage/vonage-dotnet-sdk/commit/7928779025aa381ce054bfb49d909becef1d0eb0))

- Fixing indentation
 ([ee63942](https://github.com/Vonage/vonage-dotnet-sdk/commit/ee63942f901cf59d0d2cd271ced9ef49aefa02fe))

- Fixing SMS Signing for Hash and adding HMAC SMS signing
 ([929c3bc](https://github.com/Vonage/vonage-dotnet-sdk/commit/929c3bc06bafcecb193ebc8f79a3a4e2c048b8c7))

- Cleanup
 ([ea32c59](https://github.com/Vonage/vonage-dotnet-sdk/commit/ea32c59143fc2be0fb8ff16fac08ba4b714f3dcf))

- Enhancing so user doesn't have to generate their own signature string
 ([890e26a](https://github.com/Vonage/vonage-dotnet-sdk/commit/890e26afb2576c8f58144f054e02825c8eacd2eb))

- Changing data type to IDictionary and adding release notes to nuspec
 ([54621bd](https://github.com/Vonage/vonage-dotnet-sdk/commit/54621bdbb30abc43190790c8709a550dfe396f6f))


## [v4.0.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v4.0.0) (2019-06-11)

### Bug Fixes

- Fixes to use the right authorization
 ([81d57f1](https://github.com/Vonage/vonage-dotnet-sdk/commit/81d57f1cab5df88cc61f30d292431fb77dee4a52))

- Fixed method signature
 ([f7a9ab7](https://github.com/Vonage/vonage-dotnet-sdk/commit/f7a9ab7fc7ac1461e3d3bee13ddd23682054a8f0))


### Merges

- Merge pull request #156 from Nexmo/ApplicationV2

Application v2 - READY TO MERGE ([369cc93](https://github.com/Vonage/vonage-dotnet-sdk/commit/369cc9326b63aa9f9f97803c619abaa38d0d59ae))


### Other

- [WIP] : Application V2 first commit
 ([2814e13](https://github.com/Vonage/vonage-dotnet-sdk/commit/2814e136551917f1bcca0ea4226f7d6edc837814))

- Hacky fix for GET Application
 ([d746057](https://github.com/Vonage/vonage-dotnet-sdk/commit/d746057ee594fc3a27be99f2126521f7b699a1b2))

- Hacky fix for GET List until the API is properly fixed
 ([5c9505b](https://github.com/Vonage/vonage-dotnet-sdk/commit/5c9505bde26c85eea89bc202de47897138636d34))

- Fixing all the typos from coding at 1AM
 ([0f67b10](https://github.com/Vonage/vonage-dotnet-sdk/commit/0f67b10068adbc1df03c9878a9da16119dd381aa))

- Fixed capabilities objects, we can only have one WebHook per type
 ([b73ea3e](https://github.com/Vonage/vonage-dotnet-sdk/commit/b73ea3e4523a606c7ed8aea4472f0b82e4a3ad0a))

- Added tests for Application V2
 ([687c3b8](https://github.com/Vonage/vonage-dotnet-sdk/commit/687c3b8c671439800a1f98ccd2f6a45f338fc9f8))

- API was fixed
 ([731ca4c](https://github.com/Vonage/vonage-dotnet-sdk/commit/731ca4c0fda225125f3a0e5d1b04bd2c2b9d4665))


## [v3.4.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v3.4.0) (2019-05-10)

### Bug Fixes

- Fixing code and renaming creds to credentials
 ([1fdac31](https://github.com/Vonage/vonage-dotnet-sdk/commit/1fdac31aaf3b802d142bb8c6b07c4d75d58170b0))

- Fixed conflict
 ([a9f1d87](https://github.com/Vonage/vonage-dotnet-sdk/commit/a9f1d87834963640be6d3e12596c39f84f782371))


### Merges

- Merge pull request #151 from Nexmo/Feature_completion

Feature completion ([93f41fb](https://github.com/Vonage/vonage-dotnet-sdk/commit/93f41fb7468ffb2251c77467a390c3a78f4eb248))


### Other

- Added package properties
 ([545c479](https://github.com/Vonage/vonage-dotnet-sdk/commit/545c47904b3c0a882942907a183c5075969b0286))

- Added GetPrefixPricing method
 ([80d4f16](https://github.com/Vonage/vonage-dotnet-sdk/commit/80d4f1627d45e4467f29f6d614e99f21d6e86234))

- Added submitConversion method and tests
 ([41309f3](https://github.com/Vonage/vonage-dotnet-sdk/commit/41309f3ebf82e373d4fb835b8716b576579b8392))

- Changed creds to credentials based on MAnik's review
 ([5582b1b](https://github.com/Vonage/vonage-dotnet-sdk/commit/5582b1b26c84362243fdbc598e313646997f9048))

- GetRecording ([6546e31](https://github.com/Vonage/vonage-dotnet-sdk/commit/6546e31afad4fc190cdc2ab442fee2de25654818))

- More merge conflicts
 ([35434a0](https://github.com/Vonage/vonage-dotnet-sdk/commit/35434a0d18088105fe1ed338763e550949645711))


### Reverts

- Revert "changed creds to credentials based on MAnik's review"

This reverts commit 5582b1b26c84362243fdbc598e313646997f9048.
 ([38c54c2](https://github.com/Vonage/vonage-dotnet-sdk/commit/38c54c2753f2bedded497fddbd6b57c4d3d85a46))

- Revert "WIP: getRecording"

This reverts commit 6546e31afad4fc190cdc2ab442fee2de25654818.
 ([d2695ed](https://github.com/Vonage/vonage-dotnet-sdk/commit/d2695ed42e85cf4eaf5f044bfcbf49218f23bee9))


## [v3.3.1](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v3.3.1) (2019-04-23)

### Merges

- Merge pull request #128 from MrEpiX/patch-1

Updated readme small text fixes ([f9cb0ea](https://github.com/Vonage/vonage-dotnet-sdk/commit/f9cb0ea50690e04e6c60c8c719b8b0a1e716f31c))

- Merge pull request #121 from cirojr/master

Removed unused App.config from project ([452ffc9](https://github.com/Vonage/vonage-dotnet-sdk/commit/452ffc92bc7c8a0bf43e08ca7dbbd496d271413e))

- Merge pull request #141 from Nexmo/CallEditCommandFix

Fixed Destination in call command ([95cf8af](https://github.com/Vonage/vonage-dotnet-sdk/commit/95cf8af65b10221d467e35e93390677e6cdb215e))

- Merge pull request #148 from Nexmo/bug_124_fix

Fixed bug 124 ([835c0cf](https://github.com/Vonage/vonage-dotnet-sdk/commit/835c0cf28f5b1b41c7c352e675b0471c18d528e1))

- Merge pull request #153 from Nexmo/NCCOImplementation

Implemented NCCO param for creating a call ([3c0bddb](https://github.com/Vonage/vonage-dotnet-sdk/commit/3c0bddbc8f1526f7469774ff596ddf35050e2011))


### Other

- Updated changelog.md
 ([66e186b](https://github.com/Vonage/vonage-dotnet-sdk/commit/66e186bc0a490341833e4ed8ef0ccbffe77ea50b))

- Update README.md (#118)

Fixing Link for Redacting a message ([edce7ce](https://github.com/Vonage/vonage-dotnet-sdk/commit/edce7ce26cf28cf399a1cecdea435b43485e0742))

- Fix mismatch on casing on the name of the folder Nexmo.Api (#119)

 ([34f09f4](https://github.com/Vonage/vonage-dotnet-sdk/commit/34f09f44b048918bf92307175caaaad2076a4fbc))

- Update README.md (#120)

 ([921e0f5](https://github.com/Vonage/vonage-dotnet-sdk/commit/921e0f5e02b5ffb48650a8964dd332ae48597691))

- Updated readme small text fixes

Added a dot to the end of the License-section and removed an unused Create Account-link. ([4056681](https://github.com/Vonage/vonage-dotnet-sdk/commit/4056681a5bf60117d6061617af7e41d459ebfb8e))

- Removed unused App.config from project
 ([b84ab0a](https://github.com/Vonage/vonage-dotnet-sdk/commit/b84ab0a54316c1c19591095b2337c87b06cf91b5))

- Fixed Destination in call command
 ([5d52ab6](https://github.com/Vonage/vonage-dotnet-sdk/commit/5d52ab655fcd7a9222e1c41feb9c95a80f52b86d))

- Updated test dependencies
 ([abc375c](https://github.com/Vonage/vonage-dotnet-sdk/commit/abc375cf9319c387f583013a18d3f41069b04dce))

- Fixed bug 124
 ([56a2481](https://github.com/Vonage/vonage-dotnet-sdk/commit/56a248160f629f7a77550a6db5ccff274261f424))

- Implemented NCCO param for creating a call
 ([4915afc](https://github.com/Vonage/vonage-dotnet-sdk/commit/4915afc2eebbc14a4a5bc7b8c8dfba28c880620f))


## [v3.2.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v3.2.0) (2018-09-28)

### Merges

- Merge pull request #115 from Nexmo/secretapi

Implement API Secret calls ([7a7a3ab](https://github.com/Vonage/vonage-dotnet-sdk/commit/7a7a3ab3c2fec275b5651ca1a93313eefd5ac69d))


### Other

- Implement API Secret calls
 ([6d3aee5](https://github.com/Vonage/vonage-dotnet-sdk/commit/6d3aee5fc8fd165486e6a6703a2e4aeb9a98fb9c))

- Renamed methods to meet specs
 ([d768981](https://github.com/Vonage/vonage-dotnet-sdk/commit/d7689818ea6195db001a6b2c2d454eed164d856c))


## [v3.1.1](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v3.1.1) (2018-08-30)

### Merges

- Merge pull request #106 from Nexmo/Credentials_fix

adding default constructor to credentials class ([9173182](https://github.com/Vonage/vonage-dotnet-sdk/commit/9173182f1d56af49fa5cd85d6c9959993e971bac))


### Other

- Adding default constructor to credentials class
 ([6bf91d3](https://github.com/Vonage/vonage-dotnet-sdk/commit/6bf91d334326d3a1fa4da2919e2780927387f276))

- Updated CHANGELOG and Client Lib version for release
 ([b395a29](https://github.com/Vonage/vonage-dotnet-sdk/commit/b395a29cbd06224687e9e529d3e1bd64d653177f))


## [v3.1.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v3.1.0) (2018-08-17)

### Merges

- Merge pull request #103 from Nexmo/RedactImplementation

Redact implementation ([f26578d](https://github.com/Vonage/vonage-dotnet-sdk/commit/f26578d4bf46d05ba39bb9bc93b93f0395d605fc))


### Other

- Adding constructors for basic and full auth (#92)

* Adding constructors for basic and full auth ([2391678](https://github.com/Vonage/vonage-dotnet-sdk/commit/2391678e63881915a9538f0e25e78ca71880f073))

- VS added services for test running
 ([175693c](https://github.com/Vonage/vonage-dotnet-sdk/commit/175693cf557cdd205d269ca6f5741fc4d79e4618))

- Switch to liblog (#101)

Switch to LibLog ([1eab5f7](https://github.com/Vonage/vonage-dotnet-sdk/commit/1eab5f7e3d1edc58b289167ae6da30e47343a4ba))

- Update liblog example ([d14b3d1](https://github.com/Vonage/vonage-dotnet-sdk/commit/d14b3d15bc0d754da9692d3ba04da36b5eb95408))

- Add issue template ([bce19aa](https://github.com/Vonage/vonage-dotnet-sdk/commit/bce19aac778b937ea7808586e320ab6c68a85e9d))

- Added redact transaction functionnality
 ([0c770a3](https://github.com/Vonage/vonage-dotnet-sdk/commit/0c770a3e064096fd4d780f6f3dafe487eca5b87a))

- Added Redact
 ([7a78cf8](https://github.com/Vonage/vonage-dotnet-sdk/commit/7a78cf8f59516c8e7b01bfd605c146a05eac9c3c))

- Updated README
 ([acff6e3](https://github.com/Vonage/vonage-dotnet-sdk/commit/acff6e3f28aacebf5db89ac023e160f7d092334e))

- Updated CHANGELOG
 ([b2af336](https://github.com/Vonage/vonage-dotnet-sdk/commit/b2af336e786ff40ea5d1fb19742fb53d6f786e1c))

- Renaming some methods
 ([07b5d0a](https://github.com/Vonage/vonage-dotnet-sdk/commit/07b5d0a2d38f2fa4faa3eca5ad15108b40d87d4a))


## [v3.0.1](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v3.0.1) (2018-03-05)

### Bug Fixes

- Fixed missed NumberInsight instanciation (#90)

 ([1030abe](https://github.com/Vonage/vonage-dotnet-sdk/commit/1030abe90b38efad7a2dba5cb6325f3e1af17f5e))


### Other

- Move integration tests to mstest; update mstest version
 ([12773ae](https://github.com/Vonage/vonage-dotnet-sdk/commit/12773ae3c535ebad25fa843148002663e677d56a))

- 3.0.1
 ([53d397c](https://github.com/Vonage/vonage-dotnet-sdk/commit/53d397c12b90eefef27a63e0e5f695689c27ab3e))


## [v3.0.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v3.0.0) (2018-02-24)

### Bug Fixes

- Fixed 'roaming' property to match the json (#74)

 ([6a8100c](https://github.com/Vonage/vonage-dotnet-sdk/commit/6a8100c3d4461e228508decfd80b528f25fc07a0))


### Merges

- Merge branch 'master' into 3.0

(v2.2.2 to v2.3.1)
 ([838a9d3](https://github.com/Vonage/vonage-dotnet-sdk/commit/838a9d3f3381214dc1d6b2c53442eb02bf90c8be))

- Merge remote-tracking branch 'origin/master' into 3.0
 ([52d0c57](https://github.com/Vonage/vonage-dotnet-sdk/commit/52d0c5743c3b09df33832702714d2a3071795362))

- Merge remote-tracking branch 'origin/master' into 3.0
 ([66177f6](https://github.com/Vonage/vonage-dotnet-sdk/commit/66177f6744e2ee444eaa747c746eec69f2338f01))

- Merge remote-tracking branch 'origin/master' into 3.0
 ([0994053](https://github.com/Vonage/vonage-dotnet-sdk/commit/099405388e6ea21f9e32293b9010990a42cc3a55))


### Other

- No samples in 3.0
 ([886635c](https://github.com/Vonage/vonage-dotnet-sdk/commit/886635cced2a566229cd7e675fba7130487416b8))

- Ni refresh (#62)

* Work in progress: updating Number insight

* Number Insight : updated Endpoints + fixed naming

* converting all properties name to follow c# convention + mapping to Json properties
 ([aa7b9bb](https://github.com/Vonage/vonage-dotnet-sdk/commit/aa7b9bb627b86e12a5426e165c54e8c31c27a3da))

- Fix #78 : event_url returns the url and other meta data thus it is a string[] (#81)

 ([e2710e6](https://github.com/Vonage/vonage-dotnet-sdk/commit/e2710e671c06e8abf0188d7ae5e418143b4b9568))

- #36 Updated README (#84)

* Updated README

* fixed typo
 ([94e81ae](https://github.com/Vonage/vonage-dotnet-sdk/commit/94e81ae40cc4f244c55c0d73f1f2b9b89587ee59))

- Fix silly copy & paste error with request.ip ([234e1e2](https://github.com/Vonage/vonage-dotnet-sdk/commit/234e1e201613de8dd4079e796b96d6e172b31040))

- 2018!
 ([988a159](https://github.com/Vonage/vonage-dotnet-sdk/commit/988a159ad71f9d5655a37d5827ed2aa99d1364e0))

- Remove deprecated PackageTargetFallback
 ([ac77c2b](https://github.com/Vonage/vonage-dotnet-sdk/commit/ac77c2b0085367e5291d114d456c363ff931d4a1))

- Remove IO dependency; expect contents of private key and not path to key
 ([660d6a5](https://github.com/Vonage/vonage-dotnet-sdk/commit/660d6a57fca927107d4645b69402fec8311d0ac1))

- Document credentials fields
 ([d8aed10](https://github.com/Vonage/vonage-dotnet-sdk/commit/d8aed10820d45a61c2de7bcfeabe7bff3c58a886))

- Call static methods instead of copying code; add missing doc; add missing VersionedApi Call calls
 ([3aab135](https://github.com/Vonage/vonage-dotnet-sdk/commit/3aab135584ec5e39d9aad2fdd5a482ccc8c86a8e))

- #82 fixed message_timestamp deserialization issue by changing the property to DateTime instead of string (#86)

* #82 fixed message_timestamp deserialization issue by changing the property to DateTime instead of string ([cb587be](https://github.com/Vonage/vonage-dotnet-sdk/commit/cb587be9b70c69566f97eb746e01f268f2f8a05e))

- Minor doc updates for v3.0
 ([662a3ea](https://github.com/Vonage/vonage-dotnet-sdk/commit/662a3ea3327548e0ea8e50db8bf5804ddf21f230))

- Add missing using
 ([89e5fe9](https://github.com/Vonage/vonage-dotnet-sdk/commit/89e5fe9c1812b3c4f349ffd39c77ed26a12945b9))

- Upgrade System.Security.Cryptography as that seems to stop #83 from happening
 ([d14c064](https://github.com/Vonage/vonage-dotnet-sdk/commit/d14c064b6a5e24e62ed9ee0b775e3bab78c8ebe9))

- Ensure netstandard2.0 uses correct crypto
 ([7f556aa](https://github.com/Vonage/vonage-dotnet-sdk/commit/7f556aadbedbac2f7a95acdb073208f5079bf4f1))

- Update a few deps; explicitly call out target frameworks as nuget likes that better
 ([c86ffbe](https://github.com/Vonage/vonage-dotnet-sdk/commit/c86ffbea424ed043a3192718aa33c3e6e14bef1f))

- Correct v3.0 date ([c0aa26e](https://github.com/Vonage/vonage-dotnet-sdk/commit/c0aa26ec3c427411aed2cee709e852ced9a071b0))


## [v2.3.1](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v2.3.1) (2017-11-24)

### Other

- Clarify NumberInsight changes
 ([284f785](https://github.com/Vonage/vonage-dotnet-sdk/commit/284f785fe03b4ce94efd4213ec2239bd52bbefa5))

- Put the NI changes under the correct item... ([410e68c](https://github.com/Vonage/vonage-dotnet-sdk/commit/410e68c9a157b4e364bc127c9f4632e1ecb2e80d))

- V2.3.1; Set Json serialization DefaultValueHandling to ignore (addresses Voice API usage regression)
 ([6c1de97](https://github.com/Vonage/vonage-dotnet-sdk/commit/6c1de977fd68ec5e5e913ab7f5d00458c0fc7a51))

- V2.3.1 changelog ([3b494a2](https://github.com/Vonage/vonage-dotnet-sdk/commit/3b494a2f4cce54f7acd3a3785c470a2ff549072d))


## [v2.3.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v2.3.0) (2017-11-09)

### Other

- 3.0 pre-release badge ([07a8673](https://github.com/Vonage/vonage-dotnet-sdk/commit/07a8673e2a1d553129ba268eb17ece6f2c9c505d))

- Updated ReadMe to contain more building blocks (#39)

* updated ReadMe to contain more building blocks

* updated list of examples and links to point to the new NDP + formatting
 ([1b80649](https://github.com/Vonage/vonage-dotnet-sdk/commit/1b806490a10bc240cf91a7ce14330ca5e539b757))

- Minor formatting ([362a400](https://github.com/Vonage/vonage-dotnet-sdk/commit/362a400879077f25ea7046185c951ffd1f02803c))

- V3.0 note ([ab327be](https://github.com/Vonage/vonage-dotnet-sdk/commit/ab327be272d57b77436a5bf0084ab97f45d23772))

- Specify .NET Core SDK via global.json; fixes #69
 ([5753698](https://github.com/Vonage/vonage-dotnet-sdk/commit/5753698a3327d9178968f57e9b9f1e66eac47de1))

- Start/end time can return as null
 ([4855691](https://github.com/Vonage/vonage-dotnet-sdk/commit/4855691e05fbe4dce9be3154128ebcb101b4ffc4))

- API and doc refresh; GetBalance and NI breaking changes
 ([3da2d67](https://github.com/Vonage/vonage-dotnet-sdk/commit/3da2d67c67c9acf0cd83f5bf497fdb35fa9b75a0))

- Support additional call endpoint types; closes #70
 ([360a7da](https://github.com/Vonage/vonage-dotnet-sdk/commit/360a7dac3d453cdb67d63fcf64c579eb626dfeff))

- Add Nexmo.Api.EnsureSuccessStatusCode configuration option
 ([b1ff8ee](https://github.com/Vonage/vonage-dotnet-sdk/commit/b1ff8ee21ebea2886d3482d11269d2ea811be9b0))

- Address ShortCode.RequestAlert request bug
 ([50e026c](https://github.com/Vonage/vonage-dotnet-sdk/commit/50e026c8cda529682a0bdaa72e2694494dda726b))

- Test updates for 2.3.0
 ([3ad6af2](https://github.com/Vonage/vonage-dotnet-sdk/commit/3ad6af2ad971acdb814cb3d1e6904da98d1637c7))

- Prep samples for v2.3.0
 ([4028daf](https://github.com/Vonage/vonage-dotnet-sdk/commit/4028daf38ebd1964eda3d13bf8471e3ed267f4ec))

- Expose the configuration ILoggerFactory for use with external logging implementations.
 ([f3390a5](https://github.com/Vonage/vonage-dotnet-sdk/commit/f3390a57d8dd64085fb689b2713909593f594e3b))

- Prep for 2.3.0
 ([5a2c3c9](https://github.com/Vonage/vonage-dotnet-sdk/commit/5a2c3c93bcc70bf21b8097ab733221191624644e))

- Attribution to @RabebOthmani for NI!
 ([2f0256c](https://github.com/Vonage/vonage-dotnet-sdk/commit/2f0256c3345e6f7fb3701ed699b94fecea04da7d))


## [v2.2.2](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v2.2.2) (2017-06-19)

### Other

- Switch to RSACng for .NET 4.6+
 ([70d409a](https://github.com/Vonage/vonage-dotnet-sdk/commit/70d409ac2af583bb3f33332b0e21a23d50340dd7))

- Tag 3.0.0
 ([b31d3fc](https://github.com/Vonage/vonage-dotnet-sdk/commit/b31d3fcb92611c465c03e3cfd60d634131b7d8f8))

- Remove support for configuration via web.config.
 ([24ee263](https://github.com/Vonage/vonage-dotnet-sdk/commit/24ee26355c240a14d4da4a232b831b296006070c))

- Removing examples as they will be pushed to the community samples repo
 ([85e7adb](https://github.com/Vonage/vonage-dotnet-sdk/commit/85e7adb6581ebde1cda561c24212c5a9f119982b))

- Move to VS2017
 ([faddc8b](https://github.com/Vonage/vonage-dotnet-sdk/commit/faddc8bef832b40d17d9eed38514968adef4ea7f))

- Move unit tests to mstest
 ([542667f](https://github.com/Vonage/vonage-dotnet-sdk/commit/542667fb990ade31ca90e50d4be58697c4674678))

- Instance client
 ([f10bebc](https://github.com/Vonage/vonage-dotnet-sdk/commit/f10bebcb18b9ca01cf8bee98bbbe12a44bc7c0d2))

- Nuget prerelease 1
 ([de63c12](https://github.com/Vonage/vonage-dotnet-sdk/commit/de63c120c74a0fa42e80d72c8eebc8fd34531b01))

- Update jose-jwt to 2.3.0
 ([42a50d2](https://github.com/Vonage/vonage-dotnet-sdk/commit/42a50d2892787e70ae112a0b79776daa76f02cc5))

- Move to .NET Standard 2.0, update deps, release pre2. Closes #37
 ([97aae55](https://github.com/Vonage/vonage-dotnet-sdk/commit/97aae55608b75a08ebb3366892fb131210c048ea))

- Bring back netstandard1.6 support; Remove unused System.Xml.* dependencies; netstandard2.0 should use Microsoft.Extensions.DependencyInjection v2.0
 ([961dddd](https://github.com/Vonage/vonage-dotnet-sdk/commit/961dddd470f4aaa68600ecb19430c8b2542fed10))

- Pass credentials into SetUserAgent so when file config isn't being used the option to send app version is still available
 ([7eba9cb](https://github.com/Vonage/vonage-dotnet-sdk/commit/7eba9cb1e71df935fab2246db4180b5fa5cb44cd))

- Back to keeping standard1.6
 ([660df22](https://github.com/Vonage/vonage-dotnet-sdk/commit/660df22fc4a1fbe64945059e0b28b6b643626069))

- Bring back 1.6 ([e63e26d](https://github.com/Vonage/vonage-dotnet-sdk/commit/e63e26de0b90b7dfdecb751ad375ae5cdfcb0f1a))

- Style improvements (#31)

Changed Call API to Voice API and other stylistic improvements ([1ee98e8](https://github.com/Vonage/vonage-dotnet-sdk/commit/1ee98e82dba6b7209d1a2504d3b1aa774d30ca6a))

- V2.2.2; Updated jose-jwt to 2.3.0 which is reported to address key loading issues.
 ([1e338b7](https://github.com/Vonage/vonage-dotnet-sdk/commit/1e338b7b7d2c924cad216c5369636656b6f091c2))


## [v2.2.1](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v2.2.1) (2017-03-21)

### Other

- Dependency updates
 ([1326374](https://github.com/Vonage/vonage-dotnet-sdk/commit/1326374f2ba7b5edee181d19b8d577a362d7a76b))

- Add link to separate examples repo in README
 ([e83278b](https://github.com/Vonage/vonage-dotnet-sdk/commit/e83278b791c1a16231be59a4d6f7feea9af51f8e))

- Fixed NuGet dependencies
 ([6f6a3ed](https://github.com/Vonage/vonage-dotnet-sdk/commit/6f6a3ed881d8e4024e6e13f13381c26fdb22ad06))


## [v2.2.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v2.2.0) (2017-03-10)

### Other

- Syntax
 ([daaa215](https://github.com/Vonage/vonage-dotnet-sdk/commit/daaa2157a01a1bdc2ddf71680c61073fa7006d62))

- 2.2.0
 ([5b96307](https://github.com/Vonage/vonage-dotnet-sdk/commit/5b9630727ff1c245b2fa12aa69965752f60f7a58))


## [v2.2.0-rc2](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v2.2.0-rc2) (2017-01-28)

### Other

- Allow PKCS#8 formatted private keys; auth key parser logging
 ([094c9e0](https://github.com/Vonage/vonage-dotnet-sdk/commit/094c9e05c4f4504fba74f8fa8544d5330134bb65))

- 2.2.0-rc2
 ([fe462c5](https://github.com/Vonage/vonage-dotnet-sdk/commit/fe462c520f7e2f7a1937e7d0fa0fa22632ca7833))


## [v2.2.0-rc1](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v2.2.0-rc1) (2017-01-13)

### Other

- Support optional throttling (#19)

 ([df131cc](https://github.com/Vonage/vonage-dotnet-sdk/commit/df131cce7b3354ec8cf2bd30305339a7afdc6bd7))

- Prep for 2.2.0
 ([d410dc7](https://github.com/Vonage/vonage-dotnet-sdk/commit/d410dc759ebaf026cffc1a1c51481f4a0ce1c0d9))

- Add nuget shield ([947cb0a](https://github.com/Vonage/vonage-dotnet-sdk/commit/947cb0a1bb8e4a0fe99eb329455bd765d8721100))

- Initial support for signing requests; quick example of verifying signature
 ([b6f7634](https://github.com/Vonage/vonage-dotnet-sdk/commit/b6f763434139e258621476e49dc56e583c66acc3))

- Sig verify refactoring
 ([f7c0f44](https://github.com/Vonage/vonage-dotnet-sdk/commit/f7c0f448f4950b5bba87c919852b089dbca56b9b))

- Config and request logging. Addresses the majority of #9
 ([4c8df8e](https://github.com/Vonage/vonage-dotnet-sdk/commit/4c8df8efbe4740105fcd3148aba51ce0bd2225b4))

- Cleanup
 ([5a7787e](https://github.com/Vonage/vonage-dotnet-sdk/commit/5a7787e5b067dd3348504b89522a76b5fef77c0a))

- Make account calls conform internally to the rest of the API
 ([67cddcc](https://github.com/Vonage/vonage-dotnet-sdk/commit/67cddcc1e7fdae30809a55d20ae69ff06e0d3475))

- Allow override of credentials per request. Closes #18
 ([667e9c4](https://github.com/Vonage/vonage-dotnet-sdk/commit/667e9c418999de9ae4f01ea550d99b2a795b005a))

- 🎉 2017! 🎉
 ([c87b890](https://github.com/Vonage/vonage-dotnet-sdk/commit/c87b890550c0afe4b71d851a244cacecd193abe8))

- Expose API request methods to allow custom API calls from library consumers
 ([fef75b5](https://github.com/Vonage/vonage-dotnet-sdk/commit/fef75b50fa45c251abf11adbe553031341b7023c))

- Update documentation
 ([f107383](https://github.com/Vonage/vonage-dotnet-sdk/commit/f1073838a380a41dde342fa4a0e1940378114af7))

- Push 2.2.0-rc1
 ([c9943a8](https://github.com/Vonage/vonage-dotnet-sdk/commit/c9943a84bebce772bcafc84556f8bbb058d87ec9))


## [v2.1.2](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v2.1.2) (2016-12-07)

### Other

- * Look for appsettings.json (netcore webapp convention)
* Ensure XML config parser only looks for keyvalues inside appSettings and connectionStrings elements.
* Gracefully ignore elements with key attribute but not value attribute.
 ([4e9ffc9](https://github.com/Vonage/vonage-dotnet-sdk/commit/4e9ffc924f651db249d445a5d59251c082e1208b))


## [v2.1.1](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v2.1.1) (2016-12-07)

### Other

- Remove accidental writeline
 ([094cfe9](https://github.com/Vonage/vonage-dotnet-sdk/commit/094cfe9c2d6f94ba66536195b7ec4abfb128c998))

- 2.1.1 - look for legacy app.config convention of [exec process].exe.config
 ([6096405](https://github.com/Vonage/vonage-dotnet-sdk/commit/6096405315fcb988f11d81f8496d7fb02dd6d17a))


## [v2.1.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v2.1.0) (2016-11-18)

### Other

- Read settings from web.config; fixes #14
 ([86a8544](https://github.com/Vonage/vonage-dotnet-sdk/commit/86a854413887ac4652aa2fce09ea051aba223153))

- Implement user-agent support. Fixes #10
 ([701b7b7](https://github.com/Vonage/vonage-dotnet-sdk/commit/701b7b714a3e619319603b70ffab1548ef208a4c))

- .NET Core web example
 ([0baa1e4](https://github.com/Vonage/vonage-dotnet-sdk/commit/0baa1e41dd7cd11b208a7d3d81c4440f9c85b90f))

- 2.1.0 version bump and doc changes
 ([7428b60](https://github.com/Vonage/vonage-dotnet-sdk/commit/7428b608bf742156411d4202c40e0978491e10bf))


## [v2.0.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v2.0.0) (2016-10-24)

### Merges

- Merge pull request #13 from Nexmo/netcore

.NET Core/Standard and v2.0 release ([1c048b3](https://github.com/Vonage/vonage-dotnet-sdk/commit/1c048b30a0ac6f8dd34b768b5606f8fad6a8fbb9))


### Other

- Update gitignore
 ([13458cb](https://github.com/Vonage/vonage-dotnet-sdk/commit/13458cb10cbee72339937f69fc9aac434b8d44e6))

- NumberInsight basic + standard
 ([31a7a57](https://github.com/Vonage/vonage-dotnet-sdk/commit/31a7a5717f8ce9b75f5f3b743f7b67d95ec93a51))

- Implement Verify Control call
 ([99ba720](https://github.com/Vonage/vonage-dotnet-sdk/commit/99ba7205b73538a1388252b79f21be48e3f7ad51))

- Update README
 ([51faf8c](https://github.com/Vonage/vonage-dotnet-sdk/commit/51faf8ce00e4c6721fdd7e588e5fb0391c0e3792))

- Update README
 ([fb9307d](https://github.com/Vonage/vonage-dotnet-sdk/commit/fb9307d6da1a810ca6f5ee0649562f762b1c2248))

- Application API
 ([b1d1d71](https://github.com/Vonage/vonage-dotnet-sdk/commit/b1d1d71c773b9deefb32306855943e06013404f7))

- Initial JWT support
 ([7bb116b](https://github.com/Vonage/vonage-dotnet-sdk/commit/7bb116b0dce5bd1b242d0c4a54238f193a417c19))

- Cleanup tests a bit; remove deprecated voice api; implement remainder of call-specific api calls
 ([3485e73](https://github.com/Vonage/vonage-dotnet-sdk/commit/3485e73ce51f559b0bdfa4842c69740f4e9bb367))

- Implement remainder of voice calls
 ([287747b](https://github.com/Vonage/vonage-dotnet-sdk/commit/287747b779af4e076d5c06b778eb9b9a82209fa9))

- Working on core port
 ([3ee603d](https://github.com/Vonage/vonage-dotnet-sdk/commit/3ee603da227dd4b6299664c6c91886dbdc4f5e4e))

- Minor changes to tts sample
 ([6b2dd44](https://github.com/Vonage/vonage-dotnet-sdk/commit/6b2dd44f9a19ba8f96fafec2b03874aec3cfa873))

- Reworking tests, nuspec, project
 ([050792e](https://github.com/Vonage/vonage-dotnet-sdk/commit/050792eb8d7984560287904c2436bcbaccc7865b))

- Update readmes
 ([f7b3caa](https://github.com/Vonage/vonage-dotnet-sdk/commit/f7b3caa74fe0b08cc1747d557c5f9856915274a5))

- Add note re: config file change
 ([fc6e5e5](https://github.com/Vonage/vonage-dotnet-sdk/commit/fc6e5e59bae61d88e4b3bdfef1ce9f52faa22d9e))

- Fix JWT generation (key import fail) on OSX/Linux
 ([e348b39](https://github.com/Vonage/vonage-dotnet-sdk/commit/e348b39a7bcb40c2d9534b2e3ff2b4a2665738c9))

- Dependency marking for netstandard1.6; dep cleanup
 ([688298b](https://github.com/Vonage/vonage-dotnet-sdk/commit/688298b0327ac0aa20862ef7aa3d53badb5e0db2))


## [v1.0.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v1.0.0) (2016-03-19)

### Merges

- Merge pull request #1 from jonferreira/master

Implement SMS send ([956fa90](https://github.com/Vonage/vonage-dotnet-sdk/commit/956fa904223fbc4b25779c7de8fe8b67a25e3a38))


### Other

- Initial public release.
 ([9686632](https://github.com/Vonage/vonage-dotnet-sdk/commit/9686632cf46b6981519e16a4e56fd075078f1aa3))

- Update SMS.cs ([1e36ed0](https://github.com/Vonage/vonage-dotnet-sdk/commit/1e36ed0a2e7c9e7e3fe03430c6c004cf3669bca9))

- Update NumberVerify.cs ([7abb06e](https://github.com/Vonage/vonage-dotnet-sdk/commit/7abb06e021ded1a8b596bcf8557ca69677828f98))

- Update ApiRequest.cs ([be7e509](https://github.com/Vonage/vonage-dotnet-sdk/commit/be7e50969bb86598b1440f00f3a96137f7a11366))

- Minor cleanup
 ([0517349](https://github.com/Vonage/vonage-dotnet-sdk/commit/0517349171abb55dd39182ea22f4db64fe6a57ba))

- Add SMS type and response codes (thanks @DJFliX)
 ([9eae86e](https://github.com/Vonage/vonage-dotnet-sdk/commit/9eae86edce1dc6a8b7587ccb975e593344584151))

- Starting unit and integration tests, bit of refactoring
 ([90fa677](https://github.com/Vonage/vonage-dotnet-sdk/commit/90fa6778a17667ffdef08f1ab0bed596a7875a15))

- Move to 4.5.2
 ([e633514](https://github.com/Vonage/vonage-dotnet-sdk/commit/e633514c98321273cffe6241134bb7a296b6572e))

- Implement Number calls
 ([3b907d0](https://github.com/Vonage/vonage-dotnet-sdk/commit/3b907d0c04576bae91fe9a3d3b7fbec8afc8cb60))

- Cleanup; sms test
 ([2de0c77](https://github.com/Vonage/vonage-dotnet-sdk/commit/2de0c77dbcd1de827b16f6b125d73e98345c1a09))

- Update sample configs
 ([c4f95b9](https://github.com/Vonage/vonage-dotnet-sdk/commit/c4f95b982f06e2aff628e9af73b2cab70d5a76dc))

- Implement SMS inbound and receipt
 ([4fae9ac](https://github.com/Vonage/vonage-dotnet-sdk/commit/4fae9ac2bd831382839b9612e4568b2962771fdd))

- Implement search
 ([7ec36d3](https://github.com/Vonage/vonage-dotnet-sdk/commit/7ec36d35d1e4a8722823f2a7595e2be29be6c9a7))

- Implement remainder of account calls
 ([cbe022a](https://github.com/Vonage/vonage-dotnet-sdk/commit/cbe022a7992939782526ac8c408a4e74b6020d77))

- Implement short code calls
 ([a97bee5](https://github.com/Vonage/vonage-dotnet-sdk/commit/a97bee5f6e92502869c36ed4c8e633d82efcc990))

- Update packages - mainly for JSON.net 8
 ([aaa3878](https://github.com/Vonage/vonage-dotnet-sdk/commit/aaa3878ddef3414c29ad2ca768a6cc70941a79f6))

- Implement voice call and TTS
 ([e5fbb05](https://github.com/Vonage/vonage-dotnet-sdk/commit/e5fbb05f7dd66fbcf3d10eeb20bc292b120d849d))

- Prep for nuget release
 ([89cb66d](https://github.com/Vonage/vonage-dotnet-sdk/commit/89cb66dd2b6cd91ae6c6e14e5c2afb0a96013c8e))

- Minor API updates
 ([a068d67](https://github.com/Vonage/vonage-dotnet-sdk/commit/a068d674db37a2ae3a335eafb1ea1adf5f31a73d))

- Rename SMS.SendSMS to SMS.Send
 ([489b800](https://github.com/Vonage/vonage-dotnet-sdk/commit/489b8005b680d6fb1e3c92efb29390051ff57bc6))

- Update packages
 ([6e703f7](https://github.com/Vonage/vonage-dotnet-sdk/commit/6e703f7d7d4aa2c2fa48786a18aa986bfb772c0d))

- Minor nuget spec change
 ([f314bc3](https://github.com/Vonage/vonage-dotnet-sdk/commit/f314bc32d73b31abb36dcba2223a8aabadb21466))

- Support for 4.5.2-4.6.1; housekeeping
 ([a470e35](https://github.com/Vonage/vonage-dotnet-sdk/commit/a470e357a2561a1987c9f5120030d20ef4b66e49))


<!-- generated by git-cliff -->
