using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApp
{
    [Route("api")]
    public class LoginController : Controller
    {
        private readonly IAccountDatabase _db;

        public LoginController(IAccountDatabase db)
        {
            _db = db;
        }

        [HttpPost("sign-in")]
        public async Task Login(string userName)
        {
            var account = await _db.FindByUserNameAsync(userName);
            if (account != null)
            {
                var cookie = new HttpCookie()
                {
                    Name = userName,
                    Value = DateTime.Now.ToString("dd.MM.yyyy"),
                    Expires = DateTime.Now.AddMinutes(10),
                };
                Response.SetCookie(cookie);
                return View();
                //TODO 1: Generate auth cookie for user 'userName' with external id
            }
            else return HttpStatusCode.NotFound;
            //TODO 2: return 404 if user not found
        }
    }
}