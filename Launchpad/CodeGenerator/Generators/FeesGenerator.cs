

using Agap2It.Labs.Launchpad.CodeGenerator.Models;
using Agap2It.Labs.Launchpad.CodeGenerator.Templates;
using Agap2It.Labs.Launchpad.CodeGenerator.Transformers;
using Agap2It.Labs.Launchpad.CodeGenerator.Validators;

namespace Agap2It.Labs.Launchpad.CodeGenerator.Generators
{
    public class FeesGenerator : Erc20Generator
    {
        FeesErc20Transformer _transformation;
        FeesValidator _validator;
        FeesErc20TemplateBuilder _template;
        public FeesGenerator() 
        { 
            this._transformation = new FeesErc20Transformer();
            this._validator = new FeesValidator();
            this._template = new FeesErc20TemplateBuilder();
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
            _template.WithFeesRate(specification.FeesRate);
            _template.WithAddress(specification.Address);
            return _template.Build();
        }

    }
}
