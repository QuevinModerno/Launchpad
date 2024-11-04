using Agap2It.Labs.Launchpad.CodeGenerator.Templates;
using Agap2It.Labs.Launchpad.CodeGenerator.Models;
using Agap2It.Labs.Launchpad.CodeGenerator;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Agap2It.Labs.Launchpad.Launchpad.Tests;

public class TemplateBuilderTest
{
    [Test]
    public void Add_WhenCalled_ReturnsSum()
    {
        // Arrange
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
            "function _transfer(address sender, address recipient, uint256 amount) internal override {\r\n                                " +
            "uint256 taxAmount = (amount * taxRate) / 100;\r\n                               " +
            " uint256 amountAfterTax = amount - taxAmount;\r\n                                " +
            "super._transfer(sender, taxCollector, taxAmount);\r\n                               " +
            " super._transfer(sender, recipient, amountAfterTax);\r\n                            }");



        string result = template.contractBuilder();

         
        string filePath = "C:\\Users\\Acer\\Desktop\\Estagio\\test_output.txt";
        File.WriteAllText(filePath, result);
        Console.WriteLine(result);


        // Assert
        StringAssert.Contains("address public taxCollector", result);
        StringAssert.Contains("uint256 public taxRate = 5", result);
    }
}
