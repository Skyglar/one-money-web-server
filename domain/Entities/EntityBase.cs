using System;

namespace domain.Entities
{
    public abstract class EntityBase
    {
        public long Id { get; set; }

        public Guid NetUid { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        public bool Deleted { get; set; }
    }
}
