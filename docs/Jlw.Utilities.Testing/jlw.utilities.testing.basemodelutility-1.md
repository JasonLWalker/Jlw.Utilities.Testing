[`< Back`](./)

---

# BaseModelUtility&lt;TModel&gt;

Namespace: Jlw.Utilities.Testing

```csharp
public class BaseModelUtility<TModel>
```

#### Type Parameters

`TModel`<br>

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [BaseModelUtility&lt;TModel&gt;](./jlw.utilities.testing.basemodelutility-1)

## Constructors

### **BaseModelUtility()**

```csharp
public BaseModelUtility()
```

## Methods

### **GetFieldInfoByName&lt;T&gt;(String, BindingFlags)**

```csharp
public static FieldInfo GetFieldInfoByName<T>(string sMemberName, BindingFlags flags)
```

#### Type Parameters

`T`<br>

#### Parameters

`sMemberName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`flags` [BindingFlags](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.bindingflags)<br>

#### Returns

[FieldInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.fieldinfo)<br>

### **GetFieldInfoByName(String, BindingFlags)**

```csharp
public static FieldInfo GetFieldInfoByName(string sMemberName, BindingFlags flags)
```

#### Parameters

`sMemberName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`flags` [BindingFlags](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.bindingflags)<br>

#### Returns

[FieldInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.fieldinfo)<br>

### **GetPropertyInfoByName&lt;T&gt;(String, BindingFlags)**

```csharp
public static PropertyInfo GetPropertyInfoByName<T>(string sMemberName, BindingFlags flags)
```

#### Type Parameters

`T`<br>

#### Parameters

`sMemberName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`flags` [BindingFlags](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.bindingflags)<br>

#### Returns

[PropertyInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.propertyinfo)<br>

### **GetPropertyInfoByName(String, BindingFlags)**

```csharp
public static PropertyInfo GetPropertyInfoByName(string sMemberName, BindingFlags flags)
```

#### Parameters

`sMemberName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`flags` [BindingFlags](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.bindingflags)<br>

#### Returns

[PropertyInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.propertyinfo)<br>

### **GetPropertyValueByName&lt;T&gt;(T, String, BindingFlags)**

```csharp
public static object GetPropertyValueByName<T>(T o, string sMemberName, BindingFlags flags)
```

#### Type Parameters

`T`<br>

#### Parameters

`o` T<br>

`sMemberName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`flags` [BindingFlags](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.bindingflags)<br>

#### Returns

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

### **GetPropertyValueByName(TModel, String, BindingFlags)**

```csharp
public static object GetPropertyValueByName(TModel o, string sMemberName, BindingFlags flags)
```

#### Parameters

`o` TModel<br>

`sMemberName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`flags` [BindingFlags](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.bindingflags)<br>

#### Returns

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

### **SetPropertyValueByName&lt;T&gt;(T, String, Object, BindingFlags)**

```csharp
public static void SetPropertyValueByName<T>(T o, string sMemberName, object value, BindingFlags flags)
```

#### Type Parameters

`T`<br>

#### Parameters

`o` T<br>

`sMemberName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`value` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

`flags` [BindingFlags](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.bindingflags)<br>

### **SetPropertyValueByName(TModel, String, Object, BindingFlags)**

```csharp
public static void SetPropertyValueByName(TModel o, string sMemberName, object value, BindingFlags flags)
```

#### Parameters

`o` TModel<br>

`sMemberName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`value` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

`flags` [BindingFlags](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.bindingflags)<br>

### **GetDefaultConstructor&lt;T&gt;()**

```csharp
public static ConstructorInfo GetDefaultConstructor<T>()
```

#### Type Parameters

`T`<br>

#### Returns

[ConstructorInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.constructorinfo)<br>

### **GetDefaultConstructor()**

```csharp
public static ConstructorInfo GetDefaultConstructor()
```

#### Returns

[ConstructorInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.constructorinfo)<br>

### **AssertGetDefaultConstructor&lt;T&gt;()**

```csharp
public static ConstructorInfo AssertGetDefaultConstructor<T>()
```

#### Type Parameters

`T`<br>

#### Returns

[ConstructorInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.constructorinfo)<br>

### **AssertGetDefaultConstructor()**

```csharp
public static ConstructorInfo AssertGetDefaultConstructor()
```

#### Returns

[ConstructorInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.constructorinfo)<br>

### **GetObjectInstance&lt;T&gt;()**

```csharp
public static T GetObjectInstance<T>()
```

#### Type Parameters

`T`<br>

#### Returns

T<br>

### **AssertGetObjectInstance&lt;T&gt;()**

```csharp
public static T AssertGetObjectInstance<T>()
```

#### Type Parameters

`T`<br>

#### Returns

T<br>

### **AssertFieldExists(String)**

