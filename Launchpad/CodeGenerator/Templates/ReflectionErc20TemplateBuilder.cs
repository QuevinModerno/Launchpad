using Agap2It.Labs.Launchpad.CodeGenerator.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agap2It.Labs.Launchpad.CodeGenerator.Templates
{
    public class ReflectionErc20TemplateBuilder : Erc20TemplateBuilder, IErc20TemplateBuilder<ReflectionErc20Specification>
    {
        private ReflectionErc20Specification _specification;
        
        public ReflectionErc20TemplateBuilder() 
        { 
            _specification = new ReflectionErc20Specification();
        }
        public string Build()
        {
            return base.Build();
        }

        public string Build(ReflectionErc20Specification specification)
        {
            throw new NotImplementedException();
        }

        public override ReflectionErc20TemplateBuilder WithSymbol(string symbol)
        {
            base.WithSymbol(symbol);
            this._specification.Symbol = symbol;
            return this;
        }

        public override ReflectionErc20TemplateBuilder WithInitialSupply(int supply)
        {
            base.WithInitialSupply(supply);
            this._specification.InitialSupply = supply;
            return this;
        }

        public ReflectionErc20TemplateBuilder WithFeesRate(float? fee)
        {
            this._specification.FeesRate = fee;
            return this;
        }


        override public ReflectionErc20TemplateBuilder Clear()
        {
            _specification = new ReflectionErc20Specification();
            return this;
        }

        public override string GenerateImports()
        {
            return "import \"@openzeppelin/contracts/token/ERC20/IERC20.sol\";\r\n";
        }

        public override string GenerateProperties()
        {
            return "\n    mapping(address => uint256) private _balances;\r\n    mapping(address => mapping(address => uint256)) private _allowances;\r\n    mapping(address => uint256) private reflectedFees; // Reflected fees for each token holder\r\n\r\n    string public name;\r\n    string public symbol;\r\n    uint8 public decimals = 18;\r\n    uint256 private _totalSupply;\r\n    uint256 private feesRate;\r\n    uint256 public totalFeesCollected; \r\n    // Array to store keys\r\n    address[] public feeReceiversKeys;  ";
        }


        public override string GenerateConstructors()
        {
            return
               @$"  constructor()"
               + @$"        name = {_specification.Name};\r\n        symbol = {_specification.Symbol};\r\n        _totalSupply = {_specification.InitialSupply} * 10 ** uint256(18);\r\n        _balances[msg.sender] = _totalSupply;\r\n        feesRate = {_specification.FeesRate} / 100;\r\n        totalFeesCollected = 0;\r\n\r\n        emit Transfer(address(0), msg.sender, _totalSupply);"
               + "\n  }"
            ;
        }

        public override string GenerateFunctions()
        {
            return "\n" + "    function totalSupply() public view override returns (uint256) {\r\n        return _totalSupply;\r\n    }\r\n\r\n    function balanceOf(address account) public view override returns (uint256) {\r\n        return _balances[account];\r\n    }\r\n\r\n    function transfer(address recipient, uint256 amount) public override returns (bool) {\r\n        _transfer(msg.sender, recipient, amount);\r\n        return true;\r\n    }\r\n\r\n    function allowance(address owner, address spender) public view override returns (uint256) {\r\n        return _allowances[owner][spender];\r\n    }\r\n\r\n    function approve(address spender, uint256 amount) public override returns (bool) {\r\n        _approve(msg.sender, spender, amount);\r\n        return true;\r\n    }\r\n\r\n    function transferFrom(address sender, address recipient, uint256 amount) public override returns (bool) {\r\n        _transfer(sender, recipient, amount);\r\n        _approve(sender, msg.sender, _allowances[sender][msg.sender] - amount);\r\n        return true;\r\n    }\r\n\r\n    function increaseAllowance(address spender, uint256 addedValue) public returns (bool) {\r\n        _approve(msg.sender, spender, _allowances[msg.sender][spender] + addedValue);\r\n        return true;\r\n    }\r\n\r\n    function decreaseAllowance(address spender, uint256 subtractedValue) public returns (bool) {\r\n        _approve(msg.sender, spender, _allowances[msg.sender][spender] - subtractedValue);\r\n        return true;\r\n    }\r\n\r\n    function _transfer(address sender, address recipient, uint256 amount) internal {\r\n        require(sender != address(0), \"ERC20: transfer from the zero address\");\r\n        require(recipient != address(0), \"ERC20: transfer to the zero address\");\r\n        require(_balances[sender] >= amount, \"ERC20: insufficient balance\");\r\n        \r\n        _beforeTokenTransfer(recipient, amount);\r\n        _balances[sender] -= amount;\r\n        _balances[recipient] += amount;\r\n        emit Transfer(sender, recipient, amount);\r\n    }\r\n\r\n    function _mint(address account, uint256 amount) internal virtual {\r\n        require(account != address(0), \"ERC20: burn from the zero address\");\r\n        require(_balances[account] >= amount, \"ERC20: insufficient balance\");\r\n    \r\n        _beforeTokenTransfer( address(0), amount);\r\n\r\n        _balances[account] -= amount;\r\n        _totalSupply += amount;\r\n        emit Transfer(account, address(0), amount);\r\n    }\r\n\r\n    function _burn(address account, uint256 amount) internal virtual {\r\n        require(account != address(0), \"ERC20: burn from the zero address\");\r\n        require(_balances[account] >= amount, \"ERC20: insufficient balance\");\r\n    \r\n        _beforeTokenTransfer( address(0), amount);\r\n        _balances[account] -= amount;\r\n        _totalSupply -= amount;\r\n        emit Transfer(account, address(0), amount);\r\n    }\r\n\r\n    function _approve(address owner, address spender, uint256 amount) internal {\r\n        require(owner != address(0), \"ERC20: approve from the zero address\");\r\n        require(spender != address(0), \"ERC20: approve to the zero address\");\r\n\r\n        _allowances[owner][spender] = amount;\r\n        emit Approval(owner, spender, amount);\r\n    }\r\n\r\n    function _beforeTokenTransfer(address recipient, uint256 amount) internal returns (uint256){\r\n        \r\n    //100 tokens; 5 feeRate; \r\n    //100 * 0,5\r\n    //\r\n        addReflectionOwner(recipient);\r\n        uint256 feeAmount = feesRate * amount;\r\n        totalFeesCollected = totalFeesCollected + feeAmount; // Update total fees collected\r\n        \r\n        for(uint256 i =0; i < feeReceiversKeys.length; i++){\r\n            address account = feeReceiversKeys[i];\r\n            uint256 balance = _balances[account];\r\n            uint256 feeShare = feeAmount * (balance);\r\n            feeShare = feeShare / (_totalSupply);\r\n            reflectedFees[account] = reflectedFees[account] + (feeShare);\r\n        }\r\n        return amount - feeAmount;\r\n    }\r\n\r\n    function addReflectionOwner(address _receiver) internal{\r\n        \r\n        if(balanceOf(_receiver) == 0 || _receiver != address(0))\r\n            feeReceiversKeys.push(_receiver);\r\n    }\r\n\r\n    function claimReflectedFees() public returns (bool) {\r\n        uint256 amount = reflectedFees[msg.sender]; // Get reflected fees for the caller\r\n        require(amount > 0, \"No reflected fees to claim\");\r\n        reflectedFees[msg.sender] = 0; // Reset reflected fees for the caller\r\n        _balances[msg.sender] += amount;\r\n\r\n        return true;\r\n    }";
        }

   }

}
