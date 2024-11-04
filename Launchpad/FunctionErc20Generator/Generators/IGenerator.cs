
using Agap2It.Labs.Launchpad.FunctionErc20Generator.Models;

namespace Agap2It.Labs.Launchpad.FunctionErc20Generator.Generators
{
    public interface IGenerator
    {
        public string getFunctionCode(ContractFunctionModel model);
    }
}
