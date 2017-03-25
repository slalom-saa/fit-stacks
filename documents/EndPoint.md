# Endpoint

An endpoint is a well defined unit of solution logic that is intended to be executed by:
1. An external client
2. An internal, but remote service
3. Another in-memory endpoint

## Creating an Endpoint
Getting to the point of implementing the endpoint, you should be provided with a 
service contract.  

For this example, we will use a very simple contract: [**Add Product**](add-product-endpoint.md).

### Open the Starter Solution
The empty shopping solution can be found [here](https://github.com/slalom-saa/stacks-shopping/tree/master/Empty).
It has a basic project setup with nothing more.

### Add the Slalom.Stacks nuget package
In the NuGet Package Manager enter the following command:
```
Install-Package Slalom.Stacks
```

### Add project folders
Add the following folders: **Application/Catalog/Products/Add**.

This may initially feel like a lot of folders.  It won't as the solution builds out.  Here is what the folders are for

*Application* - This is where the application logic resides for now.  As the project continues, it may make sense to split this out into another project or solution.

*Catalog* - This is the bounded context for the product catalog.  Again, it is best to keep these in the same project until it makes sense to split.

*Products* - This represents the service.  See the design standards section for why there is no class here.

*Add* - This represents the operation or endpoint.  Everything in this folder will be composed to implement the logic.

### Add the command
In the **add** folder, add a class for the request named **AddProductCommand**.
```csharp
public class AddProductCommand
{
    [NotNull("Name must be specified.")]
    public string Name { get; }

    public AddProductCommand(string name)
    {
        this.Name = name;
    }
}
```
There are only a few rules for commands:
1. The class must be immutable.  
  1.1. Properties should have no setter or use only private setters.  
  1.2. All properties should be set in the constructor.
  1.3. Very basic rules are used to validate.  Most logic should exist in external rules.
 