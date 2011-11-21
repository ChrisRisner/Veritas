<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="Veritas.UI.Web.Views.VeritasViewPage<Veritas.BusinessLayer.Screens.Blog.ArchiveScreen>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%= Model.BlogEntryScreen.BlogEntry.Title %>
</asp:Content>

<asp:Content ID="MetaContent" ContentPlaceHolderID="MetaContent" runat="server">
    <meta name="description" content="<%= Model.BlogEntryScreen.BlogEntry.Short %>" />
    <meta name="Keywords" content="<%= Model.BlogEntryScreen.BlogEntry.Keywords %>,Contact" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            //Show the feedback on this page by default
            $('.divFeedback').slideToggle(true);
        });
    </script>
    <% Html.RenderPartial("~/Views/UserControls/UcBlogEntry.ascx", Model.BlogEntryScreen); %>

</asp:Content>


