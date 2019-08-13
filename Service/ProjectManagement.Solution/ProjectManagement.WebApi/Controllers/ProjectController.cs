using ProjectManagement.Models;
using ProjectManagement.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ProjectManagement.WebApi.Controllers
{
    public class ProjectController : ApiController
    {
        private IProjectService _projectServices;
        public ProjectController(IProjectService projectServices)
        {
            this._projectServices = projectServices;
        }

        [HttpGet]
        [Route("GetProjects")]
        public ICollection<ProjectModel> GetProjects()
        {
            try
            {
                return _projectServices.GetAllProjects();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        [Route("GetProject")]
        public IHttpActionResult GetProject(int Id)
        {
            try
            {
                var result = _projectServices.GetProjectById(Id);

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
        [Route("CreateProject")]
        public IHttpActionResult CreateProject([FromBody] ProjectModel projectModel)
        {
            try
            {
                if (_projectServices.CreateProject(projectModel))
                {
                    return this.Content(HttpStatusCode.OK, projectModel);
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
        [Route("EditProject")]
        public IHttpActionResult EditProject([FromBody] ProjectModel projectModel)
        {
            try
            {
                if (_projectServices.EditProject(projectModel))
                {
                    return this.Content(HttpStatusCode.OK, projectModel);
                }
                else
                {

                    return this.Content(HttpStatusCode.NotFound, projectModel);
                }
            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex);
            }
        }

        [HttpDelete]
        [Route("DeleteProject")]
        public IHttpActionResult DeleteProject(int projectId)
        {
            try
            {
                if (_projectServices.DeleteProject(projectId))
                {
                    //return this.Ok();
                    return this.Content(HttpStatusCode.OK, projectId);
                }
                else
                {
                    return this.Content(HttpStatusCode.NotFound, projectId);
                }
            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex);
            }
        }

    }
}
