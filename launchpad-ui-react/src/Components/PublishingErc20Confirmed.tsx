import React, { useState } from 'react';
import axios from 'axios';
import ConnectWallet from './ConnectWallet';
import './PublishContract.css';


interface PublishProps {
  contract: string;
}
interface EthContractCompileOutput {
  bytecode: string | null;
  abi: string | null;
}
const PublishingErc20Confirmed = ({ contract }:PublishProps) => {
  const [showConnectWallet, setShowConnectWallet] = useState(false);
  const [abi, setAbi] = useState<string>('');
  const [bytecode, setBytecode] = useState<string>('');

  const url = "https://localhost:7128/api/Erc20CodePublisher/publishErc20";

  const abii: string = `[
    {
      "inputs": [],
      "stateMutability": "nonpayable",
      "type": "constructor"
    },
    {
      "inputs": [
        {
          "internalType": "address",
          "name": "spender",
          "type": "address"
        },
        {
          "internalType": "uint256",
          "name": "allowance",
          "type": "uint256"
        },
        {
          "internalType": "uint256",
          "name": "needed",
          "type": "uint256"
        }
      ],
      "name": "ERC20InsufficientAllowance",
      "type": "error"
    },
    {
      "inputs": [
        {
          "internalType": "address",
          "name": "sender",
          "type": "address"
        },
        {
          "internalType": "uint256",
          "name": "balance",
          "type": "uint256"
        },
        {
          "internalType": "uint256",
          "name": "needed",
          "type": "uint256"
        }
      ],
      "name": "ERC20InsufficientBalance",
      "type": "error"
    },
    {
      "inputs": [
        {
          "internalType": "address",
          "name": "approver",
          "type": "address"
        }
      ],
      "name": "ERC20InvalidApprover",
      "type": "error"
    },
    {
      "inputs": [
        {
          "internalType": "address",
          "name": "receiver",
          "type": "address"
        }
      ],
      "name": "ERC20InvalidReceiver",
      "type": "error"
    },
    {
      "inputs": [
        {
          "internalType": "address",
          "name": "sender",
          "type": "address"
        }
      ],
      "name": "ERC20InvalidSender",
      "type": "error"
    },
    {
      "inputs": [
        {
          "internalType": "address",
          "name": "spender",
          "type": "address"
        }
      ],
      "name": "ERC20InvalidSpender",
      "type": "error"
    },
    {
      "anonymous": false,
      "inputs": [
        {
          "indexed": true,
          "internalType": "address",
          "name": "owner",
          "type": "address"
        },
        {
          "indexed": true,
          "internalType": "address",
          "name": "spender",
          "type": "address"
        },
        {
          "indexed": false,
          "internalType": "uint256",
          "name": "value",
          "type": "uint256"
        }
      ],
      "name": "Approval",
      "type": "event"
    },
    {
      "anonymous": false,
      "inputs": [
        {
          "indexed": true,
          "internalType": "address",
          "name": "from",
          "type": "address"
        },
        {
          "indexed": true,
          "internalType": "address",
          "name": "to",
          "type": "address"
        },
        {
          "indexed": false,
          "internalType": "uint256",
          "name": "value",
          "type": "uint256"
        }
      ],
      "name": "Transfer",
      "type": "event"
    },
    {
      "inputs": [
        {
          "internalType": "address",
          "name": "owner",
          "type": "address"
        },
        {
          "internalType": "address",
          "name": "spender",
          "type": "address"
        }
      ],
      "name": "allowance",
      "outputs": [
        {
          "internalType": "uint256",
          "name": "",
          "type": "uint256"
        }
      ],
      "stateMutability": "view",
      "type": "function"
    },
    {
      "inputs": [
        {
          "internalType": "address",
          "name": "spender",
          "type": "address"
        },
        {
          "internalType": "uint256",
          "name": "value",
          "type": "uint256"
        }
      ],
      "name": "approve",
      "outputs": [
        {
          "internalType": "bool",
          "name": "",
          "type": "bool"
        }
      ],
      "stateMutability": "nonpayable",
      "type": "function"
    },
    {
      "inputs": [
        {
          "internalType": "address",
          "name": "account",
          "type": "address"
        }
      ],
      "name": "balanceOf",
      "outputs": [
        {
          "internalType": "uint256",
          "name": "",
          "type": "uint256"
        }
      ],
      "stateMutability": "view",
      "type": "function"
    },
    {
      "inputs": [],
      "name": "decimals",
      "outputs": [
        {
          "internalType": "uint8",
          "name": "",
          "type": "uint8"
        }
      ],
      "stateMutability": "view",
      "type": "function"
    },
    {
      "inputs": [],
      "name": "name",
      "outputs": [
        {
          "internalType": "string",
          "name": "",
          "type": "string"
        }
      ],
      "stateMutability": "view",
      "type": "function"
    },
    {
      "inputs": [],
      "name": "symbol",
      "outputs": [
        {
          "internalType": "string",
          "name": "",
          "type": "string"
        }
      ],
      "stateMutability": "view",
      "type": "function"
    },
    {
      "inputs": [],
      "name": "totalSupply",
      "outputs": [
        {
          "internalType": "uint256",
          "name": "",
          "type": "uint256"
        }
      ],
      "stateMutability": "view",
      "type": "function"
    },
    {
      "inputs": [
        {
          "internalType": "address",
          "name": "to",
          "type": "address"
        },
        {
          "internalType": "uint256",
          "name": "value",
          "type": "uint256"
        }
      ],
      "name": "transfer",
      "outputs": [
        {
          "internalType": "bool",
          "name": "",
          "type": "bool"
        }
      ],
      "stateMutability": "nonpayable",
      "type": "function"
    },
    {
      "inputs": [
        {
          "internalType": "address",
          "name": "from",
          "type": "address"
        },
        {
          "internalType": "address",
          "name": "to",
          "type": "address"
        },
        {
          "internalType": "uint256",
          "name": "value",
          "type": "uint256"
        }
      ],
      "name": "transferFrom",
      "outputs": [
        {
          "internalType": "bool",
          "name": "",
          "type": "bool"
        }
      ],
      "stateMutability": "nonpayable",
      "type": "function"
    }
  ]`;
  const bytecodee: string = "608060405234801562000010575f80fd5b506040518060400160405280600681526020017f63727970746f00000000000000000000000000000000000000000000000000008152506040518060400160405280600381526020017f637279000000000000000000000000000000000000000000000000000000000081525081600390816200008e9190620005d6565b508060049081620000a09190620005d6565b505050620000be33686c6b935b8bbd400000620000c460201b60201c565b620007e6565b5f73ffffffffffffffffffffffffffffffffffffffff168273ffffffffffffffffffffffffffffffffffffffff160362000137575f6040517fec442f050000000000000000000000000000000000000000000000000000000081526004016200012e9190620006fd565b60405180910390fd5b6200014a5f83836200014e60201b60201c565b5050565b5f73ffffffffffffffffffffffffffffffffffffffff168373ffffffffffffffffffffffffffffffffffffffff1603620001a2578060025f82825462000195919062000745565b9250508190555062000273565b5f805f8573ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020019081526020015f20549050818110156200022e578381836040517fe450d38c000000000000000000000000000000000000000000000000000000008152600401620002259392919062000790565b60405180910390fd5b8181035f808673ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020019081526020015f2081905550505b5f73ffffffffffffffffffffffffffffffffffffffff168273ffffffffffffffffffffffffffffffffffffffff1603620002bc578060025f828254039250508190555062000306565b805f808473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020019081526020015f205f82825401925050819055505b8173ffffffffffffffffffffffffffffffffffffffff168373ffffffffffffffffffffffffffffffffffffffff167fddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef83604051620003659190620007cb565b60405180910390a3505050565b5f81519050919050565b7f4e487b71000000000000000000000000000000000000000000000000000000005f52604160045260245ffd5b7f4e487b71000000000000000000000000000000000000000000000000000000005f52602260045260245ffd5b5f6002820490506001821680620003ee57607f821691505b602082108103620004045762000403620003a9565b5b50919050565b5f819050815f5260205f209050919050565b5f6020601f8301049050919050565b5f82821b905092915050565b5f60088302620004687fffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff826200042b565b6200047486836200042b565b95508019841693508086168417925050509392505050565b5f819050919050565b5f819050919050565b5f620004be620004b8620004b2846200048c565b62000495565b6200048c565b9050919050565b5f819050919050565b620004d9836200049e565b620004f1620004e882620004c5565b84845462000437565b825550505050565b5f90565b62000507620004f9565b62000514818484620004ce565b505050565b5b818110156200053b576200052f5f82620004fd565b6001810190506200051a565b5050565b601f8211156200058a5762000554816200040a565b6200055f846200041c565b810160208510156200056f578190505b620005876200057e856200041c565b83018262000519565b50505b505050565b5f82821c905092915050565b5f620005ac5f19846008026200058f565b1980831691505092915050565b5f620005c683836200059b565b9150826002028217905092915050565b620005e18262000372565b67ffffffffffffffff811115620005fd57620005fc6200037c565b5b620006098254620003d6565b620006168282856200053f565b5f60209050601f8311600181146200064c575f841562000637578287015190505b620006438582620005b9565b865550620006b2565b601f1984166200065c866200040a565b5f5b8281101562000685578489015182556001820191506020850194506020810190506200065e565b86831015620006a55784890151620006a1601f8916826200059b565b8355505b6001600288020188555050505b505050505050565b5f73ffffffffffffffffffffffffffffffffffffffff82169050919050565b5f620006e582620006ba565b9050919050565b620006f781620006d9565b82525050565b5f602082019050620007125f830184620006ec565b92915050565b7f4e487b71000000000000000000000000000000000000000000000000000000005f52601160045260245ffd5b5f62000751826200048c565b91506200075e836200048c565b925082820190508082111562000779576200077862000718565b5b92915050565b6200078a816200048c565b82525050565b5f606082019050620007a55f830186620006ec565b620007b460208301856200077f565b620007c360408301846200077f565b949350505050565b5f602082019050620007e05f8301846200077f565b92915050565b610dfb80620007f45f395ff3fe608060405234801561000f575f80fd5b5060043610610091575f3560e01c8063313ce56711610064578063313ce5671461013157806370a082311461014f57806395d89b411461017f578063a9059cbb1461019d578063dd62ed3e146101cd57610091565b806306fdde0314610095578063095ea7b3146100b357806318160ddd146100e357806323b872dd14610101575b5f80fd5b61009d6101fd565b6040516100aa9190610a74565b60405180910390f35b6100cd60048036038101906100c89190610b25565b61028d565b6040516100da9190610b7d565b60405180910390f35b6100eb6102af565b6040516100f89190610ba5565b60405180910390f35b61011b60048036038101906101169190610bbe565b6102b8565b6040516101289190610b7d565b60405180910390f35b6101396102e6565b6040516101469190610c29565b60405180910390f35b61016960048036038101906101649190610c42565b6102ee565b6040516101769190610ba5565b60405180910390f35b610187610333565b6040516101949190610a74565b60405180910390f35b6101b760048036038101906101b29190610b25565b6103c3565b6040516101c49190610b7d565b60405180910390f35b6101e760048036038101906101e29190610c6d565b6103e5565b6040516101f49190610ba5565b60405180910390f35b60606003805461020c90610cd8565b80601f016020809104026020016040519081016040528092919081815260200182805461023890610cd8565b80156102835780601f1061025a57610100808354040283529160200191610283565b820191905f5260205f20905b81548152906001019060200180831161026657829003601f168201915b5050505050905090565b5f80610297610467565b90506102a481858561046e565b600191505092915050565b5f600254905090565b5f806102c2610467565b90506102cf858285610480565b6102da858585610512565b60019150509392505050565b5f6012905090565b5f805f8373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020019081526020015f20549050919050565b60606004805461034290610cd8565b80601f016020809104026020016040519081016040528092919081815260200182805461036e90610cd8565b80156103b95780601f10610390576101008083540402835291602001916103b9565b820191905f5260205f20905b81548152906001019060200180831161039c57829003601f168201915b5050505050905090565b5f806103cd610467565b90506103da818585610512565b600191505092915050565b5f60015f8473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020019081526020015f205f8373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020019081526020015f2054905092915050565b5f33905090565b61047b8383836001610602565b505050565b5f61048b84846103e5565b90507fffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff811461050c57818110156104fd578281836040517ffb8f41b20000000000000000000000000000000000000000000000000000000081526004016104f493929190610d17565b60405180910390fd5b61050b84848484035f610602565b5b50505050565b5f73ffffffffffffffffffffffffffffffffffffffff168373ffffffffffffffffffffffffffffffffffffffff1603610582575f6040517f96c6fd1e0000000000000000000000000000000000000000000000000000000081526004016105799190610d4c565b60405180910390fd5b5f73ffffffffffffffffffffffffffffffffffffffff168273ffffffffffffffffffffffffffffffffffffffff16036105f2575f6040517fec442f050000000000000000000000000000000000000000000000000000000081526004016105e99190610d4c565b60405180910390fd5b6105fd8383836107d1565b505050565b5f73ffffffffffffffffffffffffffffffffffffffff168473ffffffffffffffffffffffffffffffffffffffff1603610672575f6040517fe602df050000000000000000000000000000000000000000000000000000000081526004016106699190610d4c565b60405180910390fd5b5f73ffffffffffffffffffffffffffffffffffffffff168373ffffffffffffffffffffffffffffffffffffffff16036106e2575f6040517f94280d620000000000000000000000000000000000000000000000000000000081526004016106d99190610d4c565b60405180910390fd5b8160015f8673ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020019081526020015f205f8573ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020019081526020015f208190555080156107cb578273ffffffffffffffffffffffffffffffffffffffff168473ffffffffffffffffffffffffffffffffffffffff167f8c5be1e5ebec7d5bd14f71427d1e84f3dd0314c0f7b2291e5b200ac8c7c3b925846040516107c29190610ba5565b60405180910390a35b50505050565b5f73ffffffffffffffffffffffffffffffffffffffff168373ffffffffffffffffffffffffffffffffffffffff1603610821578060025f8282546108159190610d92565b925050819055506108ef565b5f805f8573ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020019081526020015f20549050818110156108aa578381836040517fe450d38c0000000000000000000000000000000000000000000000000000000081526004016108a193929190610d17565b60405180910390fd5b8181035f808673ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020019081526020015f2081905550505b5f73ffffffffffffffffffffffffffffffffffffffff168273ffffffffffffffffffffffffffffffffffffffff1603610936578060025f8282540392505081905550610980565b805f808473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020019081526020015f205f82825401925050819055505b8173ffffffffffffffffffffffffffffffffffffffff168373ffffffffffffffffffffffffffffffffffffffff167fddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef836040516109dd9190610ba5565b60405180910390a3505050565b5f81519050919050565b5f82825260208201905092915050565b5f5b83811015610a21578082015181840152602081019050610a06565b5f8484015250505050565b5f601f19601f8301169050919050565b5f610a46826109ea565b610a5081856109f4565b9350610a60818560208601610a04565b610a6981610a2c565b840191505092915050565b5f6020820190508181035f830152610a8c8184610a3c565b905092915050565b5f80fd5b5f73ffffffffffffffffffffffffffffffffffffffff82169050919050565b5f610ac182610a98565b9050919050565b610ad181610ab7565b8114610adb575f80fd5b50565b5f81359050610aec81610ac8565b92915050565b5f819050919050565b610b0481610af2565b8114610b0e575f80fd5b50565b5f81359050610b1f81610afb565b92915050565b5f8060408385031215610b3b57610b3a610a94565b5b5f610b4885828601610ade565b9250506020610b5985828601610b11565b9150509250929050565b5f8115159050919050565b610b7781610b63565b82525050565b5f602082019050610b905f830184610b6e565b92915050565b610b9f81610af2565b82525050565b5f602082019050610bb85f830184610b96565b92915050565b5f805f60608486031215610bd557610bd4610a94565b5b5f610be286828701610ade565b9350506020610bf386828701610ade565b9250506040610c0486828701610b11565b9150509250925092565b5f60ff82169050919050565b610c2381610c0e565b82525050565b5f602082019050610c3c5f830184610c1a565b92915050565b5f60208284031215610c5757610c56610a94565b5b5f610c6484828501610ade565b91505092915050565b5f8060408385031215610c8357610c82610a94565b5b5f610c9085828601610ade565b9250506020610ca185828601610ade565b9150509250929050565b7f4e487b71000000000000000000000000000000000000000000000000000000005f52602260045260245ffd5b5f6002820490506001821680610cef57607f821691505b602082108103610d0257610d01610cab565b5b50919050565b610d1181610ab7565b82525050565b5f606082019050610d2a5f830186610d08565b610d376020830185610b96565b610d446040830184610b96565b949350505050565b5f602082019050610d5f5f830184610d08565b92915050565b7f4e487b71000000000000000000000000000000000000000000000000000000005f52601160045260245ffd5b5f610d9c82610af2565b9150610da783610af2565b9250828201905080821115610dbf57610dbe610d65565b5b9291505056fea2646970667358221220721fc3f9f760e35b1010b1b5a5ac6e8cc23235ceb3e450328cb057f3612504c864736f6c63430008180033";

  const handlePublishContract = async () => {
    try {
      // Realizar a chamada para a API usando Axios
      var x = contract;
      const response = await axios.post(url,{ contract }
      );
      console.log("Success:", response);

      // Lidar com a resposta da API
      //setBytecode( response.data.result.byteCode);
      //setAbi(response.data.result.abi);
      setBytecode(bytecodee);
      setAbi(abii);

      // Aqui você pode fazer o que for necessário com o bytecode e o ABI
      // Por exemplo, armazená-los no estado do componente ou passá-los para outros componentes
  
    } catch (error) {
      // Lidar com erros de requisição
      //setBytecode( abii);
      //setAbi(bytecodee);
      console.error('Erro ao publicar contrato:', error);
      // Aqui você pode exibir uma mensagem de erro ou fazer outra tratativa necessária
    }
  }

  const copyToClipboard = (text: string) => {
    navigator.clipboard.writeText(text).then(
      () => alert('Copied to clipboard!'),
      (err) => alert('Failed to copy!')
    );
  };


  return (
    <div className="container">
      <div className="content">
        <div className="left-pane">
          <h3>Contract</h3>
          <div className="textarea-container">
            <textarea 
              className="textarea-contract" 
              readOnly
              value={contract}
            />
            <button className="copy-button" onClick={() => copyToClipboard(contract)}>📋</button>
          </div>
        </div>

        <div className="right-pane">
          <div className="right-section">
            <h3>Bytecode</h3>
            <div className="textarea-container">
              <textarea className="textarea" value={bytecode} readOnly />
              <button className="copy-button" onClick={() => copyToClipboard(bytecode)}>📋</button>
            </div>
          </div>

          <div className="right-section">
            <h3>ABI</h3>
            <div className="textarea-container">
              <textarea className="textarea" value={abi} readOnly />
              <button className="copy-button" onClick={() => copyToClipboard(abi)}>📋</button>
            </div>
          </div>
        </div>
      </div>
      <div className="bottom-section">  
      {abi && bytecode ?(
        <ConnectWallet abi={abi} bytecode={bytecode} />
      ):(
          <button className="publish-button" onClick={handlePublishContract}>Publish Contract</button>        
      )}
      </div>
    </div>
  );
};


