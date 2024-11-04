using Agap2It.Labs.Launchpad.HookErc20Generator.Specifications;
using Nethereum.Contracts.QueryHandlers.MultiCall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agap2It.Labs.Launchpad.HookErc20Generator.Templates
{
    public class HookReadTemplateBuilder : IHookTemplateBuilder<HookSpecification>
    {
        private HookSpecification _specification;
        private string _result;

        public HookReadTemplateBuilder()
        {
            _specification = new HookSpecification();
            _result = "";
        }
        public string Build()
        {
            _result = "";
            var properties = GenerateProperties();
            var functions = GenerateFunctions();
            _result += GenerateImports();
            _result += GenerateGlobalVariables();
            _result += GenerateHookBody(properties, functions);
            return _result;
        }

        public string Build(HookSpecification specification)
        {
            throw new NotImplementedException();
        }

        public HookReadTemplateBuilder WithName(string name)
        {
            this._specification.Name = name;
            return this;
        }
        public HookReadTemplateBuilder WithImport(List<string> imports)
        {
            foreach(string import in imports)
            {
                this._specification.Imports.Add(import);
            }

            return this;
        }

        public HookReadTemplateBuilder WithContractAbi(string abi)
        {
            this._specification.ABI = abi;
            return this;
        }

        public HookReadTemplateBuilder WithInputParameters(string inputs)
        {
            this._specification.InputTypes = inputs;
            return this;
        }

        public HookReadTemplateBuilder WithReturnParameters(string returns)
        {
            this._specification.ReturnTypes = returns;
            return this;
        }
        public string GenerateImports()
        {
            return "import { useEffect, useState } from \"react\";\r\nimport {ethers} from 'ethers';\n";
        }

        public string GenerateGlobalVariables()
        {
            return "interface ContractInformation{\r\n    contractAddress: string,\r\n    provider: ethers.BrowserProvider;\r\n}" + GenerateAbi();

        }
        public string GenerateAbi()
        {
            return "\nconst contractABI = ["+ @$" ""function {_specification.Name}({_specification.InputTypes}) view returns ({_specification.ReturnTypes} )"","+ "\n]\n";
        }

        public string GenerateHookBody(string properties, string functions) 
        {
            return @$"const Contract{_specification.Name} = ({{contractAddress, provider}} : ContractInformation) => {{"
                     + "\n" + properties
                     + "\n" + functions
                     + "\n" + @$"    return {{ contract{_specification.Name}, loading, error }};" + "\r\n  };\r\n  \r\n"
                     + @$"export default Contract{_specification.Name};"
                ;
        }
        public string GenerateProperties()
        {
            return @$"    const [contract{_specification.Name}, setContract{_specification.Name}] = useState('');"
                + "\r\n    const [loading, setLoading] = useState(false);\r\n    const [error, setError] = useState('');";
        }

        public string GenerateFunctions() 
        {
            return "    useEffect(() => {\r\n    "
                + @$"const fetchContract{_specification.Name} = async () => "
                +  "{\r\n        if (!contractAddress || !provider) return;\r\n  \r\n        "
                + "setLoading(true);\r\n        try {\r\n          const contract = new ethers.Contract(contractAddress, contractABI, provider);\r\n          "
                + @$"const {_specification.Name} = await contract.{_specification.Name}();" + "\r\n"
                + @$"setContract{_specification.Name}({_specification.Name});" + "\r\n        } catch (error) "
                + "{\r\n            setError('Error fetching function');\r\n        } finally {\r\n          setLoading(false);\r\n        }\r\n      };\r\n  \r\n      "
                + @$"fetchContract{_specification.Name}();    "+ "\n}, [contractAddress, provider]);";
        }
    }
}
