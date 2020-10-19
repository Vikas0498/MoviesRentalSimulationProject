using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authenticate1Api
{
    public interface IAuthRepo
    {
        
        string GenerateJSONWebToken();

        Authenticate1 AuthenticateUser(Authenticate1 userdetail);
    }
}
