using ProjectManagement.Models;
using ProjectManagement.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ProjectManagement.WebApi.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("Api/User")]
    public class UserController : ApiController
    {
        private IUserService _userServices;
        public UserController(IUserService userServices)
        {
            this._userServices = userServices;
        }

        [HttpGet]
        [Route("GetUsers")]
        public ICollection<UserModel> GetUsers()
        {
            try
            {
                return _userServices.GetAllUsers();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        [Route("GetUser")]
        public IHttpActionResult GetUser(int Id)
        {
            try
            {
                var result = _userServices.GetUserById(Id);

                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("CreateUser")]
        public IHttpActionResult CreateUser([FromBody] UserModel userModel)
        {
            try
            {
                if (_userServices.CreateUser(userModel))
                {
                    return this.Content(HttpStatusCode.OK, userModel);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex);
            }

        }

        [HttpPut]
        [Route("EditUser")]
        public IHttpActionResult EditUser([FromBody] UserModel userModel)
        {
            try
            {
                if (_userServices.EditUser(userModel))
                {
                    return this.Content(HttpStatusCode.OK, userModel);
                }
                else
                {

                    return this.Content(HttpStatusCode.NotFound, userModel);
                }
            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex);
            }
        }

        [HttpDelete]
        [Route("DeleteUser")]
        public IHttpActionResult DeleteUser(int userId)
        {
            try
            {
                if (_userServices.DeleteUser(userId))
                {
                    //return this.Ok();
                    return this.Content(HttpStatusCode.OK, userId);
                }
                else
                {
                    return this.Content(HttpStatusCode.NotFound, userId);
                }
            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex);
            }
        }
    }
}
