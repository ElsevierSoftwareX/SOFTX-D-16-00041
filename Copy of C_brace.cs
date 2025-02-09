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

    public partial class C_brace_sp : Form
    {
        decimal total;
        //int i = 0;
        public C_brace_sp()
        {
            InitializeComponent();
        }

        SQLiteConnection cn = new SQLiteConnection(@"Data Source="+ Var.pp + @"\Database.db;Version=3;");
        SQLiteCommand cmd = new SQLiteCommand();
        SQLiteDataAdapter da = new SQLiteDataAdapter();
        DataSet ds = new DataSet();
        BindingSource bs = new BindingSource();

        public C_brace_sp(decimal a)
        {
            InitializeComponent();
            total = a;
            int size1 = Convert.ToInt32(total);




        }
        private void Add_Edge_Load(object sender, EventArgs e)
        {
            cn = new SQLiteConnection(@"Data Source="+ Var.pp + @"\Database.db;Version=3;"); cmd.Connection = cn;
            cn.Open();cmd.CommandText= "begin";cmd.ExecuteNonQuery();
            da.SelectCommand = new SQLiteCommand("Select * From C_brace_sp", cn);
            ds.Clear();
            da.Fill(ds);
            bs.DataSource = ds.Tables[0];

            KDH.DataBindings.Add(new Binding("Text", bs, "KDH"));
            FYDH.DataBindings.Add(new Binding("Text", bs, "FYDH"));
            RPSTDH.DataBindings.Add(new Binding("Text", bs, "RPSTDH"));
            POWER.DataBindings.Add(new Binding("Text", bs, "POWER"));
            ETA.DataBindings.Add(new Binding("Text", bs, "ETA"));

            cmd.CommandText= "end";cmd.ExecuteNonQuery();cn.Close(); 
            label6.Text = "Brace number:   " + (bs.Position + 1) + " of  " + total.ToString();
                
        }



        private void button_23_Click_1(object sender, EventArgs e)
        {
            bs.MoveNext();
            label6.Text = "Brace number:   " + (bs.Position + 1) + " of  " + total.ToString();

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
            label6.Text = "Brace number:   " + (bs.Position + 1) + " of  " + total.ToString();
            button_23.Text = "Next";
        }

        private void ITW_ValueChanged(object sender, EventArgs e)
        {

        }


        }
    }
                                                                                                              