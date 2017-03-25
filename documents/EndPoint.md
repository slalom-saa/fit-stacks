# Endpoint

An endpoint is a well defined unit of solution logic that is intended to be executed by:
1. An external client
2. An internal, but remote service
3. Another in-memory endpoint

## Creating an Endpoint
Getting to the point of implementing the endpoint, you should be provided with a 
service contract.  

For this example, we will use a very simple contract: **Add Product**.
<div style="border:1px solid #ccc;padding:10px">

**Name**:  Add Product

**Path**:  catalog/products/add

**Version**: 1

**Timeout**: 5 seconds

**Summary**: Adds product to the product catalog.

**Input**:

| Name | Description |  Validation |
| ---- | ----------- |
| Name | The name of the product to add. | Must not be null. |

**Output**: Returns a simple string that represents the ID of the added product.
</div>

To implement this endpoint.
1. Open up the empty shopping solution found [here](https://github.com/slalom-saa/stacks-shopping/tree/master/Empty).
2. Add the Slalom.Stacks nuget package.
```
Install-Package Slalom.Stacks
```
3. Add the following folders: **Application/Catalog/Products/Add**.
> This may initially feel like a lot of folders.  It won't as the solution builds out.  
>
> Here is what the folders are for
>
> Application - This is where the application logic resides for now.  As the project continues, it may make sense to split this out into another project or solution.
>
> Catalog - This is the bounded context for the product catalog.  Again, it is best to keep these in the same project until it makes sense to split.
>
> Products - This represents the service.  See the design standards section for why there is no class here.
>
> Add - This represents the operation or endpoint.  Everything in this folder will be composed to implement the logic.

4. In the **add** folder, add a class for the request named **AddProductRequest**.
 