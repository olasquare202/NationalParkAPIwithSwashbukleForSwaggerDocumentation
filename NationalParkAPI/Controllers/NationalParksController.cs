using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NationalParkAPI.Dtos;
using NationalParkAPI.Models;
using NationalParkAPI.Repository.IRepository;

namespace NationalParkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]//Dis Response code is apply to all mtds in this controller
    public class NationalParksController : Controller
    {
        //Let get all our mtds 'CRUD' from repository using Dependency Injection. Also we need Automapper
        private readonly INationalParkRepository _nationalParkRepository;
        private readonly IMapper _mapper;

        public NationalParksController(INationalParkRepository nationalParkRepository, IMapper mapper)
        {
            _nationalParkRepository = nationalParkRepository;
            _mapper = mapper;
        }
        //How to use XML comment to improve our documentation
        /// <summary>
        /// Get list of all the national parks.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllNationalParks")]
        [ProducesResponseType(200, Type = typeof(List<NationalParkDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetAllNationalParks()
        {
            var objList = _nationalParkRepository.GetNationalParks();
            return Ok(objList);
        }


        /// <summary>
        /// Get only one national park.
        /// </summary>
        /// <param name="nationalParkId">The Id of the National Park</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetNationalPark", Name = "GetNationalPark")]
        [ProducesResponseType(200, Type = typeof(NationalParkDto))]
        //[ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]

        public IActionResult GetNationalPark(int nationalParkId)
        {
            var objList1 = _nationalParkRepository.GetNationalParkById(nationalParkId);
            if (objList1 == null)
            {
                return NotFound();
            }
//Automapper made it simple with just a line of code
            var objList1Dto = _mapper.Map<NationalParkDto>(objList1);//Alternatively
            //var objList1Dto = new NationalParkDto()
            //{
            //    Created = objList1.Created,
            //    Id = objList1.Id,
            //    Name = objList1.Name,
            //    State = objList1.State,
            //}
            //return Ok(objList1Dto);
            return Ok(objList1Dto);
        }
        [HttpPost]
        [Route("CreateNationalPark")]
        [ProducesResponseType(201, Type = typeof(NationalParkDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]


        public IActionResult CreateNationalPark([FromBody] NationalParkDto nationalParkDto)
        {
            if(nationalParkDto == null)
            {
                //ModelState contain all d error messages
                return BadRequest(ModelState);
            }
            if (_nationalParkRepository.NationalParkExists(nationalParkDto.Name))
            {
                ModelState.AddModelError("", "National Park Exist");
                return StatusCode(404, ModelState);
            }
           
            var nationalParkObj = _mapper.Map<NationalPark>(nationalParkDto);
            if (!_nationalParkRepository.CreateNationalPark(nationalParkObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {nationalParkObj.Name}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetNationalPark", new {nationalParkId= nationalParkObj.Id}, nationalParkObj);
        }
        [HttpPatch("UpdateNationalPark", Name = "UpdateNationalPark")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult UpdateNationalPark(int nationalParkId, [FromBody] NationalParkDto nationalParkDto)
        {
            if (nationalParkDto == null || nationalParkId != nationalParkDto.Id)
            {
                //ModelState contain all d error messages
                return BadRequest(ModelState);
            }
            if (_nationalParkRepository.NationalParkExists(nationalParkDto.Name))
            {
                ModelState.AddModelError("", "National Park Exist");
                return StatusCode(404, ModelState);
            }
            var nationalParkObj = _mapper.Map<NationalPark>(nationalParkDto);
            if (!_nationalParkRepository.UpdateNationalPark(nationalParkObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {nationalParkObj.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("DeleteNationalPark", Name = "DeleteNationalPark")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult DeleteNationalPark(int nationalParkId)
        {
            if (!_nationalParkRepository.NationalParkExists(nationalParkId))
            {
                return NotFound(nationalParkId);
            }
            var nationalParkObj = _nationalParkRepository.GetNationalParkById(nationalParkId);
            if (!_nationalParkRepository.DeleteNationalPark(nationalParkObj))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {nationalParkObj.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
