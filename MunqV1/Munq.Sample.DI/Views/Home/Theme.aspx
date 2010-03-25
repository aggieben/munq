<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Theme
</asp:Content>

<asp:Content ID="HeaderContent1" ContentPlaceHolderID="HeaderContent" runat="server">
<script runat="server">
    protected override void OnPreInit(EventArgs ev)
    {
        try
        {
            this.Theme = Profile.theme;
        }
        catch
        {
            this.Theme = "Default";
        }
    }
</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Theme</h2>
    <% using(Html.BeginForm("SetTheme", "Home") ){ %>
    <%=Html.DropDownList("theme") %>
    <input type="submit" value="Change Theme" />
    <% } %> 
    

</asp:Content>
