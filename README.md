# Stormpath Middleware for ASP.NET
This library makes it incredibly simple to add user authentication features to your application, such as login, signup, authorization, and social login.

[Stormpath](https://stormpath.com) is a User Management API that reduces development time with instant-on, scalable user infrastructure. Stormpath's intuitive API and expert support make it easy for developers to authenticate, manage and secure users and roles in any application.

:bulb: If you're using ASP.NET Core 1.0, grab our [ASP.NET Core library](https://github.com/stormpath/stormpath-aspnetcore) instead.

## Current Status

This library is currently in beta, but has been tested extensively and is stable. We're working on shaving off the last remaining rough edges before tagging the first official release. If you run into something odd, please [file an issue](https://github.com/stormpath/stormpath-aspnet/issues)!

## Working Example

Head over to the [stormpath-aspnet-example](https://github.com/stormpath/stormpath-aspnet-example) repository to see a working example of ASP.NET MVC5 + Stormpath in action. :+1:

## Quickstart

You can add Stormpath to a new or existing ASP.NET project with only two lines of code! Here's how:

1. **[Sign up](https://api.stormpath.com/register) for Stormpath**

2. **Get your key file**

  [Download your key file](https://support.stormpath.com/hc/en-us/articles/203697276-Where-do-I-find-my-API-key-) from the Stormpath Console.

3. **Store your key as environment variables**

  Open your key file and grab the **API Key ID** and **API Key Secret**, then run these commands in PowerShell (or the Windows Command Prompt) to save them as environment variables:

  ```
  setx STORMPATH_CLIENT_APIKEY_ID "[value-from-properties-file]"
  setx STORMPATH_CLIENT_APIKEY_SECRET "[value-from-properties-file]"
  ```

4. **Store your Stormpath Application href in an environment variable**

  Grab the `href` (called **REST URL** in the Stormpath Console UI) of your Application. It should look something like this:

  `https://api.stormpath.com/v1/applications/q42unYAj6PDLxth9xKXdL`

  Save this as an environment variable:

  ```
  setx STORMPATH_APPLICATION_HREF "[your Application href]"
  ```
  
  > :bulb: It's also possible to specify the Application href at runtime by passing a configuration object when you initialize the middleware.

5. **Create a project**

 Skip this step if you are adding Stormpath to an existing project.
 
 Use the **Web** - **ASP.NET Web Application** - **MVC** template in Visual Studio, with Authentication set to **No Authentication**.
 
6. **Add `Startup.cs`**

 If your project doesn't contain an OWIN Startup class (`Startup.cs`) file, you'll need to add one. Right-click on your solution, select **Add** - **New Item**, and search for "OWIN Startup class".
 
7. **Install the middleware package**

 Using the Manage NuGet Packages window, install the ``Stormpath.AspNet`` package. (Select **Include prerelease** to show the newest version of the package.)
 
 Alternatively, you can use the Package Manager Console:
 
 ```
 PM> install-package -pre Stormpath.AspNet
 ```
 
8. **Configure and add the middleware**


 Edit your `Startup.cs` file, and add Stormpath to your application pipeline in `Configuration`:
 
 ```csharp
  public void Configuration(IAppBuilder app)
  {
      app.UseStormpath();
  }
 ```
 
 The `UseStormpath` method takes an optional configuration object. If you want to hardcode the Application href, instead of storing it in environment variables, for example:
 
 ```csharp
 var myConfiguration = new StormpathConfiguration
 {
     Application = new ApplicationConfiguration
     {
         Href = "your-application-href"
     }
 }
 ```
 
9. **That's it!**

  Compile and run your project, and use a browser to access `/login`. You should see a login view. MVC and Web API routes can be protected by adding `[Authorize]` attributes to the appropriate controller or method.


## Getting Help
If you encounter problems while using this library, file an issue here on Github or reach out to support@stormpath.com.
