# Slalom Stacks

[![Build status](https://ci.appveyor.com/api/projects/status/6nb0ud2cpm4rkuyx/branch/master?svg=true)](https://ci.appveyor.com/project/slalom-saa/stacks/branch/master)   [![NuGet Version](http://img.shields.io/nuget/v/Slalom.Stacks.svg?style=flat)](https://www.nuget.org/packages/Slalom.Stacks/)

## Getting Started
1. Create a new .NET Core console application named **HelloWorldService** in **Visual Studio 2015**.
2. Update the framework in project.json to use **.NET 4.6.1** (other frameworks will be supported soon).  The file should look like:
```json
{
  "version": "1.0.0-*",
  "buildOptions": {
    "emitEntryPoint": true,
    "xmlDoc": true
  },
  "dependencies": {
  },
  "frameworks": {
    "net461": {
      "imports": "dnxcore50"
    }
  }
}
``` 
3.	Add the **Slalom.Stacks** NuGet package.  

```
Install-Package Slalom.Stacks
```
4.	Create a class named **HelloWorldRequest**:
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
5.	Create an endpoint named **HelloWorld**: 
```csharp
public class HelloWorld : EndPoint<HelloWorldRequest>
{
    public override void Receive(HelloWorldRequest instance)
    {
        Console.WriteLine("Hello " + instance.Name + "!");
    }
}
```	
6.	Initialize a new Stack and send the message:
```csharp
public static void Main(string[] args)
{
    using (var stack = new Stack())
    {
        stack.Send(new HelloWorldRequest("Fred")).Wait();
    }
}
```	
7. Run the application.  The output should be:
```console
Hello Fred!
Press any key to continue...
```
Your final **Program.cs** file should look like:
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Services;

namespace HelloWorldService
{
    public class HelloWorldRequest
    {
        public string Name { get; }

        public HelloWorldRequest(string name)
        {
            this.Name = name;
        }
    }

    public class HelloWorld : EndPoint<HelloWorldRequest>
    {
        public override void Receive(HelloWorldRequest instance)
        {
            Console.WriteLine("Hello " + instance.Name + "!");
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            using (var stack = new Stack())
            {
                stack.Send(new HelloWorldRequest("Fred")).Wait();
            }
        }
    }
}
```

Now, on to [hosting](https://github.com/slalom-saa/stacks-aspnetcore/blob/master/README.md)...