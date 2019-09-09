using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Api.Common.Repository.Contracts.Core.Exceptions
{    
    [Serializable]
    public class ModelException : Exception
    {
        public ModelException(string message, IEnumerable<ValidationResult> errors) : base(message)
        {
            Errors = errors;
        }

        public IEnumerable<ValidationResult> Errors { get; protected set; }

        public ModelException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
        protected ModelException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}