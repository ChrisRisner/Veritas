<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="Veritas.UI.Web.Views.VeritasViewPage<Veritas.BusinessLayer.Screens.Admin.Categories.CategoriesEditScreen>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%: Model.blogConfig.Title %> - Edit Category
</asp:Content>

<asp:Content ID="MetaContent" ContentPlaceHolderID="MetaContent" runat="server">
    <meta name="description" content="<%= Model.blogConfig.Title %> - Contact" />
    <meta name="Keywords" content="<%= Model.blogConfig.Title %>,Contact" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit Category</h2>

    <div class="divDashboard-Topic">
        <p class="pDashHeader">Edit Category Details</p>
        <%= Html.ValidationSummary() %>

        <% using (Html.BeginForm("Categories/Save", "Admin")) { %>
        <%= Html.Hidden("BlogCategoryId", Model.BlogCategoryId) %>

        <dl>
            <dt>
                <label for="Title">Name</label>
            </dt>
            <dd>
                <%=Html.TextBox("Title", Model.Title, new { MaxLength = 150, @class="itext" })%>    
                <label class="required"></label>
                <%=Html.ValidationMessage("Title", "*")%>
            </dd>
        </dl>
        <dl>
            <dt>
                <label for="Description">Description</label>
            </dt>
            <dd>
                <%=Html.TextArea("Description", Model.Description, new { MaxLength = 1000, @class="itext" })%>    
                <label class="required"></label>
                <%=Html.ValidationMessage("Description", "*")%>
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
        <input type="submit" value="Save Category" />
        <% } %>
    </div>

</asp:Content>

