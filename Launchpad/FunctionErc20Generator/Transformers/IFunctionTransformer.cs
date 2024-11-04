using Agap2It.Labs.Launchpad.FunctionErc20Generator.Specifications;
using Agap2It.Labs.Launchpad.FunctionErc20Generator.Models;

namespace Agap2It.Labs.Launchpad.FunctionErc20Generator.Transformers
{
    public interface IFunctionTransformer
    {
        public FunctionSpecification GetSpecification(ContractFunctionModel functions);

    }
}
