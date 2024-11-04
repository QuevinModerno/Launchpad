using Agap2It.Labs.Launchpad.CodeGenerator.Models;
using Agap2It.Labs.Launchpad.CodeGenerator.Templates;
using Agap2It.Labs.Launchpad.CodeGenerator.Transformers;
using Agap2It.Labs.Launchpad.CodeGenerator.Validators;



namespace Agap2It.Labs.Launchpad.CodeGenerator.Generators
{
    public class BurnGenerator : Erc20Generator
    {
        BurnErc20Transformer _transformation;
        Erc20Validator _validator;
        BurnErc20TemplateBuilder _template;


        public BurnGenerator() 
        {
            this._transformation = new BurnErc20Transformer();
            this._validator = new Erc20Validator();
            this._template = new BurnErc20TemplateBuilder();
        }

        override public string? GetSmartContractCode(Erc20Model model)
        {
            var specification = _transformation.GetSpecification(model);

            _validator.Validator(specification);

            _template
               .WithName(specification.Name)
               .WithSymbol(specification.Symbol)
               .WithInitialSupply(specification.InitialSupply)
               ;
            return _template.Build();
        }
    }
}
