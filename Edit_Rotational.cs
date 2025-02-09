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

    public partial class Edit_Rotational : Form
    {
        decimal total;
        
        public Edit_Rotational()
        {
            InitializeComponent();
        }
        
        SQLiteConnection cn = new SQLiteConnection(@"Data Source="+ Var.pp + @"\Database.db;Version=3;");
        SQLiteCommand cmd = new SQLiteCommand();
        SQLiteDataAdapter da = new SQLiteDataAdapter();
        DataSet ds = new DataSet();
        BindingSource bs = new BindingSource();

        public Edit_Rotational(decimal a)
        {
            InitializeComponent();
        
            total = a;
            int size1 = Convert.ToInt32(total);
       
            this.SizeGripStyle = SizeGripStyle.Hide;
            this.ShowInTaskbar = false;



        }
        private void Add_Rotational_Load(object sender, EventArgs e)
        {
            cn = new SQLiteConnection(@"Data Source="+ Var.pp + @"\Database.db;Version=3;"); cmd.Connection = cn;
            cn.Open();cmd.CommandText= "begin";cmd.ExecuteNonQuery(); 
            da.SelectCommand = new SQLiteCommand("Select * From Rotational", cn);
            ds.Clear();
            da.Fill(ds);
            bs.DataSource = ds.Tables[0];

            KHYSR.DataBindings.Add(new Binding("Text", bs, "KHYSR"));
            EI.DataBindings.Add(new Binding("Text", bs, "EI"));
            PCP.DataBindings.Add(new Binding("Text", bs, "PCP"));
            PYP.DataBindings.Add(new Binding("Text", bs, "PYP"));
            UYP.DataBindings.Add(new Binding("Text", bs, "UYP"));
            UUP.DataBindings.Add(new Binding("Text", bs, "UUP"));
            EI3P.DataBindings.Add(new Binding("Text", bs, "EI3P"));
            PCN.DataBindings.Add(new Binding("Text", bs, "PCN"));
            PYN.DataBindings.Add(new Binding("Text", bs, "PYN"));
            UYN.DataBindings.Add(new Binding("Text", bs, "UYN"));
            UUN.DataBindings.Add(new Binding("Text", bs, "UUN"));
            EI3N.DataBindings.Add(new Binding("Text", bs, "EI3N"));
            cmd.CommandText= "end";cmd.ExecuteNonQuery();cn.Close(); 
            label6.Text = "Rotational Spring type Number:   " + (bs.Position + 1) + " of  " + total.ToString();
                
        }



        private void label60_Click(object sender, EventArgs e)
        {

        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {
                    }

        private void NEV_2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button_23_Click_1(object sender, EventArgs e)
        {
            bs.MoveNext();
            label6.Text = "Rotational Spring type Number:   " + (bs.Position + 1) + " of  " + total.ToString();

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
            label6.Text = "Rotational Spring type Number:   " + (bs.Position + 1) + " of  " + total.ToString();
            button_23.Text = "Next";
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

        }
    }
