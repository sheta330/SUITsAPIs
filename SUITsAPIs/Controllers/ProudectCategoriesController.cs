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
    public class ProudectCategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProudectCategoriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetProudectWithCategorie")]
        public async Task<ActionResult<Proudect>> GetProudectWithCategorie(int Id)
        {
            string[] includes = { "Categorie" , "Proudect" };
            var data = await (_unitOfWork.ProudectCategories.FindAllAsync((x => x.categorieId == Id), includes));
            return Ok(data);
        }

        [HttpGet("GetCategoriewithProudec")]
        public async Task<ActionResult<Proudect>> GetCategoriewithProudec(int Id)
        {
            string[] includes = { "Categorie", "Proudect" };
            var data = await (_unitOfWork.ProudectCategories.FindAllAsync((x => x.Proudectid == Id), includes));
            return Ok(data);
        }

        [HttpPost("AddProudectCategorie")]
        public async Task<IActionResult> AddProudectCategorie(int Proudect, int Categorieid)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var checkrepet = _unitOfWork.ProudectCategories.FindFirstAsync(x => x.categorieId == Categorieid && x.Proudectid == Proudect).Result;
            if (checkrepet != null)
            {
                return Ok("this product alredey in this category");
            }

            if (Proudect == 0)
            {
                Proudect = _unitOfWork.Proudects.last(p => p.Proudectid).Proudectid;
            }

            var date = new ProudectCategories
            {
                Proudectid = Proudect,
                categorieId = Categorieid
            };
            await _unitOfWork.ProudectCategories.AddAsync(date);
            _unitOfWork.Save();
            return Ok(date);
        }

        [HttpDelete("DeletecatProudect")]
        public async Task<IActionResult> DeletecatProudect(int Proudectid, int Categorieid)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resalt = await _unitOfWork.ProudectCategories.SingleOrDefaultAsync(x => x.Proudectid == Proudectid && x.categorieId == Categorieid);
            if (resalt == null)
                return NotFound($"this Id is  wrong");
            _unitOfWork.ProudectCategories.Delete(resalt);
            _unitOfWork.Save();
            return Ok(resalt);
        }
    }
}
