### Hello World Example

The following example demonstrates the quickest way to get something up and running.  For a 
more real-world example, try the [Stacks Walkthrough](walkthrough/overview.md)  .

1. Create a new .NET Core console application named **HelloWorldService** in **Visual Studio 2017**.
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
4.	Create an endpoint named **HelloWorld**.
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
> There will be some references that you need to resolve.  

> **At this time, not all references are immediately available in VS 2017.  You may need to restart your Visual Studio instance when you add the next section.**
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
9. Use PostMan to call http://localhost:5000/_system/requests and view the requests.
10. Use PostMan to call http://localhost:5000/_system/responses and view all service responses.
