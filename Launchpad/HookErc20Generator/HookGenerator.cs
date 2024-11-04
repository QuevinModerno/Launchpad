using Agap2It.Labs.Launchpad.HookErc20Generator.Generators;
using Agap2It.Labs.Launchpad.HookErc20Generator.Models;
using Nethereum.ABI;
using System.Text.Json;

namespace Agap2It.Labs.Launchpad.HookErc20Generator
{
    public class HookGenerator : IHookGenerator
    {
        private HookReadGenerator _ReadGenerator;
        private HookWriteGenerator _WriteGenerator;
        private string _Abi;

        public HookGenerator() 
        {
            _ReadGenerator = new HookReadGenerator();
            _WriteGenerator = new HookWriteGenerator();
            _Abi = "";
        }
        public ContractFunctionModel[] GetContractFunctions(string abi)
        {
            try 
            {
                var deserialiser = new Nethereum.ABI.ABIDeserialisation.ABIJsonDeserialiser();
                var contractABI = deserialiser.DeserialiseContract(abi);
                this._Abi = abi;

                return contractABI.Functions.Select(f => new ContractFunctionModel
                {
                    Name = f.Name,
                    InputTypes = f.InputParameters.Select(p => p.Type).ToArray(),
                    ReturnTypes = f.OutputParameters.Select(p => p.Type).ToArray(),
                    IsReadOnly = f.Constant  // Verificar se é de leitura
                }).ToArray();
            }
            catch (JsonException) 
            { 
                throw new ArgumentException("ERROR ABI.");
            }
            catch (Exception ex) 
            { 
                throw new ArgumentException("ERROR ABI."); 
            }          
        }

        public string GetHookCode(ContractFunctionModel model)
        {
            switch(model.IsReadOnly) 
            {
                case true: return _ReadGenerator.getHookCode(model, _Abi);
                case false: return _WriteGenerator.getHookCode(model, _Abi);

            }
            
        }
    }
}
