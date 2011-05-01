<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="Veritas.UI.Web.Views.VeritasViewPage<Veritas.BusinessLayer.Screens.Admin.Settings.SettingsEditScreen>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: Model.newBlogConfig.Title %> - Feedback Settings
</asp:Content>

<asp:Content ID="MetaContent" ContentPlaceHolderID="MetaContent" runat="server">
    <meta name="description" content="<%= Model.newBlogConfig.Title %> - Contact" />
    <meta name="Keywords" content="<%= Model.newBlogConfig.Title %>,Contact" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Feedback Settings</h2>

    <% Html.RenderPartial("~/Views/UserControls/UcAdminSettingsMenu.ascx"); %>                            

    <fieldset>
        <legend>Details</legend>
        <%= Html.ValidationSummary() %>
        <% using (Html.BeginForm("Settings/Save", "Admin", new { subSettingName = "Feedback" }))
           { %>
            
            <dl>
                <dt>
                    <label for="AllowComments">Allow Comments</label>
                </dt>
                <dd>
                    <%=Html.CheckBox("newBlogConfig.AllowComments", Model.newBlogConfig.AllowComments, new {  @class="icheckbox" })%>    
                    <label class="required"></label>    
                    <%=Html.ValidationMessage("newBlogConfig.AllowComments", "*")%>
                </dd>
            </dl>

            <dl>
                <dt>
                    <label for="EnableFeedbackAuthorNotifications">Enable Feedback Author Notifications</label>
                </dt>
                <dd>
                    <%=Html.CheckBox("newBlogConfig.EnableFeedbackAuthorNotifications", Model.newBlogConfig.EnableFeedbackAuthorNotifications, new { @class = "icheckbox" })%>    
                    <label class="required"></label>    
                    <%=Html.ValidationMessage("newBlogConfig.EnableFeedbackAuthorNotifications", "*")%>
                </dd>
            </dl>

            <dl>
                <dt>
                    <label for="EnableFeedbackRssFeed">Enable Feedback RSS Feed</label>
                </dt>
                <dd>
                    <%=Html.CheckBox("newBlogConfig.EnableFeedbackRssFeed", Model.newBlogConfig.EnableFeedbackRssFeed, new { @class = "icheckbox" })%>    
                    <label class="required"></label>    
                    <%=Html.ValidationMessage("newBlogConfig.EnableFeedbackRssFeed", "*")%>
                </dd>
            </dl>

            <dl>
                <dt>
                    <label for="FeedbackRequiresApproval">Feedback Requires Approval</label>
                </dt>
                <dd>
                    <%=Html.CheckBox("newBlogConfig.FeedbackRequiresApproval", Model.newBlogConfig.FeedbackRequiresApproval, new { @class = "icheckbox" })%>    
                    <label class="required"></label>    
                    <%=Html.ValidationMessage("newBlogConfig.FeedbackRequiresApproval", "*")%>
                </dd>
            </dl>

            <dl>
                <dt>
                    <label for="NotifyAdminsForFeedback">Notify Admins for feedback</label>
                </dt>
                <dd>
                    <%=Html.CheckBox("newBlogConfig.NotifyAdminsForFeedback", Model.newBlogConfig.NotifyAdminsForFeedback, new { @class = "icheckbox" })%>    
                    <label class="required"></label>    
                    <%=Html.ValidationMessage("newBlogConfig.NotifyAdminsForFeedback", "*")%>
                </dd>
            </dl>

            <dl>
                <dt>
                    <label for="DaysUntilCommentsClose">Days until Comments Close</label>
                </dt>
                <dd>
                    <%=Html.TextBox("newBlogConfig.DaysUntilCommentsClose", Model.newBlogConfig.DaysUntilCommentsClose, new { size = 70, @class = "itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("newBlogConfig.DaysUntilCommentsClose", "*")%>
                </dd>
            </dl>

            <dl>
                <dt>
                    <label for="FeedbackCount">Feedback Count</label>
                </dt>
                <dd>
                    <%=Html.TextBox("newBlogConfig.FeedbackCount", Model.newBlogConfig.FeedbackCount, new { size = 70, @class = "itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("newBlogConfig.FeedbackCount", "*")%>
                </dd>
            </dl>

            <dl>
                <dt>
                    <label for="UseDefaultComments">Use Default Comments</label>
                </dt>
                <dd>
                    <%=Html.CheckBox("newBlogConfig.BlogCommentInfo.UseDefaultComments", Model.newBlogConfig.BlogCommentInfo.UseDefaultComments, new { @class = "icheckbox" })%>    
                    <label class="required"></label>    
                    <%=Html.ValidationMessage("newBlogConfig.BlogCommentInfo.UseDefaultComments", "*")%>
                </dd>
            </dl>
            
            <dl>
                <dt>
                    <label for="UseDisqusComments">Use Disqus Comments</label>
                </dt>
                <dd>
                    <%=Html.CheckBox("newBlogConfig.BlogCommentInfo.UseDisqusComments", Model.newBlogConfig.BlogCommentInfo.UseDisqusComments, new { @class = "icheckbox" })%>    
                    <label class="required"></label>    
                    <%=Html.ValidationMessage("newBlogConfig.BlogCommentInfo.UseDisqusComments", "*")%>
                </dd>
            </dl>
        
            <dl>
                <dt>
                    <label for="DisqusAccountName">Disqus Account Name</label>
                </dt>
                <dd>
                    <%=Html.TextBox("newBlogConfig.BlogCommentInfo.DisqusAccountName", Model.newBlogConfig.BlogCommentInfo.DisqusAccountName, new { size = 70, @class = "itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("newBlogConfig.BlogCommentInfo.DisqusAccountName", "*")%>
                </dd>
            </dl>    

            <dl>
                <dt>
                    <label for="DisqusCommentScript">Disqus Comment Script</label>
                </dt>
                <dd>
                    <%=Html.TextBox("newBlogConfig.BlogCommentInfo.DisqusCommentScript", Model.newBlogConfig.BlogCommentInfo.DisqusCommentScript, new { size = 70, @class = "itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("newBlogConfig.BlogCommentInfo.DisqusCommentScript", "*")%>
                </dd>
            </dl>

            <input type="submit" value="Save" />
        <% } %>
    </fieldset>

</asp:Content>

