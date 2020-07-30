using System;
using System.Collections.Generic;
using System.Text;

namespace Hzexe.Lanzou.Model.Lanzou
{

    public class ResponseBase {
        public int? zt { get; set; }
    }

    public class LanZouFileResult: ResponseBase
    {
        public int? zt { get; set; }
        public string info { get; set; }
        public LanZouFileResultInfo[] text { get; set; }

    }

    public class LanZouFileResultInfo
    {
        public string icon { get; set; }
        public string id { get; set; }
        public string f_id { get; set; }
        public string name_all { get; set; }
        public string name { get; set; }
        public string size { get; set; }
        public string time { get; set; }
        public string downs { get; set; }
        public string onof { get; set; }
        public string is_newd { get; set; }

    }

    public class LanZouResult
    {
        public int? zt { get; set; }
        public LanZouInfo info { get; set; }
        public string text { get; set; }

    }
    public class LanZouInfo
    {
        public string pwd { get; set; }
        public string onof { get; set; }
        public string f_id { get; set; }
        public string taoc { get; set; }
        public string is_newd { get; set; }
    }

    public class LanZouPwd
    {
        public int zt { get; set; }
        public string info { get; set; }
        public string text { get; set; }
    }
}
