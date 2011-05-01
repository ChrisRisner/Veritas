<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="Veritas.UI.Web.Views.VeritasViewPage<Veritas.BusinessLayer.Screens.Admin.Settings.SettingsEditScreen>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: Model.newBlogConfig.Title %> - Marketing Settings
</asp:Content>

<asp:Content ID="MetaContent" ContentPlaceHolderID="MetaContent" runat="server">
    <meta name="description" content="<%= Model.newBlogConfig.Title %> - Contact" />
    <meta name="Keywords" content="<%= Model.newBlogConfig.Title %>,Contact" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Marketing Settings</h2>

    <% Html.RenderPartial("~/Views/UserControls/UcAdminSettingsMenu.ascx"); %>                            

    <fieldset>
        <legend>Details</legend>
        <%= Html.ValidationSummary() %>
        <% using (Html.BeginForm("Settings/Save", "Admin", new { subSettingName = "Marketing" }))
           { %>

            <dl>
                <dt>
                    <label for="ShowEntryAds">Show Ads in Entries</label>
                </dt>
                <dd>
                    <%=Html.CheckBox("newBlogConfig.BlogMarketingInfo.ShowEntryAds", Model.newBlogConfig.BlogMarketingInfo.ShowEntryAds, new { @class = "icheckbox" })%>    
                    <label class="required"></label>    
                    <%=Html.ValidationMessage("newBlogConfig.BlogMarketingInfo.ShowEntryAds", "*")%>
                </dd>
            </dl>

            <dl>
                <dt>
                    <label for="ShowSideBarAds">Show Ads in Side Bar</label>
                </dt>
                <dd>
                    <%=Html.CheckBox("newBlogConfig.BlogMarketingInfo.ShowSideBarAds", Model.newBlogConfig.BlogMarketingInfo.ShowSideBarAds, new { @class = "icheckbox" })%>    
                    <label class="required"></label>    
                    <%=Html.ValidationMessage("newBlogConfig.BlogMarketingInfo.ShowSideBarAds", "*")%>
                </dd>
            </dl>

            <dl>
                <dt>
                    <label for="AdScriptEntry">Ad Script Entry</label>
                </dt>
                <dd>
                    <%=Html.TextArea("newBlogConfig.BlogMarketingInfo.AdScriptEntry", Model.newBlogConfig.BlogMarketingInfo.AdScriptEntry, new { rows = "10", cols = "70", @class = "itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("newBlogConfig.BlogMarketingInfo.AdScriptEntry", "*")%>
                </dd>
            </dl>

            <dl>
                <dt>
                    <label for="AdScriptSideBar">Ad Script Side Bar</label>
                </dt>
                <dd>
                    <%=Html.TextArea("newBlogConfig.BlogMarketingInfo.AdScriptSideBar", Model.newBlogConfig.BlogMarketingInfo.AdScriptSideBar, new { rows = "10", cols = "70", @class = "itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("newBlogConfig.BlogMarketingInfo.AdScriptSideBar", "*")%>
                </dd>
            </dl>

            <input type="submit" value="Save" />
        <% } %>

    </fieldset>

</asp:Content>

