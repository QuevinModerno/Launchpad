using Microsoft.VisualStudio.TestTools.UnitTesting;
using Agap2It.Labs.Launchpad.CodeGenerator.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agap2It.Labs.Launchpad.CodeGenerator.Templates.Tests
{
    [TestClass()]
    public class TemplateBuilderTests
    {
        [TestMethod()]
        public void test() {

            StandardModel erc20 = new();
            erc20.Name = "Teste";
            erc20.Symbol = "TesteSynbo";
            erc20.InitialSupply = 1;
        TemplateBuilder template = new TemplateBuilder(erc20);

        // Act
        template.AddVariableDeclarations("address public taxCollector");

        template.AddVariableDeclarations("uint256 public taxRate = 5");
        template.AddParameters("address _taxCollector");


        template.InitiateVariablesToConstructor("require(_taxCollector != address(0), \"Invalid tax collector address\")");
        template.InitiateVariablesToConstructor("taxCollector = _taxCollector");


        template.AddFunctions(" " +
            "function _transfer(address sender, address recipient, uint256 amount) internal override {\n" +
            "uint256 taxAmount = (amount * taxRate) / 100;\n" +
            " uint256 amountAfterTax = amount - taxAmount;\n" +
            "super._transfer(sender, taxCollector, taxAmount);\n" +
            " super._transfer(sender, recipient, amountAfterTax);\n}");



        string result = template.contractBuilder();


        string filePath = "C:\\Users\\Acer\\Desktop\\Estagio\\test_output.txt";
        File.WriteAllText(filePath, result);
        Console.WriteLine(result);


        // Assert
        StringAssert.Contains("address public taxCollector", result);
        StringAssert.Contains("uint256 public taxRate = 5", result);
        }
    }
}
