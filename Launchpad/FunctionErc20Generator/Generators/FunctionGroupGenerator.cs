using Agap2It.Labs.Launchpad.FunctionErc20Generator.Models;
using Agap2It.Labs.Launchpad.FunctionErc20Generator.Specifications;
using Agap2It.Labs.Launchpad.FunctionErc20Generator.Templates;
using Agap2It.Labs.Launchpad.FunctionErc20Generator.Transformers;
using Agap2It.Labs.Launchpad.FunctionErc20Generator.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agap2It.Labs.Launchpad.FunctionErc20Generator.Generators
{
    public class FunctionGroupGenerator : IGenerator
    {
        FunctionTransformer _Transformer;
        FunctionValidator _Validator;
        FunctionGroupTemplateBuilder _Template;
        FunctionSpecification _specification;

        public FunctionGroupGenerator() 
        {
            _Transformer = new FunctionTransformer();
            _Validator = new FunctionValidator();
            _Template = new FunctionGroupTemplateBuilder();
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

            if (!model.IsReadOnly)
            {
                _Template = new FunctionGroupWriteTemplateBuilder();
            }
            else
            {
                _Template = new FunctionGroupTemplateBuilder();
            }

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
