using System;
using System.Collections.Generic;
using System.Text;

namespace Hzexe.Lanzou.Model.Lanzou
{
    public class GetDirResponse: ResponseBase
    {
        /// <summary>
        /// 
        /// </summary>
        public InfoItem[] info { get; set; }
        /// <summary>
        /// 子目录集合
        /// </summary>
        public TextItem[] text { get; set; }
        public class InfoItem
        {
            /// <summary>
            /// 
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string folder_des { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int folderid { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int now { get; set; }
        }

        public class TextItem
        {
            /// <summary>
            /// 
            /// </summary>
            public string onof { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string folderlock { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string is_lock { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string fol_id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string folder_des { get; set; }
        }
    }
}
