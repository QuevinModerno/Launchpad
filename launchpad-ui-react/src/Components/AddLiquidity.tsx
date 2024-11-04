import React, { useEffect, useState } from 'react';
import { ethers } from 'ethers';
import addLiquidityETH from './AddLiquidityErc20';
import Contract from "./GetContractErc20";

interface LiquidityComponentProps {
  userAddress: string;
  signer: ethers.JsonRpcSigner | null;
}

const LiquidityComponent = ({ signer, userAddress }: LiquidityComponentProps) => {
  const [tokenAAddress, setTokenAAddress] = useState<string>('');
  const [amountTokenA, setAmountTokenA] = useState<string>('');
  const [amountEthRequired, setAmountEthRequired] = useState<string>('');


  const handleAddLiquidity = async () => {
    if (!signer || !tokenAAddress || !amountTokenA || !amountEthRequired) return;

    try {
      await addLiquidityETH(userAddress, signer, tokenAAddress, amountTokenA, amountEthRequired);
    } catch (error) {
      console.error('Error adding liquidity:', error);
      // Tratar o erro conforme necessário
    }
  };

  return (
    <div>
      <h1>Uniswap Liquidity Pool</h1>
      
      <div>
        <p>Connected Account: {userAddress}</p>
      
        <input 
          type="text" 
          placeholder="Token Address" 
          value={tokenAAddress} 
          onChange={(e) => setTokenAAddress(e.target.value)} 
        />
        <input 
          type="text" 
          placeholder="Amount of Token A" 
          value={amountTokenA} 
          onChange={(e) => setAmountTokenA(e.target.value)} 
        />
        <input 
          type="text" 
          placeholder="ETH Address" 
          value={amountEthRequired} 
          onChange={(e) => setAmountEthRequired(e.target.value)} 
        />
        
        {tokenAAddress && amountTokenA &&(
          <div>
            <Contract tokenAAddress={tokenAAddress} amountTokenA = {amountTokenA} />
          </div>
        )}
        <button onClick={handleAddLiquidity}>Add Liquidity</button>
      </div>
    </div>
  );
};

export default LiquidityComponent;



