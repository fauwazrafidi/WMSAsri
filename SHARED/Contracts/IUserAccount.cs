using SHARED.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SHARED.DTOs.ServiceResponses;

namespace SHARED.Contracts
{
    public interface IUserAccount
    {
        Task<GeneralResponse> CreateAccount(UserDTO userDTO);
        Task<LoginResponse> LoginAccount(LoginDTO loginDTO);
    }
}
