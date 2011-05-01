<%@ Control Language="C#" Inherits="Veritas.UI.Web.Views.VeritasViewUserControl" %>
<div id="adminSettingsBarMenu" align="center">
    <ul id="adminSettingsMenu">
        <li id="liGeneral"><%= VeritasForm.ActionLink("General", "Settings/Edit", new { subSettingName = "General" })%></li>
        <li id="liFeedback"><%= VeritasForm.ActionLink("Feedback", "Settings/Edit", new { subSettingName = "Feedback" })%></li>
        <li id="liSpam"><%= VeritasForm.ActionLink("Spam", "Settings/Edit", new { subSettingName = "Spam" })%></li>
        <li id="liMarketing"><%= VeritasForm.ActionLink("Marketing", "Settings/Edit", new { subSettingName = "Marketing" })%></li>
        <li id="liSyndicationAndPublishing"><%= VeritasForm.ActionLink("Syndication and Publishing", "Settings/Edit", new { subSettingName = "SyndicationAndPublishing" })%></li>
        <li id="liLogging"><%= VeritasForm.ActionLink("Logging", "Settings/Edit", new { subSettingName = "Logging" })%></li>
        <li id="liSecurity"><%= VeritasForm.ActionLink("Security", "Settings/Edit", new { subSettingName = "Security" })%></li>
        <li id="liEmail"><%= VeritasForm.ActionLink("Email", "Settings/Edit", new { subSettingName = "Email" })%></li>
    </ul>   
</div>