# External Command Guidelines

This section contains rules for external commands and uses the 
send notification example.

```csharp
[Request("/notifications/send")]
public class SendNotification
{
    public string Email { get; }

    public string Message { get; }

    public SendNotification(string email, string message)
    {
        this.Email = email;
        this.Message = message;
    }
}
```

All message properties should be immutable.  They should not contain a
setter or the setter should be private.
```csharp
public string Email { get; }
```
There should be no fields in a message.
All properties should be set in the constructor.  Overrides can be used if a parameter is optional.
```csharp
public SendNotification(string email, string message)
{
    this.Email = email;
    this.Message = message;
}
```
There should be no validation on properties.  This is done on the receiving end.
```csharp
[NotNull("Name must be specified.")]
```
There should be a request attribute that specifies the path.  This is what is used to route.
```csharp
[Request("/notifications/send")]
```