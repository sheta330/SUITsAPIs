using SUITsAPIs.Models;
using SUITsAPIs.Models.JWT_Helper_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SUITsAPIs.Core.IRepositorys
{
    public interface IAuthServiceRepository : IGenericRepository<AuthModel> 
    {
        Task<AuthModel> RegisterAsync(RegisterModel model);
        Task<AuthModel> GetTokenAsync(TokenRequestModel model);
        Task<string> AddRoleAsync(AddRoleModel model);
    }
}
