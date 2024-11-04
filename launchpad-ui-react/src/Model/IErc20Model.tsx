import { Component } from "react";


interface ERC20StandardProps {}

interface ERC20StandardState {
  tokenName: string;
  tokenSymbol: string;
  tokenSupply: number | string;
  showFeesRate: boolean;
  feesRate: string;
  showBurnRate: boolean;
}