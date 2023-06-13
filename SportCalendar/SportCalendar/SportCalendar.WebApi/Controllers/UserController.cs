using SportCalendar.Model;
using SportCalendar.ServiceCommon;
using SportCalendar.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        [Authorize(Roles = "Super_admin")]
        public async Task<HttpResponseMessage> GetAllAsync(int pageNumber = 1, int pageSize = 10, string orderBy = "Id", string sortOrder = "ASC",
                                                            string searchQuery = null, DateTime? fromDate = null, DateTime? toDate = null, 
                                                            DateTime? fromTime = null, DateTime? toTime = null)
        {
            try
            {
                List<User> usersList = await UserService.GetAllAsync();
                if(usersList != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, usersList);
                }
                return Request.CreateResponse(HttpStatusCode.NotFound, "User table is empty!");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Ooops, something went wrong!");
            }
            
        }

        // GET: api/User/5
        public async Task<HttpResponseMessage> GetByUserIdAsync(Guid id)
        {
            try
            {
                User result = await UserService.GetByUserIdAsync(id);

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
        public async Task<HttpResponseMessage> PutAsync(Guid id, [FromBody] User updateUser)
        {
            try
            {              
                User updatedUser = await UserService.UpdateUserAsync(id, updateUser);

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
        public async Task<HttpResponseMessage> DeleteAsync(Guid id)
        {
            try 
            {
                User result = await UserService.DeleteUserAsync(id);
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
