[`< Back`](./)

---

# MockWrappedDbCommand&lt;TCommand&gt;

Namespace: Jlw.Utilities.Testing

```csharp
public class MockWrappedDbCommand<TCommand> : WrappedDbCommand, System.Data.IDbCommand, System.IDisposable
```

#### Type Parameters

`TCommand`<br>

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [WrappedDbCommand](./jlw.utilities.testing.wrappeddbcommand) → [MockWrappedDbCommand&lt;TCommand&gt;](./jlw.utilities.testing.mockwrappeddbcommand-1)<br>
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

### **MockWrappedDbCommand()**

```csharp
public MockWrappedDbCommand()
```

## Methods

### **Cancel()**

```csharp
public void Cancel()
```

### **CreateParameter()**

```csharp
public IDbDataParameter CreateParameter()
```

#### Returns

IDbDataParameter<br>

### **ExecuteNonQuery()**

```csharp
public int ExecuteNonQuery()
```

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **ExecuteScalar()**

```csharp
public object ExecuteScalar()
```

#### Returns

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

### **Prepare()**

```csharp
public void Prepare()
```

### **ExecuteReader()**

```csharp
public IDataReader ExecuteReader()
```

#### Returns

IDataReader<br>

### **ExecuteReader(CommandBehavior)**

```csharp
public IDataReader ExecuteReader(CommandBehavior behavior)
```

#### Parameters

`behavior` CommandBehavior<br>

#### Returns

IDataReader<br>

### **Dispose()**

```csharp
public void Dispose()
```

---

[`< Back`](./)
