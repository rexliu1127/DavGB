using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using SB.DBLibrary;
using SB.Agent.Common;
using SB.Agent.Sites;
using SB.Agent.Users;
using SB.Common.Files;
public partial class Logout : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

       
        Session.Abandon();
        Response.Redirect("index.aspx");
    }
}