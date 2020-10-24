using Business_Model.Helper;
using Business_Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuildMyUnicorn.Business_Layer
{
    public class ToDoTaskManager
    {
        private readonly string SaveQuery = "INSERT into tbl_todo_task ({0}) VALUES ({1})";

        private readonly string UpdateQuery = "UPDATE tbl_todo_task SET {0} WHERE ToDOTaskID = @ToDOTaskID ";

        private readonly string DeleteQuery = "DELETE tbl_todo_task WHERE ToDOTaskID = @ToDOTaskID ";

        private readonly string ItemQuery = "SELECT *FROM tbl_todo_task ";

        private readonly string ClientTeamQuery = "SELECT ClientID As [Key], FirstName +' ' + LastName As Value  FROM tbl_client ";



        public List<ListItem<Guid, string>> GetTeamMembers()
        {
            var clientId = Guid.Parse(HttpContext.Current.User.Identity.Name);
            return SharedManager.GetList<ListItem<Guid, string>>(ClientTeamQuery + $"WHERE TeamClientID = '{clientId}' ").ToList();
        }

        public ToDoTask GetTodoItem(Guid id)
        {
            return SharedManager.GetItem<ToDoTask>(ItemQuery + $" WHERE ToDOTaskID = '{id}'");
        }

        public List<ToDoTask> GetTodoList()
        {
            return SharedManager.GetList<ToDoTask>(ItemQuery).ToList();
        }

        public ResponseModel SaveToDo(ToDoTask toDo)
        {
            toDo.ToDoTaskID = Guid.NewGuid();
            toDo.AssignedBy = Guid.Parse(HttpContext.Current.User.Identity.Name);
            toDo.AssignedOn = DateTime.UtcNow;
            SetPercentComplete(toDo);
            toDo.SetBasicProperties();

            if (toDo.Status == ToDoStatus.Completed)
                toDo.CompletedOn = DateTime.UtcNow;
            var responseModel = SharedManager.Save(toDo, SaveQuery);
            responseModel.EntityID = toDo.ToDoTaskID;
            return responseModel;
        }

        private static void SetPercentComplete(ToDoTask toDo)
        {
            toDo.PercentComplete = toDo.PercentComplete < 0 ? 0 : toDo.PercentComplete > 100 ? 100 : toDo.PercentComplete;
            if (toDo.PercentComplete == 100)
                toDo.Status = ToDoStatus.Completed;

            if (toDo.Status == ToDoStatus.Completed)
                toDo.PercentComplete = 100;
        }

        public ResponseModel UpdateToDo(ToDoTask toDo)
        {
            var dbToDo = GetTodoItem(toDo.ToDoTaskID);

            toDo.EntityState = EntityState.Modified;
            toDo.SetBasicProperties();

            SetPercentComplete(toDo);

            if (toDo.Status == ToDoStatus.Completed)
            {
                if (dbToDo.Status != ToDoStatus.Completed)
                    toDo.CompletedOn = DateTime.UtcNow;
            }
            else
                toDo.CompletedOn = DateTime.UtcNow;

            var resposne = SharedManager.Update(toDo, UpdateQuery);
            resposne.EntityID = toDo.ToDoTaskID;
            return resposne;
        }

        public int DeleteToDo(Guid id)
        {
            return SharedManager.Delete<ToDoTask>(DeleteQuery, id);
        }

    }
}