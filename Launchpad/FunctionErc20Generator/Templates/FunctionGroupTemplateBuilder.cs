using Agap2It.Labs.Launchpad.FunctionErc20Generator.Specifications;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.Contracts.QueryHandlers.MultiCall;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Agap2It.Labs.Launchpad.FunctionErc20Generator.Templates
{
    public class FunctionGroupTemplateBuilder : IFunctionTemplateBuilder<FunctionSpecification>
    {
        private FunctionSpecification _specification;
        private string _result;
        public FunctionGroupTemplateBuilder()
        {
            _specification = new FunctionSpecification();
            _result = string.Empty;
        }


        public string Build()
        {
            _result = "";
            string task = GenerateFunctionTask();
            _result += GenerateImports();
            _result += GenerateNamespace();
            _result += GenerateFunctionBody(task);

            return _result;     

        }

        public string Build(FunctionSpecification specification)
        {
            throw new NotImplementedException();
        }


        virtual public FunctionGroupTemplateBuilder WithName(string name)
        {
            this._specification.Name = name;
            return this;
        }

        virtual public FunctionGroupTemplateBuilder WithInputParameters(string[] inputs)
        {
            this._specification.InputTypes = inputs;
            return this;
        }

        virtual public FunctionGroupTemplateBuilder WithInputParametersConverted(string[] inputs)
        {
            this._specification.InputTypesConverted = inputs;
            return this;
        }
        virtual public FunctionGroupTemplateBuilder WithInputNames(string[] inputs)
        {
            this._specification.InputNames = inputs;
            return this;
        }

        virtual public FunctionGroupTemplateBuilder WithReturnParameters(string returns)
        {
            this._specification.ReturnTypes = returns;
            return this;
        }
        virtual public FunctionGroupTemplateBuilder WithReturnParametersConverted(string returns)
        {
            this._specification.ReturnTypesConverted = returns;
            return this;
        }



        public string GenerateImports()
        {
            return "using Nethereum.ABI.FunctionEncoding.Attributes;\r\nusing Nethereum.Web3;\r\n" +
                "using Nethereum.Contracts;\r\nusing System.Numerics;\r\nusing System.Threading.Tasks;\n using Erc20;\n";

        }

        virtual public string GenerateNamespace()
        {
            return "namespace Erc20{\n\n";
        }

        virtual public string GenerateFunctionBody(string task)
        {
            return 
                       "public class Erc20FunctionGroup {"
                     + "\n" + task
                ;
        }
        virtual public string GenerateFunctionTask()
        {
            string classParameters = "";
            for (int i = 0; i < _specification.InputTypesConverted.Length; i++)
            {
                classParameters += ", " + _specification.InputTypesConverted[i] + " " + _specification.InputNames[i];
            }

            string functionParameters = "";
            for (int i = 0; i < _specification.InputNames.Length; i++)
            {
                functionParameters += "           " + _specification.InputNames[i] + " = " + _specification.InputNames[i] + "\n";
            }

            return @$"  public async Task<{_specification.ReturnTypesConverted}> {_specification.Name}(string contractAddress, Web3 web3{classParameters})" + "\n  {\n"

            + $@"       var {_specification.Name}FunctionMessage = new {_specification.Name}Function()" + "\r\n           {\r\n "
            + $@"{functionParameters}" + "\r\n           }\r\n"
            + $@"       var {_specification.Name}Handler = web3.Eth.GetContractQueryHandler<{_specification.Name}Function>();" + "\n"
            + $@"       var result = await {_specification.Name}Handler.QueryAsync<{_specification.ReturnTypesConverted}>(contractAddress, {_specification.Name}FunctionMessage);" + "\n"
            + "         return result;" + "\n"
            + "}}";
        }

    }
}

/*
public class FunctionGroup
{
    private Web3 _web3;
    private string _owner;
    private string _contractAddress;
    public FunctionGroup(Web3 web3, string adress, string contractAddress) 
    {
        _web3 = web3;
        _owner = adress;  
        _contractAddress = contractAddress;
    }
    

    [Function("allowance", "uint256")]
    public class allowanceFunction : FunctionMessage
    {
        [Parameter("address", "owner", 1)]
        public string owner { get; set; }

        [Parameter("address", "spender", 2)]
        public string spender { get; set; }

    }
                 //           ""name""    ""input types""
    public async Task<BigInteger> allowance(string owner, string spender)
    {
        //                              ""name""
            var allowanceFunctionMessage = new allowanceFunction()
            {
                owner = owner,
                spender = spender,
            };
                                     //                                 ""name""
            var allowanceHandler = _web3.Eth.GetContractQueryHandler<allowanceFunction>();
                                  //                   ""return typesConverted""
            var result = await allowanceHandler.QueryAsync<BigInteger>(_contractAddress, allowanceFunctionMessage);

            return result;
        }

    }
    


}
*/