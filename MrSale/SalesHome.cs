﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Customers;
using System.Data.SqlClient;
using FixerSharp;
using System.Runtime.InteropServices;

namespace MrSale
{
    public partial class SalesHome : Form
    {
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int description, int ReservedValue);

        SqlDataReader readdata;
        SqlConnection sql = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\ezesunday\Documents\mrsalesnew.mdf;Integrated Security=True;Connect Timeout=30");
        DataTable datatable;
        
        public SalesHome()
        {
            InitializeComponent();
            searchCustomerDetails();
            SearchCustomer();
            label24_exchange();
            Autocomplete();
            this.WindowState = FormWindowState.Maximized;
            this.Icon = Properties.Resources.Coherence;
            this.Text = "Mr Sale -Sales Home";
            Color cblue = Color.FromArgb(149, 191,35);
            //Color cblue =  Color.FromArgb(66,139,202);
            this.BackColor = cblue;
            txtCustomerPhoneNumber.Text = "08166307166";

            // setting focus to the First tab
            tabControl1.SelectedIndex = 1;

            // setting tap page properties
            //tab1
            tabPage1.BackColor = Color.FromArgb(10, 5, 20);
            tabPage1.ForeColor = Color.White;

            //tab2
            tabPage2.ToolTipText = "Manage Customers";
            tabPage2.BackColor = Color.FromArgb(10, 5, 20);
            tabPage2.ForeColor = Color.White;
            gbCDeails.ForeColor = Color.White;
            gbCTransaction.ForeColor = Color.White;
            
            //tab3
            tabPage3.ToolTipText = "View Sales Reports";

            lblTotalSalesHeadingtext.BackColor= Color.Transparent;
            lblSalesTotal.BackColor = Color.Transparent;
            label3.BackColor = Color.Transparent;

            // datagrid
            datagridShoppingCart.BackgroundColor = Color.DeepPink;
            datagridCustomerDetails.BackgroundColor = Color.FromArgb(10, 5, 20);
            datagridCustomerDetails.ForeColor = Color.DarkBlue;
            dataGridView3.BackgroundColor = Color.FromArgb(10, 5, 20);

            //customer details
            c_dsearch.ForeColor = Color.DeepPink;
            c_dsearch.BackColor = Color.White;
            c_dsearch.Height = 30;
            Gb_searchCustomer.BackColor = cblue;

            //panels for customer details -- in customer management section
            panel_customerdetails.BackColor = Color.FromArgb(3, 109, 191);
            panel_for_label_bulk_sms.BackColor = Color.FromArgb(3, 109, 191);
            panel_for_bulksms_control.BackColor = Color.FromArgb(3, 109, 191);

            //bulksms group controls
            Gbox_for_customer_message.BackColor = Color.FromArgb(10, 5, 20);
            Gboxfor_customers_phone_number.BackColor = Color.FromArgb(10, 5, 20);


            //bulksms portal

            panel_for_label_bulk_sms.BackColor = cblue;


            //bulksms buttons

            btn_import.BackColor = Color.Transparent ;
            
            
            //button cutomisation

            btn_add.Image = Properties.Resources.btn_add;
            textBox1.BorderStyle = BorderStyle.None;

            // rectangleshape

            rectangleShape1.BackColor = Color.DeepPink;
           // rectangleShape


            // Start Timer
            timer1.Start();
                
            
            
        }

