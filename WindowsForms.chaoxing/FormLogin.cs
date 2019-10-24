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
            http = new HttpRequestClient(true);

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
            string header = @"
                Accept: image/webp,image/apng,image/*,*/*;q=0.8
                Accept-Encoding: gzip, deflate
                Accept-Language: zh-CN,zh;q=0.9
                Cache-Control: no-cache
                Connection: keep-alive
                Cookie: cookiecheck=true; duxiu=userName%5fdsr%2c%3dzhizhentest%2c%21userid%5fdsr%2c%3d21498%2c%21char%5fdsr%2c%3d%2c%21metaType%2c%3d260%2c%21dsr%5ffrom%2c%3d0%2c%21logo%5fdsr%2c%3dlogo0408%2ejpg%2c%21logosmall%5fdsr%2c%3dsmall0408%2ejpg%2c%21title%5fdsr%2c%3d%u8d85%u661f%u53d1%u73b0%2c%21url%5fdsr%2c%3d%2c%21compcode%5fdsr%2c%3d%2c%21province%5fdsr%2c%3d%u5176%u5b83%2c%21readDom%2c%3d0%2c%21isdomain%2c%3d769%2c%21showcol%2c%3d0%2c%21hu%2c%3d0%2c%21uscol%2c%3d0%2c%21isfirst%2c%3d0%2c%21istest%2c%3d1%2c%21cdb%2c%3d0%2c%21og%2c%3d0%2c%21ogvalue%2c%3d0%2c%21testornot%2c%3d1%2c%21remind%2c%3d0%2c%21datecount%2c%3d3722%2c%21userIPType%2c%3d2%2c%21lt%2c%3d0%2c%21ttt%2c%3dfxlogin%2echaoxing%2c%21enc%5fdsr%2c%3d3AFF3FEA9DCF8526817F34E0F6AD8018; AID_dsr=7209; msign_dsr=1571886137412; search_uuid=372bd951%2d1260%2d4942%2d8825%2dd2714b60b8d1; DSSTASH_LOG=C%5f9%2dUN%5f7209%2dUS%5f0%2dT%5f1571886137413; mqs=19e3b526c24d63961c594850f09cc91fb2684007674e38588d7c2e084ce6404e6d58f013518d47b7966204b865bf74394b6aea37199a56bdaafce0a096f1fa22f13e85c07318d112e4446bf79392c19a3b7ac3c5ffa4f00ee4b101eb614dfa8270d771b7fcdebbcd979554c6cf946489; UM_distinctid=16dfbb706576eb-00d53e5e14de4e-7711439-1fa400-16dfbb706586f4; route=c3d18745984feab495ff2d582d1967a5; source=""; lastFid=54781; JSESSIONID=D6B0F374A06151B93551CB3609BAA7F4
                Host: passport2.chaoxing.com
                Pragma: no-cache
                Referer: http://passport2.chaoxing.com/login?refer=http%3A%2F%2Fi.mooc.chaoxing.com
                User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.70 Safari/537.36
            ";
            System.IO.Stream stream = null;
            stream = http.httpStream(url, "GET", header, null, null, null);
            Image image = Image.FromStream(stream);
            picBoxCode.Image = image;
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
                    uname = textBoxAccount.Text,
                    password = textBoxPassword.Text,
                    numcode = textBoxCode.Text;
                string url = @"http://passport2.chaoxing.com/login?refer=http%3A%2F%2Fi.mooc.chaoxing.com";
                string header = @"
                    Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3
                    Accept-Encoding: gzip, deflate
                    Accept-Language: zh-CN,zh;q=0.9
                    Cache-Control: no-cache
                    Connection: keep-alive
                    Content-Length: 282
                    Content-Type: application/x-www-form-urlencoded
                    Cookie: cookiecheck=true; duxiu=userName%5fdsr%2c%3dzhizhentest%2c%21userid%5fdsr%2c%3d21498%2c%21char%5fdsr%2c%3d%2c%21metaType%2c%3d260%2c%21dsr%5ffrom%2c%3d0%2c%21logo%5fdsr%2c%3dlogo0408%2ejpg%2c%21logosmall%5fdsr%2c%3dsmall0408%2ejpg%2c%21title%5fdsr%2c%3d%u8d85%u661f%u53d1%u73b0%2c%21url%5fdsr%2c%3d%2c%21compcode%5fdsr%2c%3d%2c%21province%5fdsr%2c%3d%u5176%u5b83%2c%21readDom%2c%3d0%2c%21isdomain%2c%3d769%2c%21showcol%2c%3d0%2c%21hu%2c%3d0%2c%21uscol%2c%3d0%2c%21isfirst%2c%3d0%2c%21istest%2c%3d1%2c%21cdb%2c%3d0%2c%21og%2c%3d0%2c%21ogvalue%2c%3d0%2c%21testornot%2c%3d1%2c%21remind%2c%3d0%2c%21datecount%2c%3d3722%2c%21userIPType%2c%3d2%2c%21lt%2c%3d0%2c%21ttt%2c%3dfxlogin%2echaoxing%2c%21enc%5fdsr%2c%3d3AFF3FEA9DCF8526817F34E0F6AD8018; AID_dsr=7209; msign_dsr=1571886137412; search_uuid=372bd951%2d1260%2d4942%2d8825%2dd2714b60b8d1; DSSTASH_LOG=C%5f9%2dUN%5f7209%2dUS%5f0%2dT%5f1571886137413; mqs=19e3b526c24d63961c594850f09cc91fb2684007674e38588d7c2e084ce6404e6d58f013518d47b7966204b865bf74394b6aea37199a56bdaafce0a096f1fa22f13e85c07318d112e4446bf79392c19a3b7ac3c5ffa4f00ee4b101eb614dfa8270d771b7fcdebbcd979554c6cf946489; UM_distinctid=16dfbb706576eb-00d53e5e14de4e-7711439-1fa400-16dfbb706586f4; route=c3d18745984feab495ff2d582d1967a5; source=""; lastFid=54781; JSESSIONID=D2744E32BFA96356106C8C165883CD67
                    Host: passport2.chaoxing.com
                    Origin: http://passport2.chaoxing.com
                    Pragma: no-cache
                    Referer: http://passport2.chaoxing.com/login?refer=http%3A%2F%2Fi.mooc.chaoxing.com
                    Upgrade-Insecure-Requests: 1
                    User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.70 Safari/537.36
                ";
                string response = "";
                response = http.httpPost(url, header, "refer_0x001=http%253A%252F%252Fi.mooc.chaoxing.com&pid=-1&pidName=&fid="+ fid + "&fidName="+ fidName + "&allowJoin=0&isCheckNumCode=1&f=0&productid=&t=true&uname="+ uname + "&password="+ password + "&numcode="+ numcode + "&verCode=", Encoding.UTF8);

                string startStr, endStr, errorStr;
                Regex pattern;
                startStr = "<td class=\"show_error\" id=\"show_error\">";
                endStr = "&nbsp;</td>";
                pattern = new Regex("(?<=(" + startStr + "))[.\\s\\S]*?(?=(" + endStr + "))", RegexOptions.Multiline | RegexOptions.Singleline);
                errorStr = pattern.Match(response).Value;

                MessageBox.Show(errorStr);

                GetPic();
            }
            return flag;

        }

    }
}
