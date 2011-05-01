<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="Veritas.UI.Web.Views.VeritasViewPage<Veritas.BusinessLayer.Screens.Admin.Roles.RolesIndexScreen>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: Model.blogConfig.Title %> - Roles
</asp:Content>

<asp:Content ID="MetaContent" ContentPlaceHolderID="MetaContent" runat="server">
    <meta name="description" content="<%= Model.blogConfig.Title %> - Contact" />
    <meta name="Keywords" content="<%= Model.blogConfig.Title %>,Contact" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="divAdminBody">
    <h2>Roles</h2>
        <%= VeritasForm.ActionLink("Add New Role", "Roles/Edit")%>
        <div class="divDashboard-Topic">
            <p class="pDashHeader">Roles</p>
            <table class="tableAdmin">
                <thead>
                    <td></td>
                    <td>Role Name</td>
                    <td>Create Date</td>
                </thead>
                <%foreach (var item in Model.BlogRoles) { %>
                    <tr>
                        <td>
                            <%--<%= VeritasForm.ActionLink("edit", "Roles/Edit", new { roleId = item.BlogRoleId})%>--%>
                        </td>
                        <td><%= item.RoleName %></td>                                                
                        <td><%= item.CreateDate %></td>                                                
                    </tr>
                <% } %>
            </table>
        </div>
    </div>
</asp:Content>
