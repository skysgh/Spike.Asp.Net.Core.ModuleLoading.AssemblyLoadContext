# Spike.Asp.Net.Core.ModuleLoading.AssemblyLoadContext
Investigation of loading Modules into Asp.Net.Core *after* startup.

- Startup project: 
  - Spiies.Base.Host
- Localhost port: 
  - set in Host/Properties/LaunchSettings.json to:
  - Backend: https://localhost:7245
  - Front end: https://localhost:44480/
  - Proxying:
    - proxy.conf.js is set to let through to server
      any call starting with "api/**"
- Dependencies:
  - Host depends on Shared
  - Same for Module
  - but HOST DOES NOT KNOW ABOUT Module (which is example 3rd party plugin)
- Angular:
  - Views:
    - index page page is under Host/ClientApp/src folder
    - which in turn pulls in
    - Host/ClientApp/src/app contents
    - See Host/ClientApp/src/app/nav-menu to link to different views.
