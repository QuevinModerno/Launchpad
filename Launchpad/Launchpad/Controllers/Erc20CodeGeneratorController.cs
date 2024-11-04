using Agap2It.Labs.Launchpad.Business.Base;
using Agap2It.Labs.Launchpad.CodeGenerator;
using Agap2It.Labs.Launchpad.CodeGenerator.Enums;
using Agap2It.Labs.Launchpad.CodeGenerator.Models;
using Agap2It.Launchpad.Business.BusinessObjects;
using Microsoft.AspNetCore.Mvc;

namespace Agap2It.Labs.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class Erc20CodeGeneratorController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IErc20CodeGenerationBusinessObject _businessObject;
    public Erc20CodeGeneratorController(IErc20CodeGenerationBusinessObject bo, ILogger<Erc20CodeGeneratorController> logger)
    {
        _businessObject = bo;
        _logger = logger;
    }

    [HttpPost("insertAllErc20")]
    public async Task<ActionResult<OperationResult<string>>> InsertErc20Async([FromBody] Erc20Model erc20, ContractVariations variant)
    {
        try
        {
            var result = await _businessObject.GenerateErc20(erc20, variant);

            //mudar para result.Sucess!!
            if (result.Exception == null)
            {
                _logger.LogInformation("Aqui vai o resultado");
                _logger.LogInformation(result.Result); // Log the transformation string
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while processing the request");
            return StatusCode(500, new OperationResult<string>(ex.Message));
        }
    }
    /*

    [HttpPost("insertErc20")]
    public async Task<ActionResult<OperationResult<string>>> InsertErc20StandardAsync([FromBody] StandardModel erc20)
    {
        
        try
        {
            var result = await _businessObject.GenerateErc20(erc20);

            //mudar para result.Sucess!!
            if (result.Exception == null)
            {
                _logger.LogInformation("Aqui vai o resultado");
                _logger.LogInformation(result.Result); // Log the transformation string
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while processing the request");
            return StatusCode(500, new OperationResult<string>(ex.Message));
        }
    }



    [HttpPost("insertFees")]
    public async Task<ActionResult<OperationResult<string>>> InsertFeesErc20Async([FromBody] FeesModel erc20)
    {
        
        try
        {
            var result = await _businessObject.GenerateErc20(erc20);

            //mudar para result.Sucess!!
            if (result.Exception == null)
            {
                _logger.LogInformation("Aqui vai o resultado");
                _logger.LogInformation(result.Result); // Log the transformation string
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while processing the request");
            return StatusCode(500, new OperationResult<string>(ex.Message));
        }
    }

    [HttpPost("insertBurn")]
    public async Task<ActionResult<OperationResult<string>>> InsertBurnErc20Async([FromBody] BurnModel erc20)
    {
        try
        {
            var result = await _businessObject.GenerateErc20(erc20);

            //mudar para result.Sucess!!
            if (result.Exception == null)
            {
                _logger.LogInformation("Aqui vai o resultado");
                _logger.LogInformation(result.Result); // Log the transformation string
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while processing the request");
            return StatusCode(500, new OperationResult<string>(ex.Message));
        }
    }
    */

}

