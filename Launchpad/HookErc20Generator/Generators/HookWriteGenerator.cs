

using Agap2It.Labs.Launchpad.HookErc20Generator.Models;
using Agap2It.Labs.Launchpad.HookErc20Generator.Specifications;
using Agap2It.Labs.Launchpad.HookErc20Generator.Templates;
using Agap2It.Labs.Launchpad.HookErc20Generator.Transformers;
using Agap2It.Labs.Launchpad.HookErc20Generator.Validators;

namespace Agap2It.Labs.Launchpad.HookErc20Generator.Generators
{

    public class HookWriteGenerator : IGenerator
    {
        HookTransfomer _Transformer;
        HookValidator _Validator;
        HookWriteTemplateBuilder _Template;
        HookSpecification _specification;
        string _Abi;

        public HookWriteGenerator()
        {
            _Template = new HookWriteTemplateBuilder();
            _Transformer = new HookTransfomer();
            _Validator = new HookValidator();
            _specification = new HookSpecification();
            _Abi = "";
        }
        public string getHookCode(ContractFunctionModel model, string abi)
        {
            _specification = _Transformer.GetSpecification(model, _Abi);
            if (_specification == null)
            {
                throw new ArgumentException("Error transformation.");
            }
            _Validator.Validator(_specification);

            return _Template.Build();
        }
    }
}
