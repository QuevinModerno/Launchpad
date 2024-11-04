using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Agap2It.Labs.Launchpad.HookErc20Generator.Tests
{
    [TestClass()]
    public class HookGeneratorTests
    {
        [TestMethod()]
        public void GetContractFunctionsTest()
        {
            string path = @"C:\Users\Acer\Desktop\erc20abi.txt";

            // Read the file contents
            string fileContent = File.ReadAllText(path);

            HookGenerator hook = new HookGenerator();
            var functions = hook.GetContractFunctions(fileContent);

            foreach (var function in functions)
            {
                string inputTypes = string.Join(", ", function.InputTypes);
                string outputTypes = string.Join(", ", function.ReturnTypes);
                string readOnly = function.IsReadOnly ? "Read-Only" : "Read/Write";
                Console.WriteLine($"{function.Name}({inputTypes}) -> ({outputTypes}) [{readOnly}]");
            }
        }

        [TestMethod()]
        public void GetHookCodeTest()
        {
            string path = @"C:\Users\Acer\Desktop\erc20abi.txt";

            // Read the file contents
            string fileContent = File.ReadAllText(path);

            HookGenerator hook = new HookGenerator();
            var functions = hook.GetContractFunctions(fileContent);

            string filePath = "C:\\Users\\Acer\\Desktop\\testHook\\balanceof2.txt";
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine(hook.GetHookCode(functions[2]));
                }
                Console.WriteLine($"Resultado escrito no arquivo {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro ao escrever no arquivo: {ex.Message}");
            }

           
        }
    }
}