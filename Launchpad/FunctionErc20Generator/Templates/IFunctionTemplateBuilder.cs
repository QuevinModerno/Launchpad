

using Agap2It.Labs.Launchpad.FunctionErc20Generator.Specifications;

namespace Agap2It.Labs.Launchpad.FunctionErc20Generator.Templates
{
    public interface IFunctionTemplateBuilder<TErc20Specification> : IContractTemplateBuilder<TErc20Specification> where TErc20Specification : IContractFunctionsSpecification, new()
    {
    }
}
