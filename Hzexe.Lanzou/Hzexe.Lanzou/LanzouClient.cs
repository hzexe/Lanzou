using Hzexe.Lanzou.Model.Lanzou;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hzexe.Lanzou
{
    public class LanzouClient
    {
        readonly CookieContainer cookieContainer;
        const string refer = "https://pc.woozooo.com";
        const string userAgent = "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) snap Chromium/71.0.3578.98 Chrome/71.0.3578.98 Safari/537.36";
        readonly HttpMessageHandler handler;

        public LanzouClient(string cookieStr)
        {
            cookieContainer = new CookieContainer();
            string[] cookstr = cookieStr.Split(';');
            foreach (string str in cookstr)
            {
                string[] cookieNameValue = str.Split('=');
                Cookie ck = new Cookie(cookieNameValue[0].Trim().ToString(), cookieNameValue[1].Trim().ToString());
                ck.Domain = ".woozooo.com";
                cookieContainer.Add(ck);
            }
#if NETCOREAPP3_0
            handler = new SocketsHttpHandler()
            {
                AllowAutoRedirect = false,
                CookieContainer = cookieContainer,
                PooledConnectionLifetime = TimeSpan.FromHours(1),
                PooledConnectionIdleTimeout = TimeSpan.FromMinutes(20),
                MaxConnectionsPerServer = 6,
            };
#else
            handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false,
                CookieContainer = cookieContainer,
                MaxConnectionsPerServer = 6,
            };
#endif
        }
        public async Task<LanZouFileResult> FileUploadAsync(string folder_id, Stream file, string filename, int filesize)
        {
            HttpClient client = new HttpClient(handler, false);
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StringContent("1"), "task");
            content.Add(new StringContent(folder_id.ToString()), "folder_id_bb_n");
            content.Add(new StringContent("2"), "ve");
            content.Add(new StringContent("WU_FILE_0"), "id");
            content.Add(new StringContent(filename, Encoding.UTF8), "name");
            content.Add(new StringContent("application/pdf"), "type");
            content.Add(new StringContent(ToGMTFormat(DateTime.Now.AddDays(-50))), "lastModifiedDate");
            content.Add(new StringContent(filesize.ToString()), "size");
            content.Add(new StreamContent(file), "upload_file", filename);
            client.DefaultRequestHeaders.Add("user-agent", userAgent);
            client.DefaultRequestHeaders.Add("referer", refer);
            string json = null;
            try
            {
                var rm = await client.PostAsync("https://up.woozooo.com/fileup.php", content);
                if (rm.IsSuccessStatusCode)
                {
                    json = await rm.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("上传错误", ex);
            }
            finally
            {
                client.Dispose();
            }

            var lanZouFileResult = System.Text.Json.JsonSerializer.Deserialize<LanZouFileResult>(json);
            var file_id = lanZouFileResult.text[0].id;
            return lanZouFileResult;
        }

        public async Task<GetDirResponse> LsDirAsync(string folder_id)
        {
            HttpClient client = new HttpClient(handler, false);
            var dic = new Dictionary<string, string>(3);
            dic.Add("folder_id", folder_id);
            dic.Add("task", "47");
            var encodedContent = new FormUrlEncodedContent(dic);
            client.DefaultRequestHeaders.Add("user-agent", userAgent);
            client.DefaultRequestHeaders.Add("referer", refer);
            string json = null;
            try
            {
                var rm = await client.PostAsync("https://pc.woozooo.com/doupload.php", encodedContent);
                if (rm.IsSuccessStatusCode)
                {
                    json = await rm.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("上传错误", ex);
            }
            finally
            {
                client.Dispose();
            }

            var lanZouFileResult = System.Text.Json.JsonSerializer.Deserialize<GetDirResponse>(json);
            //var file_id = lanZouFileResult.text[0].id;
            return lanZouFileResult;
        }

        public async Task<GetFilesResponse> LsFilesAsync(string folder_id, int page = 1)
        {
            HttpClient client = new HttpClient(handler, false);
            var dic = new Dictionary<string, string>(3);
            dic.Add("folder_id", folder_id);
            dic.Add("task", "5");
            dic.Add("pg", "" + page);
            var encodedContent = new FormUrlEncodedContent(dic);
            client.DefaultRequestHeaders.Add("user-agent", userAgent);
            client.DefaultRequestHeaders.Add("referer", refer);
            string json = null;
            try
            {
                var rm = await client.PostAsync("https://pc.woozooo.com/doupload.php", encodedContent);
                if (rm.IsSuccessStatusCode)
                {
                    json = await rm.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("上传错误", ex);
            }
            finally
            {
                client.Dispose();
            }

            var lanZouFileResult = System.Text.Json.JsonSerializer.Deserialize<GetFilesResponse>(json);
            //var file_id = lanZouFileResult.text[0].id;
            return lanZouFileResult;
        }

        public async Task<MkdirResponse> MkdirAsync(string folder_id, string folder_name, string folder_description)
        {
            HttpClient client = new HttpClient(handler, false);
            var dic = new Dictionary<string, string>(3);
            dic.Add("parent_id", folder_id);
            dic.Add("task", "2");
            dic.Add("folder_name", folder_name);
            dic.Add("folder_description", folder_description);
            var encodedContent = new FormUrlEncodedContent(dic);
            client.DefaultRequestHeaders.Add("user-agent", userAgent);
            client.DefaultRequestHeaders.Add("x-requested-with", "XMLHttpRequest");
            client.DefaultRequestHeaders.Add("referer", refer);
            string json = null;
            try
            {
                var rm = await client.PostAsync("https://pc.woozooo.com/doupload.php", encodedContent);
                rm.EnsureSuccessStatusCode();
                json = await rm.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("目录创建错误", ex);
            }
            finally
            {
                client.Dispose();
            }

            var obj = System.Text.Json.JsonSerializer.Deserialize<MkdirResponse>(json);
            return obj;
        }


        public async Task<string> FileDownloadAsync(string url)
        {
            HttpClient client = new HttpClient(handler, false);
            client.DefaultRequestHeaders.Add("user-agent", userAgent);
            client.DefaultRequestHeaders.Add("referer", url);
            var res = await client.GetAsync(url);
            res.EnsureSuccessStatusCode();
            var html = await res.Content.ReadAsStringAsync();
            string p = @"<iframe.*?name=""\d{5,}"".*?src=""(.*?)""";
            var src = Regex.Match(html, p).Groups[1].Value;
            Uri u = new Uri(url);
            var hostbase = u.GetComponents(UriComponents.SchemeAndServer, UriFormat.Unescaped);

            var frame = hostbase + src;

            res = await client.GetAsync(frame);
            res.EnsureSuccessStatusCode();
            html = await res.Content.ReadAsStringAsync();
            /*
            var cgcroc = 'UDYHOV1sBzZUXQs0CjoHO1c_aV2UEYgA0Cj9bZFM6Bz8JL1V2D29UMQFiA2wHZFVnA2ADNwVtBzwDPA_c_c';//var harse = 'ate';
		var pposturl = '?v2';//var harse = harse;var cess = cess;
			//var ajaxup = '';
		$.ajax({
			type : 'post',
			url : '/ajaxm.php',
			//data : { 'action':'downprocess','sign':pposturl,'ves':1 },
		data : { 'action':'downprocess','sign':cgcroc,'ves':1 }, 
             */

            var mc = Regex.Match(html, @"var cgcroc = '(.+?)'");
            //var json = mc.Groups[1].Value.Replace("'", "\"");
            //JsonDocument json1 = JsonDocument.Parse(json);
            //var ps = json1.RootElement.EnumerateObject().Select(x => new KeyValuePair<string, string>(x.Name, x.Value.ToString()));
            Dictionary<string, string> ps = new Dictionary<string, string>(5)
            {
            { "action","downprocess"},
            { "sign",mc.Groups[1].Value},
            { "ves","1"},
            };
            FormUrlEncodedContent encodedContent = new FormUrlEncodedContent(ps);
            //var postContent = new StringContent(json, Encoding.UTF8, "application/json");
            var linkUrl = hostbase + "/ajaxm.php";

            res = await client.PostAsync(linkUrl, encodedContent);
            res.EnsureSuccessStatusCode();
            html = await res.Content.ReadAsStringAsync();
            var linkinfo = System.Text.Json.JsonSerializer.Deserialize<GetLinkResponse>(html);
            if (!linkinfo.zt.HasValue || linkinfo.zt.Value != 1)
                throw new Exception("获取直链失败，状态码：" + html);
            res = await client.GetAsync(linkinfo.FullUrl);
            res.EnsureSuccessStatusCode();
            html = await res.Content.ReadAsStringAsync();
            if (html.Contains("网络异常"))
            {
                System.Threading.Thread.Sleep(2000);//need keep this
                client.DefaultRequestHeaders.Remove("referer");
                client.DefaultRequestHeaders.Add("referer", linkinfo.FullUrl);
                client.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
                //需要二次验证
                var file_token = Regex.Match(html, @"'file':'(.+?)'").Groups[1].Value;
                var file_sign = Regex.Match(html, @"'sign':'(.+?)'").Groups[1].Value;
                var check_api = linkinfo.dom + "/file/ajax.php";
                var dic = new Dictionary<string, string>(3);
                dic.Add("file", file_token);
                dic.Add("sign", file_sign);
                dic.Add("el", "2");
                encodedContent = new FormUrlEncodedContent(dic);
                var c = new StringContent($"file_token={file_token}&file_sign={file_sign}&el=2", Encoding.UTF8, "application/x-www-form-urlencoded");
                res = await client.PostAsync(check_api, encodedContent);
                res.EnsureSuccessStatusCode();
                var resJson = await res.Content.ReadAsStringAsync();
                var jd = System.Text.Json.JsonDocument.Parse(resJson);
                var aurl = jd.RootElement.GetProperty("url").GetString();
                client.Dispose();
                return aurl;
            }
            else
            {
                client.Dispose();
                //重定向后的真直链
                return res.Content.Headers.ContentLocation.ToString();
            }

        }


        /// <summary>  
        /// 本地时间转成GMT格式的时间  
        /// </summary>  
        private static string ToGMTFormat(DateTime dt)
        {
            return dt.ToString("r") + dt.ToString("zzz").Replace(":", "");
        }

    }
}
