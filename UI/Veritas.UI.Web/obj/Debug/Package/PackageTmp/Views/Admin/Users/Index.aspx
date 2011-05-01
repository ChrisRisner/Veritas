<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="Veritas.UI.Web.Views.VeritasViewPage<Veritas.BusinessLayer.Screens.Admin.Users.UsersIndexScreen>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: Model.blogConfig.Title %> - Users
</asp:Content>

<asp:Content ID="MetaContent" ContentPlaceHolderID="MetaContent" runat="server">
    <meta name="description" content="<%= Model.blogConfig.Title %> - Contact" />
    <meta name="Keywords" content="<%= Model.blogConfig.Title %>,Contact" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="divAdminBody">
    <h2>Users</h2>
        <%= VeritasForm.ActionLink("Add New Users", "Users/Edit")%>
        <br />
        <div class="divDashboard-Topic">
            <p class="pDashHeader">Users</p>
            <table class="tableAdmin">
                <thead>
                    <td></td>
                    <td>Username</td>
                    <td>Email</td>
                    <td>Create Date</td>
                    <td>Last Update Date</td>
                </thead>
                <%foreach (var item in Model.BlogUsers) { %>
                    <tr>
                        <td>
                            <%= VeritasForm.ActionLink("edit", "Users/Edit", new { userId = item.BlogUserId})%>
                        </td>
                        <%--<td><%= item.BlogPageId %></td>--%>
                        <td><%= item.Username %></td>                                                
                        <td><%= item.EmailAddress %></td>                        
                        <td><%= item.CreateDate %></td>
                        <td><%= item.LastUpdateDate %></td>
                    </tr>
                <% } %>
            </table>
        </div>
    </div>
</asp:Content>
