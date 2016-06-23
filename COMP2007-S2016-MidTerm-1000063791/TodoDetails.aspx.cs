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
                Todo newTodo = new Todo();

                newTodo.TodoName = TodoNameTextBox.Text;
                newTodo.TodoNotes = TodoNotesTextBox.Text;
                

                db.Todos.Add(newTodo);
                db.SaveChanges();

                //Redirect back to the updated students page
                Response.Redirect("~/TodoList.aspx");
            }

        }
    }
}