using Agap2It.Labs.Launchpad.CodeGenerator.Models;
using Agap2It.Labs.Launchpad.CodeGenerator.Enums;
using Agap2It.Labs.Launchpad.CodeGenerator.Templates;
using Agap2It.Labs.Launchpad.CodeGenerator.Transformers;
using Agap2It.Labs.Launchpad.CodeGenerator.Validators;


namespace Agap2It.Labs.Launchpad.CodeGenerator.Generators
{
    public class Erc20Generator : IGenerator
    {
        Erc20Transformer _transformation;
        Erc20Validator _validator;
        Erc20TemplateBuilder _templateBuilder;


        public Erc20Generator() 
        {
            this._transformation = new Erc20Transformer();
            this._validator = new Erc20Validator();
            this._templateBuilder = new Erc20TemplateBuilder();
        }
        virtual public string? GetSmartContractCode(Erc20Model model)
        {
            /* 1. Transformations -> modelo!!
             * 2. Verifications  -> void ou exception
             * 3. Template -> string!
             */

            //1. Transformation:: ERC20
            if (_transformation == null)
            {
                throw new ArgumentException("Transformation");
            }
            var specification = _transformation.GetSpecification(model);


            //2. Verification

            _validator.Validator(specification);

            //3.Template
            _templateBuilder
                .WithName(specification.Name)
                .WithSymbol(specification.Symbol)
                .WithInitialSupply(specification.InitialSupply)
                ;
            return _templateBuilder.Build();
        }
    }
}
