using System;

namespace ApiTemplate.Core.Entities.Base
{
    public abstract class BaseEntity  <TKey>:IEntity
    {
        public TKey Id { get; set; }
        public DateTime CreateDate { get; set; }
    }

    public abstract class BaseEntity : BaseEntity<long>
    {

    }
}
