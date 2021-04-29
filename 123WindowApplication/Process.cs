using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace _123WindowApplication
{
    public partial class Process : Form
    {
        public Process()
        {
            InitializeComponent();
        }

        private void Process_Load(object sender, EventArgs e)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //To where your opendialog box get starting location. My initial directory location is desktop.
            openFileDialog1.InitialDirectory = "C://Desktop";
            //Your opendialog box title name.
            openFileDialog1.Title = "Select image to be upload.";
            //which type image format you want to upload in database. just add them.
            openFileDialog1.Filter = "Image Only(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            //FilterIndex property represents the index of the filter currently selected in the file dialog box.
            openFileDialog1.FilterIndex = 1;
            try
            {
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (openFileDialog1.CheckFileExists)
                    {
                        string path = System.IO.Path.GetFullPath(openFileDialog1.FileName);
                        label1.Text = path;
                        pictureBox1.Image = new Bitmap(openFileDialog1.FileName);
                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

                        //we already define our connection globaly. We are just calling the object of connection.

                    }
                }
                else
                {
                    MessageBox.Show("Please Upload image.");
                }
            }
            catch (Exception ex)
            {
                //it will give if file is already exits..
                MessageBox.Show(ex.Message);
            }
            upload();
            this.Hide();
            Main frmMain = new Main();
            frmMain.Show();
      }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedItem.ToString() == "COMPLETED")
            {
                button2.Enabled = false;
            }
            else
            {
                button2.Enabled = true;
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                
                    //we already define our connection globaly. We are just calling the object of connection.
                    SqlConnection con = new SqlConnection(GlobalVariable.cs);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update Approval set Status = 'FAILED' where ABC_TransactionID = '" + GlobalVariable.transaction_id + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Process Done");
                this.Hide();
                Main frmMain = new Main();
                frmMain.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        public void upload()
        {

            try
            {
                string filename = System.IO.Path.GetFileName(openFileDialog1.FileName);
                if (filename == null)
                {
                    MessageBox.Show("Please select a valid image.");
                }
                else
                {
                    //we already define our connection globaly. We are just calling the object of connection.
                    SqlConnection con = new SqlConnection(GlobalVariable.cs);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update Approval set Attachment = '\\Image\\" + filename + "', Status = 'COMPLETED' where ABC_TransactionID = '" + GlobalVariable.transaction_id + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Process Done");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "File Already exits");
            }

        }
    }
}
