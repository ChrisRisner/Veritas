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
        Posted on:  <%= Model.BlogEntry.PublishDate %> by <%= VeritasForm.ActionLink(Model.BlogEntry.BlogUser.Username, "About", "Home", null, new { rel = "author" })%>
    </h4>
</div>