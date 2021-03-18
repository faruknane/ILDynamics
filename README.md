# ILDynamics
A Library for dynamic method generation for .NET!

## Features & Usages
- Supports Static Method Creation
```csharp
Method<int> f = new Method<int>();
var p = f.NewParam<int>();
var v = f.NewVar<int>();

v.Assign(f.Add(p, f.Constant(2), f.Constant(3)));

f.Return(f.Add(v, p));

f.Create();

int val = f[10]; // execute the method!
Assert.AreEqual(val, 25);
```

- Supports Parameter and Variable Definition anywhere 
```csharp
Method<int> f = new Method<int>();
var v = f.NewVar<int>();

v.Assign(f.Add(f.Constant(2), f.Constant(3)));

var p = f.NewParam<int>();

f.Return(f.Add(v, p));

f.Create();

int val = f[10];
Assert.AreEqual(val, 15);
```

- Supports Reference Types
```csharp
Method<int> f = new Method<int>();
Var a = f.NewVar<int>();
a.Assign(f.Constant(5));

RefVar b = f.NewRefVar(a);
b.RefAssign(f.Constant(3));
f.Return(a);

f.Create();

int val = f[null];
Assert.AreEqual(val, 3);
```
- Supports Referance Operator
```csharp
Method<int> f = new Method<int>();
Var a = f.NewVar<int>();
a.Assign(f.Constant(5));

RefVar b = f.NewRefVar(a);
b.RefAssign(f.Constant(3));

Var c = f.NewVar<int>();
c.Assign(f.Constant(15));

b.Assign(f.GetRefByVar(c));
b.RefAssign(f.Constant(5));

f.Return(f.Add(a, c));

f.Create();

int val = f[null];
Assert.AreEqual(val, 8);
```
