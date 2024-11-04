using Agap2It.Labs.Launchpad.FunctionErc20Generator.Specifications;
using Nethereum.Contracts.QueryHandlers.MultiCall;
using System.Transactions;

namespace Agap2It.Labs.Launchpad.FunctionErc20Generator.Templates
{
    public class FunctionGroupWriteTemplateBuilder : FunctionGroupTemplateBuilder, IFunctionTemplateBuilder<FunctionSpecification>
    {
        private FunctionSpecification _specification;
        public FunctionGroupWriteTemplateBuilder() 
        {
            _specification = new FunctionSpecification();   
        }
        public string Build()
        {
            return base.Build();
        }


        override public FunctionGroupWriteTemplateBuilder WithName(string name)
        {
            this._specification.Name = name;
            return this;
        }

        override public FunctionGroupWriteTemplateBuilder WithInputParameters(string[] inputs)
        {
            this._specification.InputTypes = inputs;
            return this;
        }

        override public FunctionGroupWriteTemplateBuilder WithInputParametersConverted(string[] inputs)
        {
            this._specification.InputTypesConverted = inputs;
            return this;
        }
        override public FunctionGroupWriteTemplateBuilder WithInputNames(string[] inputs)
        {
            this._specification.InputNames = inputs;
            return this;
        }

        override public FunctionGroupWriteTemplateBuilder WithReturnParameters(string returns)
        {
            this._specification.ReturnTypes = returns;
            return this;
        }
        override public FunctionGroupWriteTemplateBuilder WithReturnParametersConverted(string returns)
        {
            this._specification.ReturnTypesConverted = returns;
            return this;
        }
        public override string GenerateNamespace()
        {
            return "namespace Erc20{\n\n";
        }

        public override string GenerateFunctionBody(string task)
        {
            return
                       "public class Erc20FunctionGroup {"
                     + "\n" + task
                ;
        }
        public override string GenerateFunctionTask()
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

            + $@"       var {_specification.Name}Handler = web3.Eth.GetContractTransactionHandler<{_specification.Name}Function>();" + "\n"
            + $@"       var {_specification.Name}FunctionMessage = new {_specification.Name}Function()" + "\r\n           {\r\n "
            + $@"{functionParameters}" + "\r\n           }\r\n"
            + $@"       var result = await {_specification.Name}Handler.SendRequestAndWaitForReceiptAsync<{_specification.ReturnTypesConverted}>(contractAddress, {_specification.Name}FunctionMessage);" + "\n"
            + "}}";
        }

    }
}
