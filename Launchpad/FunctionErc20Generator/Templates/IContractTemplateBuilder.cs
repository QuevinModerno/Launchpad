using Agap2It.Labs.Launchpad.FunctionErc20Generator.Specifications;

namespace Agap2It.Labs.Launchpad.FunctionErc20Generator.Templates
{
    public interface IContractTemplateBuilder<TSpecification> where TSpecification : IContractFunctionsSpecification
    {
        public string Build();
        public string Build(TSpecification specification);
    }
}
