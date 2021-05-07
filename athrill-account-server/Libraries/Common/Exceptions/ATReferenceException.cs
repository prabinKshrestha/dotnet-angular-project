using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AT.Common.Exceptions
{
    public class ATReferenceException : Exception
    {
        public ATReferenceException():base() { }
        public ATReferenceException(string message) : base(message) { }
        public ATReferenceException(int id) : base(GetMessage(id)) { }
        public ATReferenceException(int id, string typeName) : base(GetMessage(id, typeName)) { }

        private static string GetMessage(int id, string typeName = null)
        {
            if(typeName != null)
            {
                return $"Reference Exception found. ID : {id} was not found for {typeName}.";
            }
            else
            {
                return $"Reference Exception found. ID : {id} was not found.";
            }
        }

    }
}
