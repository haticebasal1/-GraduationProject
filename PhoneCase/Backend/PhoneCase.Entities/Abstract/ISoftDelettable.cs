using System;

namespace PhoneCase.Entities.Abstract;

public interface ISoftDeletable
{
    bool IsDeleted { get; set; }
}

