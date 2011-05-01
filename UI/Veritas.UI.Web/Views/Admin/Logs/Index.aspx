<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="Veritas.UI.Web.Views.VeritasViewPage<Veritas.BusinessLayer.Screens.Admin.Logs.LogsIndexScreen>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: Model.blogConfig.Title %> - Logs
</asp:Content>

<asp:Content ID="MetaContent" ContentPlaceHolderID="MetaContent" runat="server">
    <meta name="description" content="<%= Model.blogConfig.Title %> - Contact" />
    <meta name="Keywords" content="<%= Model.blogConfig.Title %>,Contact" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="divAdminBody">
    <h2>Logs</h2>

    <div class="divDashboard-Topic">
        <table class="tableAdmin">
            <thead>
                <td>Blog Log ID</td>
                <td>Level</td>
                <td>Create Date</td>
                <td>Logger</td>
                <td>Url</td>
                <td>Message</td>
                <td>Exception</td>
            </thead>
            <% foreach (var item in Model.BlogLogs)
               { %>
               <tr valign="top">
                    <td><%= item.BlogLogId %></td>
                    <td><%= item.EventLevel %></td>                
                    <td><%= item.CreateDate %></td>
                    <td><%= item.Logger %></td>                
                    <td><%= item.Url %></td>
                    <td><%= item.Message %></td>
                    <td><%= item.Exception %></td>
               </tr>
            <% } %>
        </table>
    </div>


    </div>
</asp:Content>
