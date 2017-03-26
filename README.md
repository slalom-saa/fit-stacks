# Slalom Stacks

[![Build status](https://ci.appveyor.com/api/projects/status/6nb0ud2cpm4rkuyx/branch/master?svg=true)](https://ci.appveyor.com/project/slalom-saa/stacks/branch/master)   [![NuGet Version](http://img.shields.io/nuget/v/Slalom.Stacks.svg?style=flat)](https://www.nuget.org/packages/Slalom.Stacks/)

[Getting Started](#getting-started-hello-world) | [Design](https://github.com/slalom-saa/stacks/blob/master/documents/1.%20Design/overview.md) | [Development](https://github.com/slalom-saa/stacks/blob/master/documents/2.%20Development/overview.md)

## Getting Started (Hello World)
1. Create a new .NET Core console application named **HelloWorldService** in **Visual Studio 2015**.
2.	Add the **Slalom.Stacks** and **Slalom.Stacks.Web.AspNetCore** NuGet packages.  
```
Install-Package Slalom.Stacks
Install-Package Slalom.Stacks.Web.AspNetCore
```
3.	Create a class named **HelloWorldRequest**.
```csharp
public class HelloWorldRequest
{
    public string Name { get; }

    public HelloWorldRequest(string name)
    {
        this.Name = name;
    }
}
```
4.	Create an endpoint named **HelloWorld**.  Resolve any references as you go.
```csharp
[EndPoint("hello/greet")]
public class HelloWorld : EndPoint<HelloWorldRequest, string>
{
    public override string Receive(HelloWorldRequest instance)
    {
        return "Hello " + instance.Name + "!";
    }
}
```	
5.	Initialize a new Stack and run the web host.
```csharp
public static void Main(string[] args)
{
    using (var stack = new Stack())
    {
        stack.RunWebHost();
    }
}
```	
6. Start the application.
7. In a web browser, navigate to http://localhost:5000/swagger.
8. Enter a name and click "Try it out!"
```json
{
  "name": "My Name"
}
```
---
Now, on to...
* [Design](https://github.com/slalom-saa/stacks/blob/master/documents/1.%20Design/overview.md)
* [Development](https://github.com/slalom-saa/stacks/blob/master/documents/2.%20Development/overview.md) 