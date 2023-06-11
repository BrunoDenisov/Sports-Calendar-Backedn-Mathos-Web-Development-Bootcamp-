using SportCalendar.Model;
using SportCalendar.ServiceCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.UI;

namespace SportCalendar.WebApi.Controllers
{
    public class UserController : ApiController
    {
        public UserController(IUserService userService) 
        {
            UserService = userService;
        }
        protected IUserService UserService { get; set; }
        // GET: api/User
        public async Task<HttpResponseMessage>  Get()
        {
            List<User> usersList = await UserService.GetAllAsync();

            return Request.CreateResponse(HttpStatusCode.OK, usersList);
        }

        // GET: api/User/5
        public async Task<HttpResponseMessage> Get(string username)
        {
            try
            {
                User result = await UserService.GetByUsernameAsync(username);

                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                };

                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);     
            }

        }

        // POST: api/User
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/User/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/User/5
        public void Delete(int id)
        {
        }
    }
}
