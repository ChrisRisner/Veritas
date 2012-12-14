<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="Veritas.UI.Web.Views.VeritasViewPage<Veritas.BusinessLayer.Screens.Admin.Entries.EntriesEditScreen>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%: Model.blogConfig.Title %> - Edit Entry
</asp:Content>

<asp:Content ID="MetaContent" ContentPlaceHolderID="MetaContent" runat="server">
    <meta name="description" content="<%= Model.blogConfig.Title %> - Contact" />
    <meta name="Keywords" content="<%= Model.blogConfig.Title %>,Contact" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="divAdminBody">
    <h2>Edit Entry</h2>

    <%= Html.ValidationSummary() %>
    <% using (Html.BeginForm("Entries/Save", "Admin"))
       { %>
            <%= Html.Hidden("entryId", Model.BlogEntry.BlogEntryId)%>
            <dl>
                <dt>
                    <label for="Title">Title</label>
                </dt>
                <dd>
                    <%=Html.TextBox("BlogEntry.Title", Model.BlogEntry.Title, new { size=70, @class="itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("BlogEntry.Title", "*")%>
                </dd>
            </dl>
            <dl>
                <dt>
                    <label for="Short">Short</label>
                </dt>
                <dd>
                    <%=Html.TextArea("BlogEntry.Short", Model.BlogEntry.Short, new { rows="3", cols="70", @class="itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("BlogEntry.Short", "*")%>
                </dd>
            </dl>
            <dl>
                <dt>
                    <label for="Keywords">Keywords</label>
                </dt>
                <dd>
                    <%=Html.TextArea("BlogEntry.Keywords", Model.BlogEntry.Keywords, new { rows="3", cols="70", @class="itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("BlogEntry.Keywords", "*")%>
                </dd>
            </dl>
            
            <dl>
            <dt>
                <label for="PostType">Post Type</label>
            </dt>
            <dd>
                <%=Html.DropDownList("PostTypeSelectList")%>
                <label class="required"></label>
                <%=Html.ValidationMessage("BlogEntry.PostType", "*")%>
            </dd>
        </dl>


            <dl>
                <dt>
                    <label for="Text">Text</label>
                </dt>
                <dd>
                    <%=Html.TextArea("BlogEntry.Text", Model.BlogEntry.Text, new { rows="10", cols="70", @class="itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("BlogEntry.Text", "*")%>
                </dd>
            </dl>
            <dl>
                <dt>
                    <label for="PreviousEntryInSeries">Previous Entry in Series</label>
                </dt>
                <dd>
                    <%=Html.DropDownList("PreviousEntryInSeriesSelectList")%>
                    <label class="required"></label>
                    <%=Html.ValidationMessage("BlogEntry.PreviousEntryInSeries", "*")%>
                </dd>
            </dl>
            <dl>
                <dt>
                    <label for="NextEntryInSeries">Next Entry in Series</label>
                </dt>
                <dd>
                    <%=Html.DropDownList("NextEntryInSeriesSelectList")%>
                    <label class="required"></label>
                    <%=Html.ValidationMessage("BlogEntry.NextEntryInSeries", "*")%>
                </dd>
            </dl>
            <dl>
                <dt>
                    <label for="PreviousEntryText">Previous Entry Text (blank will use default)</label>
                </dt>
                <dd>
                    <%=Html.TextBox("BlogEntry.PreviousEntryText", Model.BlogEntry.PreviousEntryText, new { size="70", @class="itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("BlogEntry.PreviousEntryText", "*")%>
                </dd>
            </dl>
            <dl>
                <dt>
                    <label for="NextEntryText">Next Entry Text (blank will use default)</label>
                </dt>
                <dd>
                    <%=Html.TextBox("BlogEntry.NextEntryText", Model.BlogEntry.NextEntryText, new { size="70", @class="itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("BlogEntry.NextEntryText", "*")%>
                </dd>
            </dl>
            <dl>
                <dt>
                    <label for="LogoUrl">Logo URL</label>
                </dt>
                <dd>
                    <%=Html.TextBox("BlogEntry.LogoUrl", Model.BlogEntry.LogoUrl, new { size="70", @class="itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("BlogEntry.LogoUrl", "*")%>
                </dd>
            </dl>
            
            <% if (Model.BlogEntry.BlogEntryId > 0)
               { %>
                <dl>
                    <dt>
                        <label for="EntryName">Entry Name</label>
                    </dt>
                    <dd>
                        <%= Model.BlogEntry.EntryName %>
                    </dd>
                </dl>
                <dl>
                    <dt>
                        <label for="CreatedBy">Created By</label>
                    </dt>
                    <dd>
                        <%= Model.BlogEntry.BlogUser.Username%>
                    </dd>
                </dl>                            
                <dl>
                    <dt>
                        <label for="PublishDate">Publish Date</label>
                    </dt>
                    <dd>
                        <%= Model.BlogEntry.PublishDate%>
                    </dd>
                </dl>
                <dl>
                    <dt>
                        <label for="CreateDate">Create Date</label>
                    </dt>
                    <dd>
                        <%= Model.BlogEntry.CreateDate%>
                    </dd>
                </dl>
                <dl>
                    <dt>
                        <label for="LastUpdateDate">Last Update Date</label>
                    </dt>
                    <dd>
                        <%= Model.BlogEntry.LastUpdateDate%>
                    </dd>
                </dl>
            <% } %>
            <input type="submit" value="Save Entry" />
        <% } %>


    </div>
</asp:Content>

