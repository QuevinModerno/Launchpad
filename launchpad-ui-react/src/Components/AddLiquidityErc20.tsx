import React, { useEffect, useState } from 'react';
import { Contract, ethers } from 'ethers';
import { abi as IUniswapV2Router02ABI } from '@uniswap/v2-periphery/build/IUniswapV2Router02.json';

const addLiquidityETH = async (
  userAdress: string,
  signer: ethers.JsonRpcSigner | null,
  tokenAddress: string,
  tokenAmount: string,
  EthAmount: string

) => {
  if (!signer || !tokenAddress || !tokenAmount) return;


  const routerAddress = '0x7a250d5630B4cF539739dF2C5dAcb4c659F2488D'; // Uniswap V2 Router address
  const routerContract = new Contract(routerAddress, IUniswapV2Router02ABI, signer);

  const tokenContract = new Contract(tokenAddress, [
    "function approve(address spender, uint256 amount) public returns (bool)"
  ], signer);

  const amountTokenParsed = ethers.parseUnits(tokenAmount, 18);
  const amountTokenMin = ethers.parseUnits((parseFloat(tokenAmount) * 0.9).toString(), 18);
  const deadline = Math.floor(Date.now() / 1000) + 60 * 20; // 20 minutos a partir de agora

  // Get the amount of ETH required for the given token amount
  const amountEthParsed = ethers.parseUnits(EthAmount, 18);
  const amountEthMin = ethers.parseUnits((parseFloat(EthAmount) * 0.9).toString(), 18); // 90% of the required ETH amount

  // Aprovar o Uniswap Router para gastar os tokens
  await tokenContract.approve(routerAddress, amountTokenParsed);

  const tx = await routerContract.addLiquidityETH(
    tokenAddress,
    amountTokenParsed,
    amountTokenMin,
    amountEthMin,
    userAdress,
    deadline,
    { value: amountEthParsed }
  );

  await tx.wait();
  console.log('Liquidity added with ETH');
};

export default addLiquidityETH;

/*
  return (
    <div>
      <h1>Uniswap Liquidity Pool</h1>
      {account ? (
        <div>
          <p>Connected Account: {account}</p>
          <input 
            type="text" 
            placeholder="Token A Address" 
            value={tokenAAddress} 
            onChange={(e) => setTokenAAddress(e.target.value)} 
          />
          <input 
            type="text" 
            placeholder="Token B Address" 
            value={tokenBAddress} 
            onChange={(e) => setTokenBAddress(e.target.value)} 
          />
          <input 
            type="text" 
            placeholder="Amount of Token A" 
            value={amountTokenA} 
            onChange={(e) => setAmountTokenA(e.target.value)} 
          />
          <input 
            type="text" 
            placeholder="Amount of Token B" 
            value={amountTokenB} 
            onChange={(e) => setAmountTokenB(e.target.value)} 
          />
          <button onClick={addLiquidity}>Add Liquidity</button>
        </div>
      ) : (
        <p>Please connect to MetaMask</p>
      )}
    </div>
  );
};

export default addLiquidity;


/*
import React, { useState } from 'react';
import { ethers } from 'ethers';
import { Token, Fetcher, Route, Trade, TokenAmount, TradeType, Percent } from '@uniswap/sdk';
import useMetaMask from './ConnectWallet';

const AddLiquidity: React.FC = () => {
  const { provider, account, connect } = useMetaMask();
  const [tokenA, setTokenA] = useState<string>('');
  const [tokenB, setTokenB] = useState<string>('');
  const [amountA, setAmountA] = useState<string>('');
  const [amountB, setAmountB] = useState<string>('');

  const handleAddLiquidity = async () => {
    if (!provider || !account) {
      alert('Please connect to MetaMask.');
      return;
    }
    const uniswapRouterAddress = "0xE592427A0AEce92De3Edee1F18E0157C05861564"; // Uniswap V3 Router
    const uniswapRouterAbi = [
        'function addLiquidityETH(address token,uint amountTokenDesired,uint amountTokenMin,uint amountETHMin,address to,uint deadline) payable returns (uint amountToken, uint amountETH, uint liquidity)'
    ];
    const signer = provider.getSigner();
    const tokenAContract = new ethers.Contract(tokenA, ERC20_ABI, signer);
    const tokenBContract = new ethers.Contract(tokenB, ERC20_ABI, signer);

    const amountADesired = ethers.parseUnits(amountA, 18);
    const amountBDesired = ethers.parseUnits(amountB, 18);

    await tokenAContract.approve(uniswapRouterAddress, amountADesired);
    await tokenBContract.approve(uniswapRouterAddress, amountBDesired);

    const uniswapRouter = new ethers.Contract(uniswapRouterAddress, uniswapRouterAbi, signer);

    const deadline = Math.floor(Date.now() / 1000) + 60 * 20; // 20 minutes from the current Unix time
    await uniswapRouter.addLiquidity(
      tokenA,
      tokenB,
      amountADesired,
      amountBDesired,
      amountADesired.mul(99).div(100), // slippage tolerance of 1%
      amountBDesired.mul(99).div(100), // slippage tolerance of 1%
      account,
      deadline
    );
  };

  return (
    <div>
      <h1>Add Liquidity</h1>
      <div>
        <input
          type="text"
          placeholder="Token A Address"
          value={tokenA}
          onChange={(e) => setTokenA(e.target.value)}
        />
        <input
          type="text"
          placeholder="Token B Address"
          value={tokenB}
          onChange={(e) => setTokenB(e.target.value)}
        />
        <input
          type="text"
          placeholder="Amount A"
          value={amountA}
          onChange={(e) => setAmountA(e.target.value)}
        />
        <input
          type="text"
          placeholder="Amount B"
          value={amountB}
          onChange={(e) => setAmountB(e.target.value)}
        />
        <button onClick={handleAddLiquidity}>Add Liquidity</button>
      </div>
    </div>
  );
};

export default AddLiquidity;
*/