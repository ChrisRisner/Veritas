<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="Veritas.UI.Web.Views.VeritasViewPage<Veritas.BusinessLayer.Screens.Admin.Feedbacks.FeedbacksEditScreen>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
<%: Model.blogConfig.Title %> - Feedbacks
</asp:Content>

<asp:Content ID="MetaContent" ContentPlaceHolderID="MetaContent" runat="server">
    <meta name="description" content="<%= Model.blogConfig.Title %> - Contact" />
    <meta name="Keywords" content="<%= Model.blogConfig.Title %>,Contact" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit Feedback</h2>

    <div class="divDashboard-Topic">
        <p class="pDashHeader">Edit Category Details</p>

        <p class="pFeedbackAdminItemImage">
            <img width="50" src="<%= VeritasForm.GetGravatarLink(Model.BlogFeedback.BlogFeedbackAuthor.Email) %>" alt="gravatar"/>
        </p>
        <p class="pFeedbackAdminItemText">        
            <%: Model.BlogFeedback.Body%>        
            <br />
            Left by <%= VeritasForm.GetLinkForFeedbackAuthor(Model.BlogFeedback)%> at <%= Model.BlogFeedback.CreateDate%> 
            <br />
            on <%= VeritasForm.ActionLink(Model.BlogFeedback.BlogEntry.Title, "Entries/Index/" + Model.BlogFeedback.BlogEntryId, "Admin")%>                
        </p>    



        <%= Html.ValidationSummary() %>

        <% using (Html.BeginForm("Feedbacks/Save", "Admin")) { %>
        <%= Html.Hidden("feedbackId", Model.BlogFeedback.BlogFeedbackId) %>

        <dl>
            <dt>
                <label for="Body">Body</label>
            </dt>
            <dd>
                <%=Html.TextArea("BlogFeedback.Body", Model.BlogFeedback.Body, new { rows="7", cols="70", @class="itext" })%>    
                <label class="required"></label>
                <%=Html.ValidationMessage("BlogFeedback.Body", "*")%>
            </dd>
        </dl>

        <dl>
            <dt>
                <label for="Status">Status</label>
            </dt>
            <dd>
                <%--<%=Html.TextBox("BlogFeedback.UserAgent", Model.BlogFeedback.UserAgent, new { @class = "itext" })%>    --%>
                <%=Html.DropDownList("StatusSelectList")%>
                <label class="required"></label>
                <%=Html.ValidationMessage("BlogFeedback.Status", "*")%>
            </dd>
        </dl>
      

        <dl>
            <dt>
                <label for="NotifyAuthorOnFeedback">NotifyAuthorOnFeedback</label>
            </dt>
            <dd>
                <%=Html.CheckBox("BlogFeedback.NotifyAuthorOnFeedback", Model.BlogFeedback.NotifyAuthorOnFeedback, new { @class = "icheckbox" })%>    
                <label class="required"></label>
            </dd>
        </dl>

        <dl>
            <dt>
                <label for="InReplyToFeedbackId">In Reply To Feedback Id</label>
            </dt>
            <dd>
                <%=Html.TextBox("BlogFeedback.InReplyToFeedbackId", Model.BlogFeedback.InReplyToFeedbackId, new { @class = "itext" })%>    
                <label class="required"></label>
                <%=Html.ValidationMessage("BlogFeedback.InReplyToFeedbackId", "*")%>
            </dd>
        </dl>
        
        <dl>
            <dt>
                <label for="IpAddress">IP Address</label>
            </dt>
            <dd>
                <%= Model.BlogFeedback.IpAddress %>
            </dd>
        </dl>
        <dl>
            <dt>
                <label for="Title">Title</label>
            </dt>
            <dd>
                <%= Model.BlogFeedback.Title %>
            </dd>
        </dl>
        <dl>
            <dt>
                <label for="UserAgent">UserAgent</label>
            </dt>
            <dd>
                <%= Model.BlogFeedback.UserAgent %>                
            </dd>
        </dl>

        
        <dl>
            <dt>
                <label for="CreateDate">Create Date</label>
            </dt>
            <dd>                
                <%= Model.BlogFeedback.CreateDate %>                
            </dd>
        </dl>

        <dl>
            <dt>
                <label for="LastUpdateDate">Last Update Date</label>
            </dt>
            <dd>                
                <%= Model.BlogFeedback.LastUpdateDate %>                
            </dd>
        </dl>

        <input type="submit" value="Save Category" />
        <% } %>
    </div>


</asp:Content>
