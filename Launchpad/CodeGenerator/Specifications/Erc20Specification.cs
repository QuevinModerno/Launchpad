

namespace Agap2It.Labs.Launchpad.CodeGenerator.Specifications
{
    public class Erc20Specification : SmartContractSpecification, IErc20Specification
    {
        public int InitialSupply { get; set; }
        public string? Name { get; set; }
        public string? Symbol { get; set; }
    }
}
