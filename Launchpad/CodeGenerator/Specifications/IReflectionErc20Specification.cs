using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agap2It.Labs.Launchpad.CodeGenerator.Specifications
{
    public interface IReflectionErc20Specification : IErc20Specification
    {
          public float? FeesRate { get; set; }

    }
}
