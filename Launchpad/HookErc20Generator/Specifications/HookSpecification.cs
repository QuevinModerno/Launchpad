

namespace Agap2It.Labs.Launchpad.HookErc20Generator.Specifications
{
    public class HookSpecification : IHookSpecification
    {
        public string? Name { get ; set ; }
        public string? InputTypes { get ; set ; }
        public string? ReturnTypes { get ; set ; }
        public string? ABI { get ; set ; }
        public List<string> Imports { get; set; } = new();
    }
}
