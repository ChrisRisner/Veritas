<%@ Control Language="C#" Inherits="Veritas.UI.Web.Views.VeritasViewUserControl<Veritas.BusinessLayer.Screens.ScreenBase>" %>
<%@ Import Namespace="Veritas.BusinessLayer" %>
<%@ Import Namespace="Veritas.DataLayer.Models" %>
<div id="topBarMenu" align="center">
    <ul id="topMenu">
        <% foreach (BlogMenuItem item in Model.blogMenuItems)
           { %>
           <li>
                <%= (item.IsView ?
                   VeritasForm.ActionLink(item.LinkText, item.ViewName, "Home")
                   :
                    VeritasForm.ActionLink(item.LinkText, "ViewContent/"+item.ViewName, "Home"))
                 %>
           </li>
        <% } %>
    </ul>
    <b id="topMenuBottom" style="padding-left:0px"></b>
</div>