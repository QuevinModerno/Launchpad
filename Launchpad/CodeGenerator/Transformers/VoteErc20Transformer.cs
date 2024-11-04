using Agap2It.Labs.Launchpad.CodeGenerator.Models;
using Agap2It.Labs.Launchpad.CodeGenerator.Specifications;
using Agap2It.Labs.Launchpad.CodeGenerator.Templates;

namespace Agap2It.Labs.Launchpad.CodeGenerator.Transformers
{
    public class VoteErc20Transformer : Erc20Transformer, IErc20Transformer
    {
        public VoteErc20Transformer() : base()
        {
            
        }

        public override VoteErc20Specification GetSpecification(Erc20Model model)
        {
            return new()
            {
                Name = model.Name,
                InitialSupply = model.InitialSupply,
                Symbol = model.Symbol,
                Description = model.Description
            };
        }
    }
}
