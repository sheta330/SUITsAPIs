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
using System.Threading.Tasks;

namespace SUITsAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class categorieController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public categorieController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult> Getcategories()
        {
            var data = await _unitOfWork.categories.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult> categorie(int Id)
        {
            var data = await _unitOfWork.categories.GetByIdAsync(Id);
            return Ok(data);
        }

        [HttpPost("Addcategorie")]
        public async Task<IActionResult> Addcategorie([FromBody] categorie model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            model.categorieSlug = model.categorieName.ToUpper();
            var resalt = await _unitOfWork.categories.FindAsync(x => x.categorieSlug == model.categorieSlug);
            if (resalt != null)
                return BadRequest("this categorie alredy exist");


            var date = new categorie
            {
                categorieName = model.categorieName,
                categorieSlug = model.categorieName.ToUpper(),
                categorieCreatedate = DateTime.Now,
                imagepath = ""
            };
            await _unitOfWork.categories.AddAsync(date);
            _unitOfWork.Save();
            return Ok(resalt);
        }

        #region [HttpPut("image")] test
        [HttpPut("image")]
        public async Task<IActionResult> image(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id == 1)
            {
                var data = _unitOfWork.categories.last(p => p.categorieId).categorieId;
            }
            var resalt = await _unitOfWork.categories.SingleOrDefaultAsync(x => x.categorieId == id);
            if (resalt == null)
                return NotFound($"this Id is {id} wrong");

            try
            {
                imageprocess upload = new imageprocess();
                var file = Request.Form.Files[0];
                string newpath = upload.DbPath(file, "categories", (resalt.categorieSlug + ".jpg"));
                resalt.imagepath = newpath;
                _unitOfWork.Save();
                return Ok(resalt);
            }
            catch (Exception ex)
            {
                return StatusCode(500, @"newpath");
            }

             _unitOfWork.Save();
            
            return Ok(resalt);
        }

        #endregion

        [HttpPut("updatecategorie")]
        public async Task<IActionResult> updatecategorie([FromBody] categorie model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resalt = await _unitOfWork.categories.SingleOrDefaultAsync(x => x.categorieId == model.categorieId);
            if (resalt == null)
                return NotFound($"this Id is {model.categorieId} wrong");
            model.categorieSlug = model.categorieName.ToUpper();
            var categorieslug = await _unitOfWork.categories.FindFirstAsync(x => x.categorieSlug == model.categorieSlug);
            if (categorieslug != null)
                return BadRequest($"this name is {model.categorieName} is already exist");
            resalt.imagepath = "";
            resalt.categorieCreatedate = model.categorieCreatedate;
            resalt.categorieName = model.categorieName;
            resalt.categorieSlug = model.categorieName.ToUpper();
            _unitOfWork.Save();
            return Ok(resalt);
        }
        [HttpDelete("Deletecategorie")]
        public async Task<IActionResult> Deletecategorie(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resalt = await _unitOfWork.categories.FindFirstAsync(x => x.categorieId == id);
            if (resalt == null)
                return NotFound($"this Id is {id} wrong");
            imageprocess imageprocess = new imageprocess();
            var deletres = imageprocess.delete(resalt.imagepath);
           
                _unitOfWork.categories.Delete(resalt);
                _unitOfWork.Save();
                return Ok(resalt);
          
        }
    }
}
