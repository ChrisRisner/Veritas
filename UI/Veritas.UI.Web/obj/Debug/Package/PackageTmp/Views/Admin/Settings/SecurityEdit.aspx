<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="Veritas.UI.Web.Views.VeritasViewPage<Veritas.BusinessLayer.Screens.Admin.Settings.SettingsEditScreen>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: Model.newBlogConfig.Title %> - Security Settings
</asp:Content>

<asp:Content ID="MetaContent" ContentPlaceHolderID="MetaContent" runat="server">
    <meta name="description" content="<%= Model.newBlogConfig.Title %> - Contact" />
    <meta name="Keywords" content="<%= Model.newBlogConfig.Title %>,Contact" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Security Settings</h2>

    <% Html.RenderPartial("~/Views/UserControls/UcAdminSettingsMenu.ascx"); %>                            
    
    <fieldset>
        <legend>Details</legend>
        <%= Html.ValidationSummary() %>
        <% using (Html.BeginForm("Settings/Save", "Admin", new { subSettingName = "Security" }))
           { %>

            <dl>
                <dt>
                    <label for="SecurityQuestionOne">Question One</label>
                </dt>
                <dd>
                    <%=Html.TextBox("newBlogConfig.SecurityQuestionOne", Model.newBlogConfig.SecurityQuestionOne, new { size = 70, @class = "itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("newBlogConfig.SecurityQuestionOne", "*")%>
                </dd>
            </dl>

            <dl>
                <dt>
                    <label for="SecurityQuestionTwo">Question Two</label>
                </dt>
                <dd>
                    <%=Html.TextBox("newBlogConfig.SecurityQuestionTwo", Model.newBlogConfig.SecurityQuestionTwo, new { size = 70, @class = "itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("newBlogConfig.SecurityQuestionTwo", "*")%>
                </dd>
            </dl>

            <dl>
                <dt>
                    <label for="SecurityQuestionThree">Question Three</label>
                </dt>
                <dd>
                    <%=Html.TextBox("newBlogConfig.SecurityQuestionThree", Model.newBlogConfig.SecurityQuestionThree, new { size = 70, @class = "itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("newBlogConfig.SecurityQuestionThree", "*")%>
                </dd>
            </dl>











            <dl>
                <dt>
                    <label for="SecurityQuestionAnswerOne">Answer One</label>
                </dt>
                <dd>
                    <%=Html.TextBox("newBlogConfig.SecurityQuestionAnswerOne", Model.newBlogConfig.SecurityQuestionAnswerOne, new { size = 70, @class = "itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("newBlogConfig.SecurityQuestionAnswerOne", "*")%>
                </dd>
            </dl>

            <dl>
                <dt>
                    <label for="SecurityQuestionAnswerTwo">Answer Two</label>
                </dt>
                <dd>
                    <%=Html.TextBox("newBlogConfig.SecurityQuestionAnswerTwo", Model.newBlogConfig.SecurityQuestionAnswerTwo, new { size = 70, @class = "itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("newBlogConfig.SecurityQuestionAnswerTwo", "*")%>
                </dd>
            </dl>

            <dl>
                <dt>
                    <label for="SecurityQuestionAnswerThree">Answer Three</label>
                </dt>
                <dd>
                    <%=Html.TextBox("newBlogConfig.SecurityQuestionAnswerThree", Model.newBlogConfig.SecurityQuestionAnswerThree, new { size = 70, @class = "itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("newBlogConfig.SecurityQuestionAnswerThree", "*")%>
                </dd>
            </dl>

            <input type="submit" value="Save" />
        <% } %>
    </fieldset>

</asp:Content>

