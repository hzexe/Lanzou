using System;
using System.Collections.Generic;
using System.Text;

namespace Hzexe.Lanzou.Model.Lanzou
{
    public class GetLinkResponse : ResponseBase
    {
        public string dom { get; set; }


        public int inf { get; set; }

        public string url { get; set; }

        /// <summary>
        /// 完整地址
        /// </summary>
        public string FullUrl => dom + "/file/" + url;
    }
}
