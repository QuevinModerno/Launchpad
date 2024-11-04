using Agap2It.Labs.Launchpad.CodeGenerator.Models;
using Agap2It.Labs.Launchpad.CodeGenerator.Specifications;

namespace Agap2It.Labs.Launchpad.CodeGenerator.Generators
{
    public interface IGenerator 
    {
        public string? GetSmartContractCode(Erc20Model model);
    }
}
