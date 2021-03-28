using System.Data;
using Dapper;

namespace Todo.Data.Handlers
{

    public class BinaryIdHandler : SqlMapper.TypeHandler<BinaryId>
    {
        public override BinaryId Parse(object value)
        {
            return new BinaryId((byte[])value);
        }

        public override void SetValue(IDbDataParameter parameter, BinaryId value)
        {
            parameter.Value = value.Id.ToByteArray();
        }
    }
}
