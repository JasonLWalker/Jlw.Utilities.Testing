[`< Back`](./)

---

# Int64SourceAttribute

Namespace: Jlw.Utilities.Testing.DataSources

```csharp
public class Int64SourceAttribute : DataSourceAttributeBase, Microsoft.VisualStudio.TestTools.UnitTesting.ITestDataSource
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Attribute](https://docs.microsoft.com/en-us/dotnet/api/system.attribute) → [DataSourceAttributeBase](./jlw.utilities.testing.datasources.datasourceattributebase) → [Int64SourceAttribute](./jlw.utilities.testing.datasources.int64sourceattribute)<br>
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

### **Int64SourceAttribute()**

```csharp
public Int64SourceAttribute()
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
