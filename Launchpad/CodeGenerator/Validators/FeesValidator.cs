
using Agap2It.Labs.Launchpad.CodeGenerator.Specifications;
using System.ComponentModel.DataAnnotations;

namespace Agap2It.Labs.Launchpad.CodeGenerator.Validators
{
    public class FeesValidator : Erc20Validator
    {
        public FeesValidator() { }

        public void Validator(FeesErc20Specification _model)
        {
            base.Validator(_model);
            if(_model.FeesRate <= 0 || _model.FeesRate > 99) { throw new ValidationException("Invalid Value: FeesRate"); }
        }
    }
}
