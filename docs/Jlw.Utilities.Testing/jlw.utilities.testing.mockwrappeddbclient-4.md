[`< Back`](./)

---

# MockWrappedDbClient&lt;TConnection, TWrap, TCommand, TParam&gt;

Namespace: Jlw.Utilities.Testing

```csharp
public class MockWrappedDbClient<TConnection, TWrap, TCommand, TParam> : , Jlw.Utilities.Data.DbUtility.IModularDbClient, 
```

#### Type Parameters

`TConnection`<br>

`TWrap`<br>

`TCommand`<br>

`TParam`<br>

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → ModularDbClient&lt;TConnection&gt; → ModularDbClient&lt;TConnection, TWrap, TParam, DbConnectionStringBuilder&gt; → ModularDbClient&lt;TConnection, TWrap, TParam&gt; → [MockWrappedDbClient&lt;TConnection, TWrap, TCommand, TParam&gt;](./jlw.utilities.testing.mockwrappeddbclient-4)<br>
Implements IModularDbClient, IModularDbClient&lt;TConnection, TWrap, TParam, DbConnectionStringBuilder&gt;

## Properties

### **CommandTimeout**

```csharp
public int CommandTimeout { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

## Constructors

### **MockWrappedDbClient(String)**

```csharp
public MockWrappedDbClient(string path)
```

#### Parameters

`path` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

---

[`< Back`](./)
