using Agap2It.Labs.Launchpad.Business.Base;
using Agap2It.Labs.Launchpad.CodeGenerator;
using EthGenerator.Contracts.Compiler;
using EthGenerator.Contracts.Compiler.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agap2It.Launchpad.Business.BusinessObjects
{
    public class Erc20CodePublisherBusinessObject : IErc20CodePublisherBusinessObject
    {
        private readonly ILogger<Erc20CodePublisherBusinessObject> _logger;
        EthContractCompilationConfiguration _configuration;

        public Erc20CodePublisherBusinessObject(ILogger<Erc20CodePublisherBusinessObject> logger)
        {
            _logger = logger;
            _configuration = new EthContractCompilationConfiguration();
        }
        public async Task<OperationResult<EthContract?>> PublishErc20(string contract)
        {
            try
            {
                await Task.Delay(1000);

                _configuration.OptimizationRuns = 20;
                _configuration.Optimize = true;
                _configuration.Version = "0.8.26";


                EthContract result = EthCompiler.Compile(contract, _configuration);
                return result == null ? throw new Exception("Error in code generation.")
                    : new OperationResult<EthContract?>(result);
            }
            catch (Exception ex)
            {
                //Aceitar as excessoes de verificações / Erro na producao
                _logger.LogError(ex.Message);
                return new OperationResult<EthContract?>(null);
            }
        }
    }
}
