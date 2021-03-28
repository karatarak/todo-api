using System.Data;
using Dapper;

namespace Todo.Data.Handlers
{

    public class UuidHandler : SqlMapper.TypeHandler<Uuid>
    {
        public override Uuid Parse(object value)
        {
            return new Uuid((byte[])value);
        }

        public override void SetValue(IDbDataParameter parameter, Uuid value)
        {
            parameter.Value = value.Id.ToByteArray();
        }
    }
}
