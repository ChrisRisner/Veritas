<%@ Control Language="C#" Inherits="Veritas.UI.Web.Views.VeritasViewUserControl<Veritas.DataLayer.Models.BlogFeedback>" %>
<%@ Import Namespace="Veritas.BusinessLayer" %>
<%@ Import Namespace="Veritas.DataLayer.Models" %>

<div class="divFeedbackItem">    
    <p class="pFeedbackItemImage">
        <img width="50" src="<%= VeritasForm.GetGravatarLink(Model.BlogFeedbackAuthor.Email) %>" alt="gravatar"/>
    </p>
    <p class="pFeedbackItemText">        
        <%: Model.Body %>        
        <br />
        Left by <%= VeritasForm.GetLinkForFeedbackAuthor(Model)%> at <%= Model.CreateDate %>        
    </p>    
</div>
