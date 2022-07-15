using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SUITsAPIs.Core.IRepositorys.IConfigration;
using SUITsAPIs.Models.Core_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SUITsAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class subcategoryproductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public subcategoryproductsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetProudectWithsubCategorie")]
        public async Task<ActionResult<Proudect>> GetProudectWithsubCategorie(int Id)
        {
            string[] includes = { "sub_category", "Proudect" };
            var data = await (_unitOfWork.sub_category_prodacts.FindAllAsync((x => x.sub_categoryId == Id),includes));
            return Ok(data);
        }

        [HttpGet("GetProudectsWithsubCategorie")]
        public async Task<ActionResult<Proudect>> GetProudecstWithsubCategorie(int Id)
        {
            string[] includes = { "sub_category", "Proudect" };
            var data = await (_unitOfWork.sub_category_prodacts.FindAllAsync((x => x.sub_categoryId == Id),includes));
            return Ok(data);
        }

        [HttpGet("GetsubCategoriewithProudec")]
        public async Task<ActionResult<Proudect>> GetsubCategoriewithProudec(int Id)
        {
            string[] includes = { "sub_category", "Proudect" };
            var data = await (_unitOfWork.sub_category_prodacts.FindAllAsync((x => x.Proudectid == Id),includes));
            return Ok(data);
        }

        [HttpGet("GetAllProudecWithSubCategorie")]
        public async Task<ActionResult<Proudect>> GetAllProudecWithSubCategorie(int Id)
        {
            string[] includes = { "sub_category", "Proudect" };
            var data = await (_unitOfWork.sub_category_prodacts.FindAllAsync((x => x.sub_categoryId == Id),includes));
            return Ok(data);
        }

        [HttpPost("AddProudectsubCategorie")]
        public async Task<IActionResult> AddProudectsubCategorie(int Proudect, int subCategorieid)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (Proudect == 0)
            {
                Proudect = _unitOfWork.Proudects.last(p => p.Proudectid).Proudectid;
            }

            var checkrepet = _unitOfWork.sub_category_prodacts.FindFirstAsync(x => x.sub_categoryId == subCategorieid && x.Proudectid == Proudect).Result;
            if (checkrepet != null)
            {
                return Ok("this product alredey in this sub category");
            }

            var date = new sub_category_prodacts
            {
                Proudectid = Proudect,
                sub_categoryId = subCategorieid
            };
            await _unitOfWork.sub_category_prodacts.AddAsync(date);
            _unitOfWork.Save();
            return Ok(date);
        }
        [HttpDelete("DeletesubcatProudect")]

        public async Task<IActionResult> DeletecatsubProudect(int Proudectid, int subCategorieid)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resalt = await _unitOfWork.sub_category_prodacts.SingleOrDefaultAsync(x => x.Proudectid == Proudectid && x.sub_categoryId == subCategorieid);
            if (resalt == null)
                return NotFound($"this Id is  wrong");
            _unitOfWork.sub_category_prodacts.Delete(resalt);
            _unitOfWork.Save();
            return Ok(resalt);
        }
    }
}