```csharp
public FieldInfo AssertFieldExists(string sMemberName)
```

#### Parameters

`sMemberName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[FieldInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.fieldinfo)<br>

### **AssertPropertyExists(String)**

```csharp
public PropertyInfo AssertPropertyExists(string sMemberName)
```

#### Parameters

`sMemberName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[PropertyInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.propertyinfo)<br>

### **AssertGetFieldInfoByName(String, BindingFlags)**

```csharp
public FieldInfo AssertGetFieldInfoByName(string sMemberName, BindingFlags flags)
```

#### Parameters

`sMemberName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`flags` [BindingFlags](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.bindingflags)<br>

#### Returns

[FieldInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.fieldinfo)<br>

### **AssertGetPropertyInfoByName(String, BindingFlags)**

```csharp
public PropertyInfo AssertGetPropertyInfoByName(string sMemberName, BindingFlags flags)
```

#### Parameters

`sMemberName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`flags` [BindingFlags](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.bindingflags)<br>

#### Returns

[PropertyInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.propertyinfo)<br>

### **AssertPropertyIsReadable(String)**

```csharp
public PropertyInfo AssertPropertyIsReadable(string sMemberName)
```

#### Parameters

`sMemberName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[PropertyInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.propertyinfo)<br>

### **AssertPropertyIsWritable(String)**

```csharp
public PropertyInfo AssertPropertyIsWritable(string sMemberName)
```

#### Parameters

`sMemberName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[PropertyInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.propertyinfo)<br>

### **AssertPropertyGet(String, BindingFlags)**

```csharp
public object AssertPropertyGet(string sMemberName, BindingFlags flags)
```

#### Parameters

`sMemberName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`flags` [BindingFlags](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.bindingflags)<br>

#### Returns

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

### **AssertPropertySet(String, BindingFlags)**

```csharp
public object AssertPropertySet(string sMemberName, BindingFlags flags)
```

#### Parameters

`sMemberName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`flags` [BindingFlags](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.bindingflags)<br>

#### Returns

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

### **AssertPropertyScopeForGetAccessor(String, MethodAttributes)**

```csharp
public PropertyInfo AssertPropertyScopeForGetAccessor(string sMemberName, MethodAttributes attrs)
```

#### Parameters

`sMemberName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`attrs` [MethodAttributes](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodattributes)<br>

#### Returns

[PropertyInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.propertyinfo)<br>

### **AssertPropertyScopeForSetAccessor(String, MethodAttributes)**

```csharp
public PropertyInfo AssertPropertyScopeForSetAccessor(string sMemberName, MethodAttributes attrs)
```

#### Parameters

`sMemberName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`attrs` [MethodAttributes](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodattributes)<br>

#### Returns

[PropertyInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.propertyinfo)<br>

### **AssertAccessScopeForMethodAttributes(MethodInfo, MethodAttributes)**

```csharp
public void AssertAccessScopeForMethodAttributes(MethodInfo m, MethodAttributes attrs)
```

#### Parameters

`m` [MethodInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodinfo)<br>

`attrs` [MethodAttributes](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodattributes)<br>

### **AssertSetPropertyValueByName(TModel, String, Object)**

```csharp
public void AssertSetPropertyValueByName(TModel o, string sMemberName, object value)
```

#### Parameters

`o` TModel<br>

`sMemberName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`value` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

### **AssertGetPropertyValueByName(TModel, String, BindingFlags)**

```csharp
public object AssertGetPropertyValueByName(TModel o, string sMemberName, BindingFlags flags)
```

#### Parameters

`o` TModel<br>

`sMemberName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`flags` [BindingFlags](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.bindingflags)<br>

#### Returns

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

### **AssertTypeAssignmentForObjectProperty(TModel, String, Type, BindingFlags)**

```csharp
public void AssertTypeAssignmentForObjectProperty(TModel o, string sMemberName, Type type, BindingFlags flags)
```

#### Parameters

`o` TModel<br>

`sMemberName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`type` [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>

`flags` [BindingFlags](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.bindingflags)<br>

### **AssertInstanceImplementsType(Object)**

```csharp
public void AssertInstanceImplementsType(object instance)
```

#### Parameters

`instance` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

### **Should_BeInstanceOf(Type)**

```csharp
public void Should_BeInstanceOf(Type t)
```

#### Parameters

`t` [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>

### **GetPropertyAccess(MethodAttributes, MethodAttributes)**

```csharp
public static AccessModifiers GetPropertyAccess(MethodAttributes getAttr, MethodAttributes setAttr)
```

#### Parameters

`getAttr` [MethodAttributes](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodattributes)<br>

`setAttr` [MethodAttributes](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodattributes)<br>

#### Returns

[AccessModifiers](./jlw.utilities.testing.accessmodifiers)<br>

---

[`< Back`](./)
