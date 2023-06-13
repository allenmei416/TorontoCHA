using Microsoft.AspNetCore.Mvc;
using TorontoCHA.BusinessDomain;
using TorontoCHA.Entity;

namespace TorontoCHA.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TchaAccountController : ControllerBase
    {
        private readonly ITchaAccountBD _tchaAccountBD;

        private readonly ILogger<TchaAccountController> _logger;

        public TchaAccountController(ILogger<TchaAccountController> logger, ITchaAccountBD tchaAccountBD)
        {
            _logger = logger;
            _tchaAccountBD = tchaAccountBD;
        }

        [HttpPost]
        [Route("CreateTchaAccount")]
        public async Task<IActionResult> CreateTchaAccount(TchaAccount tchaAccount)
        {
            try
            {
                if(tchaAccount != null)
                {
                    var data = _tchaAccountBD.CreateTchaAccount(tchaAccount);
                    if (data != 0)
                        return Ok(data);
                    else
                        return Ok(data);
                }
                else
                {
                    return Ok("Error");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("LoginTchaAccount")]
        public async Task<IActionResult> LoginTchaAccount(string username, string password)
        {
            try
            {
                if (username != null && password != null)
                {
                    TchaAccount data = _tchaAccountBD.LoginTchaAccount(username, password);
                    if (data != null)
                        return Ok(data);
                    else
                        return Ok(data);
                }
                else
                {
                    return Ok("Error");
                }
            }
            catch (Exception ex)
            {
                string message = "An unhandled exception has occured while executing the request.";
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost]
        [Route("DeleteTchaAccount")]
        public async Task<IActionResult> DeleteTchaAccount(int accountId)
        {
            try
            {
                if (accountId != null)
                {
                    int data = _tchaAccountBD.DeleteTchaAccount(accountId);
                    if (data != 0)
                        return Ok("Successful");
                    else
                        return Ok("Error");
                }
                else
                {
                    return Ok("Error");
                }
            }
            catch (Exception ex)
            {
                string message = "An unhandled exception has occured while executing the request.";
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost]
        [Route("UpdateAccount")]
        public async Task<IActionResult> UpdateAccount(TchaAccount tchaAccount)
        {
            try
            {
                if (tchaAccount != null)
                {
                    var data = _tchaAccountBD.UpdateAccount(tchaAccount);
                    if (data != 0)
                        return Ok(data);
                    else
                        return Ok(data);
                }
                else
                {
                    return Ok("Error");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetAccountById")]
		public async Task<IActionResult> GetAccountById(int accountId)
		{
			try
			{
				if (accountId != null)
				{
					TchaAccount data = _tchaAccountBD.GetAccountById(accountId);
					if (data != null)
						return Ok(data);
					else
						return Ok(data);
				}
				else
				{
					return Ok("Error");
				}
			}
			catch (Exception ex)
			{
				string message = "An unhandled exception has occured while executing the request.";
				return BadRequest(ex.ToString());
			}
		}
	}
}