[`< Back`](./)

---

# RandomStringSourceAttribute

Namespace: Jlw.Utilities.Testing.DataSources

```csharp
public class RandomStringSourceAttribute : DataSourceAttributeBase, Microsoft.VisualStudio.TestTools.UnitTesting.ITestDataSource
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Attribute](https://docs.microsoft.com/en-us/dotnet/api/system.attribute) → [DataSourceAttributeBase](./jlw.utilities.testing.datasources.datasourceattributebase) → [RandomStringSourceAttribute](./jlw.utilities.testing.datasources.randomstringsourceattribute)<br>
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

### **RandomStringSourceAttribute()**

```csharp
public RandomStringSourceAttribute()
```

### **RandomStringSourceAttribute(Int32, Int32, String)**

```csharp
public RandomStringSourceAttribute(int numStrings, int length, string validChars)
```

#### Parameters

`numStrings` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`length` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`validChars` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **RandomStringSourceAttribute(Int32, Int32, Int32, String)**

```csharp
public RandomStringSourceAttribute(int numStrings, int minLength, int maxLength, string validChars)
```

#### Parameters

`numStrings` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`minLength` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`maxLength` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`validChars` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

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
