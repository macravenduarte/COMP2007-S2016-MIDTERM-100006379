<%@ Page Title="Todo List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TodoList.aspx.cs" Inherits="COMP2007_S2016_MidTerm_1000063791.TodoList" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="comtainer">
        <div class="row">
            <div class="col-md-offset-2 col-md-8">

                <h1>TO List</h1>
                <a href="TodoDetails.aspx" class="btn btn-success btn-sm"><i class="fa fa-plus"></i> Add Todo</a>
                <asp:GridView runat="server" CssClass="table table-striped table-border table-hover" ID="TodoGridView" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="TodoID" HeaderText="Todo ID" Visible="true" />
                        <asp:BoundField DataField="TodoName" HeaderText="Todo Name" Visible="true" />
                        <asp:BoundField DataField="TodoNotes" HeaderText="Todo Notes" Visible="true" />
                        <%--asp:BoundField DataField="Completed" HeaderText="Completed Completed" Visible="true" />  --%>
                    </Columns>

                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
