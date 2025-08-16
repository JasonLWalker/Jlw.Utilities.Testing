[`< Back`](./)

---

# MockFailedDbCommand&lt;TCommand&gt;

Namespace: Jlw.Utilities.Testing

```csharp
public class MockFailedDbCommand<TCommand> : MockWrappedDbCommand`1, System.Data.IDbCommand, System.IDisposable
```

#### Type Parameters

`TCommand`<br>

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [WrappedDbCommand](./jlw.utilities.testing.wrappeddbcommand) → MockWrappedDbCommand&lt;TCommand&gt; → [MockFailedDbCommand&lt;TCommand&gt;](./jlw.utilities.testing.mockfaileddbcommand-1)<br>
Implements IDbCommand, [IDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.idisposable)

## Properties

### **CommandTimeout**

```csharp
public int CommandTimeout { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **CommandType**

```csharp
public CommandType CommandType { get; set; }
```

#### Property Value

CommandType<br>

### **Parameters**

```csharp
public IDataParameterCollection Parameters { get; }
```

#### Property Value

IDataParameterCollection<br>

### **Transaction**

```csharp
public IDbTransaction Transaction { get; set; }
```

#### Property Value

IDbTransaction<br>

### **UpdatedRowSource**

```csharp
public UpdateRowSource UpdatedRowSource { get; set; }
```

#### Property Value

UpdateRowSource<br>

### **CommandText**

```csharp
public string CommandText { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Connection**

```csharp
public IDbConnection Connection { get; set; }
```

#### Property Value

IDbConnection<br>

### **DataPath**

```csharp
public string DataPath { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **DbCommand**

```csharp
public IDbCommand DbCommand { get; set; }
```

#### Property Value

IDbCommand<br>

## Constructors

### **MockFailedDbCommand()**

```csharp
public MockFailedDbCommand()
```

---

[`< Back`](./)
