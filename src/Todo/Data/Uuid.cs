using System;

namespace Todo.Data
{
    public class BinaryId
    {
        public Guid Id { get; }

        public BinaryId()
        {
            Id = Guid.NewGuid();
        }
        
        public BinaryId(Guid id)
        {
            Id = id;
        }

        public BinaryId(string id)
        {
            Id = new Guid(id);
        }

        public BinaryId(byte[] id)
        {
            Id = new Guid(id);
        }

        public override string ToString()
        {
            return Id.ToString("N");
        }
    }
}
