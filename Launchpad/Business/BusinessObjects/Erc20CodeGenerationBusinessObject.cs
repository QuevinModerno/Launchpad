using Agap2It.Labs.Launchpad.Business.Base;
using Agap2It.Labs.Launchpad.CodeGenerator;
using Agap2It.Labs.Launchpad.CodeGenerator.Enums;
using Agap2It.Labs.Launchpad.CodeGenerator.Models;
using Microsoft.Extensions.Logging;



namespace Agap2It.Launchpad.Business.BusinessObjects;

public class Erc20CodeGenerationBusinessObject : IErc20CodeGenerationBusinessObject
{
    private readonly ILogger<Erc20CodeGenerationBusinessObject> _logger;
    private readonly IContractGenerator _codeGenerator;
    
    public Erc20CodeGenerationBusinessObject(ILogger<Erc20CodeGenerationBusinessObject> logger)
    {
        _logger = logger;
        _codeGenerator = new ContractGenerator();
    }

    public async Task<OperationResult<string>> GenerateErc20(Erc20Model erc20, ContractVariations variant)
    {
        //Mudar excepcoes. Criar excessoes numa classe
        try
        {
            await Task.Delay(1000);
            //Execute operations for this function

            //Invocar o ICodeGenerator
            var contract = _codeGenerator.GetSmartContract(erc20, variant);

            _logger.LogInformation("Resultado do erccodegenerator");

            _logger.LogInformation(contract);

            return contract == null ? throw new Exception("Error in code generation.")
                : new OperationResult<string>(contract);
        }
        catch (Exception ex)
        {
            //Aceitar as excessoes de verificações / Erro na producao
            _logger.LogError(ex.Message);
            return new OperationResult<string>(ex.Message);
        }
    }


}