        /// <summary>
        ///  Search Customers details
        ///  for the purpose of updates or to review jobs
        /// </summary>
        public void searchCustomerDetails()
        {
            Customer customer = new Customer();
            customer.CustomerName = txtCustomerName.Text;
            txtCustomerName.AutoCompleteMode = AutoCompleteMode.Append;
            txtCustomerName.AutoCompleteSource = AutoCompleteSource.CustomSource;

            AutoCompleteStringCollection col = new AutoCompleteStringCollection();

            try
            {
                using (SqlCommand command = new SqlCommand("select * from customers", sql))
                {

                    sql.Open();
                    readdata = command.ExecuteReader();

                    while (readdata.Read())
                    {
                        //string CustomerName = readdata.GetString(0);
                        // col.Add(CustomerName);
                    }

                    sql.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        

        private void tabControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Escape)
            {
                
                    Application.Exit();
               
               
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox2.Text != "")
                {
                    double result = Convert.ToDouble(textBox1.Text) * 12;
                    textBox2.Text = result.ToString();
                }else{
                    textBox2.Clear();
                }


            }
            catch (Exception ex)
            {
              // MessageBox.Show(ex.Message);
               
            }
            
          

        }

       
        private void SalesHome_Load(object sender, EventArgs e)
        {
            sql.Open();
            try
            {
                using (SqlCommand command = new SqlCommand("select cs_Name,cs_PhoneNumber,cs_ID FROM customers", sql))
                {
                    SqlDataAdapter sda = new SqlDataAdapter();
                    datatable = new DataTable();
                    sda.SelectCommand = command;
                    sda.Fill(datatable);

                    datagridCustomerDetails.DataSource = datatable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sql.Close();
            }
            
           
 
        }


        
        #region Customer Search Method
        

        #endregion
        #region Closing Form Event Handler
        private void SalesHome_FormClosing(object sender, FormClosingEventArgs e)
        {
            Console.Beep();
            
            DialogResult dr = MessageBox.Show(@"Do you really want to  Exit?", "Exit", MessageBoxButtons.YesNo);
            if (dr==DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {
                e.Cancel = true;
            }
           
        }
        #endregion

        private void label24_Click(object sender, EventArgs e)
        {
           
        }

        #region Foreign Exchange Event Handler

        public void label24_exchange()
        {
            int desc;
            if (InternetGetConnectedState(out desc, 0) == true)
            {


                try
                {
                    //ExchangeRate exrate = Fixer.Rate(Symbols.USD, Symbols.EUR);
                    //double OneUsdtoEuro = exrate.Convert(1);
                    //label24.Text = Math.Ceiling(OneUsdtoEuro).ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }else
            {

                label24.Text = "No Intert Connectivity";
            }
        }
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            this.lbl_time.Text = dateTime.ToString();
        }
        public void Autocomplete()
        {
            c_dsearch.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            c_dsearch.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection col = new AutoCompleteStringCollection();

            // connection from database

            sql.Open();
            try
            {
                using (SqlCommand command = new SqlCommand("select cs_Name,cs_PhoneNumber,cs_ID FROM customers", sql))
                {
                    SqlDataReader readdata;
                    readdata = command.ExecuteReader();
                    while (readdata.Read())
                    {
                        string sugest = readdata.GetString(0).ToString();
                        col.Add(sugest);
                    }
                    c_dsearch.AutoCompleteCustomSource = col;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sql.Close();
            }
            

        }

        private void c_dsearch_TextChanged(object sender, EventArgs e)
        {
            DataView dv = new DataView(datatable);
            dv.RowFilter = string.Format("cs_Name LIKE '%{0}%'",c_dsearch.Text);
            datagridCustomerDetails.DataSource = dv;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            int desc;
            if (InternetGetConnectedState(out desc,0)==true)
            {
                MessageBox.Show("Connected"); 
            }
            else
            {
                MessageBox.Show("Check your Internet Connection ");
            }

        }

        private void CustomerNameTextChange(object sender, EventArgs e)
        {
            DataView div = new DataView();
            div.RowFilter = string.Format("cs_Name LIKE '%{0}%'",txtCustomerName);
            
            //still working on this line
             sql.Open();
            string query = "select cs_PhoneNumber from customers where cs_Name='"+txtCustomerName.Text+"'";

            using (SqlCommand command = new SqlCommand(query, sql))
            {
                readdata = command.ExecuteReader();
                if (readdata.HasRows)
                {
                    while (readdata.Read())
                    {
                        txtCustomerPhoneNumber.Text = readdata["cs_PhoneNumber"].ToString();

                    }

                }
            }
            sql.Close();
	     
        }
        public void SearchCustomer()
        {

            // autocomplete 
            txtCustomerName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtCustomerName.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection complete = new AutoCompleteStringCollection();

            sql.Open();
            try
            {
                using (SqlCommand command = new SqlCommand("Select cs_Name From customers", sql))
                {
                    readdata = command.ExecuteReader();

                    while (readdata.Read())
                    {
                        string textboxsearch = readdata.GetString(0).ToString();
                        complete.Add(textboxsearch);
                    }
                    txtCustomerName.AutoCompleteCustomSource = complete;
                }

            }catch (Exception ex){
                MessageBox.Show(ex.Message);

            }finally{
            sql.Close();
            }

        }

        








    }
}
