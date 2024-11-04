

using Agap2It.Labs.Launchpad.CodeGenerator.Enums;
using Agap2It.Labs.Launchpad.CodeGenerator.Models;

namespace Agap2It.Labs.Launchpad.CodeGenerator
{
    public interface IContractGenerator
    {
        public string? GetSmartContract(Erc20Model model, ContractVariations variant);

    }
}
