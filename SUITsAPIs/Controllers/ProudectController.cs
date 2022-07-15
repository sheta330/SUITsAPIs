using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SUITsAPIs.Core.IRepositorys.IConfigration;
using SUITsAPIs.Data;
using SUITsAPIs.Helper;
using SUITsAPIs.Models.Core_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SUITsAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProudectController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProudectController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        [HttpGet]
        public async Task<ActionResult> GetProudects()
        {
            string[] includes = { "User", "Sex" };
            return Ok(await (_unitOfWork.Proudects.GetAllAsync(includes)));
        }
        
        [HttpGet("{Id}")]
        public async Task<ActionResult<Proudect>> Proudect(int Id)
        {
            string[] includes = { "User", "Sex" };
            return Ok(await (_unitOfWork.Proudects.FindAsync((id=>id.Proudectid == Id), includes)));
        }
        [HttpGet("Proudectwithname")]
        public async Task<ActionResult<Proudect>> Proudectwithname(string name)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            string[] includes = { "User", "Sex" };
            var Proudects = _unitOfWork.Proudects.FindAllAsync((id => id.Proudectname == name), includes);
            if (Proudects == null)
                return BadRequest($"this Brand Id is {name} wrong");

            return Ok(await (Proudects));
        }

        //[HttpGet("Proudectwithasex")]
        //public async Task<ActionResult<Proudect>> Proudectwithasex(int id)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    string[] includes = { "User", "Sex" };
        //    var Proudects = _unitOfWork.Proudects.FindAllAsync((id => id.Sexid == id), includes);
        //    if (Proudects == null)
        //        return BadRequest($"this sex Id is {id} wrong");

        //    return Ok(await Proudects.ToListAsync());
        //}

        [HttpPost("AddProudect")]
        public async Task<IActionResult> AddProudect([FromBody] Proudect model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Sexid = _unitOfWork.Sexs.SingleOrDefaultAsync(x => x.Sexid == model.Sexid);
            if (Sexid == null)
                return BadRequest($"this Brand Id is {model.Sexid} wrong");

            var UserId = _unitOfWork.Users.SingleOrDefaultAsync(x => x.Id == model.UserId);
            if (UserId == null)
                return BadRequest($"this User Id is {model.UserId} wrong");

            var date = new Proudect
            {
                Createdate = model.Createdate,
                Proudectname = model.Proudectname,
                Sexid = model.Sexid,
                Proudectprice = model.Proudectprice,
                Proudectdesc = model.Proudectdesc,
                UserId = model.UserId,
                ProudSlug = model.Proudectname.ToUpper(),
            };
            await _unitOfWork.Proudects.AddAsync(date);
            Thread.Sleep(5000);
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
                id = _unitOfWork.Proudects.last(p => p.Proudectid).Proudectid;
            }

            var resalt = await _unitOfWork.Proudects.SingleOrDefaultAsync(x => x.Proudectid == id);
            if (resalt == null)
                return NotFound($"this Id is {id} wrong");

            try
            {
                imageprocess upload = new imageprocess();
                var file = Request.Form.Files[0];
                string newpath = upload.DbPath(file, "Product", ("Proudectid" + resalt.Proudectid + ".jpg"));
                resalt.ProudectImage = newpath;
                _unitOfWork.Save();
                return Ok(resalt);
            }
            catch (Exception ex)
            {
                return StatusCode(500, @"Wrong Path"+ ex);
            }
        }

        [HttpPut("updateProudect")]
        public async Task<IActionResult> updateProudect([FromBody] Proudect model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resalt = await _unitOfWork.Proudects.SingleOrDefaultAsync(x => x.Proudectid == model.Proudectid);
            if (resalt == null)
                return NotFound($"this Id is {model.Proudectid} wrong");
            resalt.Createdate = model.Createdate;
            resalt.ProudSlug = model.Proudectname.ToUpper();
            resalt.Proudectname = model.Proudectname;
            resalt.Sexid = model.Sexid;
            resalt.Proudectprice = model.Proudectprice;
            resalt.Proudectdesc = model.Proudectdesc;
            resalt.UserId = model.UserId;
            _unitOfWork.Proudects.Update(resalt);
            _unitOfWork.Save();
            return Ok(resalt);
        }

        [HttpDelete("DeleteProudect")]
        public async Task<IActionResult> DeleteProudect(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resalt = await _unitOfWork.Proudects.SingleOrDefaultAsync(x => x.Proudectid == id);
            if (resalt == null)
                return NotFound($"this Id is {id} wrong");
            imageprocess imageprocess = new imageprocess();
            var deletres = imageprocess.delete(resalt.ProudectImage);
                _unitOfWork.Proudects.Delete(resalt);
                _unitOfWork.Save();
                return Ok(resalt);
            
        }

    }
}
