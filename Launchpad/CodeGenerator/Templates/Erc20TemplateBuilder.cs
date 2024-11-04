
using Agap2It.Labs.Launchpad.CodeGenerator.Specifications;


namespace Agap2It.Labs.Launchpad.CodeGenerator.Templates
{

    public class Erc20TemplateBuilder : IErc20TemplateBuilder<Erc20Specification>
    {
        private Erc20Specification _specification;
        private string _result = "";


        public Erc20TemplateBuilder() 
        { 
            this._specification = new Erc20Specification(); 
        }
        public Erc20TemplateBuilder(Erc20Specification specification)
        {
            this._specification = specification;
        }
        public IErc20Specification getSpecification() 
        { 
            return this._specification; 
        }

        public string Build()
        {
            _result = "";
            var properties = GenerateProperties();
            var functions = GenerateFunctions();
            var constructors = GenerateConstructors();
            _result += GenerateLicense() + "\n";
            _result += GenerateVersion() + "\n";
            _result += GenerateImports();
            _result += GenerateContractBody(properties, functions, constructors);
            return _result;
        }

        public string Build(Erc20Specification specification)
        {
            _result = "";
            var properties = GenerateProperties();
            var functions = GenerateFunctions();
            var constructors = GenerateConstructors();
            _result += GenerateLicense() + "\n" ;
            _result += GenerateVersion() + "\n" ;
            _result += GenerateImports();
            _result += GenerateContractBody(properties, functions, constructors);
            return _result;

        }

        virtual public Erc20TemplateBuilder Clear() 
        {
            _specification = new Erc20Specification();
            return this;
        }

        public virtual Erc20TemplateBuilder WithName(string name) 
        {
            this._specification.Name = name;
            return this;
        }

        public virtual Erc20TemplateBuilder WithSymbol(string symbol)
        {
            this._specification.Symbol = symbol;
            return this;
        }

        public virtual Erc20TemplateBuilder WithInitialSupply(int supply)
        {
            this._specification.InitialSupply = supply;
            return this;
        }

        public virtual Erc20TemplateBuilder WithLicense(string license)
        {
            this._specification.License = license;
            return this;
        }

        public virtual Erc20TemplateBuilder WithVersion(string version)
        {
            this._specification.Version = version;
            return this;
        }

        public virtual Erc20TemplateBuilder WithImport(string import)
        {
            this._specification.Imports.Add(import);
            return this;
        }

        virtual public string GenerateProperties()
        {
            return "";
        }
        virtual public string GenerateConstructors()
        {
            return @$"
                constructor() ERC20(""{_specification.Name}"", ""{_specification.Symbol}"") {{
                    _mint(msg.sender, {_specification.InitialSupply} * 10**18); // initial supply
                }}
            ";
        }
        virtual public string GenerateFunctions()
        {
            return "";
        }
        public string GenerateLicense()
        {
            return @"// SPDX-License-Identifier: MIT";
        }
        public string GenerateVersion()
        {
            return @"pragma solidity 0.8.26;";
        }
        virtual public string GenerateImports()
        {
            return "import \"@openzeppelin/contracts/token/ERC20/ERC20.sol\";\n";
        }
        public string GenerateContractBody2(string properties, string functions, string constructors)
        {
            return @$"contract {_specification.Name} is ERC20{{
                {properties}
                {constructors}
                {functions}  
}}";
        }

        public string GenerateContractBody(string properties, string functions, string constructors)
        {
            string contractBody = $@"contract {_specification.Name} is ERC20{{";

            if (!string.IsNullOrWhiteSpace(properties))
            {
                contractBody += $@"{properties}";
            }

            if (!string.IsNullOrWhiteSpace(constructors))
            {
                contractBody += $@"{constructors}";
            }

            if (!string.IsNullOrWhiteSpace(functions))
            {
                contractBody += $@"{functions}";
            }
            contractBody += "}";

            return contractBody.Trim(); 
        }

        /* protected void AddImports(string import) 
         { 
             this.Template += import += ";\n"; 
         }

         protected void AddVariableDeclarations(string parameter) { this.VariablesDeclarations += parameter; } //Falta adicionar as virgulas aqui!
         protected void AddParameters(string parameter) { this.Parameters += parameter += ";\n"; }
         protected void InitiateVariablesToConstructor(string parameter) { this.InitiateVariablestoConstructor += parameter; }
         protected void AddFunctions(string parameter) 
         { 
             this.Functions += parameter; 
         }
        */
    }
}
