[`< Back`](./)

---

# KvpObjectInt64SourceAttribute

Namespace: Jlw.Utilities.Testing.DataSources

```csharp
public class KvpObjectInt64SourceAttribute : DataSourceAttributeBase, Microsoft.VisualStudio.TestTools.UnitTesting.ITestDataSource
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Attribute](https://docs.microsoft.com/en-us/dotnet/api/system.attribute) → [DataSourceAttributeBase](./jlw.utilities.testing.datasources.datasourceattributebase) → [KvpObjectInt64SourceAttribute](./jlw.utilities.testing.datasources.kvpobjectint64sourceattribute)<br>
Implements ITestDataSource<br>
Attributes [AttributeUsageAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.attributeusageattribute)

## Properties

### **TypeId**

```csharp
public object TypeId { get; }
```

#### Property Value

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

## Constructors

### **KvpObjectInt64SourceAttribute()**

```csharp
public KvpObjectInt64SourceAttribute()
```

## Methods

### **GetData(MethodInfo)**

```csharp
public IEnumerable<Object[]> GetData(MethodInfo methodInfo)
```

#### Parameters

`methodInfo` [MethodInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodinfo)<br>

#### Returns

[IEnumerable&lt;Object[]&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

---

[`< Back`](./)
