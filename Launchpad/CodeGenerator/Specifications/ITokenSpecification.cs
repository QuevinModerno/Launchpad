

namespace Agap2It.Labs.Launchpad.CodeGenerator.Specifications
{
    public interface ITokenSpecification : ISmartContractSpecification
    {
        public string? Name { get; set; }
        public string? Symbol { get; set; }
    }
}
