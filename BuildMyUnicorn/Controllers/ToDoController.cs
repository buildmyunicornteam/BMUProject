using BuildMyUnicorn.Business_Layer;
using Business_Model.Helper;
using Business_Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuildMyUnicorn.Controllers
{
    public class ToDoController : WebController
    {
        private readonly ToDoTaskManager _todoManager;

        public ToDoController()
        {
            _todoManager = new ToDoTaskManager();
        }
        public ActionResult AddTodo()
        {
            FillTeamMemberDropDown();
            return View();
        }

        [HttpPost]
        public JsonResult AddTodo(ToDoTask todo)
        {
            ResponseModel response = new ResponseModel();
            if (ModelState.IsValid)
            {
                response = _todoManager.SaveToDo(todo);

                if (!response.HasError)
                {
                    response.Message = "To-Do created successfully ";
                    return Json(response);
                }
                
            }
            else
            {
                response.Error = "Model Validation Error";
            }
            Response.StatusCode = 400;
            return Json(response);
        }


        public ActionResult EditTodo(Guid id)
        {
            FillTeamMemberDropDown();
            var todoTask = _todoManager.GetTodoItem(id);
            return View(todoTask);
        }

        [HttpPost]
        public JsonResult EditTodo(ToDoTask todo)
        {
            ResponseModel response = new ResponseModel();
            if (ModelState.IsValid)
            {
                response = _todoManager.UpdateToDo(todo);

                if (!response.HasError)
                {
                    response.Message = "To-Do updated successfully ";
                    return Json(response);
                }

            }
            else
            {
                response.Error = "Model Validation Error";
            }
            Response.StatusCode = 400;
            return Json(response);
        }


        public ActionResult List()
        {
            return View(_todoManager.GetTodoList());
        }

        [HttpPost]
        public ActionResult DeleteTodo(Guid id)
        {
            _todoManager.DeleteToDo(id);
            return PartialView("_TodoListPartial", _todoManager.GetTodoList());
        }

       public void FillTeamMemberDropDown()
        {
            ViewBag.AssignedTo = new SelectList(_todoManager.GetTeamMembers(), "Key", "Value");
        }
    }
}