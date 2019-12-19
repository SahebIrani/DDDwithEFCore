using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

using Microsoft.Extensions.DependencyModel;

namespace DDDwithEFCore_02_Common.Infrastructures.EFCore.Helpers
{
	public static class AssemblyHelpers
	{
		public static IEnumerable<Assembly> LoadFromSearchPatterns(params string[] searchPatterns)
		{
			if (searchPatterns == null || searchPatterns.Length == 0)
				return Enumerable.Empty<Assembly>();

			HashSet<Assembly> assemblies = new HashSet<Assembly>();

			foreach (string searchPattern in searchPatterns)
			{
				Regex searchRegex = new Regex(searchPattern, RegexOptions.IgnoreCase);
				List<RuntimeLibrary> moduleAssemblyFiles = DependencyContext
					.Default
					.RuntimeLibraries
					.Where(x => searchRegex.IsMatch(x.Name))
					.ToList()
				;

				foreach (RuntimeLibrary assemblyFiles in moduleAssemblyFiles)
					assemblies.Add(Assembly.Load(new AssemblyName(assemblyFiles.Name)));
			}

			return assemblies.ToList();
		}
	}
}
