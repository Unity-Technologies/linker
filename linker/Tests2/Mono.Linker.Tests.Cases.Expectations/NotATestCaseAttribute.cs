﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mono.Linker.Tests.Cases.Expectations
{
    [AttributeUsage(AttributeTargets.Class)]
    public class NotATestCaseAttribute : Attribute
    {
    }
}
