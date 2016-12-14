Maintenance App
===============

The BikeSharing 360 Maintentance App is an application developed in **Apache Cordova** used by maintenance people of the company.

Every user can see incidences assigned to him, view its details and mark incidences resolved.

This application can be used to show the features of Tools for Apache Cordova in Visual Studio 2017.

Compile the application
------------------------

* **Prerequisites** Visual Studio 2017RC with the Cordova workload installed.
* **NPM Task Runner** This extension is needed to integrate the NPM tasks used inside the VS2017 "task runner". This projects uses NPM tasks instead of gulp tasks, so you need to have this extension installed to run the NPM tasks from VS2017 automatically in the build process. Can download it from the [marketplace](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.NPMTaskRunner).

Solution to use is _BikeSharing.Maintenance.sln_ and contains one project (_src/BikeSharing.Maintenance/BikeSharing.Maintenance.Cordova.jsproj_).

Run the application
-------------------

Application needs a valid "Feedback API endpoint". The Feedback API is one of the microservices, so be sure to download the [microservices repo](https://github.com/Microsoft/BikeSharing360_BackendServices) and run a feedback API.

Once feedback api was up & running edit the file _src/app/app.config.ts_ and provide the feedback API URL.

** Note:** This app will always use the user with ID 1, and has no login enabled. You can enable a login by setting to "true" the value of _loginRequired_ in the same config file. The login is against an Azure Active Directory (that you must provide). If you enable the login against AAD then need to edit the file _src/app/app.auth.ts_ with the required values to connect to the AAD.
