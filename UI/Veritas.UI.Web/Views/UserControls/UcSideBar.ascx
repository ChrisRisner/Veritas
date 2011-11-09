<%@ Control Language="C#" Inherits="Veritas.UI.Web.Views.VeritasViewUserControl<Veritas.BusinessLayer.Screens.ScreenBase>" %>
<%@ Import Namespace="Veritas.BusinessLayer" %>
<%@ Import Namespace="Veritas.DataLayer.Models" %>
<div class="syndication">    
    <%--Blog Entry Feed--%>
    <%= string.IsNullOrEmpty(Model.blogConfig.FeedburnerName) ?
        "<a href=\"" + VeritasForm.Content("~/Syndication/rss.aspx") + "\"><img src=\"" 
            + VeritasForm.Content("~/Content/Media/feed.png") 
            +"\" alt=\"Site Feed\" style=\"border-width:0px;\" /> RSS (blog)</a>"
        :
        "<a href=\"http://feeds.feedburner.com/"+ Model.blogConfig.FeedburnerName + "\"><img src=\"" +
            VeritasForm.Content("~/Content/Media/feed.png") +"\" alt=\"Site Feed\" style=\"border-width:0px;\" /> RSS</a>"                        
    %>
    <br />
    <%--Blog Feedback Feed--%>
    <% if (Model.blogConfig.EnableFeedbackRssFeed)
        { %>                
            <a href="<%= VeritasForm.Content("~/Syndication/commentRss.aspx") %>">
                <img src="<%= VeritasForm.Content("~/Content/Media/feed.png") %>"
                 alt="RSS Feed" style="border-width:0px;" /> RSS (comment)</a>        
            <br />
    <% } %> 
    <%--Social Networks--%>
    <% if (!string.IsNullOrEmpty(Model.blogConfig.GooglePlusUrl))
       { %>
       <a href='<%= Model.blogConfig.GooglePlusUrl %>'><img src="../../Content/Media/google_plus_logo.png" /></a>
       <br />
    <% } %>
    <% if (!string.IsNullOrEmpty(Model.blogConfig.FacebookUrl))
       { %>
       <a href='<%= Model.blogConfig.FacebookUrl %>'><img src="../../Content/Media/facebook_logo.png" /></a>
       <br />
    <% } %>
    <% if (!string.IsNullOrEmpty(Model.blogConfig.TwitterUrl))
       { %>
       <a href='<%= Model.blogConfig.TwitterUrl %>'><img src="../../Content/Media/twitter_logo.png" /></a>
       <br />
    <% } %>    
</div>
<%--Side bar ads--%>
<div class="sideBarAds">
    <% if (Model.blogConfig.BlogMarketingInfo.ShowSideBarAds) { %>
        <%= Model.blogConfig.BlogMarketingInfo.AdScriptSideBar %>        
    <% } %>
</div>
<%-- Tags --%>
<div class="sideBarTags">
    <% Html.RenderPartial("~/Views/UserControls/UcTagCloud.ascx"); %>            
</div>

