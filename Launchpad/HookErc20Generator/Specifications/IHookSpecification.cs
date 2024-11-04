using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agap2It.Labs.Launchpad.HookErc20Generator.Specifications
{
    public interface IHookSpecification : IContractFunctionsSpecification
    {
        public List<string>? Imports { get; set; }
    }
}
