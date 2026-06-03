# Breaking changes in version 7.0.0

This document covers all breaking changes introduced with v6.0.0.

If you migrate from v5.X.X to v6.X.X (or above), you will have to handle the following points in your codebase.

* Enum values are now capitalised in alignment with accepted coding practices and are Pascal Case
* All classes that were marked as deprecated in 5.x are now removed
* NCCO now inherits from List, it no longer has the `Actions` property; to add an action use `ncco.Add(action);`
* Strings with values `"true"` or `"false"` are now represented as `bool` in code