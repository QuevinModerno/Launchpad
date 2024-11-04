using EthGenerator.Contracts.Compiler.Model;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace EthGenerator.Contracts.Compiler;
public class EthCompiler
{
    public static EthContract Compile(string code, EthContractCompilationConfiguration configuration)
    {
        string compilerFileName = $"solc-{configuration.Version.Replace(".", "_")}.exe";
        var basePath = configuration.BasePath ?? AppDomain.CurrentDomain.BaseDirectory;
        string compilerPath = Path.Combine(basePath, "Support", compilerFileName);
        string contractsPath = Path.Combine(basePath, "Support", "contracts");
        List<EthContract> result = new();
        if (!File.Exists(compilerPath))
        {
            throw new FileNotFoundException($"SOL compiler for version {configuration.Version} was not found");
        }

        Match pragmaMatch = Regex.Match(code, @"pragma\s+solidity\s+([^;]+);");
        if (!pragmaMatch.Success || !pragmaMatch.Groups[1].Value.Contains(configuration.Version))
        {
            throw new ArgumentException("Pragma Version mismatch");
        }

        string tempFilePath = Path.GetTempFileName() + ".sol";
        File.WriteAllText(tempFilePath, code);

        string optimizationOption = configuration.Optimize ? $"--optimize --optimize-runs={configuration.OptimizationRuns} " : "";
        string importPathOption = $"--include-path {contractsPath}/ --base-path . --allow-paths .";
        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = compilerPath,
            Arguments = $"{importPathOption} {optimizationOption}--combined-json bin,abi -- {tempFilePath} ",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = false
        };
        
        using Process process = Process.Start(psi) ?? throw new Exception("Failed to compile");
        using StreamReader reader = process.StandardOutput;
        string output = reader.ReadToEnd();
        if(string.IsNullOrEmpty(output)) throw new Exception("Failed to compile");
        File.Delete(tempFilePath);

        var ethResult = JsonConvert.DeserializeObject<EthCompileOutput>(output);
        var entry = ethResult?.Contracts.First?.First ?? throw new Exception("Failed to compile");

        return new EthContract() { Code = code, Abi = JsonConvert.SerializeObject(entry["abi"]), ByteCode = JsonConvert.SerializeObject(entry["bin"]).Replace("\"", "")};
    }

    public static EthContract Compile(EthContract contract, EthContractCompilationConfiguration configuration)
    {
        return Compile(contract.Code!, configuration);
    }
}
