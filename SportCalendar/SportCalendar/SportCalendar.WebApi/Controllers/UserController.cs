using SportCalendar.Model;
using SportCalendar.ServiceCommon;
using SportCalendar.WebApi.Models;
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
                    RESTUser restUser = new RESTUser()
                    {
                        Id = result.Id,
                        FirstName = result.FirstName,
                        LastName = result.LastName,
                        Password = result.Password,
                        Email = result.Email,
                        RoleId = result.RoleId,
                        IsActive = result.IsActive,
                        UpdatedByUserId = result.UpdatedByUserId,
                        DateCreated = result.DateCreated,
                        DateUpdated = result.DateUpdated,
                        Username = result.Username,
                    };
                    return Request.CreateResponse(HttpStatusCode.OK, restUser);
                };

                return Request.CreateResponse(HttpStatusCode.NotFound, "User not found!!");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Ooops, something went wrong!!");     
            }

        }

        // POST: api/User
        public async Task<HttpResponseMessage> PostAsync([FromBody] User newUser)
        {
            try
            {
                User addedUser = await UserService.InsertUserAsync(newUser);
                if(addedUser != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, addedUser);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, "User already in database");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Ooops, something went wrong!!");
            }
        }

        // PUT: api/User/5
        public async Task<HttpResponseMessage> PutAsync(string username, [FromBody] User updateUser)
        {
            try
            {              
                User updatedUser = await UserService.UpdateUserAsync(username, updateUser);

                if(updatedUser != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, updatedUser);
                }
                return Request.CreateResponse(HttpStatusCode.NotFound, "User not found");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Oops, something went wrong");
            }
        }

        // DELETE: api/User/5
        public async Task<HttpResponseMessage> DeleteAsync(string username)
        {
            try 
            {
                User result = await UserService.DeleteUserAsync(username);
                if(result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                return Request.CreateResponse(HttpStatusCode.NotFound, "User not found");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Ooops, something went wrong!!");
            }
            
        }
    }
}
