[`< Back`](./)

---

# DbInstanceFixtureBase&lt;TInstance, TRepo&gt;

Namespace: Jlw.Utilities.Testing

```csharp
public class DbInstanceFixtureBase<TInstance, TRepo>
```

#### Type Parameters

`TInstance`<br>

`TRepo`<br>

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [DbInstanceFixtureBase&lt;TInstance, TRepo&gt;](./jlw.utilities.testing.dbinstancefixturebase-2)<br>
Attributes TestClassAttribute

## Properties

### **TestContext**

```csharp
public TestContext TestContext { get; set; }
```

#### Property Value

TestContext<br>

### **DbClient**

```csharp
public IModularDbClient DbClient { get; }
```

#### Property Value

IModularDbClient<br>

### **Instance**

```csharp
public TInstance Instance { get; }
```

#### Property Value

TInstance<br>

### **DefaultRepo**

```csharp
public TRepo DefaultRepo { get; }
```

#### Property Value

TRepo<br>

### **ConnectionString**

```csharp
public string ConnectionString { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### **DbInstanceFixtureBase()**

```csharp
public DbInstanceFixtureBase()
```

## Methods

### **InitializeFixture(String, TInstance, TRepo)**

```csharp
public void InitializeFixture(string connString, TInstance instance, TRepo repo)
```

#### Parameters

`connString` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`instance` TInstance<br>

`repo` TRepo<br>

---

[`< Back`](./)
