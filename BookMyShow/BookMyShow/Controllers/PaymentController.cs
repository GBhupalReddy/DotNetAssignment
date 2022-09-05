using AutoMapper;
using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Entities;
using BookMyShow.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMyShow.Controllers
{
    
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
        // GET: api/<PaymentController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            _logger.LogInformation("Getting list of all Payments");
            var result = await _paymentRepository.GetPaymentsAsync();
            return Ok(result);
        }

        // GET api/<PaymentController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                return BadRequest();
            }
            _logger.LogInformation("Getting Id : {id} Payment", id);
            var result = await _paymentRepository.GetPaymentAsync(id);
            return Ok(result);
        }

        // POST api/<PaymentController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PaymentVm paymentVm)
        {
            _logger.LogInformation("add new Payment");
            var payment=_mapper.Map<PaymentVm,Payment>(paymentVm);
            var result=await _paymentRepository.AddPaymentAsync(payment);
            return Ok(result);
        }

        // PUT api/<PaymentController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] PaymentVm paymentVm)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {Id}",id);
                return BadRequest();
            }
            _logger.LogInformation("Update Id: {id} Payment", id);
            var payment = _mapper.Map<PaymentVm, Payment>(paymentVm);
            var result =await _paymentRepository.UpdatePaymentAsynce(id,payment);
            return Ok(result);
        }

        // DELETE api/<PaymentController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {Id}", id);
               
            }
            _logger.LogInformation("Deleted Id :  {id}  Payment", id);
            await _paymentRepository.DeletePaymentAsync(id);
        }
    }
}
