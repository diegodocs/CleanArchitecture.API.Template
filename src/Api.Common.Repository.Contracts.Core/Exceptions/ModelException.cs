using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Common.Repository.Contracts.Core.Exceptions
{
    public class ModelException : Exception
    {
        public ModelException(string message, IEnumerable<ValidationResult> errors) : base(message)
        {
            Errors = errors;
        }

        public IEnumerable<ValidationResult> Errors { get; protected set; }
    }
}