import React from 'react';
import './App.css';
import ERC20Standard from './Components/ERC20Standard';
import { ChakraProvider } from '@chakra-ui/react'
import { Routes, Route, Link } from 'react-router-dom';

/*<div className="App">
      <h1> Its time to create your token!</h1>

      <div className="form">
        <ERC20Standard/>
      </div>
      </div>
      */

function App() {
  return (
    
    <Routes>
      <Route path='/' element={<ERC20Standard />} /> 
    </Routes>
  );
}

export default App;
