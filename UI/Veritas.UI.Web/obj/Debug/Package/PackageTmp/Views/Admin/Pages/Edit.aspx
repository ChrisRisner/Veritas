<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="Veritas.UI.Web.Views.VeritasViewPage<Veritas.BusinessLayer.Screens.Admin.Pages.PagesEditScreen>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%: Model.blogConfig.Title %> - Pages
</asp:Content>

<asp:Content ID="MetaContent" ContentPlaceHolderID="MetaContent" runat="server">
    <meta name="description" content="<%= Model.blogConfig.Title %> - Contact" />
    <meta name="Keywords" content="<%= Model.blogConfig.Title %>,Contact" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="divAdminBody">
    <h2>Edit Page</h2>

    
    <%= Html.ValidationSummary() %>

        <% using (Html.BeginForm("Pages/Save", "Admin")) { %>
        <%= Html.Hidden("pageId", Model.BlogPage.BlogPageId) %>                                                
            


            <dl>
                <dt>
                    <label for="Title">Title</label>
                </dt>
                <dd>
                    <%=Html.TextBox("BlogPage.PageTitle", Model.BlogPage.PageTitle, new { size=70, @class="itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("BlogPage.PageTitle", "*")%>
                </dd>
            </dl>

            <dl>
                <dt>
                    <label for="Description">Description</label>
                </dt>
                <dd>
                    <%=Html.TextBox("BlogPage.Description", Model.BlogPage.Description, new { size = 70, @class = "itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("BlogPage.Description", "*")%>
                </dd>
            </dl>
            
            <dl>
                <dt>
                    <label for="Keywords">Keywords</label>
                </dt>
                <dd>
                    <%=Html.TextBox("BlogPage.Keywords", Model.BlogPage.Keywords, new { size = 70, @class = "itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("BlogPage.Keywords", "*")%>
                </dd>
            </dl>
            
            <dl>
                <dt>
                    <label for="PageContent">PageContent</label>
                </dt>
                <dd>
                    <%=Html.TextArea("BlogPage.PageContent", Model.BlogPage.PageContent, new { rows = "10", cols = "70", @class = "itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("BlogPage.PageContent", "*")%>
                </dd>
            </dl>
            
            

            <% if (Model.BlogPage.BlogPageId > 0)
               { %>
                <dl>
                    <dt>
                        <label for="EncodedName">Encoded Title</label>
                    </dt>
                    <dd>
                        <%= Model.BlogPage.EncodedTitle %>
                    </dd>
                </dl>
                <dl>
                    <dt>
                        <label for="CreatedBy">Created By</label>
                    </dt>
                    <dd>
                        <%= Model.BlogPage.BlogUser.Username%>
                    </dd>
                </dl>                            
                <dl>
                    <dt>
                        <label for="CreateDate">Create Date</label>
                    </dt>
                    <dd>
                        <%= Model.BlogPage.CreateDate%>
                    </dd>
                </dl>
                <dl>
                    <dt>
                        <label for="LastUpdateDate">Last Update Date</label>
                    </dt>
                    <dd>
                        <%= Model.BlogPage.LastUpdateDate%>
                    </dd>
                </dl>
            <% } %>
            <input type="submit" value="Save" />
            
        

    <% } %>

    </div>
</asp:Content>


