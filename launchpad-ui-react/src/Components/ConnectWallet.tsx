import { ethers } from 'ethers';
import React, { useState } from 'react';
import addLiquidity from './AddLiquidityErc20';
import LiquidityComponent from './AddLiquidity';

interface ConnectWalletProps {
  abi: string;
  bytecode: string;
}
let provider = new ethers.BrowserProvider((window as any).ethereum);

const ConnectWallet = ({ abi, bytecode }: ConnectWalletProps) => {
  const [userAddress, setUserAddress] = useState<string | null>(null);
  const [signer, setSigner] = useState<ethers.JsonRpcSigner | null>(null);

  const connectMetaMask = async (): Promise<void> => {
    if ((window as any).ethereum) {
      try {
        await provider.send('eth_requestAccounts', []);
        const signer = provider.getSigner();
        const userAddress = (await signer).address;
        setUserAddress(userAddress);
        setSigner(await signer)
      } catch (error) {
        console.error('User denied account access', error);
      }
    } else {
      console.error('MetaMask not found');
    }
  };

  const deployContract = async () => {
    if (!signer) {
      console.error('No signer found');
      return;
    }

    const factory = new ethers.ContractFactory(abi, bytecode, signer);

    try {
      const contract = await factory.deploy();
      await contract.getDeployedCode();
      console.log('Contract deployed at address:', contract.getAddress);
    } catch (error) {
      console.error('Error deploying contract', error);
    }
  };
//<button onClick={deployContract}>Deploy Contract</button>
  return (
    <div>
      {userAddress ? (
        <div>
          <button onClick = {deployContract}> Deploy contract</button>
          <div>Connected Account: {userAddress}</div>
          {userAddress &&(
          <LiquidityComponent userAddress = {userAddress} signer = {signer}  /> 
          )}    
        </div>
      ) : (
        <div>

        <button onClick={connectMetaMask}>Connect MetaMask</button>
        </div>
      )}
    </div>
  );
};

export default ConnectWallet;