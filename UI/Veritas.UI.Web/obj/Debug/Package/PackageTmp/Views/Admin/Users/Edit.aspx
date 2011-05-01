<%@ Page Title="" Language="C#" ValidateRequest="false" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="Veritas.UI.Web.Views.VeritasViewPage<Veritas.BusinessLayer.Screens.Admin.Users.UsersEditScreen>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: Model.blogConfig.Title %> - Edit User
</asp:Content>

<asp:Content ID="MetaContent" ContentPlaceHolderID="MetaContent" runat="server">
    <meta name="description" content="<%= Model.blogConfig.Title %> - Contact" />
    <meta name="Keywords" content="<%= Model.blogConfig.Title %>,Contact" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="divAdminBody">
    <h2>Edit User</h2>

    <%= Html.ValidationSummary() %>
    <% using (Html.BeginForm("Users/Save", "Admin"))
       { %>
            <%= Html.Hidden("userId", Model.BlogUser.BlogUserId)%>
            <dl>
                <dt>
                    <label for="UserName">User Name</label>
                </dt>
                <dd>
                    <%=Html.TextBox("BlogUser.Username", Model.BlogUser.Username, new { size=70, @class="itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("BlogUser.Username", "*")%>
                </dd>
            </dl>
            <dl>
                <dt>
                    <label for="Email">Email Address</label>
                </dt>
                <dd>
                    <%=Html.TextBox("BlogUser.EmailAddress", Model.BlogUser.EmailAddress, new { size=70, @class="itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("BlogUser.EmailAddress", "*")%>
                </dd>
            </dl>
            <dl>
                <dt>
                    <label for="NotifyForFeedback">NotifyForFeedback</label>
                </dt>
                <dd>
                    <%=Html.CheckBox("BlogUser.NotifyForFeedback", Model.BlogUser.NotifyForFeedback, new { @class = "icheckbox" })%>    
                    <label class="required"></label>
                </dd>
            </dl>

            <dl>
                <dt>
                    <label for="About">About</label>
                </dt>
                <dd>
                    <%=Html.TextArea("BlogUser.About", Model.BlogUser.About, new { rows="3", cols="70", @class="itext" })%>    
                    <label class="required"></label>
                    <%=Html.ValidationMessage("BlogUser.About", "*")%>
                </dd>
            </dl>
            <% if (Model.BlogUser.BlogUserId == 0)
               { %>
                <dl>
                    <dt>
                        <label for="Password">Password</label>
                    </dt>
                    <dd>
                        <%=Html.TextBox("BlogUser.Password", Model.BlogUser.Password, new { size=70, @class="itext" })%>    
                        <label class="required"></label>
                        <%=Html.ValidationMessage("BlogUser.Password", "*")%>
                    </dd>
                </dl>

            <% } else if (Model.BlogUser.BlogUserId> 0)
               { %>
                <dl>
                    <dt>
                        <label for="CreateDate">Create Date</label>
                    </dt>
                    <dd>
                        <%= Model.BlogUser.CreateDate%>
                    </dd>
                </dl>               
                <dl>
                    <dt>
                        <label for="LastUpdateDate">Last Update Date</label>
                    </dt>
                    <dd>
                        <%= Model.BlogUser.LastUpdateDate%>
                    </dd>
                </dl>                
            <% } %>
            <input type="submit" value="Save User" />
        <% } %>


    </div>
</asp:Content>
