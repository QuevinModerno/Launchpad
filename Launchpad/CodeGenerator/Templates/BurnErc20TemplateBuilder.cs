

using Agap2It.Labs.Launchpad.CodeGenerator.Specifications;

namespace Agap2It.Labs.Launchpad.CodeGenerator.Templates
{
    public class BurnErc20TemplateBuilder : Erc20TemplateBuilder, IErc20TemplateBuilder<BurnErc20Specification>
    {
        private BurnErc20Specification _specification ;

        public BurnErc20TemplateBuilder() 
        {
            _specification = new BurnErc20Specification();
        }
        public string Build(BurnErc20Specification specification)
        {
            return base.Build(specification);
        }

        public override string GenerateProperties()
        {
            return @"mapping(address => uint256) public burnedTokensByAccount;";
        }
        public override string GenerateFunctions()
        {
            return @"
                    /**
                     * @dev Burns a specific amount of tokens from the caller and updates the record of burned tokens.
                     * @param amount The amount of token to be burned.
                     */
                    function burn(uint256 amount) public {
                        _burn(msg.sender, amount);
                        burnedTokensByAccount[msg.sender] += amount; // Update the burned tokens record for the caller
                    }";
        }

        override public Erc20TemplateBuilder Clear()
        {
            _specification = new BurnErc20Specification();
            return this;
        }

    }
}
