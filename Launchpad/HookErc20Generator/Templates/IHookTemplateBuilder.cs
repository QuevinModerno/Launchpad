

using Agap2It.Labs.Launchpad.HookErc20Generator.Specifications;

namespace Agap2It.Labs.Launchpad.HookErc20Generator.Templates
{
    public interface IHookTemplateBuilder<TErc20Specification> : IContractTemplateBuilder<TErc20Specification> where TErc20Specification : IHookSpecification, new()
    {
    }
}
