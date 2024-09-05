using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARED.DTOs
{
    public record class UserSession(string? Id, string? UserName, string? Role);
}
