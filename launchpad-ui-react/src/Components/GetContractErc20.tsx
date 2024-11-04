import React, { useEffect, useState } from 'react';
import { ethers } from 'ethers';

interface ContractProps {
  tokenAAddress: string;
  amountTokenA: string,
}

const contractABI = [
  'function name() view returns (string)',
  'function symbol() view returns (string)',
  'function totalSupply() view returns (uint256)'
];

const Contract: React.FC<ContractProps> = ({ tokenAAddress, amountTokenA }) => {
  const [contractName, setContractName] = useState<string>('');
  const [contractSymbol, setContractSymbol] = useState<string>('');
  const [totalSupply, setTotalSupply] = useState<string>('');
  const [liquidity, setLiquidity] = useState<string>('');


  const provider = new ethers.BrowserProvider((window as any).ethereum);

  useEffect(() => {
    const fetchContract = async () => {
      if (!tokenAAddress || !provider) return;

      try {
        const contract = new ethers.Contract(tokenAAddress, contractABI, provider);
        const [name, symbol, supply] = await Promise.all([
          contract.name(),
          contract.symbol(),
          contract.totalSupply(),
        ]);
        var liq = parseInt(supply.toString()) / 1e18;
        var liquid = parseInt(amountTokenA) / liq;
        liquid = liquid * 100;
        setContractName(name);
        setContractSymbol(symbol);
        setTotalSupply(liq.toString());
        setLiquidity( liquid.toString() );

        console.log('Nome do contrato:', name);
        console.log('Símbolo do contrato:', symbol);
      } catch (error) {
        console.error('Erro ao buscar informações do contrato:', error);
      }
    };

    fetchContract();
  }, [tokenAAddress, amountTokenA]);

  return (
    <div>
      <h2>Detalhes do Contrato</h2>
      <p>Nome: {contractName}</p>
      <p>Símbolo: {contractSymbol}</p>
      <p>Total Supply: {totalSupply}</p>
      <p>Liquidity: {amountTokenA} / {totalSupply}  = {liquidity} %</p>


      {/* Outros detalhes do contrato, se necessário */}
    </div>
  );
};

export default Contract;
