<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="Veritas.UI.Web.Views.VeritasViewPage<Veritas.BusinessLayer.Screens.Home.UploadScreen>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= Model.blogConfig.Title %>
</asp:Content>

<asp:Content ID="MetaContent" ContentPlaceHolderID="MetaContent" runat="server">
    <meta name="description" content="<%= Model.blogConfig.Title %> - Contact" />
    <meta name="Keywords" content="<%= Model.blogConfig.Title %>,Contact" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Upload</h2>

    <%= Html.ValidationSummary() %>
    <% using (Html.BeginForm("Upload", "Home", FormMethod.Post, new { enctype = "multipart/form-data" }))
        {%>
   
        <p>
            <input type="file" id="fileUpload" name="fileUpload" value="Choose your file" /> <br />
        </p>
        <p>
            <label for="FirstQuestion">Question 1 : <%= Model.Question1 %></label>        
            <%= Html.TextBox("Answer1", "", new { size = "40" })%>
        </p>
        <p>
            <label for="SecondQuestion">Question 2 : <%= Model.Question2 %></label>
            <%= Html.TextBox("Answer2", "", new { size = "40" })%>
        </p>
        <p>
            <label for="ThirdQuestion">Question 3 : <%= Model.Question3 %></label>
            <%= Html.TextBox("Answer3", "", new { size = "40" })%>
        </p>    
        <input type="submit" value="Upload" />    
    <% } %>

</asp:Content>
