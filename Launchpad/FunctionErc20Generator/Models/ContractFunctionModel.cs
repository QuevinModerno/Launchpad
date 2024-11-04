

namespace Agap2It.Labs.Launchpad.FunctionErc20Generator.Models
{
    public class ContractFunctionModel
    {
        public string? Name { get; set; }
        public string[]? InputTypes { get; set; }
        public string[]? InputNames { get; set; }
        public string[]? ReturnTypes { get; set; }
        public bool IsReadOnly { get; set; }
    }
}
