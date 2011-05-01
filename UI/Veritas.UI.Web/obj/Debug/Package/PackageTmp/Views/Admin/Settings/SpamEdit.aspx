<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="Veritas.UI.Web.Views.VeritasViewPage<Veritas.BusinessLayer.Screens.Admin.Settings.SettingsEditScreen>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: Model.newBlogConfig.Title %> - Spam Settings
</asp:Content>

<asp:Content ID="MetaContent" ContentPlaceHolderID="MetaContent" runat="server">
    <meta name="description" content="<%= Model.newBlogConfig.Title %> - Contact" />
    <meta name="Keywords" content="<%= Model.newBlogConfig.Title %>,Contact" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Spam Settings</h2>

    <% Html.RenderPartial("~/Views/UserControls/UcAdminSettingsMenu.ascx"); %>                            

    <fieldset>
        <legend>Details</legend>
        <%= Html.ValidationSummary() %>
        <% using (Html.BeginForm("Settings/Save", "Admin", new { subSettingName = "Spam" }))
           { %>
            
            <dl>
                <dt>
                    <label for="AkismetApiKey">Akismet API Key</label>
                </dt>
                <dd>
                    <%=Html.TextBox("newBlogConfig.AkismetApiKey", Model.newBlogConfig.AkismetApiKey, new { size = 70, @class = "itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("newBlogConfig.AkismetApiKey", "*")%>
                </dd>
            </dl>

            <input type="submit" value="Save" />
        <% } %>
    </fieldset>

</asp:Content>

