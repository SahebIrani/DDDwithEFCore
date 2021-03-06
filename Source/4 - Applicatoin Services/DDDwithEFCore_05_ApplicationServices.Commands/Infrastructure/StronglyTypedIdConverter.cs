using System;
using System.ComponentModel;
using System.Globalization;

using DDDwithEFCore_01_Common.Core.Models;

namespace DDDwithEFCore_05_ApplicationServices.Commands.Infrastructure
{
	public class StronglyTypedIdConverter<TIdentity> : TypeConverter where TIdentity : IdentityBase
	{
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) =>
			sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);

		public override object ConvertFrom(
			ITypeDescriptorContext context,
			CultureInfo culture,
			object value)
		{
			string stringValue = value as string;

			if (!string.IsNullOrEmpty(stringValue) && Guid.TryParse(stringValue, out var guid))
				return IdentityFactory.Create<TIdentity>(guid);

			return base.ConvertFrom(context, culture, value);
		}
	}
}
