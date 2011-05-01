<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="Veritas.UI.Web.Views.VeritasViewPage<Veritas.BusinessLayer.Screens.Admin.Pages.PagesIndexScreen>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: Model.blogConfig.Title %> - Pages
</asp:Content>

<asp:Content ID="MetaContent" ContentPlaceHolderID="MetaContent" runat="server">
    <meta name="description" content="<%= Model.blogConfig.Title %> - Contact" />
    <meta name="Keywords" content="<%= Model.blogConfig.Title %>,Contact" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="divAdminBody">
    <h2>Pages</h2>
        <%= VeritasForm.ActionLink("Add New Page", "Pages/Edit")%>
        <div class="divDashboard-Topic">
            <p class="pDashHeader">Pages</p>
            <table class="tableAdmin">
                <thead>
                    <td></td>
                    <%--<td>Blog Page Id</td>--%>
                    <td>Title</td>
                    <td>Description</td>
                    <td>Keywords</td>
                    <td>Encoded Title</td>                    
                    <td>Create Date</td>
                    <td>Created By</td>
                    <td>Last Update Date</td>
                    <td>Last Updated By</td>
                </thead>
                <%foreach (var item in Model.BlogPages) { %>
                    <tr>
                        <td>
                            <%= VeritasForm.ActionLink("edit", "Pages/Edit", new { pageId = item.BlogPageId})%>
                        </td>
                        <%--<td><%= item.BlogPageId %></td>--%>
                        <td><%= item.PageTitle %></td>                                                
                        <td><%= item.Description %></td>                        
                        <td><%= item.Keywords %></td>                                               
                        <%--<td><%= item.PageContent %></td>--%>
                        <td><%= item.EncodedTitle %></td>
                        <td><%= item.CreateDate %></td>
                        <td><%= item.CreatedByUser.Username %></td>
                        <td><%= item.LastUpdateDate %></td>
                        <td><%= item.LastUpdatedByUser.Username %></td>
                    </tr>
                <% } %>
            </table>
        </div>
    </div>
</asp:Content>
