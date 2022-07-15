using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SUITsAPIs.Core.IRepositorys;
using SUITsAPIs.Core.IRepositorys.IConfigration;
using SUITsAPIs.Data;
using SUITsAPIs.Models.Core_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SUITsAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SexController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
       
        public SexController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
       
        [HttpGet]
        public async Task<IEnumerable<Sex>> Getsex()
        {
            var data = await _unitOfWork.Sexs.GetAllAsync();
            return  data;
        }

        [HttpGet("getid")]
        public async Task<ActionResult> Getsexid(int id)
        {
            var data = await _unitOfWork.Sexs.GetByIdAsync(id);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(Sex gendaer)
        {
            var data = await _unitOfWork.Sexs.AddAsync(gendaer);
            _unitOfWork.Save();
            return Ok(gendaer);
        }

        [HttpPut]
        public IActionResult Updatesync(Sex data)
        {
            Sex updatedata = _unitOfWork.Sexs.Update(data);
            _unitOfWork.Save();
            return Ok(updatedata);
        }
        
        [HttpDelete]
        public IActionResult Delete(Sex data)
        {
            _unitOfWork.Sexs.Delete(data);
            _unitOfWork.Save();
            return Ok();
        }

    }
}
