using System.Data;

using Dapper;

using DDDwithEFCore_01_Common.Core.Models;

namespace DDDwithEFCore_06_ApplicationServices.Queries.Mappers
{
	public class StronglyTypedIdMapper<TIdenity> : SqlMapper.TypeHandler<TIdenity> where TIdenity : IdentityBase
	{
		public override void SetValue(IDbDataParameter parameter, TIdenity value) => parameter.Value = value.Id;

		public override TIdenity Parse(object value) => IdentityFactory.Create<TIdenity>(id: value);
	}
}
