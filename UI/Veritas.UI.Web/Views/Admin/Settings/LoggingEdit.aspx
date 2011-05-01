<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="Veritas.UI.Web.Views.VeritasViewPage<Veritas.BusinessLayer.Screens.Admin.Settings.SettingsEditScreen>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: Model.newBlogConfig.Title %> - Logging Settings
</asp:Content>

<asp:Content ID="MetaContent" ContentPlaceHolderID="MetaContent" runat="server">
    <meta name="description" content="<%= Model.newBlogConfig.Title %> - Contact" />
    <meta name="Keywords" content="<%= Model.newBlogConfig.Title %>,Contact" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Logging Settings</h2>

    <% Html.RenderPartial("~/Views/UserControls/UcAdminSettingsMenu.ascx"); %>                            

    <fieldset>
        <legend>Details</legend>
        <%= Html.ValidationSummary() %>
        <% using (Html.BeginForm("Settings/Save", "Admin", new { subSettingName = "Logging" }))
           { %>

            
            <dl>
                <dt>
                    <label for="LogEmailAddress">Log Email Address</label>
                </dt>
                <dd>
                    <%=Html.TextBox("newBlogConfig.LogEmailAddress", Model.newBlogConfig.LogEmailAddress, new { size=70, @class="itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("newBlogConfig.LogEmailAddress", "*")%>
                </dd>
            </dl>

            <dl>
                <dt>
                    <label for="LogFilePath">Log File Path</label>
                </dt>
                <dd>
                    <%=Html.TextBox("newBlogConfig.LogFilePath", Model.newBlogConfig.LogFilePath, new { size=70, @class="itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("newBlogConfig.LogFilePath", "*")%>
                </dd>
            </dl>

            <dl>
                <dt>
                    <label for="LogToDb">Log to Database</label>
                </dt>
                <dd>
                    <%=Html.CheckBox("newBlogConfig.LogToDb", Model.newBlogConfig.LogToDb, new {  @class="icheckbox" })%>    
                    <label class="required"></label>    
                    <%=Html.ValidationMessage("newBlogConfig.LogToDb", "*")%>
                </dd>
            </dl>

            <dl>
                <dt>
                    <label for="LogToEmail">Log to Email</label>
                </dt>
                <dd>
                    <%=Html.CheckBox("newBlogConfig.LogToEmail", Model.newBlogConfig.LogToEmail, new {  @class="icheckbox" })%>    
                    <label class="required"></label>    
                    <%=Html.ValidationMessage("newBlogConfig.LogToEmail", "*")%>
                </dd>
            </dl>

            <dl>
                <dt>
                    <label for="LogToFile">Log to File</label>
                </dt>
                <dd>
                    <%=Html.CheckBox("newBlogConfig.LogToFile", Model.newBlogConfig.LogToFile, new {  @class="icheckbox" })%>    
                    <label class="required"></label>    
                    <%=Html.ValidationMessage("newBlogConfig.LogToFile", "*")%>
                </dd>
            </dl>

            <input type="submit" value="Save" />
        <% } %>
    </fieldset>

</asp:Content>

