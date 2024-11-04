using Agap2It.Labs.Launchpad.CodeGenerator.Models;
using Agap2It.Labs.Launchpad.CodeGenerator.Specifications;
using Agap2It.Labs.Launchpad.CodeGenerator.Templates;
using System.ComponentModel.DataAnnotations;




namespace Agap2It.Labs.Launchpad.CodeGenerator.Transformers
{

    public class Erc20Transformer : IErc20Transformer
    {
        public Erc20Transformer() 
        {
            
        }

        virtual public Erc20Specification GetSpecification(Erc20Model model)
        {
            return new()
            {
                Name = model.Name,
                InitialSupply = model.InitialSupply,
                Symbol = model.Symbol
            };
        }

    }
}
