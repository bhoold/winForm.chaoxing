using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

using RavenUtility;

namespace WindowsForms.chaoxing
{
    public partial class FormLogin : Form
    {
        private HttpRequestClient http;
        private string school;

        public FormLogin()
        {

            InitializeComponent();

            //初始化http
            http = new HttpRequestClient();

            GetPic();
        }

        private void btnSelSchool_Click(object sender, EventArgs e)
        {
            FormSchool form = new FormSchool();
            form.ShowDialog();
            if(form.DialogResult == DialogResult.OK)
            {
                school = form.GetSelectedSchool();
                JObject json = JObject.Parse(form.GetSelectedSchool());
                textBoxSchool.Text = json.Value<string>("name");
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (Login())
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void picBoxCode_Click(object sender, EventArgs e)
        {
            GetPic();
        }

        private void GetPic()
        {
            string url = @"http://passport2.chaoxing.com/num/code";
            System.IO.Stream stream = null;
            stream = http.httpStream(url, "GET", null, null, null, null);
            if (null != stream)
            {
                Image image = Image.FromStream(stream);
                picBoxCode.Image = image;
            }

            //MessageBox.Show(image.ToString());
        }

        private bool Login()
        {
            bool flag = false;

            if (string.IsNullOrEmpty(textBoxAccount.Text))
            {
                MessageBox.Show("请输入账号", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if (string.IsNullOrEmpty(textBoxPassword.Text))
            {
                MessageBox.Show("请输入密码", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if (string.IsNullOrEmpty(textBoxCode.Text))
            {
                MessageBox.Show("请输入验证码", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                string fid = "-1", fidName = "", allowJoin = "0";
                if (!string.IsNullOrEmpty(school))
                {
                    JObject json = JObject.Parse(school);
                    fid = json.Value<string>("id");
                    fidName = json.Value<string>("name");
                    allowJoin = json.Value<string>("allowJoin");
                }

                string uname = textBoxAccount.Text,
                    password = textBoxPassword.Text,
                    numcode = textBoxCode.Text;
                password = Utility.EncodeBase64("utf-8", password);

                string url = @"http://passport2.chaoxing.com/login?refer=http%3A%2F%2Fi.mooc.chaoxing.com";
                string header = @"
                    Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3
                    Accept-Encoding: gzip, deflate
                    Accept-Language: zh-CN,zh;q=0.9
                    Cache-Control: no-cache
                    Connection: keep-alive
                    Content-Length: 282
                    Content-Type: application/x-www-form-urlencoded
                    Host: passport2.chaoxing.com
                    Origin: http://passport2.chaoxing.com
                    Pragma: no-cache
                    Referer: http://passport2.chaoxing.com/login?refer=http%3A%2F%2Fi.mooc.chaoxing.com
                    Upgrade-Insecure-Requests: 1
                    User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.70 Safari/537.36
                ";
                string response = "";
                response = http.httpPost(url, header, "refer_0x001=http%3A%2F%2Fi.mooc.chaoxing.com&pid=-1&pidName=&fid=" + fid + "&fidName="+ fidName + "&allowJoin="+ allowJoin + "&isCheckNumCode=1&f=0&productid=&t=true&uname="+ uname + "&password="+ password + "&numcode="+ numcode + "&verCode=", Encoding.UTF8);

                string startStr, endStr, errorStr;
                Regex pattern;
                startStr = "<td class=\"show_error\" id=\"show_error\">";
                endStr = "&nbsp;</td>";
                pattern = new Regex("(?<=(" + startStr + "))[.\\s\\S]*?(?=(" + endStr + "))", RegexOptions.Multiline | RegexOptions.Singleline);
                errorStr = pattern.Match(response).Value;
                if (string.IsNullOrEmpty(errorStr))
                {
                    if (response.IndexOf("RegisterNew.dll") > 0)
                    {
                        flag = true;

                        string schoolid = "", username = "", userpwd = "", encryptPwd = "", urls = "", urld = "", proc = "";

                        startStr = "<input type=\"hidden\" name=\"SchoolID\" value=\"";
                        endStr = "\" />";
                        pattern = new Regex("(?<=(" + startStr + "))[.\\s\\S]*?(?=(" + endStr + "))", RegexOptions.Multiline | RegexOptions.Singleline);
                        schoolid = pattern.Match(response).Value;

                        startStr = "<input type=\"hidden\" name=\"UserName\" value=\"";
                        endStr = "\" />";
                        pattern = new Regex("(?<=(" + startStr + "))[.\\s\\S]*?(?=(" + endStr + "))", RegexOptions.Multiline | RegexOptions.Singleline);
                        username = pattern.Match(response).Value;

                        startStr = "<input type=\"hidden\" name=\"UserPwd\" value=\"";
                        endStr = "\"/>";
                        pattern = new Regex("(?<=(" + startStr + "))[.\\s\\S]*?(?=(" + endStr + "))", RegexOptions.Multiline | RegexOptions.Singleline);
                        userpwd = pattern.Match(response).Value;

                        startStr = "<input type=\"hidden\" name=\"encryptPwd\" value=\"";
                        endStr = "\"/>";
                        pattern = new Regex("(?<=(" + startStr + "))[.\\s\\S]*?(?=(" + endStr + "))", RegexOptions.Multiline | RegexOptions.Singleline);
                        encryptPwd = pattern.Match(response).Value;

                        startStr = "<input type=\"hidden\" name=\"Urls\" value=\"";
                        endStr = "\"/>";
                        pattern = new Regex("(?<=(" + startStr + "))[.\\s\\S]*?(?=(" + endStr + "))", RegexOptions.Multiline | RegexOptions.Singleline);
                        urls = pattern.Match(response).Value;

                        startStr = "<input type=\"hidden\" name=\"Urld\" value=\"";
                        endStr = "\"/>";
                        pattern = new Regex("(?<=(" + startStr + "))[.\\s\\S]*?(?=(" + endStr + "))", RegexOptions.Multiline | RegexOptions.Singleline);
                        urld = pattern.Match(response).Value;

                        startStr = "<input type=\"hidden\" name=\"Proc\" value=\"";
                        endStr = "\"/>";
                        pattern = new Regex("(?<=(" + startStr + "))[.\\s\\S]*?(?=(" + endStr + "))", RegexOptions.Multiline | RegexOptions.Singleline);
                        proc = pattern.Match(response).Value;


                        url = @"https://reg.chaoxing.com/reg/RegisterNew.dll";
                        header = @"
                            Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3
                            Accept-Encoding: gzip, deflate, br
                            Accept-Language: zh-CN,zh;q=0.9
                            Cache-Control: no-cache
                            Connection: keep-alive
                            Content-Length: 367
                            Content-Type: application/x-www-form-urlencoded
                            Host: reg.chaoxing.com
                            Origin: http://passport2.chaoxing.com
                            Pragma: no-cache
                            Referer: http://passport2.chaoxing.com/login?refer=http%3A%2F%2Fi.mooc.chaoxing.com
                            Sec-Fetch-Mode: navigate
                            Sec-Fetch-Site: cross-site
                            Upgrade-Insecure-Requests: 1
                            User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.70 Safari/537.36
                        ";
                        response = "";
                        response = http.httpPost(url, header, "SchoolID = "+ schoolid + " & UserName = " + username + " & UserPwd = " + userpwd + " & encryptPwd = " + encryptPwd + " & Hddinfo = 1583399121 & Urls = "+ urls + " & Urld = "+ urld + " & Proc = "+ proc + " & SSVer = 4.1.1.0003", Encoding.Default);


                        Console.Write(response);

                        url = @"http://passport2.chaoxing.com/tochaoxing?refer=http%3A%2F%2Fi.mooc.chaoxing.com&username="+ username + "&Result=0&RegCode=384358e7c764b92d56cd9dc19813def18234b1a99330ea90c1b00fdaadd2b9dd22d7fe42cdd17e283077e363b2fcd49a&hddid=172052239&EncryptNo=9ecd82a32c6c58daf90744d7a8e50b3e";

                        http.httpGet(url);

                    }
                    else
                    {
                        MessageBox.Show("服务器未知错误！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
                else
                {
                    MessageBox.Show(errorStr, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }


                GetPic();
            }
            return flag;

        }

    }
}
