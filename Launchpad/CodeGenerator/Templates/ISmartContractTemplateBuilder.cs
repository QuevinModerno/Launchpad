using Agap2It.Labs.Launchpad.CodeGenerator.Specifications;

namespace Agap2It.Labs.Launchpad.CodeGenerator.Templates
{
    public interface ISmartContractTemplateBuilder <TSpecification>  where TSpecification : ISmartContractSpecification
    {
        public string Build();
        public string Build(TSpecification specification);
    }

}
