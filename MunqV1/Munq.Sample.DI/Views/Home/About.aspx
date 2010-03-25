<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="aboutTitle" ContentPlaceHolderID="TitleContent" runat="server">
    About Us
</asp:Content>

<asp:Content ID="indexHeader" ContentPlaceHolderID="HeaderContent" runat="server">
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

<asp:Content ID="aboutContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>About</h2>
    <p>
        Put content here.
    </p>
</asp:Content>
