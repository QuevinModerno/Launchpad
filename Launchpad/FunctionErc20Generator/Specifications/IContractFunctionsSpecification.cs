using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agap2It.Labs.Launchpad.FunctionErc20Generator.Specifications
{
    public interface IContractFunctionsSpecification
    {
        public string? Name { get; set; }
        public string[]? InputTypes { get; set; }
        public string[]? InputNames { get; set; }
        public string? ReturnTypes { get; set; }
        //public string? ABI { get; set; }
    }
}
