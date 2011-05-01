<%@ Control Language="C#" Inherits="Veritas.UI.Web.Views.VeritasViewUserControl<Veritas.BusinessLayer.Screens.Shared.BlogEntryScreen>" %>
<%@ Import Namespace="Veritas.BusinessLayer" %>
<%@ Import Namespace="Veritas.DataLayer.Models" %>
<%@ Import Namespace="Veritas.DataLayer" %>

<div class="entryFull">
    <h2>
        <%= "<a href=\"" + VeritasForm.Content("~/" + Model.BlogEntry.EntryName) 
            + "\" title=\"" + Model.BlogEntry.Title + "\">" + Model.BlogEntry.Title 
            + "</a>"%>
    </h2>

    <h4>    
        Posted on:  <%= Model.BlogEntry.PublishDate %> by <%= Model.BlogEntry.BlogUser.Username %>
    </h4>

    <div class="entryBody">
        <%= Model.BlogEntry.Text %>    
    </div>
    <div class="entryAds">
    <% if (Model.blogConfig.BlogMarketingInfo.ShowEntryAds) { %>
        <%= Model.blogConfig.BlogMarketingInfo.AdScriptEntry %>        
    <% } %>
    </div>
    <div class="divCategories">
        <% if (Model.BlogEntry.BlogEntryCategories.Count > 0) { %>
            Categories: 
            <%= VeritasForm.GetCategoryLinkLineForEntry(Model.BlogEntry.BlogEntryCategories.Select(p => p.BlogCategory).OrderBy(p => p.Title).ToArray()) %>
        <% } %>
    </div>

    <div class=".divShareEntry">
        <!-- AddThis Button BEGIN -->
        <a href="http://www.addthis.com/bookmark.php?v=250" 
        onmouseover="return addthis_open(this, '', '<%= "http://" + Model.blogConfig.Host + "/" + Model.BlogEntry.EntryName %>')" onmouseout="addthis_close()" onclick="return addthis_sendto()"><img src="http://s7.addthis.com/static/btn/lg-share-en.gif" 
        width="125" height="16" alt="Bookmark and Share" style="border:0"/></a>
        <script type="text/javascript" src="http://s7.addthis.com/js/250/addthis_widget.js?pub=morlockprime"></script>
        <!-- AddThis Button END -->
    </div>

    <%--If we're using default comments or this is the only entry on the page, show the comments without directing to another page.--%>
    <% if (Model.blogConfig.BlogCommentInfo.UseDefaultComments ||           
           (Model.blogConfig.BlogCommentInfo.UseDisqusComments && Model.OnlyItemOnPage)) { %>
        <a class="linkComments" href=<%= "javascript:ShowHideComments(" + Model.BlogEntry.BlogEntryId + ")" %> >Comments: <%= Model.BlogEntry.FeedbackCount %></a>
    <%--If we're using Disqus and there are multiple entries on the page, just show a link to the entry page.--%>
    <% } else if (Model.blogConfig.BlogCommentInfo.UseDisqusComments && !Model.OnlyItemOnPage) {%>
        <%--<a class="linkComments" href="<%= VeritasForm.Content("~/" + Model.BlogEntry.EntryName) %>" >Comments: <%= Model.BlogEntry.FeedbackCount %></a>        --%>

        <a class="linkComments" href="<%= VeritasForm.Content("~/" + Model.BlogEntry.EntryName.Replace("'", "%E2%80%99")) + "#disqus_thread" %>" 
            data-disqus-identifier="<%= Model.BlogEntry.EntryName.Replace("'", "%E2%80%99") %>">First Article </a>
    <% } %>

    <%--Comments--%>
    <%--Add comments code here, check for default or disqus--%>
    <% if (Model.blogConfig.BlogCommentInfo.UseDefaultComments)
       { %>        
        <div id=<%= "divComments" + Model.BlogEntry.BlogEntryId %> class="divFeedback hideDiv">
            <% foreach (var feedbackItem in Model.BlogEntry.BlogFeedbacks.Where(p => p.Status == (int) FeedbackStatus.Approved).ToArray()) {%>
                <% Html.RenderPartial("~/Views/UserControls/UcFeedback.ascx", feedbackItem); %>
            <% } %>
            <%--Entry for new comments--%>
            <% if (Model.blogConfig.AllowComments) { %>
                <%= Html.ValidationSummary()%>                   
                <form action="<%= VeritasForm.Content("~/" + Model.BlogEntry.EntryName) %>" method="post">
                   <fieldset id="commentWrapperFieldset">
                        <legend>Leave Feedback</legend>   
                        <span><%= Model.Message %></span>
                        <div style="float: left;">
                            <div>
                                <dl>
                                    <dt>
                                        <label for="Name">Name:</label>
                                    </dt>
                                    <dd>
                                        <%=Html.TextBox("Name", VeritasForm.GetUsernameFromCookie(), new { MaxLength = 60, @class="itext" })%>    
                                        <label class="required"></label>
                                    </dd>
                                </dl>
                            </div>
                            <div>
                                <dl>
                                    <dt>
                                        <label for="EmailAddress">Email address:</label>
                                    </dt>
                                    <dd>
                                        <div style="float: left;">
                                            <%=Html.TextBox("Email", VeritasForm.GetEmailAddressFromCookie(), new { MaxLength = 128, @class="itext" })%>
                                            <label class="required"></label>
                                        </div>                                
                                    </dd>
                                </dl>
                            </div>                  
                        </div>
                        <div style="float: left;margin-top: 20px;margin-left: 10px;">
                            <img class="newcommentgravatar" src="<%= VeritasForm.GetGravatarLink(VeritasForm.GetEmailAddressFromCookie()) %>" title="<%= VeritasForm.GetUsernameFromCookie() %>"/>
                        </div>         
                        <div>
                            <dl>
                                <dt><label for="WebSite">Website:</label></dt>
                                <dd>
                                    <%=Html.TextBox("WebSite", VeritasForm.GetWebsiteFromCookie(), new { MaxLength = 256, @class="itext"})%>                        
                                </dd>
                            </dl>
                        </div>
                        <div>
                            <dl>
                                <dt style="width: 120px"><label for="EmailMeResponses">Email me responses</label></dt>
                                <dd>
                                    <div>
                                        <%= Html.CheckBox("NotifyMeOnFeedback", VeritasForm.GetNotifyMeFromCookie(), new { @class="icheckbox" })%>
                                    </div>
                                </dd>
                            </dl>
                        </div>
                        <div>
                            <dl>
                                <dt><label for="FeedbackText">Feedback:</label></dt>
                                <dd>
                                    <%=Html.TextArea("FeedbackText", new { cols = 37, rows = 6, MaxLength = 6000 })%>               
                                    <label class="required"></label>
                                </dd>
                            </dl>
                        </div>
                        <div style="float: left;margin-left: 150px;">
                            <input type="submit" value="Save Feedback" />
                            <input id="Text1" type="text" value=<%= Model.BlogEntry.BlogEntryId %> visible="false" style="visibility:hidden" name="iEntryId" />
                            <input id="EntryName" type="text" value=<%= Model.BlogEntry.EntryName %> visible="false" style="visibility:hidden" name="EntryName" />
                        </div>
                    </fieldset>
                </form>
                <% } %>
        </div>        
        <br />
    <% }
       else if (Model.blogConfig.BlogCommentInfo.UseDisqusComments && Model.OnlyItemOnPage)
       { %>
        <div id=<%= "divComments" + Model.BlogEntry.BlogEntryId %> class="divFeedback hideDiv">   
            <%= Model.DisqusScript%>
        </div>
        <br />
        <% } %>
</div>
