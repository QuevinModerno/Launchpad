using Agap2It.Labs.Launchpad.FunctionErc20Generator.Models;
using Agap2It.Labs.Launchpad.FunctionErc20Generator.Generators;
using System.Text.Json;
using Nethereum.Web3;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.IO.Compression;
using Nethereum.Merkle.Patricia;
using Nethereum.Contracts.QueryHandlers.MultiCall;
using System.Runtime.InteropServices;

namespace Agap2It.Labs.Launchpad.FunctionErc20Generator
{
    public class FunctionGenerator : IFunctionGenerator
    {
        private FunctionReadGenerator _ReadGenerator;
        private FunctionGroupGenerator _GroupGenerator;
        //private HookWriteGenerator _WriteGenerator;
        private string _Abi;

        public FunctionGenerator()
        {
            _ReadGenerator = new FunctionReadGenerator();
            _GroupGenerator = new FunctionGroupGenerator(); 
           // _WriteGenerator = new HookWriteGenerator();
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
                    InputNames = f.InputParameters.Select(p => p.Name).ToArray(),
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

        public string GetFunctionsCode(ContractFunctionModel[] model)
        {
            Dictionary<string, string> functionCodes = GenerateFunctionCodes(model);

            string functions = FunctionsToString(functionCodes);            
            return functions;
        }

        public Dictionary<string, string> GetFunctionsCodeListed(ContractFunctionModel[] model)
        {
            Dictionary<string, string> functionCodes = GenerateFunctionCodes(model);
            functionCodes.Add("GroupFunction", GenerateFunctionGroup(model));
            return functionCodes;
        }
        public byte[] GetFunctionsCodeZipped(ContractFunctionModel[] model)
        {
            Dictionary<string, string> functionCodes = GenerateFunctionCodes(model);
            functionCodes.Add("FunctionGroup", GenerateFunctionGroup(model));

            return CreateZipFile(functionCodes);
        }

        public string GenerateFunctionGroup(ContractFunctionModel[] model)
        {
            Dictionary<string, string> functions = new Dictionary<string, string>();
            foreach (var contractFunction in model)
            {
                functions.Add(contractFunction.Name, _GroupGenerator.getFunctionCode(contractFunction) );  
            }
            return FunctionGroupToString(functions);

        }

        private string FunctionsToString(Dictionary<string, string> functionCodes)
        {
            string result = "";
            bool isFirst = true;

            foreach (var functionCode in functionCodes)
            {
                string code = functionCode.Value;
                code = code.Replace("}}", "}");

                if (isFirst)
                {
                    result += code;
                    isFirst = false;
                }
                else
                {
                    // Remove additionnal using and Namespaces!
                    string[] lines = code.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                    string cleanedCode = string.Join("\n", lines.Where(line => !line.Contains("using") && !line.Contains("namespace")));
                    result +=  cleanedCode;
                }
            }
            result += "\n}";
            result = result.Trim();
           
            return result;
        }

        private string FunctionGroupToString(Dictionary<string, string> functionCodes)
        {
            string result = "";
            bool isFirst = true;

            foreach (var functionCode in functionCodes)
            {
                string code = functionCode.Value;
                code = code.Replace("}}", "}");

                if (isFirst)
                {
                    result += code;
                    isFirst = false;
                }
                else
                {
                    // Remove additionnal using and Namespaces!
                    string[] lines = code.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                    string cleanedCode = string.Join("\n", lines.Where(line => !line.Contains("using") && !line.Contains("namespace") && !line.Contains("class") ));
                    result += cleanedCode;
                }
            }
            result += "}";
            result = result.Trim();

            return result;
        }

        private Dictionary<string,string> GenerateFunctionCodes(ContractFunctionModel[] model)
        {
            Dictionary<string, string> functionCodes = new Dictionary<string, string>();

            foreach (var contractFunction in model)
            {
                string function = _ReadGenerator.getFunctionCode(contractFunction) + "\n\n";
                functionCodes.Add(contractFunction.Name, function);
               
               /* switch (contractFunction.IsReadOnly)
                {
                    case true:
                        {
                            string function = _ReadGenerator.getFunctionCode(contractFunction) + "\n\n";
                            functionCodes.Add(contractFunction.Name, function);
                            break;
                        }
                    case false: break;//_WriteGenerator.getFunctionCode(contractFunction);

                }
               */
            }

            return functionCodes;
        }

        private byte[] CreateZipFile(Dictionary<string, string> functionCodes)
        {
            string zipFilePath = "Functions.zip";

            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    foreach (var func in functionCodes)
                    {
                        string fileName = func.Key + "Function.cs";
                        AddFunctionToZipFile(archive, fileName, func.Value);
                    }
                }

                return memoryStream.ToArray();
            }
        }

        private void AddFunctionToZipFile(ZipArchive archive, string fileName, string function)
        {
            var fileEntry = archive.CreateEntry(fileName);

            using (var entryStream = fileEntry.Open())
            using (var streamWriter = new StreamWriter(entryStream))
            {
                streamWriter.Write(function);
            }
        }


        //TESTING 
        public async Task<string> GetTokenNameAsync(string contractAddress, string abi)
        {
            try
            {
                Web3 web3 = new Web3();

                var contract = web3.Eth.GetContract(abi, contractAddress);
                var nameFunction = contract.GetFunction("name");
                var result = await nameFunction.CallAsync<string>();

                return result.ToString();
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}
    
