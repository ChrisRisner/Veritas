<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="Veritas.UI.Web.Views.VeritasViewPage<Veritas.BusinessLayer.Screens.Home.ContactScreen>" %>
<%@ Import Namespace="Veritas.BusinessLayer" %>
<%@ Import Namespace="Veritas.DataLayer.Models" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    <%= Model.blogConfig.Title %> - Contact
</asp:Content>

<asp:Content ID="MetaContent" ContentPlaceHolderID="MetaContent" runat="server">
    <meta name="description" content="<%= Model.blogConfig.Title %> - Contact" />
    <meta name="Keywords" content="<%= Model.blogConfig.Title %>,Contact" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2><%= Html.Encode(Model.Message) %></h2>
<h2><%= Html.Encode(TempData["Info"]) %></h2>
<%= Html.ValidationSummary() %>

<% if (!Model.MessageSent) { %>
<% using (Html.BeginForm())
   { %>

    <table>
        <tr>
            <td>Who do you want to send this to?</td>
            <td>
                <%=Html.DropDownList("AuthorSelectList")%>
                <%=Html.ValidationMessage("AuthorSelectList", "*")%>
            </td>
        </tr>
        <tr>
            <td>Name:</td>
            <td>
                <%=Html.TextBox("Name") %>
                <%=Html.ValidationMessage("Name", "*") %>
            </td>   
        </tr>
        <tr>
            <td>Email Address:</td>
            <td>
                <%=Html.TextBox("Email") %>
                <%=Html.ValidationMessage("Email", "*") %>
            </td>
        </tr>
        <tr>
            <td>Web site:</td>
            <td>
                <%=Html.TextBox("WebSite") %>
            </td>
        </tr>
        <tr>
            <td>Message:</td>
            <td>
                <%=Html.TextArea("EmailMessage", new { cols = 50, rows=6 })%>               
                <%=Html.ValidationMessage("EmailMessage", "*") %>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <input type="submit" value="Send" />
            </td>
        </tr>
    </table>

<% } %>
<% } %>
</asp:Content>
