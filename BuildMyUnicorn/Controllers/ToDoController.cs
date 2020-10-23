using BuildMyUnicorn.Business_Layer;
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
        public ActionResult AddTodo(ToDoTask todo)
        {
            if (ModelState.IsValid)
            {
                var returnVal = _todoManager.SaveToDo(todo);

                if (returnVal.HasError)
                {
                    ViewBag.Error = returnVal.Error;
                    return View();
                }

                TempData["Message"] = "To-Do created successfully ";
                return RedirectToAction("EditToDo", new { id = returnVal.EntityID });
            }
            FillTeamMemberDropDown();
            return View();
        }


        public ActionResult EditTodo(Guid id)
        {
            FillTeamMemberDropDown();
            var todoTask = _todoManager.GetTodoItem(id);
            return View(todoTask);
        }

        [HttpPost]
        public ActionResult EditTodo(ToDoTask todo)
        {
            if (ModelState.IsValid)
            {
                _todoManager.UpdateToDo(todo);
                return RedirectToAction("EditToDo", new { id = todo.ToDoTaskID });
            }
            FillTeamMemberDropDown();
            return View(todo);
        }


        public ActionResult List()
        {
            return View(_todoManager.GetTodoList());
        }

        public ActionResult DeleteTodo(Guid id)
        {
            _todoManager.DeleteToDo(id);
            TempData["Message"] = "To-Do deleted successfully ";
            return RedirectToAction("List");
        }

       public void FillTeamMemberDropDown()
        {
            ViewBag.AssignedTo = new SelectList(_todoManager.GetTeamMembers(), "Key", "Value");
        }
    }
}