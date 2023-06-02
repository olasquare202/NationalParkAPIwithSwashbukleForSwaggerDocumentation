using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NationalParkAPI.Models;
using NationalParkAPI.Models.Dtos;
//using TrailAPI.Dtos;
//using TrailAPI.Models;
using TrailAPI.Repository.IRepository;
using TrailAPI.Repository.IRepository;

namespace TrailAPI.Controllers
{
    [Route("api/Trails")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]//Dis Response code is apply to all mtds in this controller
    public class TrailsController : Controller
    {
        //Let get all our mtds 'CRUD' from repository using Dependency Injection. Also we need Automapper
        private readonly ITrailRepository _trailRepository;
        private readonly IMapper _mapper;

        public TrailsController(ITrailRepository trailRepository, IMapper mapper)
        {
            _trailRepository = trailRepository;
            _mapper = mapper;
        }
        //How to use XML comment to improve our documentation
        /// <summary>
        /// Get list of all the trails.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllTrails")]
        [ProducesResponseType(200, Type = typeof(List<TrailDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetAllTrails()
        {
            var objList = _trailRepository.GetTrails();
            return Ok(objList);
        }


        /// <summary>
        /// Get only one trail.
        /// </summary>
        /// <param name="trailId">The Id of the National Park</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetTrail", Name = "GetTrail")]
        [ProducesResponseType(200, Type = typeof(TrailDto))]
        //[ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]

        public IActionResult GetTrail(int trailId)
        {
            var objList1 = _trailRepository.GetTrailById(trailId);
            if (objList1 == null)
            {
                return NotFound();
            }
//Automapper made it simple with just a line of code
            var objList1Dto = _mapper.Map<TrailDto>(objList1);//Alternatively
            //var objList1Dto = new TrailDto()
            //{
            //    Created = objList1.Created,
            //    Id = objList1.Id,
            //    Name = objList1.Name,
            //    State = objList1.State,
            //}
            //return Ok(objList1Dto);
            return Ok(objList1Dto);
        }
        /// <summary>
        /// This Comment lines were generated automatically by Swashbukle
        /// Just press /3x
        /// </summary>
        /// <param name="trailDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateTrail")]
        [ProducesResponseType(201, Type = typeof(TrailDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]


        public IActionResult CreateTrail([FromBody] TrailCreateDto trailDto)
        {
            if(trailDto == null)
            {
                //ModelState contain all d error messages
                return BadRequest(ModelState);
            }
            if (_trailRepository.TrailExists(trailDto.Name))
            {
                ModelState.AddModelError("", "Trail Exist");
                return StatusCode(404, ModelState);
            }
           
            var trailObj = _mapper.Map<Trail>(trailDto);
            if (!_trailRepository.CreateTrail(trailObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {trailObj.Name}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetTrail", new {trailId= trailObj.Id}, trailObj);
        }
        [HttpPatch("UpdateTrail", Name = "UpdateTrail")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult UpdateTrail(int trailId, [FromBody] TrailUpdateDto trailDto)
        {
            if (trailDto == null || trailId != trailDto.Id)
            {
                //ModelState contain all d error messages
                return BadRequest(ModelState);
            }
            if (_trailRepository.TrailExists(trailDto.Name))
            {
                ModelState.AddModelError("", "National Park Exist");
                return StatusCode(404, ModelState);
            }
            var trailObj = _mapper.Map<Trail>(trailDto);
            if (!_trailRepository.UpdateTrail(trailObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {trailObj.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("DeleteTrail", Name = "DeleteTrail")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult DeleteTrail(int trailId)
        {
            if (!_trailRepository.TrailExists(trailId))
            {
                return NotFound(trailId);
            }
            var trailObj = _trailRepository.GetTrailById(trailId);
            if (!_trailRepository.DeleteTrail(trailObj))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {trailObj.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}

