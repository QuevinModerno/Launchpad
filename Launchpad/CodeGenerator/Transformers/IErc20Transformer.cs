using Agap2It.Labs.Launchpad.CodeGenerator.Models;
using Agap2It.Labs.Launchpad.CodeGenerator.Specifications;

namespace Agap2It.Labs.Launchpad.CodeGenerator.Transformers
{
    public interface IErc20Transformer
    {
        public Erc20Specification GetSpecification(Erc20Model model);
    }
}
