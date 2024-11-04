using Agap2It.Labs.Launchpad.CodeGenerator.Specifications;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agap2It.Labs.Launchpad.CodeGenerator.Validators
{
    public class ReflectionValidator : Erc20Validator
    {
        public ReflectionValidator() { }
        public void Validator(FeesErc20Specification _model)
        {
            base.Validator(_model);
            if (_model.FeesRate <= 0 || _model.FeesRate > 99) { throw new ValidationException("Invalid Value: FeesRate"); }
        }
    }
}
