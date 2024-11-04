
namespace Agap2It.Labs.Launchpad.FunctionErc20Generator.Specifications
{
    public interface IFunctionSpecification : IContractFunctionsSpecification
    {
        public List<string>? Imports { get; set; }
    }
}
