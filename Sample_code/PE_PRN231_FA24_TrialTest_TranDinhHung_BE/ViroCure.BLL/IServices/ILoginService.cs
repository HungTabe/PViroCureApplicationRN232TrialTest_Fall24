using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViroCure.BLL.DTOs;
using ViroCure.DAL.Entities;

namespace ViroCure.BLL.IServices
{
    public interface ILoginService
    {
        // Login function
        Task<LoginResponseDto> LoginFunc(string email, string password);
    }
}
