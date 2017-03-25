# Slalom Stacks

[![Build status](https://ci.appveyor.com/api/projects/status/6nb0ud2cpm4rkuyx/branch/master?svg=true)](https://ci.appveyor.com/project/slalom-saa/stacks/branch/master)   [![NuGet Version](http://img.shields.io/nuget/v/Slalom.Stacks.svg?style=flat)](https://www.nuget.org/packages/Slalom.Stacks/)

## Getting Started
1. Create a new .NET Core console application named **HelloWorldService** in **Visual Studio 2015**.
2.	Add the **Slalom.Stacks** NuGet package.  

```
Install-Package Slalom.Stacks
```
3.	Create a class named **HelloWorldRequest**:
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
4.	Create an endpoint named **HelloWorld**: 
```csharp
public class HelloWorld : EndPoint<HelloWorldRequest>
{
    public override void Receive(HelloWorldRequest instance)
    {
        Console.WriteLine("Hello " + instance.Name + "!");
    }
}
```	
5.	Initialize a new Stack and send the message:
```csharp
public static void Main(string[] args)
{
    using (var stack = new Stack())
    {
        stack.Send(new HelloWorldRequest("Fred")).Wait();
    }
}
```	
6. Run the application.  The output should be:
```console
Hello Fred!
Press any key to continue...
```
Your final **Program.cs** file should look like:
```csharp
using System;
using System.Linq;
using Slalom.Stacks;
using Slalom.Stacks.Messaging;

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
---
Now, on to...
* [hosting](https://github.com/slalom-saa/stacks-aspnetcore/blob/master/README.md)
* [A more in-depth and real-world look at endpoint](https://github.com/slalom-saa/stacks/blob/develop/documents/EndPoint.md)