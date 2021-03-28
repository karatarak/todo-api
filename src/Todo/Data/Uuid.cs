using System;

namespace Todo.Data
{
    public class Uuid
    {
        public Guid Id { get; }

        public Uuid()
        {
            Id = Guid.NewGuid();
        }
        
        public Uuid(Guid id)
        {
            Id = id;
        }

        public Uuid(string id)
        {
            Id = new Guid(id);
        }

        public Uuid(byte[] id)
        {
            Id = new Guid(id);
        }

        public override string ToString()
        {
            return Id.ToString("N");
        }
    }
}
