

using Agap2It.Labs.Launchpad.HookErc20Generator.Specifications;

namespace Agap2It.Labs.Launchpad.HookErc20Generator.Templates
{
    public class HookWriteTemplateBuilder : IHookTemplateBuilder<HookWriteSpecification>
    {
        private HookWriteSpecification _specification;
        private string _result;

        public HookWriteTemplateBuilder()
        {
            _specification = new HookWriteSpecification();
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

        public string Build(HookWriteSpecification specification)
        {
            throw new NotImplementedException();
        }

        public HookWriteTemplateBuilder WithName(string name)
        {
            this._specification.Name = name;
            return this;
        }
        public HookWriteTemplateBuilder WithImport(List<string> imports)
        {
            foreach (string import in imports)
            {
                this._specification.Imports.Add(import);
            }

            return this;
        }

        public HookWriteTemplateBuilder WithContractAbi(string abi)
        {
            this._specification.ABI = abi;
            return this;
        }

        public HookWriteTemplateBuilder WithInputParameters(string inputs)
        {
            this._specification.InputTypes = inputs;
            return this;
        }

        public HookWriteTemplateBuilder WithReturnParameters(string returns)
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
            string[] inputTypesArray = this._specification.InputTypes.Split(new[] { ", " }, StringSplitOptions.None);
            string results = "";
            for(int i = 0; i < inputTypesArray.Length;)
            {
                results += inputTypesArray[i] + ": string; \r\n";
            }
            return "\r\ninterface TransferComponentProps {\r\n"   
                + results
                + "contractAddress: string;\r\n}" + GenerateAbi();


        }
        public string GenerateAbi()
        {
            return "\nconst contractABI = [" + @$" ""function {_specification.Name}({_specification.InputTypes}) external returns ({_specification.ReturnTypes} )""," + "\n]\n";
        }

        public string GenerateHookBody(string properties, string functions)
        {
            return @$"const Contract{_specification.Name} = ({{{_specification.InputTypes}, contractAddress}} : ContractInformation) => {{"
                     + "\n" + properties
                     + "\n" + functions
                     + "\n" + @$"    return {{ contract{_specification.Name}, loading, error }};" + "\r\n  };\r\n  \r\n"
                     + @$"export default Contract{_specification.Name};"
                ;
        }
        public string GenerateProperties()
        {
            return "\r\n    const [status, setStatus] = useState('');\r\n    const [loading, setLoading] = useState(false);\r\n    const [error, setError] = useState('');";
        }

        public string GenerateFunctions()
        {
            return "    useEffect(() => {\r\n    "
                + @$"const handleContract{_specification.Name} = async () => "
                + "{\r\n        if (!(window as any).ethereum) setError('MetaMask is not installed'); \nreturn;\r\n  \r\n        "
                + "setLoading(true);\r\n setError(''); \r\n setStatus('');\r\n \r\n        try {\r\n          await (window as any).ethereum.request({ method: 'eth_requestAccounts' });\r\n          "
                + "const provider = new ethers.BrowserProvider((window as any).ethereum);\r\n                const sign = await provider.getSigner();\r\n                setStatus('Connected to MetaMask.');"
                + "const contract = new ethers.Contract(contractAddress, contractABI, sign);\r\n                setStatus('Contract instance created.');\r\n\r\n"
                + @$"const tx = await contract.{_specification.Name}({_specification.InputTypes});\r\n                setStatus('Transaction sent. Waiting for confirmation...');\r\n                await tx.wait();\r\n                setStatus('Succeded!');"
                + "       } catch (error) "
                + "{\r\n            setError('Function failed');\r\n        } finally {\r\n          setLoading(false);\r\n        }\r\n      };\r\n  \r\n      "
                + @$"handleContract{_specification.Name}();    " + "\n}, [recipient, amount, contractAddress]);";
        }
    }
}
