<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="Veritas.UI.Web.Views.VeritasViewPage<Veritas.BusinessLayer.Screens.Admin.IndexScreen>" %>
<%@ Import Namespace="Veritas.BusinessLayer" %>
<%@ Import Namespace="Veritas.DataLayer.Models" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    <%= Model.blogConfig.Title %>
</asp:Content>

<asp:Content ID="MetaContent" ContentPlaceHolderID="MetaContent" runat="server">
    <meta name="description" content="<%= Model.blogConfig.Title %> - Contact" />
    <meta name="Keywords" content="<%= Model.blogConfig.Title %>,Contact" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Dashboard</h2>

    <%: Model.Message %>

    <div class="divDashboard-Topic">
        <p class="pDashHeader">Stats</p>
        <div class="divDashColumn">
            <p class="pDashColumnHeader">Content</p>
            <p><%= VeritasForm.ActionLink(Model.EntryCount + " Posts", "Entries/Index", "Admin")%></p>
            <p><%= VeritasForm.ActionLink(Model.PageCount + " Pages", "Pages/Index", "Admin")%></p>
            <p><%= VeritasForm.ActionLink(Model.CategoryCount + " Categories", "Categories/Index", "Admin")%></p>
        </div>
        <div class="divDashColumn">
            <p class="pDashColumnHeader">Discussion</p>
            <p><%= VeritasForm.ActionLink(Model.FeedbackTotalCount + " Feedbacks", "Feedbacks/Index", "Admin")%></p>            
            <p><%= VeritasForm.ActionLink(Model.FeedbackNotYetApproved + " Pending Approval", "Feedbacks/Index", new { type="pending"} ) %></p>
            <p><%= VeritasForm.ActionLink(Model.FeedbackDeniedCount + " Denied", "Feedbacks/Index", new { type="denied"} ) %></p>
        </div>              
    </div>

    <div class="divDashboard-Topic">
        <p class="pDashHeader">Recent Comments</p>
        <% foreach (BlogFeedback item in Model.LastThreeFeedbacks) { %>
            <p class="pFeedbackItemImage">
                <img width="50" src="<%= VeritasForm.GetGravatarLink(item.BlogFeedbackAuthor.Email) %>" alt="gravatar"/>
            </p>
            <p class="pFeedbackItemText">        
                <%: item.Body %>        
                <br />
                Left by <%= VeritasForm.GetLinkForFeedbackAuthor(item)%> at <%= item.CreateDate %> 
                <br />
                on <%= VeritasForm.ActionLink(item.BlogEntry.Title, "Entries/Index/" + item.BlogEntryId, "Admin")%>                
            </p>    
        <% } %>
    </div>
    <br />
    <div class="divDashboard-Topic">
        <p class="pDashHeader">Misc</p>
        <ul>
            <li><%= VeritasForm.ActionLink("Reset Cache", "ResetCache", "Admin") %></li>
        </ul>
</asp:Content>


