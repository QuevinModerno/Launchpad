

namespace Agap2It.Labs.Launchpad.CodeGenerator.Specifications
{
    public interface IErc20Specification : ITokenSpecification
    {
        public int InitialSupply { get; set; }
    }
}
