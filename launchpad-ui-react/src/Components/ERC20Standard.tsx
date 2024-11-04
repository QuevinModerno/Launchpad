import React, { useState } from 'react';
import axios from 'axios';
import PublishingErc20Confirmed from './PublishingErc20Confirmed'

const url = "https://localhost:7128/api/Erc20CodeGenerator/insertAllErc20";


interface ERC20StandardProps {}

const ERC20Standard: React.FC<ERC20StandardProps> = () => {

  
  const [tokenName, setTokenName] = useState<string>('');
  const [tokenSymbol, setTokenSymbol] = useState<string>('');
  const [tokenSupply, setTokenSupply] = useState<number | string>('');
  const [address, setAddress] = useState<string>('');
  const [showFeesRate, setShowFeesRate] = useState(false); // State to track if user wants fees 
  const [feesRate, setFeesRate] = useState('');   // State to hold the value of the FeesRate textbox
  const [showBurnRate, setShowBurnRate] = useState(false); // State to track if user wants Burning
  const [showVote, setShowVote] = useState(false); // State to track if user wants Vote
  const [showReflection, setShowReflection] = useState(false); // State to track if user wants Vote
  const [description, setDescription] = useState<string>('');
  const [variant, setVariant] = useState<number>(1);
  const [publishedData, setPublishedData] = useState<string>('');

 

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    

    // Parse the Initial Supply value to an integer--
    const supplyValue = parseInt(tokenSupply as string);
    try{
      var resp = null;
      
  console.log(tokenName, tokenSymbol, tokenSupply, feesRate, variant);

      const feesValue = parseFloat(feesRate as string);
      resp = await axios.post(url, {
        Name: tokenName,
        Symbol: tokenSymbol,
        InitialSupply: supplyValue,
        feesRate: feesValue,
        description: description,
        address: address,
      }, {
        params: {
          variant: variant
        }
      });
      if(resp == null){
        throw Error;
      }
      console.log("Success:", resp.data);
      //const responseString = JSON.stringify(resp.data.result);
      setPublishedData(resp.data.result);   
    
    } catch (error) {
      console.log("Error:", error);
    }
  };

/*
  const handleCheckboxChange = () => {
    setShowFeesRate(!showFeesRate); 
    

    if (!showFeesRate) {
        setFeesRate('');
        setVariant(2);
    }else{
      setVariant(1);
    }
    

  };
  */
  const handleCheckboxChange = (name: string) => {
    if (name === 'Fees') {
      setShowFeesRate(!showFeesRate);
      setVariant(showFeesRate ? 1 : 2); // Set variable to 2 when 'Fees' checkbox is checked, otherwise set it to 0
    } else if (name === 'Burn') {
      setShowBurnRate(!showBurnRate);
      setVariant(showBurnRate ? 1 : 3); // Set variable to 3 when 'Burning' checkbox is checked, otherwise set it to 0
    }
    else if(name == 'Vote'){
      setShowVote(!showVote);
      setVariant(showBurnRate ? 1:4);
    }
    else if(name == 'Reflection'){
      setShowReflection(!showReflection);
      setVariant(showReflection ? 1:4);
    }
  };

  return (
    <div>
      {!publishedData ? (
      <div>
      
        <h1> Its time to create your token!</h1>
        <form onSubmit={handleSubmit} className="form">
          <div className="form-row">
              <label htmlFor='TokenName'>Name</label>
              <input
              type='text'
              id='TokenName'
              name='TokenName'
              value={tokenName}
              onChange={(e) => setTokenName(e.target.value)}
              />
          </div>
          
          <div className="form-row">
            <label htmlFor='TokenSymbol'>Symbol</label>
            <input
              type='text'
              id='TokenSymbol'
              name='TokenSymbol'
              value={tokenSymbol}
              onChange={(e) => setTokenSymbol(e.target.value)}
            />
          </div>
          <div className="form-row">
            <label htmlFor='TokenSupply'>Initial Supply</label>
            <input
              type='number'
              id='TokenSupply'
              name='TokenSupply'
              value={tokenSupply}
              onChange={(e) => setTokenSupply(e.target.value)} 
            />
          </div>


          <div>
              <label>
                <input
                  type="checkbox"
                  checked={showFeesRate}
                  onChange={() =>handleCheckboxChange('Fees')}
                />  
                Fees
              </label>
              <label>
                <input
                  type="checkbox"
                  checked={showBurnRate}
                  onChange={() => handleCheckboxChange('Burn')}
                />  
                Burning
              </label>
              <label>
                <input
                  type="checkbox"
                  checked={showVote}
                  onChange={() => handleCheckboxChange('Vote')}
                />  
                Vote
              </label>
              <label>
                <input
                  type="checkbox"
                  checked={showVote}
                  onChange={() => handleCheckboxChange('Reflection')}
                />  
                Reflection
              </label>
          </div>

          {showVote && !showFeesRate && !showReflection && (
              <div>
                <label>
                  Description:
                  <input
                    type="text"
                    value={description}
                    onChange={(e) => setDescription(e.target.value)}
                  />
                </label>
              </div>
          )}
          {/* FeesRate textbox */}
            {showFeesRate && !showVote && !showReflection &&(
              <div>
                <label>
                  Fees Rate:
                  <input
                    type="text"
                    value={feesRate}
                    onChange={(e) => setFeesRate(e.target.value)}
                  />
                </label>
                <label>
                  Address TaxColector:
                  <input
                    type="text"
                    value={address}
                    onChange={(e) => setAddress(e.target.value)}
                  />
                </label>
              </div>
            )}
              {showReflection &&  !showVote && !showFeesRate &&(
              <div>
                <label>
                  Fees Rate:
                  <input
                    type="text"
                    value={feesRate}
                    onChange={(e) => setFeesRate(e.target.value)}
                  />
                </label>
              </div>
              )}


          <button type='submit'>Submit</button>
        </form>
        
      </div>
    ): (
      <PublishingErc20Confirmed
        contract={publishedData}
      />
    )}
  </div>
    
  );
};

export default ERC20Standard;

