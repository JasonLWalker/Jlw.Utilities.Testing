[`< Back`](./)

---

# SqlLocalDbInstanceFixtureBase&lt;TRepo&gt;

Namespace: Jlw.Utilities.Testing

```csharp
public class SqlLocalDbInstanceFixtureBase<TRepo> : DbInstanceFixtureBase`2
```

#### Type Parameters

`TRepo`<br>

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → DbInstanceFixtureBase&lt;TemporarySqlLocalDbInstance, TRepo&gt; → [SqlLocalDbInstanceFixtureBase&lt;TRepo&gt;](./jlw.utilities.testing.sqllocaldbinstancefixturebase-1)<br>
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
public TemporarySqlLocalDbInstance Instance { get; }
```

#### Property Value

TemporarySqlLocalDbInstance<br>

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

### **SqlLocalDbInstanceFixtureBase()**

```csharp
public SqlLocalDbInstanceFixtureBase()
```

## Methods

### **TestInitialize()**

```csharp
public void TestInitialize()
```

### **TestCleanup()**

```csharp
public void TestCleanup()
```

---

[`< Back`](./)
