using Agap2It.Labs.Launchpad.Business.Base;
using Agap2It.Labs.Launchpad.CodeGenerator;
using Microsoft.Extensions.Logging;
using System;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Agap2It.Launchpad.Business.BusinessObjects
{
    public class Erc20AddLiquidityBusinessObject : IErc20AddLiquidityBusinessObject
    {
        private readonly ILogger<Erc20AddLiquidityBusinessObject> _logger;

        public Erc20AddLiquidityBusinessObject(ILogger<Erc20AddLiquidityBusinessObject> logger)
        {
            _logger = logger;
        }

        public Task<OperationResult<AddLiquidityModel>> AddLiquidity(string tokenAdress, string tokenAmmount)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetTokenNameAsync(string contractAddress, string abi)
        {
            try
            {
                Web3 web3 = new Web3();

                var contract = web3.Eth.GetContract(abi, contractAddress);
                var nameFunction = contract.GetFunction("name");
                var result = await nameFunction.CallAsync<string>();
                Console.WriteLine(result);

                return result.ToString();
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
        public async Task<string> GetTokenSymbolAsync(string contractAddress, string abi, string Web3Url)
        {
            try
            {
                Web3 web3 = new Web3(Web3Url);

                var contract = web3.Eth.GetContract(abi, contractAddress);
                var nameFunction = contract.GetFunction("symbol");
                var result = await nameFunction.CallAsync<string>();

                return result.ToString();
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
        public async Task<string> GetTokenSupplyAsync(string contractAddress, string abi, string Web3Url)
        {
            try
            {
                Web3 web3 = new Web3(Web3Url);

                var contract = web3.Eth.GetContract(abi, contractAddress);
                var nameFunction = contract.GetFunction("supply");
                var result = await nameFunction.CallAsync<string>();

                return result.ToString();
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}
/*
        public async Task<OperationResult<Nethereum.RPC.Eth.DTOs.TransactionInput?>> AddLiquidity(string recipient, string token0, string token1, string token0Amount, string token1Amount)
        {/*
            try
            
            {
                string abi = "";
                Web3 web = new Web3();
                var routerContract = web.Eth.GetContract(abi, "UNISWAP_ROUTER_ADDRESS");
                var function = routerContract.GetFunction("addLiquidity");

                var amountADesiredWei = Web3.Convert.ToWei(token0Amount);
                var amountBDesiredWei = Web3.Convert.ToWei(token1Amount);
                var amountAMin = Web3.Convert.ToWei(0); // Ajuste conforme necessário
                var amountBMin = Web3.Convert.ToWei(0); // Ajuste conforme necessário
                var deadline = DateTimeOffset.Now.ToUnixTimeSeconds() + 1200;
                await Task.Delay(1000);


                var txInput = function.CreateTransactionInput(
                    from: recipient,
                    gas: new HexBigInteger(300000),
                    value: new HexBigInteger(0),
                    token0,
                    token1,
                    new HexBigInteger(amountADesiredWei),
                    new HexBigInteger(amountBDesiredWei),
                    new HexBigInteger(amountAMin),
                    new HexBigInteger(amountBMin),
                    "asd",
                    new HexBigInteger(deadline)
                );

                return new OperationResult<Nethereum.RPC.Eth.DTOs.TransactionInput?>(txInput);
            }
            catch (Exception ex)
            {
                //Aceitar as excessoes de verificações / Erro na producao
                _logger.LogError(ex.Message);
                return new OperationResult<Nethereum.RPC.Eth.DTOs.TransactionInput?>(null);
            }

        }
    }
*/
    

