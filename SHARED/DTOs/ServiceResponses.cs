using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARED.DTOs
{
    public class ServiceResponses
    {
        public record class GeneralResponse(bool Flag, string Message);
        public record GeneralApiResponse(bool Flag, string Message = null!);
        public record class LoginResponse(bool Flag, string Token, string Message);
    }
}
