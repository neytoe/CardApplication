using AutoMapper;
using CardApplication.Core.Interface;
using CardApplication.Dto;
using CardApplication.Model;
using CardApplication.Utilies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CardApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly ICheapPaymentGateway _cheapPayment;
        private readonly IExpensivePaymentGateway _expensivePayment;
        private readonly IPremiumPaymentGateway _premiumPayment;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public PaymentController(ILogger<PaymentController> logger, IServiceProvider provider, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _cheapPayment = provider.GetRequiredService<ICheapPaymentGateway>();
            _expensivePayment = provider.GetRequiredService<IExpensivePaymentGateway>();
            _premiumPayment = provider.GetRequiredService<IPremiumPaymentGateway>();
            _transactionRepository = provider.GetRequiredService<ITransactionRepository>();
        }

        [HttpPost]
        public async Task<IActionResult> ProcessPayment(PaymentDto payment)
        {
            if (!ModelState.IsValid) return BadRequest(new { message = "Bad Request", errors = "Invalid credentials provided" });

            if (Utilities.IsModelValid(payment))
            {
                Payment model = null;
                try
                {
                    model = _mapper.Map<Payment>(payment);

                    if (model.Amount < 21)
                        await _cheapPayment.ProcessPayment(model);
                    else if (model.Amount < 501)
                    {
                        try
                        {
                            await _expensivePayment.ProcessPayment(model);
                        }
                        catch (Exception)
                        {
                            await _cheapPayment.ProcessPayment(model);
                        }
                    }
                    else
                    {
                        try
                        {
                            await _premiumPayment.ProcessPayment(model);
                        }
                        catch (Exception)
                        {
                            int i = 1;
                            do
                            {
                                var response = await _cheapPayment.ProcessPayment(model);
                                if (response)
                                    break;
                                i++;
                            }
                            while (i < 4);
                        }
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                }

                try
                {
                    var result = await _transactionRepository.GetById(model.Id);
                    return Ok(result);
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                }
            }
            return StatusCode(500, "internal error");
        }
    }
}
