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

using System.Reflection;


namespace WindowsForms.chaoxing
{
    public partial class FormSchool : Form
    {
        private HttpRequestClient http;
        private JToken citys;
        private JToken schools;
        private string selectedRowData;


        public FormSchool()
        {
            //设置重绘方式
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();

            //初始化组件
            InitializeComponent();

            //防止大数据滚动闪烁
            Type dgvType = gridViewSchool.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(gridViewSchool, true, null);

            //初始化http
            http = new HttpRequestClient(true);
        }

        private void FormSchool_Load(object sender, EventArgs e)
        {
            GetCitys();
            GetSchools();

        }


        private void GetCitys()
        {
            string url = @"http://passport2.chaoxing.com/org/allcity";
            string header = @"
                Accept: */*
                Accept-Encoding: gzip, deflate
                Accept-Language: zh-CN,zh;q=0.9
                Cache-Control: no-cache
                Connection: keep-alive
                Content-Length: 13
                Content-Type: application/x-www-form-urlencoded; charset=UTF-8
                Cookie: source=""; JSESSIONID=BF20476348793503112CFD992E4EF272; route=6cf035fa58a23c9bbcbce99d20a53f7a
                Host: passport2.chaoxing.com
                Origin: http://passport2.chaoxing.com
                Pragma: no-cache
                Referer: http://passport2.chaoxing.com/login
                User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/77.0.3865.120 Safari/537.36
                X-Requested-With: XMLHttpRequest
            ";
            string response = "";

            response = http.httpPost(url, header, "pid=undefined", Encoding.UTF8);
            if (string.IsNullOrEmpty(response))
            {
                return;
            }
            //JObject json = (JObject)JsonConvert.DeserializeObject(response);
            JObject json = JObject.Parse(response);
            citys = json.GetValue("citys");
            //foreach(object item in citys)
            //{
            //    Console.Write(item);
            //}
            gridViewRegion.DataSource = null;
            gridViewRegion.DataSource = citys;
            //gridViewRegion.Rows.Add()
            foreach (DataGridViewRow item in gridViewRegion.SelectedRows)
            {
                item.Selected = false;
            }
        }

        private void GetSchools(int cityid = 3)
        {
            string url = @"http://passport2.chaoxing.com/org/froms";
            string header = @"
                Accept: */*
                Accept-Encoding: gzip, deflate
                Accept-Language: zh-CN,zh;q=0.9
                Cache-Control: no-cache
                Connection: keep-alive
                Content-Length: 13
                Content-Type: application/x-www-form-urlencoded; charset=UTF-8
                Cookie: source=""; JSESSIONID=BF20476348793503112CFD992E4EF272; route=6cf035fa58a23c9bbcbce99d20a53f7a
                Host: passport2.chaoxing.com
                Origin: http://passport2.chaoxing.com
                Pragma: no-cache
                Referer: http://passport2.chaoxing.com/login
                User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/77.0.3865.120 Safari/537.36
                X-Requested-With: XMLHttpRequest
            ";
            string response = "";

            response = http.httpPost(url, header, "allowjoin=0&cityid="+ cityid +"&letter=&pid=-1", Encoding.UTF8);
            if (string.IsNullOrEmpty(response))
            {
                return;
            }
            //JObject json = (JObject)JsonConvert.DeserializeObject(response);
            JObject json = JObject.Parse(response);
            schools = json.GetValue("froms");
            //foreach(object item in citys)
            //{
            //    Console.Write(item);
            //}
            gridViewSchool.DataSource = null;
            gridViewSchool.DataSource = schools;
            //gridViewRegion.Rows.Add()
            foreach (DataGridViewRow item in gridViewSchool.SelectedRows)
            {
                item.Selected = false;
            }
            selectedRowData = "";
        }

        private void SearchSchools(string keyword)
        {
            string url = @"http://passport2.chaoxing.com/org/searchforms";
            string header = @"
                Accept: */*
                Accept-Encoding: gzip, deflate
                Accept-Language: zh-CN,zh;q=0.9
                Cache-Control: no-cache
                Connection: keep-alive
                Content-Length: 13
                Content-Type: application/x-www-form-urlencoded; charset=UTF-8
                Cookie: source=""; JSESSIONID=BF20476348793503112CFD992E4EF272; route=6cf035fa58a23c9bbcbce99d20a53f7a
                Host: passport2.chaoxing.com
                Origin: http://passport2.chaoxing.com
                Pragma: no-cache
                Referer: http://passport2.chaoxing.com/login
                User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/77.0.3865.120 Safari/537.36
                X-Requested-With: XMLHttpRequest
            ";
            string response = "";

            response = http.httpPost(url, header, "allowjoin=0&filter=" + keyword + "&pid=-1", Encoding.UTF8);
            if (string.IsNullOrEmpty(response))
            {
                return;
            }
            //JObject json = (JObject)JsonConvert.DeserializeObject(response);
            JObject json = JObject.Parse(response);
            schools = json.GetValue("froms");
            //foreach(object item in citys)
            //{
            //    Console.Write(item);
            //}
            gridViewSchool.DataSource = null;
            gridViewSchool.DataSource = schools;
            //gridViewRegion.Rows.Add()
            foreach (DataGridViewRow item in gridViewSchool.SelectedRows)
            {
                item.Selected = false;
            }
            selectedRowData = "";
        }

        private void gridViewRegion_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1) {
                int i = -1;
                foreach(JToken item in citys)
                {
                    i++;
                    if(e.RowIndex == i)
                    {
                        GetSchools(item.Value<int>("id"));
                        break;
                    }
                }

            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = textBoxSearch.Text;
            if (!string.IsNullOrEmpty(textBoxSearch.Text))
            {
                foreach (DataGridViewRow item in gridViewRegion.SelectedRows)
                {
                    item.Selected = false;
                }
                foreach (DataGridViewRow item in gridViewSchool.SelectedRows)
                {
                    item.Selected = false;
                }
                SearchSchools(keyword);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedRowData))
            {
                MessageBox.Show("请选择学校", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                DialogResult = DialogResult.OK;
            }
        }

        public string GetSelectedSchool()
        {
            return selectedRowData;
        }

        private void gridViewSchool_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                int i = -1;
                foreach (JToken item in schools)
                {
                    i++;
                    if (e.RowIndex == i)
                    {

                        selectedRowData = item.ToString();
                        break;
                    }
                }

            }
        }
    }
}
