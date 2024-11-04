

using Agap2It.Labs.Launchpad.HookErc20Generator.Models;
using Agap2It.Labs.Launchpad.HookErc20Generator.Specifications;

namespace Agap2It.Labs.Launchpad.HookErc20Generator.Transformers
{
    public interface IHookTransformer
    {
        public HookSpecification GetSpecification(ContractFunctionModel functions, string abi);
    }
}
