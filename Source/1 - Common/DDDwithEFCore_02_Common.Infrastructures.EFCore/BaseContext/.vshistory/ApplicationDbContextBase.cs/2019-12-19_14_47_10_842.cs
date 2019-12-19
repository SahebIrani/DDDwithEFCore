using System;
using System.Collections.Generic;
using System.Reflection;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DDDwithEFCore_02_Common.Infrastructures.EFCore.BaseContext
{
	public abstract class ApplicationDbContextBase : DbContext
	{
		protected ApplicationDbContextBase(DbContextOptions dbContextOptions, IConfiguration configuration)
			: base(dbContextOptions) =>
			_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

		private readonly IConfiguration _configuration;

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			string qualifiedAssemblyPattern = _configuration.GetValue<string>("QualifiedAssemblyPattern");

			IEnumerable<Assembly> assemblies = AssemblyHelpers.LoadFromSearchPatterns(qualifiedAssemblyPattern);

			builder.Register(assemblies);

			RegisterConventions(builder);
		}

		protected virtual void RegisterConventions(ModelBuilder builder) { }
	}
}
