namespace EthGenerator.Contracts.Compiler.Model;

public class EthContractCompilationConfiguration
{
    public bool Optimize { get; set; }
    public int OptimizationRuns { get; set; }
    public string Version { get; set; }
    public string? BasePath { get; set; }
}
