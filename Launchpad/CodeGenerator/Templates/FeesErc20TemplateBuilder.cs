using Agap2It.Labs.Launchpad.CodeGenerator.Specifications;


namespace Agap2It.Labs.Launchpad.CodeGenerator.Templates
{
    public class FeesErc20TemplateBuilder : Erc20TemplateBuilder, IErc20TemplateBuilder<FeesErc20Specification>
    {
        private FeesErc20Specification _specification;
        public FeesErc20TemplateBuilder() 
        {
            _specification = new FeesErc20Specification();
        }

        public string Build() 
        {
            return base.Build();
        }

        public string Build(FeesErc20Specification specification) { return base.Build(specification); }


        public override Erc20TemplateBuilder WithName(string name)
        {
            base.WithName(name);
            this._specification.Name = name;
            return this;
        }

        public override Erc20TemplateBuilder WithSymbol(string symbol)
        {
            base.WithSymbol(symbol);
            this._specification.Symbol = symbol;
            return this;
        }

        public override Erc20TemplateBuilder WithInitialSupply(int supply)
        {
            base.WithInitialSupply(supply);
            this._specification.InitialSupply = supply;
            return this;
        }

        public FeesErc20TemplateBuilder WithFeesRate(float? fee)
        {
            this._specification.FeesRate = fee;
            return this;
        }
        public FeesErc20TemplateBuilder WithAddress(string? address)
        {
            this._specification.Address = address;
            return this;
        }

        override public Erc20TemplateBuilder Clear()
        {
            _specification = new FeesErc20Specification();
            return this;
        }
        public override string GenerateProperties()
        {
            return "\n   address public _taxCollector;"
                    + "\n" + $@"    uint256 public taxRate = {this._specification.FeesRate};" + "\n";
        }


        public override string GenerateConstructors()
        {
            return
               @$"  constructor() ERC20(""{_specification.Name}"", ""{_specification.Symbol}"") {{"
               + @$"      _taxCollector = {_specification.Address};"
               + @$"      _mint(msg.sender, {_specification.InitialSupply} * 10**18); // initial supply"
               + "\n  }"
            ;
        }

        public override string GenerateFunctions()
        {
            return "\n" +
@$"   function transfer(address to, uint256 value) override public virtual returns (bool) {{
        uint256 taxAmount = (value * taxRate) / 100;
        uint256 amountAfterTax = value - taxAmount;
        address owner = _msgSender();
        _transfer(owner, to, amountAfterTax);
        _transfer(owner, _taxCollector, taxAmount);
        return true;
     }}";
        }

    }
}
