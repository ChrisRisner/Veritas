<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="Veritas.UI.Web.Views.VeritasViewPage<Veritas.BusinessLayer.Screens.Admin.Categories.CategoriesIndexScreen>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%: Model.blogConfig.Title %> - Categories
</asp:Content>

<asp:Content ID="MetaContent" ContentPlaceHolderID="MetaContent" runat="server">
    <meta name="description" content="<%= Model.blogConfig.Title %> - Contact" />
    <meta name="Keywords" content="<%= Model.blogConfig.Title %>,Contact" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Categories</h2>

    <div class="divDashboard-Topic">
        <p class="pDashHeader">Add New Category</p>
        <% using (Html.BeginForm("Categories/Save", "Admin")) { %>
        <dl>
            <dt>
                <label for="Title">Name</label>
            </dt>
            <dd>
                <%=Html.TextBox("Title", Model.Title, new { MaxLength = 150, @class="itext" })%>    
                <label class="required"></label>
            </dd>
        </dl>
        <dl>
            <dt>
                <label for="Description">Description</label>
            </dt>
            <dd>
                <%=Html.TextArea("Description", Model.Description, new { MaxLength = 1000, @class="itext" })%>    
                <label class="required"></label>
            </dd>
        </dl>
        <dl>
            <dt>
                <label for="IsActive">Active</label>
            </dt>
            <dd>
                <%=Html.CheckBox("IsActive", Model.IsActive, new {  @class="icheckbox" })%>    
                <label class="required"></label>
            </dd>
        </dl>
        <input type="submit" value="Add New Category" />
        <% } %>
    </div>

    <div class="divDashboard-Topic">
        <p class="pDashHeader">Existing Categories</p>
        <table>
            <thead>
                <td></td>
                <td>Title</td>
                <td>Description</td>
                <td>Entries</td>
            </thead>
            <% foreach (var item in Model.BlogCategories)
               { %>
            <tr>
                <td>
                    <%= VeritasForm.ActionLink("edit", "Categories/Edit", new { categoryId = item.BlogCategoryId })%>
                </td>
                <td>
                    <%: item.Title %>
                </td>
                <td>
                    <%: item.Description %>
                </td>
                <td>
                    <%: item.BlogEntryCategories.Count %>
                </td>
            </tr>
            <% } %>
        </table>
    </div>
</asp:Content>

