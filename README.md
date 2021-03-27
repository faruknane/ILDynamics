# ILDynamics
A Library for dynamic method operations!

## Features

### Method Generation
- Static Method Creation
- Parameter and Variable Definition anywhere 
- Reference Types
- Reference Operator
- Instance/Static Method Calls

### Method Cloner
The method cloning is possible with ILDynamics library. 
#### Filters
- **NoFilter:** Copies the current IL Code, no filter is applied.
- **MethodCallSwapper:** Swaps some method calls in a method with other methods specified.
- **ParameterRemover:** Can remove unused parameters. (for removing closure parameter of Action and Func purposes)

## Usages
- Static Method Creation
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

- Parameter and Variable Definition anywhere 
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

- Reference Types
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
- Reference Operator
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
- Instance Method Call
```csharp
Method<string> f = new Method<string>();
var a = f.NewParam<int>();
var tostr = typeof(int).GetMethod("ToString", Array.Empty<Type>());
f.Return(a.Call(tostr));
f.Create();
string val = f[5];
Assert.AreEqual(val, "5");
```
