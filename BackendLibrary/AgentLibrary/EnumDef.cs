using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// EnumDef 的摘要描述
/// </summary>
namespace SB.Agent.Common
{
    public  enum  Role
    {
        Company=99,
        Whitelabel=13,
        Director =12,
        Partner =11,
        Super=4,
        Master=3,
        Agent=2,
    }
    public enum AccStatus
    { 
        All=0,
        Open=1,
        Suspend=2,
        Closed=3,
        Disabled=4
    }
    public enum ResultCode
    { 
        Success=0,
        SystemError=1000,
        IdPwdError=1001,
        AccClosed=1002,
        OldPwdError=1003
    }
	public enum GroupType //Abu Add 20130310
    { 
        GroupA = 1,
        GroupB = 2,
        GroupC = 3,
        GroupD = 4
    }
    public enum CommType //Abu Add 20130310
    { 
        Comm1X2 = 1, 
        CommOthers = 2,
        Comm137 = 3,
        CommNumber = 4
    
    }
}