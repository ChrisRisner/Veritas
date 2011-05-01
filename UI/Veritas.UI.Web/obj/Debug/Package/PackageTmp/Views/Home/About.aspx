<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="Veritas.UI.Web.Views.VeritasViewPage<Veritas.BusinessLayer.Screens.Home.AboutScreen>" %>
<%@ Import Namespace="Veritas.BusinessLayer" %>
<%@ Import Namespace="Veritas.DataLayer.Models" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    <%= Model.blogConfig.Title %> - About
</asp:Content>

<asp:Content ID="MetaContent" ContentPlaceHolderID="MetaContent" runat="server">
    <meta name="description" content="<%= Model.blogConfig.Title %> - About" />
    <meta name="Keywords" content="<%= Model.blogConfig.Title %>,About" />
</asp:Content>

<asp:Content ID="aboutContent" ContentPlaceHolderID="MainContent" runat="server">

    <% if (Model.blogConfig.ShowBlogAbout) %>
    <% { %>
        <h2>About this Site</h2>
        <%= Model.blogConfig.BlogAbout%>
        <br /><br />
    <% } %>
    <% if (Model.blogConfig.ShowAuthorsAbout) %>
    <% { %>
        <% foreach(BlogUser user in Model.BlogUsers) %>
           <% { %>
            <p>
            <h3>about <%= user.Username %></h3>
            <%= user.About %>
            <br />
            <br />
            </p>
           <% } %>
    <% } %>

</asp:Content>
