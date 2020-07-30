using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hzexe.Lanzou;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Hzexe.Lanzou.Tests
{
    [TestClass()]
    public class LanZhouHelperTests
    {
        [TestMethod()]
        public void FileUploadAndDownAsyncTest()
        {
            LanzouClient client = new LanzouClient("phpdisk_info=xxxx; ylogin=xxxxx");
            var f = File.OpenRead(@"c:\somefile.pdf");
            var tt = client.FileUploadAsync("2158627", f, DateTime.Now.ToFileTime() + ".zip", (int)f.Length).Result;
            f.Close();
            Assert.AreEqual(tt.zt, 1);
            var url = client.FileDownloadAsync(tt.text[0].is_newd + "/" + tt.text[0].f_id).Result;
            Assert.IsNotNull(url);
        }
    }
}