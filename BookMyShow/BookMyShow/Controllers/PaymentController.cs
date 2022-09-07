using AutoMapper;
using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Specs;
using BookMyShow.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMyShow.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class PaymentController : ApiControllerBase
    {

        private readonly IPaymentRepository _paymentRepository;
        private readonly ILogger<PaymentController> _logger;
        private readonly IMapper _mapper;
        public PaymentController(IPaymentRepository paymentRepository, ILogger<PaymentController> logger, IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: <PaymentController>
        [Route("")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<IEnumerable<PaymentDto>>> Get()
        {
            _logger.LogInformation("Getting list of all Payments");
            var result = await _paymentRepository.GetPaymentsAsync();
            return Ok(result);
        }

        // GET <PaymentController>/5
        [Route("{id}")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> Get(int id)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                return BadRequest("Please Enter Valid Data");
            }
            _logger.LogInformation("Getting Id : {id} Payment", id);
            var paymentResult = await _paymentRepository.GetPaymentAsync(id);
            var result = _mapper.Map<Payment, PaymentDto>(paymentResult);
            if (result is null)
                return NotFound("Please Enter Valid Data");
            return Ok(result);
        }

        // POST <PaymentController>
        [Route("")]
        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult> Post([FromBody] PaymentVm paymentVm)
        {
            _logger.LogInformation("add new Payment");
            var payment=_mapper.Map<PaymentVm,Payment>(paymentVm);
            var paymentResult = await _paymentRepository.AddPaymentAsync(payment);
            var result = _mapper.Map<Payment, PaymentDto>(paymentResult);
            return Ok(result);
        }

        // PUT <PaymentController>/5
        [Route("{id}")]
        [HttpPut]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult> Put(int id, [FromBody] PaymentVm paymentVm)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {Id}",id);
                return BadRequest("Please Enter Valid Data");
            }
            _logger.LogInformation("Update Id: {id} Payment", id);
            var payment = _mapper.Map<PaymentVm, Payment>(paymentVm);
            var paymentResult = await _paymentRepository.UpdatePaymentAsynce(id,payment);
            var result = _mapper.Map<Payment, PaymentDto>(paymentResult);
            if (result.Equals(null))
                return NotFound();
            return Ok(result);
        }

        // DELETE <PaymentController>/5
        [Route("{id}")]
        [HttpDelete]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
        public async Task Delete(int id)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                BadRequest("Please Enter Valid Data");
            }
            _logger.LogInformation("Deleted Id :  {id}  Payment", id);
            await _paymentRepository.DeletePaymentAsync(id);
        }
    }
}
