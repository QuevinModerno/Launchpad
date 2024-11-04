using Agap2It.Labs.Launchpad.HookErc20Generator.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agap2It.Labs.Launchpad.HookErc20Generator.Templates
{
    public interface IContractTemplateBuilder<TSpecification> where TSpecification : IContractFunctionsSpecification
    {
        public string Build();
        public string Build(TSpecification specification);
    }
}
