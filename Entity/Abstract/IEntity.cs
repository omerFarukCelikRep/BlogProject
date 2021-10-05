using Entity.Enums;
using System;

namespace Entity.Abstract
{
    public interface IEntity
    {
        Guid Id { get; set; }
        Status Status { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime? ModifiedDate { get; set; }
        DateTime? DeletedDate { get; set; }
    }
}
