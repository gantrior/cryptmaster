# Crypt Master

# Intro
This code was created as technology demonstration of web service using C# backend which solves algorithmical problem

# Development
## Requirements
You will need to have:

* [ASP.NET Core 1.0](https://www.microsoft.com/net/core) - for backend compilation
* [NodeJS](https://nodejs.org/en/) - for building utils
* [Typescript] - for frontend compilation 
  * Install that using npm: `npm install -g typescript`

## On start

from the root folder, type following commands to build and run solution:

```
cd src/CryptMaster
npm install
tsc
dotnet restore
dotnet run
```

## Visual studio
You can use any IDE you want, but to use Visual Studio you must have:

* [Visual Studio 2015 Update 3](https://go.microsoft.com/fwlink/?LinkId=691129)
* [.NET Core 1.0.0 - VS 2015 Tooling Preview 2](https://go.microsoft.com/fwlink/?LinkID=824849)

You can then just open "CryptMaster.sln", build and start application from Visual Studio

## Technology
I have decided to use following technologies due to my personal preference:

* Backend 
  * [ASP.NET Core 1.0](https://www.microsoft.com/net/core)
  * MVC for API
  * Autofac for dependency injection
  * XUnit tested
* Frontend
  * Angular 2 RC5
  * Bootstrap design
  * API documentation via "Swagger"

# The Code

## Morse code Algorithm
Morse code encryption algorithm. The class focuses on speed of Encrypt/Decrypt method.
Encryption algorithm explained:
* When service is created it prepares encryptionDictionary with all supported morse characters
* When Encrypt is called, it iterates through the text and simply look for morse representation in dictionary
Decryption algorithm explained:
* When service it prepares [binary tree](https://en.wikipedia.org/wiki/Morse_code)
* When Decrypt is called it iterates through the text and walk through binary tree to look for character
General rules:
* Characters are separated by space
* Words are separated by 2 spaces

## General implementation notes and decisions

* Code uses Dependency Injection and it is easy to extend by another algorithm by just implementing interface ICryptService (Autofac will find it in the solution automatically)
* API returns BadRequest in case of wrong input (for example "a" when expecting morse code etc), however for user friendly input it is returning also partially translated content with the response. To show to the user where was the error
* Swagger is integrated with the solution. Therefore API documentation can be accessed at runtime through "/swagger/ui/index.html" (ex: http://localhost:51092/swagger/ui/index.html)

## Code

* `src/CryptMaster/Startup.cs` - The entry point of the Web, here all the bootstrapping is done
* `src/CryptMaster/DefaultModule.cs` - Initializes dependencies using Autofac
* `src/CryptMaster/Views/Home/Index.cshtml` - main view which is returned from server (basically returns whole SPA)
* `src/CryptMaster/Services/*` - Algorithm implementations
* `src/CryptMaster/Controllers/*` - MVC rendered routes, basically just one which returns index.html (SPA)
* `src/CryptMaster/API/*` - Implementation of REST API (which serves to SPA frontend)
* `src/CryptMaster/wwwroot/*` - frontend implementation
* `src/CryptMaster/wwwroot/systemjs.config.js` - system configuration for angular2 (bootstrapping)
* `src/CryptMaster/wwwroot/app/app*.ts` - initialization of angular2 routes, modules and services
* `src/CryptMaster/wwwroot/app/crypt/*` - code which handles encryption/decryption page
* `src/CryptMaster/wwwroot/app/shared/*.ts` - angular2 services which sends actual requests to the backend
  
# Testing
There are XUnit tests implemented which tests main business logic (morse code encryption/decryption)

# What is next
What could be improved?
* write frontend unittests (using karma)
* write frontend e2e tests
  
# License

MIT
