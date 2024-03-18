using BrDateTimeUtils;
using System;
namespace ExternalEntities.Domain.Abstractions
{
    public abstract class BaseEntity
    {
        protected int _id;
        public virtual int Id => _id;
        public DateTime CreatedDate => _createdDate;
        protected DateTime _createdDate = DateTime.Now.Brasilia();
        public DateTime UpdateDate { get; set; } = DateTime.Now.Brasilia();
        public string UserUpdate { get; set; } = "SISTEMA";
        protected void setUpdateDate() => UpdateDate = DateTime.Now.Brasilia();
        protected void setId(int id) => _id = id;

        public BaseEntity() { }

        public BaseEntity(int id)
        {
            _id = id;
        }
    }
}
