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
            if (string.IsNullOrEmpty(school))
            {
                MessageBox.Show("请选择学校");
            }
            else
            {
                if (Login())
                {
                    DialogResult = DialogResult.OK;
                }
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

            if (string.IsNullOrEmpty(textBoxSchool.Text))
            {
                MessageBox.Show("请选择学校");
            }
            else if (string.IsNullOrEmpty(textBoxAccount.Text))
            {
                MessageBox.Show("请输入账号");
            }
            else if (string.IsNullOrEmpty(textBoxPassword.Text))
            {
                MessageBox.Show("请输入密码");
            }
            else if (string.IsNullOrEmpty(textBoxCode.Text))
            {
                MessageBox.Show("请输入验证码");
            }
            else
            {
                JObject json = JObject.Parse(school);
                string fid = json.Value<string>("id"),
                    fidName = json.Value<string>("name"),
                    allowJoin = json.Value<string>("allowJoin");

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
                    MessageBox.Show("服务器未知错误");
                }
                else
                {
                    MessageBox.Show(errorStr);
                }


                GetPic();
            }
            return flag;

        }

    }
}
