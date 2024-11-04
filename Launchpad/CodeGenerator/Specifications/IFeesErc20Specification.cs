namespace Agap2It.Labs.Launchpad.CodeGenerator.Specifications
{
    public interface IFeesErc20Specification : IErc20Specification
    {
        public float? FeesRate { get; set; }
    }
}