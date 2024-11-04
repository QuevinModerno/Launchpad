

using Agap2It.Labs.Launchpad.CodeGenerator.Models;
using Agap2It.Labs.Launchpad.CodeGenerator.Specifications;
using Agap2It.Labs.Launchpad.CodeGenerator.Templates;


namespace Agap2It.Labs.Launchpad.CodeGenerator.Transformers
{
    public class BurnErc20Transformer : Erc20Transformer, IErc20Transformer
    {
        public BurnErc20Transformer() : base() 
        {
        }

        public override BurnErc20Specification GetSpecification(Erc20Model model)
        {
            return  new ()
            {
                Name = model.Name,
                InitialSupply = model.InitialSupply,
                Symbol = model.Symbol,
            };
        }


    }
}
