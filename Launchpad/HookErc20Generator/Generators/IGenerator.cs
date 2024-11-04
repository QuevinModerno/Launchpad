using Agap2It.Labs.Launchpad.HookErc20Generator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agap2It.Labs.Launchpad.HookErc20Generator.Generators
{
    public interface IGenerator
    {
        public string getHookCode(ContractFunctionModel model, string abi);
    }
}
