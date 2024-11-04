using Agap2It.Labs.Launchpad.Business.Base;
using Agap2It.Labs.Launchpad.CodeGenerator.Enums;
using Agap2It.Labs.Launchpad.CodeGenerator.Models;
using EthGenerator.Contracts.Compiler.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agap2It.Launchpad.Business.BusinessObjects
{
    public interface IErc20CodePublisherBusinessObject
    {
        Task<OperationResult<EthContract?>> PublishErc20(string contract);

    }
}
