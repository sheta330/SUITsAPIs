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
    public class subcatigoreController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public subcatigoreController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult> Getsubcategorys()
        {
            string[] includes = { "Categorie" };
            return Ok(await (_unitOfWork.sub_category.GetAllAsync(includes)));
        }

        [HttpPut("updatesubcategorie")]
        public async Task<IActionResult> updatecategorie([FromBody] sub_category model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resalt = await _unitOfWork.sub_category.SingleOrDefaultAsync(x => x.id == model.id);
            if (resalt == null)
                return NotFound($"this Id is {model.categorieId} wrong");
            model.subcategoryslug = model.subcategoryname.ToUpper();
            var subcategorieslug = await _unitOfWork.sub_category.FindFirstAsync(x => x.subcategoryslug == model.subcategoryslug);
            if (subcategorieslug != null)
                return BadRequest($"this name is {model.subcategoryname} is already exist");
            resalt.imagepath = "";
            resalt.subcategoryname = model.subcategoryname;
            resalt.subcategoryslug = model.subcategoryslug.ToUpper();
            resalt.categorieId = model.categorieId;
            _unitOfWork.Save();
            return Ok(resalt);
        }
        [HttpGet("getsubcategorywithid")]
        public async Task<ActionResult> Getsubcategoryswithcategors(int id)
        {
            string[] includes = { "Categorie" };
            return Ok(await (_unitOfWork.sub_category.FindAllAsync((x => x.id == id),includes)));
        }
        
        [HttpGet("getsubcategorywithcategoryid")]
        public async Task<ActionResult> getsubcategorywithcategoryid(int id)
        {
            string[] includes = { "Categorie" };
            return Ok(await (_unitOfWork.sub_category.FindAllAsync((x => x.categorieId == id),includes)));
        }

        [HttpPost("Addsubcategory")]
        public async Task<IActionResult> AddProudect([FromBody] sub_category model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            model.subcategoryslug = model.subcategoryslug.ToUpper();
            var resalt = await _unitOfWork.sub_category.FindFirstAsync(x => x.subcategoryslug == model.subcategoryslug);
            if (resalt != null)
                return BadRequest("this sub categorie alredy exist");

            var date = new sub_category
            {
                subcategoryname = model.subcategoryname,
                subcategoryslug = model.subcategoryname.ToUpper(),
                categorieId = model.categorieId,
                imagepath = model.imagepath
            };
            await _unitOfWork.sub_category.AddAsync(date);
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
                id = _unitOfWork.sub_category.last(p => p.id).id;
            }

            var resalt = await _unitOfWork.sub_category.SingleOrDefaultAsync(x => x.id == id);
            if (resalt == null)
                return NotFound($"this Id is {id} wrong");

            try
            {
                imageprocess upload = new imageprocess();
                var file = Request.Form.Files[0];
                string newpath = upload.DbPath(file, "subcat", ("subcat" + resalt.id + ".jpg"));
                resalt.imagepath = newpath;
                _unitOfWork.Save();
                return Ok(resalt);
            }
            catch (Exception ex)
            {
                return StatusCode(500, @"newpath");
            }
        }

        [HttpDelete("Deletesubcategory")]

        public async Task<IActionResult> DeleteProudect(int subcategory)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resalt = await _unitOfWork.sub_category.SingleOrDefaultAsync(x => x.id == subcategory);
            if (resalt == null)
                return NotFound($"this Id is wrong");
            imageprocess imageprocess = new imageprocess();
            var deletres = imageprocess.delete(resalt.imagepath);
            if (deletres)
            {
                _unitOfWork.sub_category.Delete(resalt);
                _unitOfWork.Save();
                return Ok(resalt);
            }
            else
            {
                return Ok("can not delete it");
            }
        }
    }
}