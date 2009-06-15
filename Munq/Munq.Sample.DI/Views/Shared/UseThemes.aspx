<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
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
