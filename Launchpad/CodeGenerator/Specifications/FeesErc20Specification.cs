

namespace Agap2It.Labs.Launchpad.CodeGenerator.Specifications
{
    public class FeesErc20Specification : Erc20Specification, IFeesErc20Specification
    {
        public float? FeesRate { get; set; }
        public string? Address { get; set; }
    }
}
