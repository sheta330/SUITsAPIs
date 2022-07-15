using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SUITsAPIs.Core.IRepositorys.IConfigration;
using SUITsAPIs.Helper;
using SUITsAPIs.Models.Core_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SUITsAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class offerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public offerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult> GetProudects()
        {
            return Ok(await (_unitOfWork.offers.GetAllAsync()));
        }
        [HttpPost("Addoffer")]
        public async Task<IActionResult> AddProudect([FromBody] offer model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var date = new offer
            {
                offerdesc = model.offerdesc
            };
            await _unitOfWork.offers.AddAsync(date);
            _unitOfWork.Save();
            return Ok(date);
        }
        [HttpPut("image")]
        public async Task<IActionResult> image(int id)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id == 1)
            {
                id = _unitOfWork.offers.last(p => p.offerid).offerid;
            }

            var resalt = await _unitOfWork.offers.SingleOrDefaultAsync(x => x.offerid == id);
            if (resalt == null)
                return NotFound($"this Id is {id} wrong");

            try
            {
                imageprocess upload = new imageprocess();
                var file = Request.Form.Files[0];
                string newpath = upload.DbPath(file, "offer", ("offer" + resalt.offerid + ".jpg"));
                resalt.imagepath = newpath;
                _unitOfWork.Save();
                return Ok(resalt);
            }
            catch (Exception ex)
            {
                return StatusCode(500, @"Wrong path"+ex);
            }
        }

        [HttpDelete("DeleteProudect")]
        public async Task<IActionResult> DeleteProudect(int id)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resalt = await _unitOfWork.offers.SingleOrDefaultAsync(x => x.offerid == id);
            if (resalt == null)
                return NotFound($"this Id is {id} wrong");
            imageprocess imageprocess = new imageprocess();
            var deletres = imageprocess.delete(resalt.imagepath);
                _unitOfWork.offers.Delete(resalt);
                _unitOfWork.Save();
                return Ok(resalt);
        }

        [HttpPut("updateoffer")]
        public async Task<IActionResult> updateoffer([FromBody] offer model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resalt = await _unitOfWork.offers.SingleOrDefaultAsync(x => x.offerid == model.offerid);
            if (resalt == null)
                return NotFound($"this Id is {model.offerid} wrong");
            resalt.imagepath = "";
            resalt.offerdesc = model.offerdesc;
            _unitOfWork.Save();
            return Ok(resalt);
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<offer>> offerid(int Id)
        {
            return Ok(await (_unitOfWork.offers.FindFirstAsync(x => x.offerid == Id)));
        }

    }

}