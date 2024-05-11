using Back_Dr.Sale;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrystalReport.Components
{
    public partial class LoadRotray : UserControl
    {
        private string sql;

        public LoadRotray()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           

            if (checkBox1.Checked)
            {
                if(checkBox2.Checked)
                {

                      sql = $"select cust_name,cust_phone,cust_addres,pcity,cust_date,Balance from Customer where Balance <0 AND Balance <0 AND cust_date >= '{dateTimePicker1.Text} 00:00:00' AND  cust_date <= '{dateTimePicker2.Text} 23:59:59' ; ";
                    List<dynamic> bal = MainEngine_.GetDataScript<dynamic>(sql).ToList();

                    dataGridView1.Rows.Clear();
                    foreach (var item in bal)
                    {
                        dataGridView1.Rows.Add(item.cust_name,item.cust_phone,item.cust_addres,item.pcity,item.Balance);
                    }

                }
                else
                {



                     sql = $"select cust_name,cust_phone,cust_addres,pcity,cust_date,Balance from Customer";
                    List<dynamic> bal = MainEngine_.GetDataScript<dynamic>(sql).ToList();

                    dataGridView1.Rows.Clear();
                    foreach (var item in bal)
                    {
                        dataGridView1.Rows.Add(item.cust_name, item.cust_phone, item.cust_addres, item.pcity,item.cust_date, item.Balance);
                    }


                    //qry without date all

                }

            }

            else if (city.Text != "" && area.Text != "")
            {
                if (checkBox2.Checked)
                {
                      sql = $"select cust_name,cust_phone,cust_addres,pcity,cust_date,Balance from Customer where Balance <0 AND pcity='" + city.Text + "' AND cust_addres='" + area.Text + "' AND cust_date >= '" + dateTimePicker1.Text + " 00:00:00' AND  cust_date <= '" + dateTimePicker2.Text + " 23:59:59'  ";
                    List<dynamic> bal = MainEngine_.GetDataScript<dynamic>(sql).ToList();

                    dataGridView1.Rows.Clear();
                    foreach (var item in bal)
                    {
                        dataGridView1.Rows.Add(item.cust_name, item.cust_phone, item.cust_addres, item.pcity, item.cust_date, item.Balance);
                    }
                }
                else
                {

                    
                    sql = $"select cust_name,cust_phone,cust_addres,pcity,cust_date,Balance from Customer where Balance <0 AND pcity='" + city.Text + "' AND cust_addres='" + area.Text + "' ";
                    List<dynamic> balS = MainEngine_.GetDataScript<dynamic>(sql).ToList();

                    dataGridView1.Rows.Clear();
                    foreach (var item in balS)
                    {
                        dataGridView1.Rows.Add(item.cust_name, item.cust_phone, item.cust_addres, item.pcity, item.cust_date, item.Balance);
                    }
                }

            }





            else if (city.Text != "")
            {
                if (checkBox2.Checked)
                {
                    
                    sql = $"select cust_name,cust_phone,cust_addres,pcity,cust_date,Balance from Customer where Balance <0 AND pcity='"+city.Text+ "' AND cust_date >= '"+dateTimePicker1.Text+" 00:00:00' AND  cust_date <= '"+dateTimePicker2.Text+" 23:59:59' ; ";
                    List<dynamic> bal = MainEngine_.GetDataScript<dynamic>(sql).ToList();

                    dataGridView1.Rows.Clear();
                    foreach (var item in bal)
                    {
                        dataGridView1.Rows.Add(item.cust_name, item.cust_phone, item.cust_addres, item.pcity,item.cust_date, item.Balance);
                    }

                }
                else
                {
                    
                    sql = $"select cust_name,cust_phone,cust_addres,pcity,cust_date,Balance from Customer where Balance <0 AND pcity='" + city.Text + "'";
                    List<dynamic> bal = MainEngine_.GetDataScript<dynamic>(sql).ToList();

                    dataGridView1.Rows.Clear();
                    foreach (var item in bal)
                    {
                        dataGridView1.Rows.Add(item.cust_name, item.cust_phone, item.cust_addres, item.pcity,item.cust_date, item.Balance);
                    }
                }

            }


           

            sumqty.Text = ((dataGridView1.Rows.Count)-1).ToString();

            double totalSum = dataGridView1.Rows.Cast<DataGridViewRow>()
            .Sum(row => Convert.ToDouble(row.Cells[5].Value ?? 0));

            totalamt.Text = totalSum.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReportStd rst = new ReportStd(sql, "AMIRSHAKH123",area.Text,city.Text);
            rst.Show();
        }

        private void LoadRotray_Load(object sender, EventArgs e)
        {
            try
            {
                city.Items.AddRange(MainEngine_.GetDataScript<string>("select DISTINCT pcity from Customer").ToArray());


            }
            catch (Exception ex)
            {
                 
            }
        }

        private void area_SelectedIndexChanged(object sender, EventArgs e)
        {
             

           

        }

        private void area_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void city_SelectedIndexChanged(object sender, EventArgs e)
        {
            area.Items.Clear();
            area.Items.AddRange(MainEngine_.GetDataScript<string>("select DISTINCT cust_addres from Customer where pcity = '" + city.Text + "'").ToArray());

        }
    }
}
