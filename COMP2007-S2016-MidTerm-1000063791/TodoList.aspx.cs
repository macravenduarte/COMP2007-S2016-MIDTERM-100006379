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
    public partial class TodoList : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //If loading the page for the first time
            if (!IsPostBack)
            {
                //Get Students from EF db
                this.GetTodoList();
            }
        }

        protected void GetTodoList()
        {
            //Connect to EF
            using (TodoConnection db = new TodoConnection())
            {
                //Quesry the Students table using EF and LINQ
                //this behaves like a foreach loop
                var TodoItems = (from allItems in db.Todos
                                select allItems);

                                 
                //Bind the result to the Gridview
                TodoGridView.DataSource = TodoItems.ToList();
                TodoGridView.DataBind();
            }
        }
    }
}