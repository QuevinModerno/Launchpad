using Agap2It.Labs.Launchpad.FunctionErc20Generator.Models;
using Agap2It.Labs.Launchpad.FunctionErc20Generator.Templates;
using Agap2It.Labs.Launchpad.FunctionErc20Generator.Specifications;
using Agap2It.Labs.Launchpad.FunctionErc20Generator.Transformers;
using Agap2It.Labs.Launchpad.FunctionErc20Generator.Validators;


namespace Agap2It.Labs.Launchpad.FunctionErc20Generator.Generators
{
    public class FunctionReadGenerator : IGenerator
    {
        FunctionTransformer _Transformer;
        FunctionValidator _Validator;
        FunctionReadTemplateBuilder _Template;
        FunctionSpecification _specification;
        public FunctionReadGenerator()
        {
            _Transformer = new FunctionTransformer();
            _Validator = new FunctionValidator();
            _Template = new FunctionReadTemplateBuilder();
            _specification = new FunctionSpecification();
        }
        public string getFunctionCode(ContractFunctionModel model)
        {
            _specification = _Transformer.GetSpecification(model);
            if (_specification == null)
            {
                throw new ArgumentException("Error transformation.");
            }
            _Validator.Validator(_specification);
            _Template
                .WithName(_specification.Name)
                .WithInputParameters(_specification.InputTypes)
                .WithInputParametersConverted(_specification.InputTypesConverted)
                .WithInputNames(_specification.InputNames)
                .WithReturnParameters(_specification.ReturnTypes)
                .WithReturnParametersConverted(_specification.ReturnTypesConverted);

            return _Template.Build();
        }

    }
}
