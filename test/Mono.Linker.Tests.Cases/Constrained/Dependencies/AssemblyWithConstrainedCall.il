
//  Microsoft (R) .NET Framework IL Disassembler.  Version 4.0.30319.1
//  Copyright (c) Microsoft Corporation.  All rights reserved.



// Metadata version: v4.0.30319
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

.class public auto ansi beforefieldinit Mono.Linker.Tests.Cases.Constrained.Dependencies.AssemblyWithConstrainedCall
       extends [mscorlib]System.Object
{
  .class interface abstract auto ansi nested private IFoo
  {
    .method public hidebysig newslot abstract virtual 
            instance int32  Method() cil managed
    {
    } // end of method IFoo::Method

  } // end of class IFoo

  .class sequential ansi sealed nested private beforefieldinit One
         extends [mscorlib]System.ValueType
         implements Mono.Linker.Tests.Cases.Constrained.Dependencies.AssemblyWithConstrainedCall/IFoo
  {
    .pack 0
    .size 1
    .method public hidebysig newslot virtual final 
            instance int32  Method() cil managed
    {
      // Code size       2 (0x2)
      .maxstack  8
      IL_0000:  ldc.i4.1
      IL_0001:  ret
    } // end of method One::Method

  } // end of class One

  .class sequential ansi sealed nested private beforefieldinit Two
         extends [mscorlib]System.ValueType
         implements Mono.Linker.Tests.Cases.Constrained.Dependencies.AssemblyWithConstrainedCall/IFoo
  {
    .pack 0
    .size 1
    .method public hidebysig newslot virtual final 
            instance int32  Method() cil managed
    {
      // Code size       2 (0x2)
      .maxstack  8
      IL_0000:  ldc.i4.2
      IL_0001:  ret
    } // end of method Two::Method

  } // end of class Two

  .method public hidebysig static void  UseTheCode() cil managed
  {
    // Code size       29 (0x1d)
    .maxstack  1
    .locals init (valuetype Mono.Linker.Tests.Cases.Constrained.Dependencies.AssemblyWithConstrainedCall/One V_0,
             valuetype Mono.Linker.Tests.Cases.Constrained.Dependencies.AssemblyWithConstrainedCall/Two V_1)
    IL_0000:  ldloca.s   V_0
    IL_0002:  initobj    Mono.Linker.Tests.Cases.Constrained.Dependencies.AssemblyWithConstrainedCall/One
    IL_0008:  ldloc.0
    IL_0009:  call       void Mono.Linker.Tests.Cases.Constrained.Dependencies.AssemblyWithConstrainedCall::Helper(valuetype Mono.Linker.Tests.Cases.Constrained.Dependencies.AssemblyWithConstrainedCall/One)
    IL_000e:  ldloca.s   V_1
    IL_0010:  initobj    Mono.Linker.Tests.Cases.Constrained.Dependencies.AssemblyWithConstrainedCall/Two
    IL_0016:  ldloc.1
    IL_0017:  call       void Mono.Linker.Tests.Cases.Constrained.Dependencies.AssemblyWithConstrainedCall::HelperToUseTwo(valuetype Mono.Linker.Tests.Cases.Constrained.Dependencies.AssemblyWithConstrainedCall/Two)
    IL_001c:  ret
  } // end of method AssemblyWithConstrainedCall::UseTheCode

  .method private hidebysig static void  HelperToUseTwo(valuetype Mono.Linker.Tests.Cases.Constrained.Dependencies.AssemblyWithConstrainedCall/Two arg) cil managed
  {
    // Code size       1 (0x1)
    .maxstack  8
    IL_0000:  ret
  } // end of method AssemblyWithConstrainedCall::HelperToUseTwo

  .method private hidebysig static void  Helper(valuetype Mono.Linker.Tests.Cases.Constrained.Dependencies.AssemblyWithConstrainedCall/One arg) cil managed
  {
    // Code size       15 (0xf)
    .maxstack  8
    IL_0000:  ldarga.s   arg
    IL_0002:  constrained. Mono.Linker.Tests.Cases.Constrained.Dependencies.AssemblyWithConstrainedCall/One
    IL_0008:  callvirt   instance int32 Mono.Linker.Tests.Cases.Constrained.Dependencies.AssemblyWithConstrainedCall/IFoo::Method()
    IL_000d:  pop
    IL_000e:  ret
  } // end of method AssemblyWithConstrainedCall::Helper

  .method public hidebysig specialname rtspecialname 
          instance void  .ctor() cil managed
  {
    // Code size       7 (0x7)
    .maxstack  8
    IL_0000:  ldarg.0
    IL_0001:  call       instance void [mscorlib]System.Object::.ctor()
    IL_0006:  ret
  } // end of method AssemblyWithConstrainedCall::.ctor

} // end of class Mono.Linker.Tests.Cases.Constrained.Dependencies.AssemblyWithConstrainedCall


// =============================================================

// *********** DISASSEMBLY COMPLETE ***********************
// WARNING: Created Win32 resource file F:\Unity\dev\scratch\AssemblyWithConstrainedCall.res
