﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;

namespace SampleWizard
{

    public partial class Edit_Infill : Form
    {
        decimal total;
        //int i = 0;
        public Edit_Infill()
        {
            InitializeComponent();
        }
        SQLiteConnection cn = new SQLiteConnection(@"Data Source="+ Var.pp + @"\Database.db;Version=3;");
        SQLiteCommand cmd = new SQLiteCommand();
        SQLiteDataAdapter da = new SQLiteDataAdapter();
        DataSet ds = new DataSet();
        BindingSource bs = new BindingSource();
        bool y;
        public Edit_Infill(decimal a, bool x)
        {
            InitializeComponent();
       
            total = a;
            y = x;
            int size1 = Convert.ToInt32(total);
      
            this.SizeGripStyle = SizeGripStyle.Hide;
            this.ShowInTaskbar = false;


        }
        private void Edit_Infill_Load(object sender, EventArgs e)
        {
            cn = new SQLiteConnection(@"Data Source="+ Var.pp + @"\Database.db;Version=3;"); cmd.Connection = cn;
            cn.Open();cmd.CommandText= "begin";cmd.ExecuteNonQuery(); 
            da.SelectCommand = new SQLiteCommand("Select * From Infill", cn);
            ds.Clear();
            da.Fill(ds);
            bs.DataSource = ds.Tables[0];

            TMP.DataBindings.Add(new Binding("Text", bs, "TMP"));
            VLMP.DataBindings.Add(new Binding("Text", bs, "VLMP"));
            VHMP.DataBindings.Add(new Binding("Text", bs, "VHMP"));
            QMPC.DataBindings.Add(new Binding("Text", bs, "QMPC"));
            QMPB.DataBindings.Add(new Binding("Text", bs, "QMPB"));
            QMPJ.DataBindings.Add(new Binding("Text", bs, "QMPJ"));
            EAIW.DataBindings.Add(new Binding("Text", bs, "EAIW"));
            VYIW.DataBindings.Add(new Binding("Text", bs, "VYIW"));
            AIW.DataBindings.Add(new Binding("Text", bs, "AIW"));
            BTA.DataBindings.Add(new Binding("Text", bs, "BTA"));
            GMA.DataBindings.Add(new Binding("Text", bs, "GMA"));
            ETA.DataBindings.Add(new Binding("Text", bs, "ETA"));
            ALPHIW.DataBindings.Add(new Binding("Text", bs, "ALPHIW"));
            IS.DataBindings.Add(new Binding("Checked", bs, "IS_1"));
            AS.DataBindings.Add(new Binding("Text", bs, "AS_1"));
            ZS.DataBindings.Add(new Binding("Text", bs, "ZS"));
            ZBS.DataBindings.Add(new Binding("Text", bs, "ZBS"));
            SK.DataBindings.Add(new Binding("Text", bs, "SK"));
            SP1.DataBindings.Add(new Binding("Text", bs, "SP1"));
            SP2.DataBindings.Add(new Binding("Text", bs, "SP2"));
            MU.DataBindings.Add(new Binding("Text", bs, "MU"));

            
            
            cmd.CommandText= "end";cmd.ExecuteNonQuery();cn.Close();
            label6.Text = "Masonry property type number:   " + (bs.Position + 1) + " of  " + total.ToString();

            if (y)
            {
                groupBox1.Enabled = true;
                groupBox2.Enabled = false;
            }
            else
            {
                groupBox1.Enabled = false;
                groupBox2.Enabled = true;
            }
        }





        private void button_23_Click_1(object sender, EventArgs e)
        {
            bs.MoveNext();
            label6.Text = "Masonry property type number:   " + (bs.Position + 1) + " of  " + total.ToString();

            if (button_23.Text == "Finish")
            {
                this.bs.EndEdit();
                SQLiteCommandBuilder mySQLiteCommandBuilder = new SQLiteCommandBuilder(da);
                this.da.Update(ds.Tables[0]);
                this.Close();
            }
            if (bs.Position + 1 == total)
                button_23.Text = "Finish";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bs.MovePrevious();
            label6.Text = "Masonry property type number:   " + (bs.Position + 1) + " of  " + total.ToString();
            button_23.Text = "Next";
        }

        private void HBS_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.bs.EndEdit();
            SQLiteCommandBuilder mySQLiteCommandBuilder = new SQLiteCommandBuilder(da);
            this.da.Update(ds.Tables[0]);
            this.Close();
        }
        private void updateAll(string column, double value)
        {
            RangeSelector rng = new RangeSelector(ds.Tables[0].Rows.Count, "Select range of " + column + " to be changed [1-" + ds.Tables[0].Rows.Count);
            if (rng.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (rng.SelectedRange.Contains(Convert.ToInt32(ds.Tables[0].Rows[i][0])))
                    {
                        ds.Tables[0].Rows[i][column] = Convert.ToDouble(value);
                    }
                }
            }
        }

        private void btn_setRange(object sender, EventArgs e)
        {
            string prop = ((Control)sender).Name;
            prop = prop.Replace("btn_", "");
            Control[] c = this.Controls.Find(prop, true);
            if (c.Length != 1)
            {
                MessageBox.Show("Can't Find Control [" + prop + "]");
                return;
            }
            updateAll(c[0].Name, Convert.ToDouble(c[0].Text));
        }
        private void btn_IS_Click(object sender, EventArgs e)
        {
            updateAll("IS", Convert.ToDouble(IS.Checked));
        }

        }
    }
