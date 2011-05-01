<%@ Control Language="C#" Inherits="Veritas.UI.Web.Views.VeritasViewUserControl" %>
<%@ Import Namespace="Veritas.BusinessLayer" %>

<%
    if (Request.IsAuthenticated) 
    {
%>
        Howdy, <b><%= Html.Encode(Page.User.Identity.Name) %></b>!
        <%--[ <%= VeritasForm.ActionLink("Log Off", "LogOff", "Admin").AutoRemoveAlias() %> ]--%>
        [ <%= VeritasForm.ActionLink("Log Off", "LogOff", ViewData.Model)%> ]
<%
    }
    else 
    {
%> 
        [ <%= VeritasForm.ActionLink("Log On", "LogOn", ViewData.Model)%> ]
<%
    }
%>
