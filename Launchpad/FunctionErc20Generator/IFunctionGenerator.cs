using Agap2It.Labs.Launchpad.FunctionErc20Generator.Models;


namespace Agap2It.Labs.Launchpad.FunctionErc20Generator
{
    public interface IFunctionGenerator
    {
        public ContractFunctionModel[] GetContractFunctions(string abi);

        public string GetFunctionsCode(ContractFunctionModel[] model);
        public Dictionary<string, string> GetFunctionsCodeListed(ContractFunctionModel[] model);

        public byte[] GetFunctionsCodeZipped(ContractFunctionModel[] model);
    }
}
