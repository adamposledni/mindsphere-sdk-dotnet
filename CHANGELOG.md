# Changelog
## v1.1.0
- new class architecture - client are initialized by *MindSphereApiSdk* instance
- no need to provide *HttpClient* instance
- added *ClientConfiguration* (region, domain, timeout, proxy)
- added *UserCredentials* as a new credential option
- listing operations now return pagination model with embedded data
- added more options to the request classes (e.g. IncludeShared, IfNoneMatch)
- improved validations
- use of unit tests
## v1.0.1
- added parameters validation
- better exception handling
## v1.0
- initial release