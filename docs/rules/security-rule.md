# Security Rule Guidelines

This section contains rules for security rules and uses the user must be registered example.

```csharp
/// <summary>
/// Validates that a user is registered.
/// </summary>
public class user_must_be_registered : SecurityRule<AddProductCommand>
{
    /// <inheritdoc />
    public override ValidationError Validate(AddProductCommand instance)
    {
        // TODO: perform validation here
        return new ValidationError("UserNotRegistered", "You must be registered to submit a product.");
    }
}
```

The class summary should be descriptive enough for documentation.  It will show up in discovery documents.
It should not contain information on how it is implemented - only what it validates.
```csharp
/// <summary>
/// Validates that a user is registered.
/// </summary>
```

Class names should be written in specification case.  This aids in understanding and discovery documents.
```csharp
user_must_be_registered
```

The class should inherit from SecurityRule<> and the type argument should be the command.
```csharp
SecurityRule<AddProductCommand>
```

If the logic does not call async methods then the Validate method should be overridden.
```csharp
public override ValidationError Validate(AddProductCommand instance)
{
    // TODO: perform validation here
    return new ValidationError("UserNotRegistered", "You must be registered to submit a product.");
}
```
The error code should not contain any punctuation or spaces.  It is used by the UI to generate custom messages.
```csharp
"UserNotRegistered"
```
The message should be a well-written, user-facing message with a period at the end.
```csharp
"You must be registered to submit a product."
```

Access to the current user should be done through the parent class Request property.
```csharp
if (this.Request.User.Identity.IsAuthenticated)
{
}
```
Null should be returned if there are no validation errors.
```csharp
public override ValidationError Validate(AddProductCommand instance)
{
    return null;
}
```