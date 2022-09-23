using AutoMapper;
using BookMyShow.Core.Contracts.Infrastructure.Service;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Specs;
using BookMyShow.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMyShow.Controllers.V1
{
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class PaymentController : ApiControllerBase
    {

        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentController> _logger;
        private readonly IMapper _mapper;
        public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger, IMapper mapper)
        {
            _paymentService = paymentService;
            _logger = logger;
            _mapper = mapper;
        }


        // GET: <PaymentController>
        [ApiVersion("1.0")]
        [Route("")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<IEnumerable<PaymentDto>>> GetPayments()
        {
            _logger.LogInformation("Getting list of all Payments");
            var result = await _paymentService.GetPaymentsAsync();
            return Ok(result);
        }

        // GET <PaymentController>/5
        [ApiVersion("1.0")]
        [Route("{id}")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetPayment(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                return BadRequest("Please Enter Valid Data");
            }
            _logger.LogInformation("Getting Id : {id} Payment", id);
            var paymentResult = await _paymentService.GetPaymentByIdAsync(id);
            var result = _mapper.Map<Payment, PaymentDto>(paymentResult);
            if (result is null)
                return NotFound("Please Enter Valid Data");
            return Ok(result);
        }

        // POST <PaymentController>
        [ApiVersion("1.0")]
        [Route("")]
        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult> PostPayment([FromBody] PaymentVm paymentVm)
        {
            _logger.LogInformation("add new Payment");
            var paymentExit = await _paymentService.GetPaymentByBookinId(paymentVm.BookingId);
            if (paymentExit is not null)
            {
                return BadRequest("this Payment already Completed");
            }
            var payment = _mapper.Map<PaymentVm, Payment>(paymentVm);
            var paymentResult = await _paymentService.AddPaymentAsync(payment);
            var result = _mapper.Map<Payment, PaymentDto>(paymentResult);
            return Ok(result);
        }

        // PUT <PaymentController>/5
        [ApiVersion("1.0")]
        [Route("{id}")]
        [HttpPut]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult> PutPayment(int id, [FromBody] PaymentVm paymentVm)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                return BadRequest("Please Enter Valid Data");
            }
            _logger.LogInformation("Update Id: {id} Payment", id);
            var payment = _mapper.Map<PaymentVm, Payment>(paymentVm);
            var paymentResult = await _paymentService.UpdatePaymentAsynce(id, payment);
            var result = _mapper.Map<Payment, PaymentDto>(paymentResult);
            if (result.Equals(null))
                return NotFound();
            return Ok(result);
        }

        // DELETE <PaymentController>/5
        [ApiVersion("1.0")]
        [Route("{id}")]
        [HttpDelete]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
        public async Task DeletePayment(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                BadRequest("Please Enter Valid Data");
            }
            _logger.LogInformation("Deleted Id :  {id}  Payment", id);
            await _paymentService.DeletePaymentAsync(id);
        }
    }
}
