using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using DDDwithEFCore_01_Common.Core.Models;

namespace DDDwithEFCore_00_CrossCutting.Infrastructures.JsonConverters
{
	public class IdentityJsonConverter<TIdentity>
		: JsonConverter<TIdentity> where TIdentity : IdentityBase
	{
		public override bool CanConvert(Type typeToConvert) => typeToConvert == typeof(TIdentity);

		public override TIdentity Read(
			ref Utf8JsonReader reader,
			Type typeToConvert,
			JsonSerializerOptions options) => IdentityFactory.Create<TIdentity>(reader.GetGuid());

		public override void Write(
			Utf8JsonWriter writer,
			TIdentity value,
			JsonSerializerOptions options) => writer.WriteStringValue(value.Id);
	}
}
