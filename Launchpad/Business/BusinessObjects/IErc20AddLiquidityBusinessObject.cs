using Agap2It.Labs.Launchpad.Business.Base;


namespace Agap2It.Launchpad.Business.BusinessObjects
{
    public interface IErc20AddLiquidityBusinessObject
    {
        Task<OperationResult<AddLiquidityModel>> AddLiquidity(string tokenAdress, string tokenAmmount); 
    }
}
