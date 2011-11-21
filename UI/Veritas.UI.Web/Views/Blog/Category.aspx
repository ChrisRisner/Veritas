<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="Veritas.UI.Web.Views.VeritasViewPage<Veritas.BusinessLayer.Screens.Blog.CategoryScreen>" %>
<%@ Import Namespace="Veritas.BusinessLayer" %>
<%@ Import Namespace="Veritas.DataLayer.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= Model.blogConfig.Title %>
</asp:Content>

<asp:Content ID="MetaContent" ContentPlaceHolderID="MetaContent" runat="server">
    <meta name="description" content="<%= Model.blogConfig.Title %>" />
    <meta name="Keywords" content="<%= Model.blogConfig.Title %>,Contact" />
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">

    <% foreach (var entryScreen in Model.BlogEntryScreens)
       { %>
       <% if (entryScreen.BlogEntry.IsMostRecentEntry)
          {%>
            <%--Handles specifying the most recent entry as a web slice using the Microsoft standard http://en.wikipedia.org/wiki/Web_Slice--%>
            <div class='hslice entry-content' id='blogInfo' style='width: 100%; background-color: #ffffff;'>
                <span class='entry-title' style='display: none;'>
                  <%= Model.blogConfig.Title %>
                </span>
       <% } %>
       <% Html.RenderPartial("~/Views/UserControls/UcBlogEntry.ascx", entryScreen); %>
       <% if (entryScreen.BlogEntry.IsMostRecentEntry)
          { %>
          </div>
       <% } %>
    <% } %>

    <%--links to older and newer entries --%>
    <div>
        <br /><br /><br /><br />
        <div class="OlderEntries">
            <%= Model.LinkToOlderEntries(Model.CurrentStartAt) %>
        </div>
        <div class="NewerEntries">
            <%= Model.LinkToNewerEntries(Model.CurrentStartAt)%>
        </div>
    </div>    

</asp:Content>

