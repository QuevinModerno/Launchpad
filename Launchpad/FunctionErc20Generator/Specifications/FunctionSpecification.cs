


using Org.BouncyCastle.Bcpg;

namespace Agap2It.Labs.Launchpad.FunctionErc20Generator.Specifications
{
    public class FunctionSpecification : IContractFunctionsSpecification
    {
        public string? Name { get ; set ; }
        public string[]? InputTypes { get ; set ; }
        public string[]? InputTypesConverted { get ; set ; }
        public string[]? InputNames {  get ; set ; }
        public string? ReturnTypes { get ; set ; }
        public string? ReturnTypesConverted { get ; set ; }
        //public string? ABI { get ; set ; }
        //public List<string>? Imports { get; set; } = new();
    }
}
