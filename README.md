# ILDynamics
A Library for dynamic method generation for .NET!

## Example Usages
- Supports Parameter and Variable definiton in everywhere!

```csharp
ILMethod f = new ILMethod(typeof(int));

var v = f.NewVariable(typeof(int));
v.Assign(f.Sum(f.Constant(2), f.Constant(3)));

var p = f.NewParameter(typeof(int));

f.Return(f.Sum(v, p));

var method = f.Create();

int val = (int)method.Invoke(null, new object[] { 10 }); // -> 15
```

```csharp
ILMethod f = new ILMethod(typeof(int));
var p = f.NewParameter(typeof(int));
var v = f.NewVariable(typeof(int));
v.Assign(f.Sum(p, f.Constant(2), f.Constant(3)));
f.Return(f.Sum(v, p));
var method = f.Create();

int val = (int)method.Invoke(null, new object[] { 10 }); // -> 25
```

- Supports Reference Types
```csharp
ILMethod f = new ILMethod(typeof(int));
var a = f.NewVariable(typeof(int));
a.Assign(f.Constant(5));

var b = f.CreateReference(a);
b.RefAssign(f.Constant(3));
f.Return(a);

var method = f.Create();

int val = (int)method.Invoke(null, new object[] { }); // -> 3
```
