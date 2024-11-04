
using Agap2It.Labs.Launchpad.CodeGenerator.Models;
using Agap2It.Labs.Launchpad.CodeGenerator.Specifications;
using Agap2It.Labs.Launchpad.CodeGenerator.Templates;
using NUnit.Framework.Constraints;
using System.ComponentModel.DataAnnotations;

namespace Agap2It.Labs.Launchpad.CodeGenerator.Transformers
{
    public class FeesErc20Transformer : Erc20Transformer
    {
        public FeesErc20Transformer() 
        { 
           
        }
        override public FeesErc20Specification GetSpecification(Erc20Model model)
        {
            return new()
            {
                Name = model.Name,
                InitialSupply = model.InitialSupply,
                Symbol = model.Symbol,
                FeesRate = model.FeesRate,
                Address = model.Address,
            };
        }


    }
}
