<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="Veritas.UI.Web.Views.VeritasViewPage<Veritas.BusinessLayer.Screens.Admin.Uploads.UploadsIndexScreen>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%= Model.blogConfig.Title %>
</asp:Content>

<asp:Content ID="MetaContent" ContentPlaceHolderID="MetaContent" runat="server">
    <meta name="description" content="<%= Model.blogConfig.Title %> - Contact" />
    <meta name="Keywords" content="<%= Model.blogConfig.Title %>,Contact" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="divAdminBody">
    <h2>Uploads</h2>
    <div class="divDashboard-Topic">
        <p class="pDashHeader">Add New Upload</p>
        <%= Html.ValidationSummary() %>
        <% using (Html.BeginForm("Uploads/Save", "Admin", FormMethod.Post, new { enctype = "multipart/form-data" })) { %>
            <p>
                <input type="file" id="fileUpload" name="fileUpload" value="Choose your file" /> <br />
            </p>
            <input type="submit" value="Submit New Upload" />
        <% } %>
    </div>

    <div class="divDashboard-Topic">
        <p class="pDashHeader">Existing Uploads</p>
        <table>
            <thead>
                <td>File Name</td>
                <td>File Path</td>
                <td>Server Path</td>
            </thead>
            <% foreach (var item in Model.BlogMedias)
               { %>
            <tr>
                <td>
                    <%: item.FileName %>
                </td>
                <td>
                    <%: item.FilePath %>
                </td>
                <td>
                    <%: item.ServerPath %>
                </td>
            </tr>
            <% } %>
        </table>
    </div>
    </div>
</asp:Content>

