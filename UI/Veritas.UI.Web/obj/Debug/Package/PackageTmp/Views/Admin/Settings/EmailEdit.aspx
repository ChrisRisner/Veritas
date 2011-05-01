<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="Veritas.UI.Web.Views.VeritasViewPage<Veritas.BusinessLayer.Screens.Admin.Settings.SettingsEditScreen>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: Model.newBlogConfig.Title %> - Email Settings
</asp:Content>

<asp:Content ID="MetaContent" ContentPlaceHolderID="MetaContent" runat="server">
    <meta name="description" content="<%= Model.newBlogConfig.Title %> - Contact" />
    <meta name="Keywords" content="<%= Model.newBlogConfig.Title %>,Contact" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Email Settings</h2>

    <% Html.RenderPartial("~/Views/UserControls/UcAdminSettingsMenu.ascx"); %>                            

    <fieldset>
        <legend>Details</legend>
        <%= Html.ValidationSummary() %>
        <% using (Html.BeginForm("Settings/Save", "Admin", new { subSettingName = "Email" }))
           { %>
            
            <dl>
                <dt>
                    <label for="SmtpUserName">SMTP User Name</label>
                </dt>
                <dd>
                    <%=Html.TextBox("newBlogConfig.SmtpUserName", Model.newBlogConfig.SmtpUserName, new { size = 70, @class = "itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("newBlogConfig.SmtpUserName", "*")%>
                </dd>
            </dl>

            <dl>
                <dt>
                    <label for="SmtpPassword">SMTP Password</label>
                </dt>
                <dd>
                    <%=Html.TextBox("newBlogConfig.SmtpPassword", Model.newBlogConfig.SmtpPassword, new { size = 70, @class = "itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("newBlogConfig.SmtpPassword", "*")%>
                </dd>
            </dl>

            <dl>
                <dt>
                    <label for="SmtpServer">SMTP Server</label>
                </dt>
                <dd>
                    <%=Html.TextBox("newBlogConfig.SmtpServer", Model.newBlogConfig.SmtpServer, new { size = 70, @class = "itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("newBlogConfig.SmtpServer", "*")%>
                </dd>
            </dl>

            <dl>
                <dt>
                    <label for="SmtpPort">SMTP Port</label>
                </dt>
                <dd>
                    <%=Html.TextBox("newBlogConfig.SmtpPort", Model.newBlogConfig.SmtpPort, new { size = 70, @class = "itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("newBlogConfig.SmtpPort", "*")%>
                </dd>
            </dl>

            <dl>
                <dt>
                    <label for="SmtpUseSsl">SMTP Uses SSL</label>
                </dt>
                <dd>
                    <%=Html.CheckBox("newBlogConfig.SmtpUseSsl", Model.newBlogConfig.SmtpUseSsl.HasValue ?
                        Model.newBlogConfig.SmtpUseSsl.Value : false, new { @class = "icheckbox" })%>    
                    <label class="required"></label>    
                    <%=Html.ValidationMessage("newBlogConfig.SmtpUseSsl", "*")%>
                </dd>
            </dl>
            

            <input type="submit" value="Save" />
        <% } %>
    </fieldset>

</asp:Content>

