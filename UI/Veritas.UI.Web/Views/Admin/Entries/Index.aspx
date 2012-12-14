<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="Veritas.UI.Web.Views.VeritasViewPage<Veritas.BusinessLayer.Screens.Admin.Entries.EntriesIndexScreen>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%: Model.blogConfig.Title %> - Entries
</asp:Content>

<asp:Content ID="MetaContent" ContentPlaceHolderID="MetaContent" runat="server">
    <meta name="description" content="<%= Model.blogConfig.Title %> - Contact" />
    <meta name="Keywords" content="<%= Model.blogConfig.Title %>,Contact" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="divAdminBody">
    <h2>Entries</h2>
        <%= VeritasForm.ActionLink("Add New Entry", "Entries/Edit")%>
        <div class="divDashboard-Topic">
            <p class="pDashHeader">Entries</p>
            <table class="tableAdmin">
                <thead>
                    <td></td>
                    <%--<td>Blog Page Id</td>--%>
                    <td>Title</td>
                    <td>View Count</td>
                    <td>Feedback count</td>
                    <td>Keywords</td>                    
                    <td>Is Most Recent Entry</td>
                    <td>Short</td>
                    <td>Post Type</td>
                    <td>Publish Date</td>
                    <td>Logo Url</td>
                    <td>Entry Name</td>
                    <td>Created By</td>
                    <td>Create Date</td>
                    <td>Last Update Date</td>                    
                </thead>
                <%foreach (var item in Model.BlogEntries) { %>
                    <tr>
                        <td>
                            <%= VeritasForm.ActionLink("edit", "Entries/Edit", new { entryId = item.BlogEntryId})%>
                        </td>
                        
                        <td><%= item.Title %></td>
                        <td><%= item.PageViews %></td>
                        <td><%= item.FeedbackCount %></td>
                        <td><%= item.Keywords %></td>                        
                        <td><%= item.IsMostRecentEntry %></td>                        
                        <td><%= item.Short %></td>
                        <td><%= item.PostTypeText %></td>
                        <td><%= item.PublishDate %></td>
                        <td><%= item.LogoUrl %></td>
                        <td><%= item.EntryName %></td>
                        <td><%= item.BlogUser.Username %></td>
                        <td><%= item.CreateDate %></td>
                        <td><%= item.LastUpdateDate %></td>
                    </tr>
                <% } %>
            </table>
        </div>
    </div>
</asp:Content>

