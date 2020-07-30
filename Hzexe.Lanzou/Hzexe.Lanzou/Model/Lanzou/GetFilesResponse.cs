using System;
using System.Collections.Generic;
using System.Text;

namespace Hzexe.Lanzou.Model.Lanzou
{
    public class GetFilesResponse:ResponseBase
    {

        /// <summary>
        /// 
        /// </summary>
        public int info { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<TextItem> text { get; set; }

        public class TextItem
        {
            /// <summary>
            /// 
            /// </summary>
            public string icon { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string name_all { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string size { get; set; }
            /// <summary>
            /// 21 分钟前
            /// </summary>
            public string time { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string downs { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string onof { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string is_lock { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string filelock { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int is_des { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int is_ico { get; set; }
        }
    }
}
