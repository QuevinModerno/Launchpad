

using Agap2It.Labs.Launchpad.FunctionErc20Generator.Models;
using Agap2It.Labs.Launchpad.FunctionErc20Generator.Specifications;

namespace Agap2It.Labs.Launchpad.FunctionErc20Generator.Transformers
{
    public class FunctionTransformer : IFunctionTransformer
    {
        public FunctionTransformer() { }
        public FunctionSpecification GetSpecification(ContractFunctionModel function)
        {
            string[]? inputsTypeConverted = function.InputTypes;


            inputsTypeConverted = function.InputTypes.Select(s =>
            {
                if (s == "uint256")
                    return "BigInteger";
                else if (s == "uint8")
                    return "byte";
                else if (s == "bool")
                    return "bool";
                else
                    return "string";
            }).ToArray();

            /*  string inputTypes = string.Join(", ", function.InputTypes);
              string inputNames = string.Join(", " , function.InputNames);
            */
            string outputTypes = string.Join(", ", function.ReturnTypes);

            string[]? outputTypeConverted = function.ReturnTypes;


            outputTypeConverted = function.ReturnTypes.Select(s =>
            {
                if (s == "uint256")
                    return "BigInteger";
                else if (s == "uint8")
                    return "byte";
                else if (s == "bool")
                    return "bool";
                else
                    return "string";
            }).ToArray();

            string outputsTypeConverted = string.Join(", ", outputTypeConverted);

            return new()
            {
                Name = function.Name,
                InputTypes = function.InputTypes,
                InputTypesConverted = inputsTypeConverted,
                InputNames = function.InputNames,
                ReturnTypes = outputTypes,
                ReturnTypesConverted = outputsTypeConverted
            };
        }
    }
}
