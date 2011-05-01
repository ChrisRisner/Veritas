<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="Veritas.UI.Web.Views.VeritasViewPage<Veritas.BusinessLayer.Screens.Admin.FeedbackAuthors.FeedbackAuthorsDetailsScreen>" %>
<%@ Import Namespace="Veritas.DataLayer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: Model.blogConfig.Title %> - Feedback Author Details
</asp:Content>

<asp:Content ID="MetaContent" ContentPlaceHolderID="MetaContent" runat="server">
    <meta name="description" content="<%= Model.blogConfig.Title %> - Contact" />
    <meta name="Keywords" content="<%= Model.blogConfig.Title %>,Contact" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Feedback Author Details</h2>

    <table class="tableAdmin" cellpadding=0 cellspacing=0>    
        <tr>
            <td>
               <p class="pFeedbackAuthorAdminItemImage">
                    <img width="50" src="<%= VeritasForm.GetGravatarLink(Model.BlogFeedbackAuthor.Email) %>" alt="gravatar"/>
                </p>
            </td>
            <td>
                <dl>
                    <dt><label for="Url">Url</label></dt>
                    <dd>
                        <a href="<%= Model.BlogFeedbackAuthor.Url %>"><%= Model.BlogFeedbackAuthor.Url %></a>
                    </dd>
                </dl>
                <dl>
                    <dt><label for="Name">Name</label></dt>
                    <dd>
                        <%: Model.BlogFeedbackAuthor.Name %>
                    </dd>
                </dl>
                <dl>
                    <dt><label for="TotalFeedbacks">Total Feedbacks</label></dt>
                    <dd>
                        <%: Model.BlogFeedbackAuthor.FeedbackTotal %>
                    </dd>
                </dl>
            </td>
            <td>
                <dl>
                    <dt><label for="Email">Email</label></dt>
                    <dd>
                        <%: Model.BlogFeedbackAuthor.Email %>
                    </dd>
                </dl>
                <dl>
                    <dt><label for="CreateDate">Create Date</label></dt>
                    <dd>
                        <%: Model.BlogFeedbackAuthor.CreateDate %>
                    </dd>
                </dl>                       
            </td>
        </tr>
    </table>



    <div class="divDashboard-Topic">
        <p class="pDashHeader">Feedbacks</p>
            <% foreach (var item in Model.BlogFeedbackAuthor.BlogFeedbacks)
               { %>
               <%--<div class="FeedbackAdminActions">
                <p  class="FeedbackAdminActions">
                    <ul>
                        <li><%= VeritasForm.ActionLink("Edit", "Feedbacks/Edit", new { feedbackId = item.BlogFeedbackId, type = Model.TypeQueryString })%></li>
                        <% if (item.Status == (int) FeedbackStatus.Approved) { %>
                            <li><%= VeritasForm.ActionLink("Deny", "Feedbacks/Deny", new { feedbackId = item.BlogFeedbackId, type = Model.TypeQueryString })%></li>
                        <% }
                           else if (item.Status == (int)FeedbackStatus.PendingApproval)
                           {%>
                           <li><%= VeritasForm.ActionLink("Approve", "Feedbacks/Approve/", new { feedbackId = item.BlogFeedbackId, type = Model.TypeQueryString } )%></li>
                           <li><%= VeritasForm.ActionLink("Deny", "Feedbacks/Deny/", new { feedbackId = item.BlogFeedbackId, type = Model.TypeQueryString })%></li>
                        <% }
                           else if (item.Status == (int)FeedbackStatus.Denied)
                           { %>
                           <li><%= VeritasForm.ActionLink("Approve", "Feedbacks/Approve/", new { feedbackId = item.BlogFeedbackId, type = Model.TypeQueryString })%></li>
                        <% } %>
                    </ul>
                </p>
                </div>--%>
                <p class="pFeedbackAdminItemImage">
                    <img width="50" src="<%= VeritasForm.GetGravatarLink(item.BlogFeedbackAuthor.Email) %>" alt="gravatar"/>
                </p>
                <p class="pFeedbackAdminItemText">        
                    <%: item.Body %>        
                    <br />
                    Left by <%= VeritasForm.GetLinkForFeedbackAuthor(item)%> at <%= item.CreateDate %> 
                    <br />
                    on <%= VeritasForm.ActionLink(item.BlogEntry.Title, "Entries/Index/" + item.BlogEntryId, "Admin")%>                
                </p>    
            <% } %>        
    </div>
</asp:Content>
