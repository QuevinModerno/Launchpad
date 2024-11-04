using Agap2It.Labs.Launchpad.CodeGenerator.Specifications;
using System.ComponentModel.DataAnnotations;


namespace Agap2It.Labs.Launchpad.CodeGenerator.Validators
{
    public class Erc20Validator
    {
        public Erc20Validator() 
        {
        }

         public void Validator(Erc20Specification _model)
        {
            if (_model == null || _model.Name == null || _model.Symbol == null || _model.InitialSupply <= 0) { throw new ValidationException("Unfullfilled"); } //Checks if null
            if (_model.Name.Length > 32 || _model.Name.Length < 3) { throw new ValidationException("Name"); }// Name: Length or Size Verification

            // Check if all characters in the symbol are alphabetical
            if(! _model.Symbol.All(char.IsLetter)  || _model.Symbol.Length < 1 || _model.Symbol.Length > 5)
            {
                throw new ValidationException("Symbol");
            }
        }
    }
}
