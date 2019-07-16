using System;

namespace Api.Common.Repository.Contracts.Core.Entities
{
    public interface IBaseViewModel
    {
        Guid Id { get; set; }

        DateTime CreateDate { get; set; }

        DateTime? ModifiedDate { get; set; }

        bool IsActive { get; set; }
    }
}