<%@ Control Language="C#" Inherits="Veritas.UI.Web.Views.VeritasViewUserControl<Veritas.BusinessLayer.Screens.ScreenBase>" %>
<%@ Import Namespace="Veritas.BusinessLayer" %>
<%@ Import Namespace="Veritas.DataLayer.Models" %>
<div align="center">
    <span id="categoriesHeader">Categories</span>
    <div id="tags">
      <%
        foreach (var categoryTag in Model.blogCategoryTags)
        {%>        
            <%= "<a href=\"" + VeritasForm.Content("~/Category/" + categoryTag.CategoryTitle) 
                + "\" title=\"" + categoryTag.CategoryTitle + "\" class=\"" 
                + VeritasForm.GetTagClass(categoryTag.CategoryUseCount, categoryTag.TotalArticles) 
                + "\">" + categoryTag.CategoryTitle + "</a>"%>
        &nbsp;
        <% }%>
    </div>
</div>