using Agap2It.Labs.Launchpad.HookErc20Generator.Models;
using Agap2It.Labs.Launchpad.HookErc20Generator.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agap2It.Labs.Launchpad.HookErc20Generator.Transformers
{
    public class HookWriteTransformer : HookTransfomer, IHookTransformer
    {
        public HookWriteTransformer() { }

        public override HookWriteSpecification GetSpecification(ContractFunctionModel functions, string abi)
        {
            throw new NotImplementedException();
        }

    }
}