export default PublishingErc20Confirmed;



/*
import React, { useState } from 'react'
import { useLocation } from 'react-router-dom';
import ConnectWallet from './ConnectWallet';
import './PublishContract.css'


export const PublishingErc20Confirmed = () => {

  //proprierties
  const [showConnectWallet, setShowConnectWallet] = useState(false); // State to control the visibility of ConnectWallet
  const location = useLocation();
  console.log(location);
  const data = location.state.data;

  const abi: string = `[
    {
      "inputs": [],
      "stateMutability": "nonpayable",
      "type": "constructor"
    },
    {
      "inputs": [
        {
          "internalType": "address",
          "name": "spender",
          "type": "address"
        },
        {
          "internalType": "uint256",
          "name": "allowance",
          "type": "uint256"
        },
        {
          "internalType": "uint256",
          "name": "needed",
          "type": "uint256"
        }
      ],
      "name": "ERC20InsufficientAllowance",
      "type": "error"
    },
    {
      "inputs": [
        {
          "internalType": "address",
          "name": "sender",
          "type": "address"
        },
        {
          "internalType": "uint256",
          "name": "balance",
          "type": "uint256"
        },
        {
          "internalType": "uint256",
          "name": "needed",
          "type": "uint256"
        }
      ],
      "name": "ERC20InsufficientBalance",
      "type": "error"
    },
    {
      "inputs": [
        {
          "internalType": "address",
          "name": "approver",
          "type": "address"
        }
      ],
      "name": "ERC20InvalidApprover",
      "type": "error"
    },
    {
      "inputs": [
        {
          "internalType": "address",
          "name": "receiver",
          "type": "address"
        }
      ],
      "name": "ERC20InvalidReceiver",
      "type": "error"
    },
    {
      "inputs": [
        {
          "internalType": "address",
          "name": "sender",
          "type": "address"
        }
      ],
      "name": "ERC20InvalidSender",
      "type": "error"
    },
    {
      "inputs": [
        {
          "internalType": "address",
          "name": "spender",
          "type": "address"
        }
      ],
      "name": "ERC20InvalidSpender",
      "type": "error"
    },
    {
      "anonymous": false,
      "inputs": [
        {
          "indexed": true,
          "internalType": "address",
          "name": "owner",
          "type": "address"
        },
        {
          "indexed": true,
          "internalType": "address",
          "name": "spender",
          "type": "address"
        },
        {
          "indexed": false,
          "internalType": "uint256",
          "name": "value",
          "type": "uint256"
        }
      ],
      "name": "Approval",
      "type": "event"
    },
    {
      "anonymous": false,
      "inputs": [
        {
          "indexed": true,
          "internalType": "address",
          "name": "from",
          "type": "address"
        },
        {
          "indexed": true,
          "internalType": "address",
          "name": "to",
          "type": "address"
        },
        {
          "indexed": false,
          "internalType": "uint256",
          "name": "value",
          "type": "uint256"
        }
      ],
      "name": "Transfer",
      "type": "event"
    },
    {
      "inputs": [
        {
          "internalType": "address",
          "name": "owner",
          "type": "address"
        },
        {
          "internalType": "address",
          "name": "spender",
          "type": "address"
        }
      ],
      "name": "allowance",
      "outputs": [
        {
          "internalType": "uint256",
          "name": "",
          "type": "uint256"
        }
      ],
      "stateMutability": "view",
      "type": "function"
    },
    {
      "inputs": [
        {
          "internalType": "address",
          "name": "spender",
          "type": "address"
        },
        {
          "internalType": "uint256",
          "name": "value",
          "type": "uint256"
        }
      ],
      "name": "approve",
      "outputs": [
        {
          "internalType": "bool",
          "name": "",
          "type": "bool"
        }
      ],
      "stateMutability": "nonpayable",
      "type": "function"
    },
    {
      "inputs": [
        {
          "internalType": "address",
          "name": "account",
          "type": "address"
        }
      ],
      "name": "balanceOf",
      "outputs": [
        {
          "internalType": "uint256",
          "name": "",
          "type": "uint256"
        }
      ],
      "stateMutability": "view",
      "type": "function"
    },
    {
      "inputs": [],
      "name": "decimals",
      "outputs": [
        {
          "internalType": "uint8",
          "name": "",
          "type": "uint8"
        }
      ],
      "stateMutability": "view",
      "type": "function"
    },
    {
      "inputs": [],
      "name": "name",
      "outputs": [
        {
          "internalType": "string",
          "name": "",
          "type": "string"
        }
      ],
      "stateMutability": "view",
      "type": "function"
    },
    {
      "inputs": [],
      "name": "symbol",
      "outputs": [
        {
          "internalType": "string",
          "name": "",
          "type": "string"
        }
      ],
      "stateMutability": "view",
      "type": "function"
    },
    {
      "inputs": [],
      "name": "totalSupply",
      "outputs": [
        {
          "internalType": "uint256",
          "name": "",
          "type": "uint256"
        }
      ],
      "stateMutability": "view",
      "type": "function"
    },
    {
      "inputs": [
        {
          "internalType": "address",
          "name": "to",
          "type": "address"
        },
        {
          "internalType": "uint256",
          "name": "value",
          "type": "uint256"
        }
      ],
      "name": "transfer",
      "outputs": [
        {
          "internalType": "bool",
          "name": "",
          "type": "bool"
        }
      ],
      "stateMutability": "nonpayable",
      "type": "function"
    },
    {
      "inputs": [
        {
          "internalType": "address",
          "name": "from",
          "type": "address"
        },
        {
          "internalType": "address",
          "name": "to",
          "type": "address"
        },
        {
          "internalType": "uint256",
          "name": "value",
          "type": "uint256"
        }
      ],
      "name": "transferFrom",
      "outputs": [
        {
          "internalType": "bool",
          "name": "",
          "type": "bool"
        }
      ],
      "stateMutability": "nonpayable",
      "type": "function"
    }
  ]`;
  const bytecode: string = "608060405234801562000010575f80fd5b506040518060400160405280600681526020017f63727970746f00000000000000000000000000000000000000000000000000008152506040518060400160405280600381526020017f637279000000000000000000000000000000000000000000000000000000000081525081600390816200008e9190620005d6565b508060049081620000a09190620005d6565b505050620000be33686c6b935b8bbd400000620000c460201b60201c565b620007e6565b5f73ffffffffffffffffffffffffffffffffffffffff168273ffffffffffffffffffffffffffffffffffffffff160362000137575f6040517fec442f050000000000000000000000000000000000000000000000000000000081526004016200012e9190620006fd565b60405180910390fd5b6200014a5f83836200014e60201b60201c565b5050565b5f73ffffffffffffffffffffffffffffffffffffffff168373ffffffffffffffffffffffffffffffffffffffff1603620001a2578060025f82825462000195919062000745565b9250508190555062000273565b5f805f8573ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020019081526020015f20549050818110156200022e578381836040517fe450d38c000000000000000000000000000000000000000000000000000000008152600401620002259392919062000790565b60405180910390fd5b8181035f808673ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020019081526020015f2081905550505b5f73ffffffffffffffffffffffffffffffffffffffff168273ffffffffffffffffffffffffffffffffffffffff1603620002bc578060025f828254039250508190555062000306565b805f808473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020019081526020015f205f82825401925050819055505b8173ffffffffffffffffffffffffffffffffffffffff168373ffffffffffffffffffffffffffffffffffffffff167fddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef83604051620003659190620007cb565b60405180910390a3505050565b5f81519050919050565b7f4e487b71000000000000000000000000000000000000000000000000000000005f52604160045260245ffd5b7f4e487b71000000000000000000000000000000000000000000000000000000005f52602260045260245ffd5b5f6002820490506001821680620003ee57607f821691505b602082108103620004045762000403620003a9565b5b50919050565b5f819050815f5260205f209050919050565b5f6020601f8301049050919050565b5f82821b905092915050565b5f60088302620004687fffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff826200042b565b6200047486836200042b565b95508019841693508086168417925050509392505050565b5f819050919050565b5f819050919050565b5f620004be620004b8620004b2846200048c565b62000495565b6200048c565b9050919050565b5f819050919050565b620004d9836200049e565b620004f1620004e882620004c5565b84845462000437565b825550505050565b5f90565b62000507620004f9565b62000514818484620004ce565b505050565b5b818110156200053b576200052f5f82620004fd565b6001810190506200051a565b5050565b601f8211156200058a5762000554816200040a565b6200055f846200041c565b810160208510156200056f578190505b620005876200057e856200041c565b83018262000519565b50505b505050565b5f82821c905092915050565b5f620005ac5f19846008026200058f565b1980831691505092915050565b5f620005c683836200059b565b9150826002028217905092915050565b620005e18262000372565b67ffffffffffffffff811115620005fd57620005fc6200037c565b5b620006098254620003d6565b620006168282856200053f565b5f60209050601f8311600181146200064c575f841562000637578287015190505b620006438582620005b9565b865550620006b2565b601f1984166200065c866200040a565b5f5b8281101562000685578489015182556001820191506020850194506020810190506200065e565b86831015620006a55784890151620006a1601f8916826200059b565b8355505b6001600288020188555050505b505050505050565b5f73ffffffffffffffffffffffffffffffffffffffff82169050919050565b5f620006e582620006ba565b9050919050565b620006f781620006d9565b82525050565b5f602082019050620007125f830184620006ec565b92915050565b7f4e487b71000000000000000000000000000000000000000000000000000000005f52601160045260245ffd5b5f62000751826200048c565b91506200075e836200048c565b925082820190508082111562000779576200077862000718565b5b92915050565b6200078a816200048c565b82525050565b5f606082019050620007a55f830186620006ec565b620007b460208301856200077f565b620007c360408301846200077f565b949350505050565b5f602082019050620007e05f8301846200077f565b92915050565b610dfb80620007f45f395ff3fe608060405234801561000f575f80fd5b5060043610610091575f3560e01c8063313ce56711610064578063313ce5671461013157806370a082311461014f57806395d89b411461017f578063a9059cbb1461019d578063dd62ed3e146101cd57610091565b806306fdde0314610095578063095ea7b3146100b357806318160ddd146100e357806323b872dd14610101575b5f80fd5b61009d6101fd565b6040516100aa9190610a74565b60405180910390f35b6100cd60048036038101906100c89190610b25565b61028d565b6040516100da9190610b7d565b60405180910390f35b6100eb6102af565b6040516100f89190610ba5565b60405180910390f35b61011b60048036038101906101169190610bbe565b6102b8565b6040516101289190610b7d565b60405180910390f35b6101396102e6565b6040516101469190610c29565b60405180910390f35b61016960048036038101906101649190610c42565b6102ee565b6040516101769190610ba5565b60405180910390f35b610187610333565b6040516101949190610a74565b60405180910390f35b6101b760048036038101906101b29190610b25565b6103c3565b6040516101c49190610b7d565b60405180910390f35b6101e760048036038101906101e29190610c6d565b6103e5565b6040516101f49190610ba5565b60405180910390f35b60606003805461020c90610cd8565b80601f016020809104026020016040519081016040528092919081815260200182805461023890610cd8565b80156102835780601f1061025a57610100808354040283529160200191610283565b820191905f5260205f20905b81548152906001019060200180831161026657829003601f168201915b5050505050905090565b5f80610297610467565b90506102a481858561046e565b600191505092915050565b5f600254905090565b5f806102c2610467565b90506102cf858285610480565b6102da858585610512565b60019150509392505050565b5f6012905090565b5f805f8373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020019081526020015f20549050919050565b60606004805461034290610cd8565b80601f016020809104026020016040519081016040528092919081815260200182805461036e90610cd8565b80156103b95780601f10610390576101008083540402835291602001916103b9565b820191905f5260205f20905b81548152906001019060200180831161039c57829003601f168201915b5050505050905090565b5f806103cd610467565b90506103da818585610512565b600191505092915050565b5f60015f8473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020019081526020015f205f8373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020019081526020015f2054905092915050565b5f33905090565b61047b8383836001610602565b505050565b5f61048b84846103e5565b90507fffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff811461050c57818110156104fd578281836040517ffb8f41b20000000000000000000000000000000000000000000000000000000081526004016104f493929190610d17565b60405180910390fd5b61050b84848484035f610602565b5b50505050565b5f73ffffffffffffffffffffffffffffffffffffffff168373ffffffffffffffffffffffffffffffffffffffff1603610582575f6040517f96c6fd1e0000000000000000000000000000000000000000000000000000000081526004016105799190610d4c565b60405180910390fd5b5f73ffffffffffffffffffffffffffffffffffffffff168273ffffffffffffffffffffffffffffffffffffffff16036105f2575f6040517fec442f050000000000000000000000000000000000000000000000000000000081526004016105e99190610d4c565b60405180910390fd5b6105fd8383836107d1565b505050565b5f73ffffffffffffffffffffffffffffffffffffffff168473ffffffffffffffffffffffffffffffffffffffff1603610672575f6040517fe602df050000000000000000000000000000000000000000000000000000000081526004016106699190610d4c565b60405180910390fd5b5f73ffffffffffffffffffffffffffffffffffffffff168373ffffffffffffffffffffffffffffffffffffffff16036106e2575f6040517f94280d620000000000000000000000000000000000000000000000000000000081526004016106d99190610d4c565b60405180910390fd5b8160015f8673ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020019081526020015f205f8573ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020019081526020015f208190555080156107cb578273ffffffffffffffffffffffffffffffffffffffff168473ffffffffffffffffffffffffffffffffffffffff167f8c5be1e5ebec7d5bd14f71427d1e84f3dd0314c0f7b2291e5b200ac8c7c3b925846040516107c29190610ba5565b60405180910390a35b50505050565b5f73ffffffffffffffffffffffffffffffffffffffff168373ffffffffffffffffffffffffffffffffffffffff1603610821578060025f8282546108159190610d92565b925050819055506108ef565b5f805f8573ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020019081526020015f20549050818110156108aa578381836040517fe450d38c0000000000000000000000000000000000000000000000000000000081526004016108a193929190610d17565b60405180910390fd5b8181035f808673ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020019081526020015f2081905550505b5f73ffffffffffffffffffffffffffffffffffffffff168273ffffffffffffffffffffffffffffffffffffffff1603610936578060025f8282540392505081905550610980565b805f808473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020019081526020015f205f82825401925050819055505b8173ffffffffffffffffffffffffffffffffffffffff168373ffffffffffffffffffffffffffffffffffffffff167fddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef836040516109dd9190610ba5565b60405180910390a3505050565b5f81519050919050565b5f82825260208201905092915050565b5f5b83811015610a21578082015181840152602081019050610a06565b5f8484015250505050565b5f601f19601f8301169050919050565b5f610a46826109ea565b610a5081856109f4565b9350610a60818560208601610a04565b610a6981610a2c565b840191505092915050565b5f6020820190508181035f830152610a8c8184610a3c565b905092915050565b5f80fd5b5f73ffffffffffffffffffffffffffffffffffffffff82169050919050565b5f610ac182610a98565b9050919050565b610ad181610ab7565b8114610adb575f80fd5b50565b5f81359050610aec81610ac8565b92915050565b5f819050919050565b610b0481610af2565b8114610b0e575f80fd5b50565b5f81359050610b1f81610afb565b92915050565b5f8060408385031215610b3b57610b3a610a94565b5b5f610b4885828601610ade565b9250506020610b5985828601610b11565b9150509250929050565b5f8115159050919050565b610b7781610b63565b82525050565b5f602082019050610b905f830184610b6e565b92915050565b610b9f81610af2565b82525050565b5f602082019050610bb85f830184610b96565b92915050565b5f805f60608486031215610bd557610bd4610a94565b5b5f610be286828701610ade565b9350506020610bf386828701610ade565b9250506040610c0486828701610b11565b9150509250925092565b5f60ff82169050919050565b610c2381610c0e565b82525050565b5f602082019050610c3c5f830184610c1a565b92915050565b5f60208284031215610c5757610c56610a94565b5b5f610c6484828501610ade565b91505092915050565b5f8060408385031215610c8357610c82610a94565b5b5f610c9085828601610ade565b9250506020610ca185828601610ade565b9150509250929050565b7f4e487b71000000000000000000000000000000000000000000000000000000005f52602260045260245ffd5b5f6002820490506001821680610cef57607f821691505b602082108103610d0257610d01610cab565b5b50919050565b610d1181610ab7565b82525050565b5f606082019050610d2a5f830186610d08565b610d376020830185610b96565b610d446040830184610b96565b949350505050565b5f602082019050610d5f5f830184610d08565b92915050565b7f4e487b71000000000000000000000000000000000000000000000000000000005f52601160045260245ffd5b5f610d9c82610af2565b9150610da783610af2565b9250828201905080821115610dbf57610dbe610d65565b5b9291505056fea2646970667358221220721fc3f9f760e35b1010b1b5a5ac6e8cc23235ceb3e450328cb057f3612504c864736f6c63430008180033";


  const copyToClipboard = (text: string) => {
    navigator.clipboard.writeText(text).then(() => {
      alert('Copied to clipboard');
    }).catch((err) => {
      console.error('Failed to copy: ', err);
    });
  };
  
  const handlePublishContract = () => {
    setShowConnectWallet(true); // Show ConnectWallet component when the button is clicked
  };
  return (
    <div className="container">
      <div className="content">
        <div className="left-pane">
          <h3>Contract</h3>
          <div className="textarea-container">
            <textarea 
              className="textarea-contract" 
              readOnly
              value={typeof data === 'string' ? data : JSON.stringify(data, null, 2)}
            />
            <button className="copy-button" onClick={() => copyToClipboard(typeof data === 'string' ? data : JSON.stringify(data, null, 2))}>📋</button>
          </div>
        </div>

        <div className="right-pane">
          <div className="right-section">
            <h3>Bytecode</h3>
            <div className="textarea-container">
              <textarea className="textarea" value={bytecode} readOnly />
              <button className="copy-button" onClick={() => copyToClipboard(bytecode)}>📋</button>
            </div>
          </div>

          <div className="right-section">
            <h3>ABI</h3>
            <div className="textarea-container">
              <textarea className="textarea" value={abi} readOnly />
              <button className="copy-button" onClick={() => copyToClipboard(abi)}>📋</button>
            </div>
          </div>
        </div>
      </div>

      <div className="bottom-section">
        {showConnectWallet ? (
          <ConnectWallet abi={abi} bytecode={bytecode} />
        ) : (
          <button className="publish-button" onClick={handlePublishContract}>Publish Contract</button>
        )}
      </div>
    </div>
  );
}
*/