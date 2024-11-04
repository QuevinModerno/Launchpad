using Agap2It.Labs.Launchpad.HookErc20Generator.Models;
using Agap2It.Labs.Launchpad.HookErc20Generator.Specifications;
using Agap2It.Labs.Launchpad.HookErc20Generator.Templates;
using Agap2It.Labs.Launchpad.HookErc20Generator.Transformers;
using Agap2It.Labs.Launchpad.HookErc20Generator.Validators;
using Nethereum.ABI.ABIDeserialisation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agap2It.Labs.Launchpad.HookErc20Generator.Generators
{
    public class HookReadGenerator : IGenerator
    {
        HookTransfomer _Transformer;
        HookValidator _Validator;
        HookReadTemplateBuilder _Template;
        HookSpecification _specification;
        string _Abi;
        public HookReadGenerator() 
        {
            _Transformer = new HookTransfomer();
            _Validator = new HookValidator();
            _Template = new HookReadTemplateBuilder();
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
            _Template
                .WithName(_specification.Name)
                .WithContractAbi(_specification.ABI)
                .WithImport(_specification.Imports)
                .WithInputParameters(_specification.InputTypes)
                .WithReturnParameters(_specification.ReturnTypes);

            return _Template.Build();
        }
    }
}
