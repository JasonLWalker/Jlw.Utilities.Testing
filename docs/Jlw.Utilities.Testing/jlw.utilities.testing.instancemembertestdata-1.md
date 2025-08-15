[`< Back`](./)

---

# InstanceMemberTestData&lt;T&gt;

Namespace: Jlw.Utilities.Testing

```csharp
public class InstanceMemberTestData<T>
```

#### Type Parameters

`T`<br>

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [InstanceMemberTestData&lt;T&gt;](./jlw.utilities.testing.instancemembertestdata-1)

## Properties

### **SystemUnderTest**

```csharp
public T SystemUnderTest { get; protected set; }
```

#### Property Value

T<br>

### **MemberName**

```csharp
public string MemberName { get; protected set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ExpectedValue**

```csharp
public object ExpectedValue { get; protected set; }
```

#### Property Value

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

## Constructors

### **InstanceMemberTestData(T, String, Object, String, String)**

```csharp
public InstanceMemberTestData(T sut, string memberName, object expectedValue, string testDescription, string sutDescription)
```

#### Parameters

`sut` T<br>

`memberName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`expectedValue` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

`testDescription` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`sutDescription` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Methods

### **ToString()**

```csharp
public string ToString()
```

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

---

[`< Back`](./)
