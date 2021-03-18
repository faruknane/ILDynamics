# ILDynamics
A Library for dynamic method generation for .NET!

## Example Usages
- Supports Static Method Creation
```csharp
StaticMethod f = new StaticMethod(typeof(int));
var p = f.NewParam(typeof(int));
var v = f.NewVar(typeof(int));

v.Assign(f.Sum(p, f.Constant(2), f.Constant(3)));

f.Return(f.Sum(v, p));

f.Create();

int val = (int)f[10]; // execute the method!
Assert.AreEqual(val, 25);
```

- Supports Parameter and Variable definition anywhere 
```csharp
StaticMethod f = new StaticMethod(typeof(int));
var v = f.NewVar(typeof(int));

v.Assign(f.Sum(f.Constant(2), f.Constant(3)));

var p = f.NewParam(typeof(int));

f.Return(f.Sum(v, p));

f.Create();

int val = (int)f[10];
Assert.AreEqual(val, 15);
```

- Supports Reference Types
```csharp
StaticMethod f = new StaticMethod(typeof(int));
var a = f.NewVar(typeof(int));
a.Assign(f.Constant(5));

var b = f.NewRefVar(a);
b.RefAssign(f.Constant(3));
f.Return(a);

f.Create();

int val = (int)f[null];
Assert.AreEqual(val, 3);
```
- Supports Referance operator
```
StaticMethod f = new StaticMethod(typeof(int));
var a = f.NewVar(typeof(int));
a.Assign(f.Constant(5));

var b = f.NewRefVar(a);
b.RefAssign(f.Constant(3));

var c = f.NewVar(typeof(int));
c.Assign(f.Constant(15));

b.Assign(f.GetRefByVar(c));
b.RefAssign(f.Constant(5));

f.Return(f.Sum(a, c));

f.Create();

int val = (int)f[null];
Assert.AreEqual(val, 8);
```
