# Command Guidelines

This section contains rules for commands and uses the 
add product command example.

```csharp
public class AddProductCommand
{
    /// <value>The name of the product to add.</value>
    [NotNull("Name must be specified.")]
    public string Name { get; }

    public AddProductCommand(string name)
    {
        this.Name = name;
    }
}
```

Requests that change state should end in "Command".
```csharp
AddProductCommand
```

All message properties should be immutable.  They should not contain a
setter or the setter should be private.
```csharp
public string Name { get; }
```
There should be no fields in a message.
All properties should be set in the constructor.  Overrides can be used if a parameter is optional.
```csharp
public AddProductCommand(string name)
{
    this.Name = name;
}
```
The value comments should be added for the property name.  This will show up in swagger and other discovery documents.
Other comments should be added, but will not be used in service documents.
```csharp
/// <value>The name of the product to add.</value>
```
Only basic validation should be used to indicate that the command was not serialized or deserialized properly. Most rules will be
external: business or security.
```csharp
[NotNull("Name must be specified.")]
```