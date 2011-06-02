<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="Veritas.UI.Web.Views.VeritasViewPage<Veritas.BusinessLayer.Screens.Admin.Settings.SettingsEditScreen>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: Model.newBlogConfig.Title %> - General Settings
</asp:Content>

<asp:Content ID="MetaContent" ContentPlaceHolderID="MetaContent" runat="server">
    <meta name="description" content="<%= Model.newBlogConfig.Title %> - Contact" />
    <meta name="Keywords" content="<%= Model.newBlogConfig.Title %>,Contact" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>General Settings</h2>

    <% Html.RenderPartial("~/Views/UserControls/UcAdminSettingsMenu.ascx"); %>                            

    <fieldset>
        <legend>Details</legend>
        <%= Html.ValidationSummary() %>
        <% using (Html.BeginForm("Settings/Save", "Admin", new { subSettingName = "General" }))
           { %>
            <dl>
                <dt>
                    <label for="BlogAbout">Blog About</label>
                </dt>
                <dd>
                    <%=Html.TextArea("newBlogConfig.BlogAbout", Model.newBlogConfig.BlogAbout, new { rows="10",cols="70", @class = "itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("newBlogConfig.BlogAbout", "*")%>
                </dd>
            </dl>
            <dl>
                <dt>
                    <label for="BlogHeaderImage">Blog Header Image Path</label>
                </dt>
                <dd>
                    <%=Html.TextBox("newBlogConfig.BlogHeaderImage", Model.newBlogConfig.BlogHeaderImage, new { size=70, @class="itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("newBlogConfig.BlogHeaderImage", "*")%>
                </dd>
            </dl>
            <dl>
                <dt>
                    <label for="BlogHeaderIsImage">Blog Header Is Image</label>
                </dt>
                <dd>
                    <%=Html.CheckBox("newBlogConfig.BlogHeaderIsImage", Model.newBlogConfig.BlogHeaderIsImage, new {  @class="icheckbox" })%>    
                    <label class="required"></label>    
                    <%=Html.ValidationMessage("newBlogConfig.BlogHeaderIsImage", "*")%>
                </dd>
            </dl>
            <dl>
                <dt>
                    <label for="IsActive">Is Active</label>
                </dt>
                <dd>
                    <%=Html.CheckBox("newBlogConfig.IsActive", Model.newBlogConfig.IsActive, new {  @class="icheckbox" })%>    
                    <label class="required"></label>    
                    <%=Html.ValidationMessage("newBlogConfig.IsActive", "*")%>
                </dd>
            </dl>
            <dl>
                <dt>
                    <label for="ShowAuthorsAbout">Show Authors About</label>
                </dt>
                <dd>
                    <%=Html.CheckBox("newBlogConfig.ShowAuthorsAbout", Model.newBlogConfig.ShowAuthorsAbout, new {  @class="icheckbox" })%>    
                    <label class="required"></label>    
                    <%=Html.ValidationMessage("newBlogConfig.ShowAuthorsAbout", "*")%>
                </dd>
            </dl>
            <dl>
                <dt>
                    <label for="ShowBlogAbout">Show Blog About</label>
                </dt>
                <dd>
                    <%=Html.CheckBox("newBlogConfig.ShowBlogAbout", Model.newBlogConfig.ShowBlogAbout, new {  @class="icheckbox" })%>    
                    <label class="required"></label>    
                    <%=Html.ValidationMessage("newBlogConfig.ShowBlogAbout", "*")%>
                </dd>
            </dl>
            <dl>
                <dt>
                    <label for="ShowGooglePlusOne">Show Google Plus One</label>
                </dt>
                <dd>
                    <%=Html.CheckBox("newBlogConfig.ShowGooglePlusOne", Model.newBlogConfig.ShowGooglePlusOne, new {  @class="icheckbox" })%>    
                    <label class="required"></label>    
                    <%=Html.ValidationMessage("newBlogConfig.ShowGooglePlusOne", "*")%>
                </dd>
            </dl>
            <dl>
                <dt>
                    <label for="ShowGravatars">Show Gravatars</label>
                </dt>
                <dd>
                    <%=Html.CheckBox("newBlogConfig.ShowGravatars", Model.newBlogConfig.ShowGravatars, new {  @class="icheckbox" })%>    
                    <label class="required"></label>    
                    <%=Html.ValidationMessage("newBlogConfig.ShowGravatars", "*")%>
                </dd>
            </dl>
            <dl>
                <dt>
                    <label for="CopyrightText">Copyright Text</label>
                </dt>
                <dd>
                    <%=Html.TextBox("newBlogConfig.CopyrightText", Model.newBlogConfig.CopyrightText, new { size=70, @class="itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("newBlogConfig.CopyrightText", "*")%>
                </dd>
            </dl>
            <dl>
                <dt>
                    <label for="FacebookUrl">Facebook Url</label>
                </dt>
                <dd>
                    <%=Html.TextBox("newBlogConfig.FacebookUrl", Model.newBlogConfig.FacebookUrl, new { size=70, @class="itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("newBlogConfig.FacebookUrl", "*")%>
                </dd>
            </dl>
            <dl>
                <dt>
                    <label for="GoogleApiKey">GoogleApi Key</label>
                </dt>
                <dd>
                    <%=Html.TextBox("newBlogConfig.GoogleApiKey", Model.newBlogConfig.GoogleApiKey, new { size=70, @class="itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("newBlogConfig.GoogleApiKey", "*")%>
                </dd>
            </dl>
            <dl>
                <dt>
                    <label for="HeaderScript">Header Script</label>
                </dt>
                <dd>
                    <%=Html.TextArea("newBlogConfig.HeaderScript", Model.newBlogConfig.HeaderScript, new { rows="10",cols="70", @class = "itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("newBlogConfig.HeaderScript", "*")%>
                </dd>
            </dl>
            <dl>
                <dt>
                    <label for="Language">Language</label>
                </dt>
                <dd>
                    <%=Html.TextBox("newBlogConfig.Language", Model.newBlogConfig.Language, new { size=70, @class="itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("newBlogConfig.Language", "*")%>
                </dd>
            </dl>
            <dl>
                <dt>
                    <label for="PostCount">Post Count</label>
                </dt>
                <dd>
                    <%=Html.TextBox("newBlogConfig.PostCount", Model.newBlogConfig.PostCount, new { size=70, @class="itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("newBlogConfig.PostCount", "*")%>
                </dd>
            </dl>
            <dl>
                <dt>
                    <label for="PostsPerPage">Posts Per Page</label>
                </dt>
                <dd>
                    <%=Html.TextBox("newBlogConfig.PostsPerPage", Model.newBlogConfig.PostsPerPage, new { size=70, @class="itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("newBlogConfig.PostsPerPage", "*")%>
                </dd>
            </dl>
            <dl>
                <dt>
                    <label for="Skin">Skin</label>
                </dt>
                <dd>
                    <%=Html.TextBox("newBlogConfig.Skin", Model.newBlogConfig.Skin, new { size=70, @class="itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("newBlogConfig.Skin", "*")%>
                </dd>
            </dl>
            <dl>
                <dt>
                    <label for="Title">Title</label>
                </dt>
                <dd>
                    <%=Html.TextBox("newBlogConfig.Title", Model.newBlogConfig.Title, new { size=70, @class="itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("newBlogConfig.Title", "*")%>
                </dd>
            </dl>
            <dl>
                <dt>
                    <label for="Subtitle">Subtitle</label>
                </dt>
                <dd>
                    <%=Html.TextBox("newBlogConfig.SubTitle", Model.newBlogConfig.SubTitle, new { size=70, @class="itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("newBlogConfig.SubTitle", "*")%>
                </dd>
            </dl>
            <dl>
                <dt>
                    <label for="TimeZone">TimeZone</label>
                </dt>
                <dd>
                    <%=Html.TextBox("newBlogConfig.TimeZone", Model.newBlogConfig.TimeZone, new { size=70, @class="itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("newBlogConfig.TimeZone", "*")%>
                </dd>
            </dl>
            <dl>
                <dt>
                    <label for="WebStatsJavascript">Web Stats Script</label>
                </dt>
                <dd>
                    <%=Html.TextArea("newBlogConfig.WebStatsJavascript", Model.newBlogConfig.WebStatsJavascript, new { rows = "10", cols = "70", @class = "itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("newBlogConfig.WebStatsJavascript", "*")%>
                </dd>
            </dl>

            <input type="submit" value="Save" />
        <% } %>
    </fieldset>

</asp:Content>

