<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="Veritas.UI.Web.Views.VeritasViewPage<Veritas.BusinessLayer.Screens.Admin.FeedbackAuthors.FeedbackAuthorsIndexScreen>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: Model.blogConfig.Title %> - Feedback Authors
</asp:Content>

<asp:Content ID="MetaContent" ContentPlaceHolderID="MetaContent" runat="server">
    <meta name="description" content="<%= Model.blogConfig.Title %> - Contact" />
    <meta name="Keywords" content="<%= Model.blogConfig.Title %>,Contact" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Feedback Authors</h2>

    <table class="tableAdmin" cellpadding=0 cellspacing=0>    
    <% foreach (var item in Model.BlogFeedbackAuthors)
       { %>
        <tr>
            <td>
               <p class="pFeedbackAuthorAdminItemImage">             
                    <a href="<%= VeritasForm.Action("FeedbackAuthors/Details", new { feedbackAuthorId = item.BlogFeedbackAuthorId })%>">
                        <img width="50" src="<%= VeritasForm.GetGravatarLink(item.Email) %>" alt="gravatar"/>
                    </a>
                </p>
            </td>
            <td>
                <dl>
                    <dt><label for="Url">Url</label></dt>
                    <dd>
                        <a href="<%= item.Url %>"><%= item.Url %></a>
                    </dd>
                </dl>
                <dl>
                    <dt><label for="Name">Name</label></dt>
                    <dd>
                        <%: item.Name %>
                    </dd>
                </dl>
                <dl>
                    <dt><label for="TotalFeedbacks">Total Feedbacks</label></dt>
                    <dd>
                        <a href="<%= VeritasForm.Action("FeedbackAuthors/Details", new { feedbackAuthorId = item.BlogFeedbackAuthorId })%>">
                            <%: item.FeedbackTotal %>
                        </a>
                    </dd>
                </dl>
            </td>
            <td>
                <dl>
                    <dt><label for="Email">Email</label></dt>
                    <dd>
                        <%: item.Email %>
                    </dd>
                </dl>
                <dl>
                    <dt><label for="CreateDate">Create Date</label></dt>
                    <dd>
                        <%: item.CreateDate %>
                    </dd>
                </dl>                       
            </td>
        </tr>
    <% } %>
    </table>
</asp:Content>
