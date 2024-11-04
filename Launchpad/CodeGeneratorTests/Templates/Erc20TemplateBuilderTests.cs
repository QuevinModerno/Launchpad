using Microsoft.VisualStudio.TestTools.UnitTesting;

using NUnit.Framework;
using Agap2It.Labs.Launchpad.CodeGenerator.Specifications;

namespace Agap2It.Labs.Launchpad.CodeGenerator.Templates.Tests
{
    [TestClass()]
    public class Erc20TemplateBuilderTests
    {
        private Erc20TemplateBuilder _templateBuilder;


        public Erc20TemplateBuilderTests() { Setup(); }

        [SetUp]
        public void Setup()
        {
            Erc20Specification specification = new()
            {
                Name = "Test",
                Symbol = "TT",
                InitialSupply = 1
            };

            _templateBuilder = new Erc20TemplateBuilder(specification);
        }
        /*
        [TestMethod()]
        public void ClearTest()
        {
            NUnit.Framework.Assert.Fail();
        }
        

        [TestMethod()]
        public void WithNameTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void WithSymbolTest()
        {
            Assert.Fail();
        }
        
                [TestMethod()]
                public void WithInitialSupplyTest()
                {
                    Assert.Fail();
                }

                [TestMethod()]
                public void WithLicenseTest()
                {
                    Assert.Fail();
                }

                [TestMethod()]
                public void WithVersionTest()
                {
                    Assert.Fail();
                }

                [TestMethod()]
                public void WithImportTest()
                {
                    Assert.Fail();
                }


                [TestMethod()]
                public void GeneratePropertiesTest()
                {
                    Assert.Fail();
                }

                [TestMethod()]
                public void GenerateFunctionsTest()
                {
                    Assert.Fail();
                }
        */


        //GENERATION TESTS
        [TestMethod()]
        public void GenerateConstructorsTest()
        {
            Setup();
            Console.WriteLine(_templateBuilder.GenerateConstructors());

        }


        [TestMethod()]
        public void GenerateLicenseTest()
        {
            Console.WriteLine(_templateBuilder.GenerateLicense());
        }

        [TestMethod()]
        public void GenerateVersionTest()
        {
            Console.WriteLine(_templateBuilder.GenerateVersion());
        }

        [TestMethod()]
        public void GenerateImportsTest()
        {
            Console.WriteLine(_templateBuilder.GenerateImports());
        }


        [TestMethod()]
        public void BuildTest()
        {
            Console.WriteLine(_templateBuilder.Build());
        }
        /*
[TestMethod()]
public void GenerateContractBodyTest()
{

   NUnit.Framework.Assert.Fail();
}
*/
    }
}