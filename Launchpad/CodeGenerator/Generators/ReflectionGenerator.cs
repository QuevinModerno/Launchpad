

using Agap2It.Labs.Launchpad.CodeGenerator.Models;
using Agap2It.Labs.Launchpad.CodeGenerator.Templates;
using Agap2It.Labs.Launchpad.CodeGenerator.Transformers;
using Agap2It.Labs.Launchpad.CodeGenerator.Validators;

namespace Agap2It.Labs.Launchpad.CodeGenerator.Generators
{
    public class ReflectionGenerator : Erc20Generator
    {
        ReflectionErc20Transformer _transformation;
        ReflectionValidator _validator;
        ReflectionErc20TemplateBuilder _template;
        public ReflectionGenerator() 
        {
            this._transformation = new ReflectionErc20Transformer();
            this._validator = new ReflectionValidator();
            this._template = new ReflectionErc20TemplateBuilder();
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
            return _template.Build();
        }
    }
}
