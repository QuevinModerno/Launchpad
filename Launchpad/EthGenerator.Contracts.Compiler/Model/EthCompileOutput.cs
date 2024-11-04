using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace EthGenerator.Contracts.Compiler.Model;

internal class EthCompileOutput
{
    [JsonProperty("contracts")]
    public JObject Contracts { get; set; } = new();
    [JsonProperty("version")]
    public string? Version { get; set; }
}

public class EthContractCompileOutput
{
    [JsonProperty("bin")]
    public string? Bytecode { get; set; }

    [JsonProperty("abi")]
    public string? Abi { get; set; }
}
