using Agap2It.Labs.Launchpad.Business.Base;
using Agap2It.Launchpad.Business.BusinessObjects;
using Agap2It.Labs.Launchpad.Launchpad.Controllers;
using EthGenerator.Contracts.Compiler.Model;
using Microsoft.AspNetCore.Mvc;

namespace Agap2It.Labs.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class Erc20CodePublisherController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IErc20CodePublisherBusinessObject _businessObject;


    public Erc20CodePublisherController(IErc20CodePublisherBusinessObject bo, ILogger<Erc20CodePublisherBusinessObject> logger)
    {
        _logger = logger;
        _businessObject = bo;
    }
    [HttpPost("publishErc20")]
    public async Task<ActionResult<OperationResult<EthContractCompileOutput>>> InsertErc20Async([FromBody] Erc20ContractModel contract)
    {
        try
        {
            Console.WriteLine("contract : " );
            Console.WriteLine("contract : " );
            Console.WriteLine("contract : " );

            Console.WriteLine("contract : " + contract.Contract);
            var result = await _businessObject.PublishErc20(contract.Contract);

            //mudar para result.Sucess!!
            if (result.Result != null)
            {
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

}

