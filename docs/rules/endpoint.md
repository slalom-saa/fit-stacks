# Endpoint Guidelines

This section contains rules for endpoints and uses the Add Product Endpoint example.

```csharp
/// <summary>
/// Adds a product to the product catalog so that a user can search for it and it can be added to a cart, purchased and/or shipped.
/// </summary>
[EndPoint("catalog/products/add", Name = "Add Product", Timeout = 5000, Version = 1)]
public class AddProduct : EndPoint<AddProductCommand, string>
{
    public override string Receive(AddProductCommand instance)
    {
        // TODO: add logic here

        // return the ID
        return "[Added Product ID]";
    }
}
```

The name of the use case should be used to name the class.
```csharp
class AddProduct
```
If there are multiple version endpoints, then a suffix can be added.
```csharp
class AddProduct_v2
```
When an endpoint has a suffix an endpoint attribute should also be used to specify the name.
```csharp
[EndPoint("catalog/products/add", Name = "Add Product", Timeout = 5000, Version = 1)]
public class AddProduct_v2
```
If the endpoint returns a value, it should use two type arguments: the first is the request and the 
second is the response.
```csharp
EndPoint<AddProductCommand, string>
```
If the endpoint does not return a value, it should use one type arguments: the first is the request.
```csharp
EndPoint<AddProductCommand>
```
The summary should come directly from the service contract.  This texte will be used in swagger and other discovery documents.
```csharp
/// <summary>
/// Adds a product to the product catalog so that a user can search for it and it can be added to a cart, purchased and/or shipped.
/// </summary>
```
If the the endpoint does not call any async code then it should override the Receive method.
```csharp
public override string Receive(AddProductCommand instance)
{
    // TODO: add logic here

    // TODO: return the ID
    return "[Added Product ID]";
}
```
If the the endpoint does any async code then it should override the ReceiveAsync method.
```csharp
public override async Task<string> ReceiveAsync(AddProductCommand instance)
{
    // TODO: add logic here
    await Task.Delay(500);

    // TODO: return the ID
    return "[Added Product ID]";
}
```
The path should be specified using the endpoint attribute.
```csharp
[EndPoint("catalog/products/add")]
```
A timeout should be specified for the endpoint.  This information should be specified in the service contract
and will display in discovery documents.
```csharp
[EndPoint("catalog/products/add", Timeout = 5000)]
```
If the endpoint is not version one, then a version should be specified.  The 
router will use this information to choose the appropriate endpoint.
```csharp
[EndPoint("catalog/products/add", Version = 2)]
```