using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace Mono.Linker
{
	public interface ILinkerAssemblyResolver : IAssemblyResolver
	{
		IDictionary AssemblyCache { get; }

		void AddSearchDirectory(string directory);

		AssemblyDefinition CacheAssembly(AssemblyDefinition assembly);
	}
}
