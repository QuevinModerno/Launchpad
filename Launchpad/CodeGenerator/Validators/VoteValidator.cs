

using Agap2It.Labs.Launchpad.CodeGenerator.Specifications;
using System.ComponentModel.DataAnnotations;

namespace Agap2It.Labs.Launchpad.CodeGenerator.Validators
{
    public class VoteValidator : Erc20Validator
    {
        private int MaxDescriptionLength;
        public VoteValidator() { this.MaxDescriptionLength = 100; }
        public void Validator(VoteErc20Specification _model)
        {
            base.Validator(_model);
            
            if (_model.Description == null || _model.Description.Length > MaxDescriptionLength)
            {
                throw new ArgumentException("Description Size");
            }
        }
    }
}
