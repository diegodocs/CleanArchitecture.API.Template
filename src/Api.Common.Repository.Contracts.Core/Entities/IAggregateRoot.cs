using System.Collections.Generic;

namespace Api.Common.Repository.Contracts.Core.Entities
{
    public interface IAggregateRoot
    {
        int Version { get; }
        IEnumerable<object> AppliedEvents { get; }
    }
}