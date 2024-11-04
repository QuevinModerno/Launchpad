

using Agap2It.Labs.Launchpad.HookErc20Generator.Models;
using Agap2It.Labs.Launchpad.HookErc20Generator.Specifications;

namespace Agap2It.Labs.Launchpad.HookErc20Generator.Transformers
{
    public class HookTransfomer : IHookTransformer
    {
        public HookTransfomer() 
        {
        }

        public virtual HookSpecification GetSpecification(ContractFunctionModel function, string abi)
        {
            string inputTypes = string.Join(", ", function.InputTypes);
            string outputTypes = string.Join(", ", function.ReturnTypes);
            return new()
            {
                Name = function.Name,
                InputTypes = inputTypes,
                ReturnTypes = outputTypes,
                ABI = abi
            };
        }
    }
}
