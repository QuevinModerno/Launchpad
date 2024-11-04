using Agap2It.Labs.Launchpad.CodeGenerator.Models;
using Agap2It.Labs.Launchpad.CodeGenerator.Specifications;

namespace Agap2It.Labs.Launchpad.CodeGenerator.Templates
{
    public interface IErc20TemplateBuilder<TErc20Specification> : ISmartContractTemplateBuilder<TErc20Specification> where TErc20Specification : IErc20Specification, new()
    {
        

    }


  
}
