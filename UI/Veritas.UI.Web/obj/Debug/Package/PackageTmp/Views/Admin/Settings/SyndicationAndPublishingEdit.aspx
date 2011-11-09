<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="Veritas.UI.Web.Views.VeritasViewPage<Veritas.BusinessLayer.Screens.Admin.Settings.SettingsEditScreen>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: Model.newBlogConfig.Title %> - Syndication and Publishing Settings
</asp:Content>

<asp:Content ID="MetaContent" ContentPlaceHolderID="MetaContent" runat="server">
    <meta name="description" content="<%= Model.newBlogConfig.Title %> - Contact" />
    <meta name="Keywords" content="<%= Model.newBlogConfig.Title %>,Contact" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Syndication and Publishing Settings</h2>

    <% Html.RenderPartial("~/Views/UserControls/UcAdminSettingsMenu.ascx"); %>                            
    
    <fieldset>
        <legend>Details</legend>
        <%= Html.ValidationSummary() %>
        <% using (Html.BeginForm("Settings/Save", "Admin", new { subSettingName = "SyndicationAndPublishing" }))
           { %>

            <dl>
                <dt>
                    <label for="FeedburnerName">Feedburner Name</label>
                </dt>
                <dd>
                    <%=Html.TextBox("newBlogConfig.FeedburnerName", Model.newBlogConfig.FeedburnerName, new { size = 70, @class = "itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("newBlogConfig.FeedburnerName", "*")%>
                </dd>
            </dl>

            <dl>
                <dt>
                    <label for="FacebookUrl">Facebook Url</label>
                </dt>
                <dd>
                    <%=Html.TextBox("newBlogConfig.Facebookurl", Model.newBlogConfig.FacebookUrl, new { size = 70, @class = "itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("newBlogConfig.FacebookUrl", "*")%>
                </dd>
            </dl>

            <dl>
                <dt>
                    <label for="GooglePlusUrl">Google Plus Url</label>
                </dt>
                <dd>
                    <%=Html.TextBox("newBlogConfig.GooglePlusurl", Model.newBlogConfig.GooglePlusUrl, new { size = 70, @class = "itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("newBlogConfig.GooglePlusUrl", "*")%>
                </dd>
            </dl>

            <dl>
                <dt>
                    <label for="RssUrl">RssUrl</label>
                </dt>
                <dd>
                    <%=Html.TextBox("newBlogConfig.RssUrl", Model.newBlogConfig.RssUrl, new { size = 70, @class = "itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("newBlogConfig.RssUrl", "*")%>
                </dd>
            </dl>

            <dl>
                <dt>
                    <label for="TwitterUrl">TwitterUrl</label>
                </dt>
                <dd>
                    <%=Html.TextBox("newBlogConfig.TwitterUrl", Model.newBlogConfig.TwitterUrl, new { size = 70, @class = "itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("newBlogConfig.TwitterUrl", "*")%>
                </dd>
            </dl>

            <dl>
                <dt>
                    <label for="RssShowLimitedEntryInFeed">Show Limited Entry in Feed</label>
                </dt>
                <dd>
                    <%=Html.CheckBox("newBlogConfig.RssShowLimitedEntryInFeed", Model.newBlogConfig.RssShowLimitedEntryInFeed, new { @class = "icheckbox" })%>    
                    <label class="required"></label>    
                    <%=Html.ValidationMessage("newBlogConfig.RssShowLimitedEntryInFeed", "*")%>
                </dd>
            </dl>

            <input type="submit" value="Save" />
        <% } %>
    </fieldset>

</asp:Content>

