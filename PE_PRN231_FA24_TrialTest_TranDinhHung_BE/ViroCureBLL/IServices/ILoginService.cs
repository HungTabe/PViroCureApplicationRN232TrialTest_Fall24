using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViroCureBLL.DTOs;
using ViroCureDAL.Entities;

namespace ViroCureBLL.IServices
{
    public interface ILoginService
    {
        Task<LoginResponseDto> LoginFunction(string email, string password);
    }
}
