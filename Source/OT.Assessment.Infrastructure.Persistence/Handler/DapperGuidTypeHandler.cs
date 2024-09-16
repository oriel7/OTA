using Dapper;
using System.Data;

namespace OT.Assessment.Infrastructure.Persistence.Handler
{
    public class DapperGuidTypeHandler : SqlMapper.TypeHandler<Guid>
    {
        public override void SetValue(IDbDataParameter parameter, Guid guid)
        {
            parameter.Value = guid.ToString();
        }

        public override Guid Parse(object value)
        => Guid.Parse((string)value);

        //public override Guid Parse(object value)
        //{
        //    return new Guid((string)value);
        //}
    }
}
