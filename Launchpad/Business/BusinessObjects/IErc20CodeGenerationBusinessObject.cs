using Agap2It.Labs.Launchpad.Business.Base;
using Agap2It.Labs.Launchpad.CodeGenerator;
using Agap2It.Labs.Launchpad.CodeGenerator.Enums;
using Agap2It.Labs.Launchpad.CodeGenerator.Models;

namespace Agap2It.Launchpad.Business.BusinessObjects;
public interface IErc20CodeGenerationBusinessObject
{
    Task<OperationResult<string>> GenerateErc20(Erc20Model erc20, ContractVariations variant);
}
