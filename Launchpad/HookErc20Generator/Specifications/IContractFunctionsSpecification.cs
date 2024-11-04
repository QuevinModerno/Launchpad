

namespace Agap2It.Labs.Launchpad.HookErc20Generator.Specifications
{
    public interface IContractFunctionsSpecification
    {
        public string? Name { get; set; }
        public string? InputTypes { get; set; }
        public string? ReturnTypes { get; set; }
        public string? ABI { get; set; }
    }
}
