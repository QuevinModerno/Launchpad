using Microsoft.VisualStudio.TestTools.UnitTesting;
using Agap2It.Labs.Launchpad.FunctionErc20Generator;

namespace Agap2It.Labs.Launchpad.FunctionErc20Generator.Tests
{
    [TestClass()]
    public class FunctionGeneratorTests
    {
        [TestMethod()]
        public async Task GetTokenNameAsyncTestAsync()
        {
            string path = @"C:\Users\Acer\Desktop\erc20abi.txt";
            string address = "0x356D09FDC1Dc7ab993d042Ff997b98ce773A790E";

            // Read the file contents
            string abi = File.ReadAllText(path);

            FunctionGenerator function = new FunctionGenerator();

            var functionResult = await function.GetTokenNameAsync(address, abi);
            Console.WriteLine(functionResult);

            /*
            string filePath = "C:\\Users\\Acer\\Desktop\\testfunction\\balanceof2.txt";
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine(function.GetHookCode(functions[2]));
                }
                Console.WriteLine($"Resultado escrito no arquivo {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro ao escrever no arquivo: {ex.Message}");
            }
            */
        }

        [TestMethod()]
        public void GetContractFunctionsTest()
        {
            string path = @"C:\Users\Acer\Desktop\erc20abi.txt";

            // Read the file contents
            string abi = File.ReadAllText(path);

            FunctionGenerator function = new FunctionGenerator();
            var functions = function.GetContractFunctions(abi);


            string filePath = "C:\\Users\\Acer\\Desktop\\testFunction\\thisIsReadOnly.txt";
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine(function.GetFunctionsCode(functions));
                }
                Console.WriteLine($"Resultado escrito no arquivo {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro ao escrever no arquivo: {ex.Message}");
            }

        }

        [TestMethod()]
        public void GetContractFunctionsTest1()
        {
            string path = @"C:\Users\Acer\Desktop\erc20abi.txt";

            // Read the file contents
            string abi = File.ReadAllText(path);

            FunctionGenerator functionGen = new FunctionGenerator();
            var functions = functionGen.GetContractFunctions(abi);

            foreach (var function in functions)
            {
                string inputTypes = string.Join(", ", function.InputTypes);
                string inputNames = string.Join(", ", function.InputNames);
                string outputTypes = string.Join(", ", function.ReturnTypes);
                string readOnly = function.IsReadOnly ? "Read-Only" : "Read/Write";
                Console.WriteLine($"{function.Name}({inputTypes} - {inputNames}) -> ({outputTypes} ) [{readOnly}]");
            }
        }

        [TestMethod()]
        public void GenerateFunctionGroupTest()
        {
            string path = @"C:\Users\Acer\Desktop\erc20abi.txt";

            // Read the file contents
            string abi = File.ReadAllText(path);

            FunctionGenerator function = new FunctionGenerator();
            var functions = function.GetContractFunctions(abi);


            string filePath = "C:\\Users\\Acer\\Desktop\\testFunction\\FunctionGroup.txt";
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine(function.GenerateFunctionGroup(functions));
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