using Microsoft.AspNetCore.Mvc;
using ProjektAnjaParson_Backend.DataModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjektAnjaParson_Backend.Contollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        // GET api/<LoginController>/5
        [HttpGet("{username}/{password}")]
        public CUser Get(string username, string password)
        {
            List<CUser> users = new List<CUser>();
            UserController caller = new UserController();
            string hPassword = Security.Hash.Execute(password);

            users = caller.Get();

            foreach (var user in users)
            {
                if(user.Username == username && user.Password == hPassword )
                    return user;
            }
            return new CUser();
        }
    }
}
