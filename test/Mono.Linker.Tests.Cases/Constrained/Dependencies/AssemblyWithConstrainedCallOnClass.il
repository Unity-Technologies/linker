.assembly extern mscorlib
{
}
.assembly 'library'
{
  .hash algorithm 0x00008004
  .ver 0:0:0:0
}
.module library.dll

// =============== CLASS MEMBERS DECLARATION ===================

.class public auto ansi beforefieldinit Mono.Linker.Tests.Cases.Constrained.Dependencies.AssemblyWithConstrainedCallOnClass
       extends [mscorlib]System.Object
{
  .class abstract auto ansi nested public beforefieldinit Base
         extends [mscorlib]System.Object
  {
    .method public hidebysig newslot virtual 
            instance void  Method() cil managed
    {
      // Code size       1 (0x1)
      .maxstack  8
      IL_0000:  ret
    } // end of method Base::Method

    .method family hidebysig specialname rtspecialname 
            instance void  .ctor() cil managed
    {
      // Code size       7 (0x7)
      .maxstack  8
      IL_0000:  ldarg.0
      IL_0001:  call       instance void [mscorlib]System.Object::.ctor()
      IL_0006:  ret
    } // end of method Base::.ctor

  } // end of class Base

  .class auto ansi nested public beforefieldinit One
         extends Mono.Linker.Tests.Cases.Constrained.Dependencies.AssemblyWithConstrainedCallOnClass/Base
  {
    .method public hidebysig virtual instance void 
            Method() cil managed
    {
      // Code size       1 (0x1)
      .maxstack  8
      IL_0000:  ret
    } // end of method One::Method

    .method public hidebysig specialname rtspecialname 
            instance void  .ctor() cil managed
    {
      // Code size       7 (0x7)
      .maxstack  8
      IL_0000:  ldarg.0
      IL_0001:  call       instance void Mono.Linker.Tests.Cases.Constrained.Dependencies.AssemblyWithConstrainedCallOnClass/Base::.ctor()
      IL_0006:  ret
    } // end of method One::.ctor

  } // end of class One

  .class auto ansi nested public beforefieldinit Two
         extends Mono.Linker.Tests.Cases.Constrained.Dependencies.AssemblyWithConstrainedCallOnClass/Base
  {
    .method public hidebysig virtual instance void 
            Method() cil managed
    {
      // Code size       1 (0x1)
      .maxstack  8
      IL_0000:  ret
    } // end of method Two::Method

    .method public hidebysig specialname rtspecialname 
            instance void  .ctor() cil managed
    {
      // Code size       7 (0x7)
      .maxstack  8
      IL_0000:  ldarg.0
      IL_0001:  call       instance void Mono.Linker.Tests.Cases.Constrained.Dependencies.AssemblyWithConstrainedCallOnClass/Base::.ctor()
      IL_0006:  ret
    } // end of method Two::.ctor

  } // end of class Two

  .method public hidebysig static void  UseTheCode() cil managed
  {
    // Code size       24 (0x18)
    .maxstack  1
    .locals init (class Mono.Linker.Tests.Cases.Constrained.Dependencies.AssemblyWithConstrainedCallOnClass/One V_0)
    IL_0000:  newobj     instance void Mono.Linker.Tests.Cases.Constrained.Dependencies.AssemblyWithConstrainedCallOnClass/One::.ctor()
    IL_0005:  stloc.0
    IL_0006:  ldloca.s   V_0
    IL_0008:  call       void Mono.Linker.Tests.Cases.Constrained.Dependencies.AssemblyWithConstrainedCallOnClass::UseFoo(class Mono.Linker.Tests.Cases.Constrained.Dependencies.AssemblyWithConstrainedCallOnClass/One&)
    IL_000d:  newobj     instance void Mono.Linker.Tests.Cases.Constrained.Dependencies.AssemblyWithConstrainedCallOnClass/Two::.ctor()
    IL_0012:  call       void Mono.Linker.Tests.Cases.Constrained.Dependencies.AssemblyWithConstrainedCallOnClass::UseOther(class Mono.Linker.Tests.Cases.Constrained.Dependencies.AssemblyWithConstrainedCallOnClass/Two)
    IL_0017:  ret
  } // end of method AssemblyWithConstrainedCallOnClass::UseTheCode

  .method private hidebysig static void  UseFoo(class Mono.Linker.Tests.Cases.Constrained.Dependencies.AssemblyWithConstrainedCallOnClass/One& f) cil managed
  {
    // Code size       8 (0x8)
    .maxstack  8
    IL_0000:  ldarg.0
    IL_0001:  constrained. Mono.Linker.Tests.Cases.Constrained.Dependencies.AssemblyWithConstrainedCallOnClass/One
    IL_0007:  callvirt   instance void Mono.Linker.Tests.Cases.Constrained.Dependencies.AssemblyWithConstrainedCallOnClass/Base::Method()
    IL_000c:  ret
  } // end of method AssemblyWithConstrainedCallOnClass::UseFoo

  .method private hidebysig static void  UseOther(class Mono.Linker.Tests.Cases.Constrained.Dependencies.AssemblyWithConstrainedCallOnClass/Two b) cil managed
  {
    // Code size       1 (0x1)
    .maxstack  8
    IL_0000:  ret
  } // end of method AssemblyWithConstrainedCallOnClass::UseOther

  .method public hidebysig specialname rtspecialname 
          instance void  .ctor() cil managed
  {
    // Code size       7 (0x7)
    .maxstack  8
    IL_0000:  ldarg.0
    IL_0001:  call       instance void [mscorlib]System.Object::.ctor()
    IL_0006:  ret
  } // end of method AssemblyWithConstrainedCallOnClass::.ctor

} // end of class Mono.Linker.Tests.Cases.Constrained.Dependencies.AssemblyWithConstrainedCallOnClass


// =============================================================

// *********** DISASSEMBLY COMPLETE ***********************
// WARNING: Created Win32 resource file F:\Unity\dev\scratch\AssemblyWithConstrainedCallOnClass.res
