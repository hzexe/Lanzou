using System;
using System.Collections.Generic;
using System.Text;

namespace Hzexe.Lanzou.Model.Lanzou
{
    public class MkdirResponse: ResponseBase
    {
        /// <summary>
        /// 创建成功
        /// </summary>
        public string info { get; set; }
        /// <summary>
        /// ID
        /// </summary>
        public string text { get; set; }
    }
}
