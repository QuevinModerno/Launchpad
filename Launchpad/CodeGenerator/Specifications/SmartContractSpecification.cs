namespace Agap2It.Labs.Launchpad.CodeGenerator.Specifications
{
    public class SmartContractSpecification : ISmartContractSpecification
    {
        public string License { get ; set ; }
        public string Version { get ; set ; }
        public List<string> Imports { get; set; } = new();
    }
}
