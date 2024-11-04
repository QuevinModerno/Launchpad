using Agap2It.Labs.Launchpad.CodeGenerator.Models;
using Agap2It.Labs.Launchpad.CodeGenerator.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agap2It.Labs.Launchpad.CodeGenerator.Transformers
{
    public class ReflectionErc20Transformer :Erc20Transformer
    {
        public ReflectionErc20Transformer() { }

        override public ReflectionErc20Specification GetSpecification(Erc20Model model)
        {
            return new()
            {
                Name = model.Name,
                InitialSupply = model.InitialSupply,
                Symbol = model.Symbol,
                FeesRate = model.FeesRate,
            };
        }
    }
}
