using Agap2It.Labs.Launchpad.FunctionErc20Generator.Specifications;
using Nethereum.Web3;
using Org.BouncyCastle.Asn1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agap2It.Labs.Launchpad.FunctionErc20Generator.Templates
{
    public class FunctionReadTemplateBuilder : IFunctionTemplateBuilder<FunctionSpecification>
    {
        private FunctionSpecification _specification;
        private string _result;
        public FunctionReadTemplateBuilder() 
        {
            _specification = new FunctionSpecification();
            _result = string.Empty;
        }
        public string Build()
        {
            _result = "";
            var parameters = GenerateParameters();
            _result += GenerateImports();
            _result += GenerateNamespace();
            _result += GenerateFunctionBody(parameters) + "\n\n";
           // _result += GenerateFunctionTask();
            return _result;
        }


        public string Build(FunctionSpecification specification)
        {
            throw new NotImplementedException();
        }


        public FunctionReadTemplateBuilder WithName(string name)
        {
            this._specification.Name = name;
            return this;
        }
       /* public FunctionReadTemplateBuilder WithImport(List<string> imports)
        {
            foreach (string import in imports)
            {
                this._specification.Imports.Add(import);
                this._specification.Imports.Add("; \r\n");
            }

            return this;
        }

        public FunctionReadTemplateBuilder WithContractAbi(string abi)
        {
            this._specification.ABI = abi;
            return this;
        }
       */

        public FunctionReadTemplateBuilder WithInputParameters(string[] inputs)
        {
            this._specification.InputTypes = inputs;
            return this;
        }

        public FunctionReadTemplateBuilder WithInputParametersConverted(string[] inputs)
        {
            this._specification.InputTypesConverted = inputs;
            return this;
        }
        public FunctionReadTemplateBuilder WithInputNames(string[] inputs)
        {
            this._specification.InputNames = inputs;
            return this;
        }

        public FunctionReadTemplateBuilder WithReturnParameters(string returns)
        {
            this._specification.ReturnTypes = returns;
            return this;
        }
        public FunctionReadTemplateBuilder WithReturnParametersConverted(string returns)
        {
            this._specification.ReturnTypesConverted = returns;
            return this;
        }




        public string GenerateFunctionBody(string parameters)
        {
            return @$"[Function(""{_specification.Name}"", ""{_specification.ReturnTypes}"" )]" + "\n"
                     + @$"public class {_specification.Name}Function : FunctionMessage " + "\n{" 
                     + "\n" + parameters
                     + "}"
                ;
        }
        public string GenerateParameters()
        {
            string parameters = "";
            for(int i = 0; i <_specification.InputNames.Length ; i++) 
            {
                parameters += @$"   [Parameter(""{_specification.InputTypes[i]}"", ""{_specification.InputNames[i]}"", {i+1})]" + "\n"
                            + @$"   public {_specification.InputTypesConverted[i]} {_specification.InputNames[i]} {{ get;set; }}" + "\n\n"          
                    ;
                

            }
            return parameters;
        }

        public string GenerateImports()
        {
            return "using Nethereum.ABI.FunctionEncoding.Attributes;\r\nusing Nethereum.Contracts; using System.Numerics;\r\n";
        }


        public string GenerateNamespace()
        {
            return "namespace Erc20{\n\n";
        }
        public string GenerateFunctionTask()
        {
            string classParameters = "" ;
            for (int i = 0; i < _specification.InputTypesConverted.Length; i++) {
                if (i != 0)
                {
                    classParameters += ", ";
                }
                classParameters += _specification.InputTypesConverted[i] +" "+ _specification.InputNames[i];
            }
            
            string functionParameters = "" ;
            for (int i = 0; i < _specification.InputNames.Length; i++)
            {
                functionParameters += "           " + _specification.InputNames[i] + " = " + _specification.InputNames[i] + "\n";
            }

                return @$"  public async Task<{_specification.ReturnTypesConverted}> {_specification.Name}({classParameters})" + "\n  {\n"

                + $@"       var {_specification.Name}FunctionMessage = new {_specification.Name}Function()"+ "\r\n           {\r\n "
                + $@"{functionParameters}" + "\r\n           }\r\n"
                + $@"       var {_specification.Name}Handler = _web3.Eth.GetContractQueryHandler<{_specification.Name}Function>();" + "\n"
                + $@"       var result = await {_specification.Name}Handler.QueryAsync<{_specification.ReturnTypesConverted}>(_contractAddress, {_specification.Name}FunctionMessage);"+"\n"
                + "         return result;" + "\n"
                + " }}\n";
        }
        
        

    }
}
