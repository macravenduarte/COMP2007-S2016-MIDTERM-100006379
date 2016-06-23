using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//Required for connecting to Ef db
using COMP2007_S2016_MidTerm_1000063791.Models;
using System.Web.ModelBinding;
using System.Linq.Dynamic;


namespace COMP2007_S2016_MidTerm_1000063791
{
    public partial class TodoList : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //If loading the page for the first time
            if (!IsPostBack)
            {
                Session["SortColumn"] = "TodoID"; //Default sort column
                Session["SortDirection"] = "ASC";

                //Get Students from EF db
                this.GetTodoList();
            }
        }

        protected void GetTodoList()
        {
            string SortString = Session["SortColumn"].ToString() +
                    " " + Session["SortDirection"].ToString();

            //Connect to EF
            using (TodoConnection db = new TodoConnection())
            {
                //Query the Todos table using EF and LINQ
                var TodoItems = (from allItems in db.Todos
                                select allItems);
                                 
                //Bind the result to the Gridview
                TodoGridView.DataSource = TodoItems.AsQueryable().OrderBy(SortString).ToList();
                TodoGridView.DataBind();
            }
        }

        protected void TodoGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //Store which row was selected for deletion
            int selectedRow = e.RowIndex;

            //Get the selected StudentID using the Grid's DataKEy collection
            int TodoID = Convert.ToInt32(TodoGridView.DataKeys[selectedRow].Values["TodoID"]);

            //Use EF to find the selected student in the DB and remove it
            using (TodoConnection db = new TodoConnection())
            {
                //Create object of the student class and store the query string inside of it
                Todo deletedTodoItem = (from studentRecords in db.Todos
                                        where studentRecords.TodoID == TodoID
                                        select studentRecords).FirstOrDefault();

                //Remove the selected student from the db
                db.Todos.Remove(deletedTodoItem);

                //Save changes back to the db
                db.SaveChanges();

                //Refresh the grid
                this.GetTodoList();

            }
        }

        protected void TodoGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //Set the new page number
            TodoGridView.PageIndex = e.NewPageIndex;

            //Refresh the grid
            this.GetTodoList();
        }

        protected void TodoGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            //Get the colunm to sort by
            Session["SortColumn"] = e.SortExpression;

            //refresh the grid
            this.GetTodoList();

            //Toggle the direction
            Session["SortDirection"] = Session["SortDirection"].ToString() == "ASC" ? "DESC" : "ASC";
        }

        protected void TodoGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(IsPostBack)
            {
                if (e.Row.RowType == DataControlRowType.Header) //if header row has been clicked
                {
                    LinkButton linkButton = new LinkButton();

                    for(int index = 0; index < TodoGridView.Columns.Count; index++)
                    {
                        if(TodoGridView.Columns[index].SortExpression == Session["SortColumn"].ToString())
                        {
                            if(Session["SortDirection"].ToString() == "ASC")
                            {
                                linkButton.Text = " <i class = 'fa fa-caret-up fa-lg'></i>";
                            }
                            else
                            {
                                linkButton.Text = " <i class = 'fa fa-caret-down fa-lg'></i>";
                            }

                            e.Row.Cells[index].Controls.Add(linkButton);
                        }
                        
                    }
                }
            }
        }

        protected void PageSizeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Set the new page size
            TodoGridView.PageSize = Convert.ToInt32(PageSizeDropDownList.SelectedValue);

            //Refresh the grid
            this.GetTodoList();
        }
    }
}