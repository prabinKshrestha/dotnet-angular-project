using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public class FromFormDataAttribute : Attribute { }
}
