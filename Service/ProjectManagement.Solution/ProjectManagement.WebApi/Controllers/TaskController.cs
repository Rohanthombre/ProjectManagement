using ProjectManagement.Models;
using ProjectManagement.Services;
using ProjectManagement.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace ProjectManagement.WebApi.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("Api/Task")]
    public class TaskController : ApiController
    {
        private ITaskService _taskServices;
        public TaskController(ITaskService taskServices)
        {
            this._taskServices = taskServices;
        }

        [HttpGet]
        [Route("GetTasks")]
        public ICollection<TaskModel> GetTasks()
        {
            try
            {
                return _taskServices.GetAllTasks();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [ResponseType(typeof(TaskModel))]
        [HttpGet]
        [Route("GetTask")]
        public IHttpActionResult GetTask(int Id)
        {
            try
            {
                var result = _taskServices.GetTaskById(Id);

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
        [Route("CreateTask")]
        public IHttpActionResult CreateTask([FromBody] TaskModel taskModel)
        {
            try
            {
                if (_taskServices.CreateTask(taskModel))
                {
                    return this.Content(HttpStatusCode.OK, taskModel);
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
        [Route("EditTask")]
        public IHttpActionResult EditTask([FromBody] TaskModel taskModel)
        {
            try
            {
                if (_taskServices.EditTask(taskModel))
                {
                    return this.Content(HttpStatusCode.OK, taskModel);
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

        [HttpDelete]
        [Route("DeleteTask")]
        public IHttpActionResult DeleteTask(int taskId)
        {
            try
            {
                if (_taskServices.DeleteTask(taskId))
                {
                    //return this.Ok();
                    return this.Content(HttpStatusCode.OK, taskId);
                }
                else
                {
                    return this.Content(HttpStatusCode.NotFound, taskId);
                }
            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("EndTask")]
        public IHttpActionResult EndTask(int taskId)
        {
            try
            {
                if (_taskServices.EndTask(taskId))
                {
                    return this.Content(HttpStatusCode.OK, taskId);
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
    }
}
