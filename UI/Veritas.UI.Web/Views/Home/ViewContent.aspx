<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="Veritas.UI.Web.Views.VeritasViewPage<Veritas.BusinessLayer.Screens.Home.ViewContentScreen>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= Model.blogConfig.Title + " " + Model.BlogPage.PageTitle %>
</asp:Content>

<asp:Content ID="MetaContent" ContentPlaceHolderID="MetaContent" runat="server">
    <meta name="description" content="<%= Model.blogConfig.Title %> - <%= Model.BlogPage.PageTitle %>"/>
    <meta name="Keywords" content="<%= Model.blogConfig.Title %>,Contact" />
    <% if (Model.blogConfig.UseTwitterCards) { %>
        <%= VeritasForm.GetDefaultTwitterMetaInfo() %>
    <% } %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <p>
        <%= Model.BlogPage.PageContent %>
    </p>

</asp:Content>
