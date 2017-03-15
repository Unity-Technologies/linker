using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mono.Linker.Tests.Cases.Expectations.Assertions
{
    /// <summary>
    /// Base attribute for attributes that mark up the expected behavior of the linker on a member
    /// </summary>
    public abstract class BaseExpectedLinkedBehaviorAttribute : Attribute
    {
    }
}
