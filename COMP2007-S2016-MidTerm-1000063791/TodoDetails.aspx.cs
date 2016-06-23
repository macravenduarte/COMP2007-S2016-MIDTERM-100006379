using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//Required for connecting to Ef db
using COMP2007_S2016_MidTerm_1000063791.Models;
using System.Web.ModelBinding;

namespace COMP2007_S2016_MidTerm_1000063791
{
    public partial class TodoDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!IsPostBack) && (Request.QueryString.Count > 0))
            {
                //Get Students from EF db
                this.GetTodoList();
            }
        }

        protected void GetTodoList()
        {
            //Populate the form with existing data from the database
            int TodoID = Convert.ToInt32(Request.QueryString["TodoID"]);

            using (TodoConnection db = new TodoConnection())
            {
                //Connect to the EF db
                Todo updateTodoItem = (from todoItem in db.Todos
                                       where todoItem.TodoID == TodoID
                                       select todoItem).FirstOrDefault();

                //map the student properties to the form controls
                if (updateTodoItem != null)
                {
                    TodoNameTextBox.Text = updateTodoItem.TodoName;
                    TodoNotesTextBox.Text = updateTodoItem.TodoNotes;
                    
                }
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/TodoList.aspx");
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            //Use EF to connect to the server
            using (TodoConnection db = new TodoConnection())
            {
                //Use the Student model to save a new record
                Todo newTodoItem = new Todo();

                int TodoID = 0;

                if (Request.QueryString.Count > 0) //URL has a StudentID in it
                {
                    //Get the id from the url
                    TodoID = Convert.ToInt32(Request.QueryString["TodoID"]);
                    newTodoItem = (from todoItem in db.Todos
                                   where todoItem.TodoID == TodoID
                                   select todoItem).FirstOrDefault();
                }

                newTodoItem.TodoName = TodoNameTextBox.Text;
                newTodoItem.TodoNotes = TodoNotesTextBox.Text;

                if (TodoID == 0)
                {
                    db.Todos.Add(newTodoItem);
                }

                // use LINQ to ADO.NET to add / insert new student into the database
                db.Todos.Add(newTodoItem);

                // save our changes
                db.SaveChanges();

                //Redirect back to the updated students page
                Response.Redirect("~/TodoList.aspx");
            }

        }
    }
}