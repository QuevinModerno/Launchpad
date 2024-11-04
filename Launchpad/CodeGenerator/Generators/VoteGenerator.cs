using Agap2It.Labs.Launchpad.CodeGenerator.Models;
using Agap2It.Labs.Launchpad.CodeGenerator.Templates;
using Agap2It.Labs.Launchpad.CodeGenerator.Transformers;
using Agap2It.Labs.Launchpad.CodeGenerator.Validators;

namespace Agap2It.Labs.Launchpad.CodeGenerator.Generators
{
    public class VoteGenerator : Erc20Generator
    {
        VoteErc20Transformer _transformation;
        VoteValidator _validator;
        VoteErc20TemplateBuilder _template;
        public VoteGenerator() 
        {
            this._transformation = new VoteErc20Transformer();
            this._validator = new VoteValidator();
            this._template = new VoteErc20TemplateBuilder();
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
