# Business Rule Guidelines

This section contains rules for business rules and uses the name must be unique example.

```csharp
/// <summary>
/// Validates that the product name is unique.
/// </summary>
public class name_must_be_unique : BusinessRule<AddProductCommand>
{
    /// <inheritdoc />
    public override ValidationError Validate(AddProductCommand instance)
    {
        // TODO: perform validation here
        return new ValidationError("NameNotUnique", "A product with the same name already exists.");
    }
}
```

The class summary should be descriptive enough for documentation.  It will show up in discovery documents.
It should not contain information on how it is implemented - only what it validates.
```csharp
/// <summary>
/// Validates that the product name is unique.
/// </summary>
```

Class names should be written in specification case.  This aids in understanding and discovery documents.
```csharp
name_must_be_unique
```

The class should inherit from SecurityRule<> and the type argument should be the command.
```csharp
BusinessRule<AddProductCommand>
```

If the logic does not call async methods then the Validate method should be overridden.
```csharp
public override ValidationError Validate(AddProductCommand instance)
{
    // TODO: perform validation here
    return new ValidationError("NameNotUnique", "A product with the same name already exists.");
}
```

If the logic does call async methods then the ValidateAsync method should be overridden.
```csharp
public override async Task<ValidationError> ValidateAsync(AddProductCommand instance)
{
    // TODO: perform validation here
    await Task.Delay(500);

    return new ValidationError("NameNotUnique", "A product with the same name already exists.");
}
```

The error code should not contain any punctuation or spaces.  It is used by the UI to generate custom messages.
```csharp
"NameNotUnique"
```
The message should be a well-written, user-facing message with a period at the end.
```csharp
"A product with the same name already exists."
```

Access to domain data can be done using the base class Domain property.
```csharp
if (await this.Domain.Exists<Product>(e => e.Name == instance.Name))
{
    return new ValidationError("NameNotUnique", "A product with the same name already exists.");
}
```
Null should be returned if there are no validation errors.
```csharp
public override ValidationError Validate(AddProductCommand instance)
{
    return null;
}
```