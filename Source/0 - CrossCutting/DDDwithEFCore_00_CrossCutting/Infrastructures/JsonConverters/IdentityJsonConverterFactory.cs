using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using DDDwithEFCore_01_Common.Core.Models;

namespace DDDwithEFCore_00_CrossCutting.Infrastructures.JsonConverters
{
	public class IdentityJsonConverterFactory : JsonConverterFactory
	{
		public override bool CanConvert(Type typeToConvert) =>
			!typeToConvert.IsGenericType &&
			typeToConvert.BaseType != null &&
			typeToConvert.BaseType == typeof(IdentityBase)
		;

		public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
		{
			Type converterType = typeof(IdentityJsonConverter<>).MakeGenericType(typeToConvert);

			return (JsonConverter)Activator.CreateInstance(converterType);
		}
	}
}
