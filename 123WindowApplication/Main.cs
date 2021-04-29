using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace _123WindowApplication
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            lblUser.Text = GlobalVariable.name;

            using (SqlConnection con = new SqlConnection(GlobalVariable.cs))
            {
                con.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter("select * from PaymentView", con);
                DataTable dtbl = new DataTable();
                
                sqlDA.Fill(dtbl);
                dgv.DataSource = dtbl;
                
             
            }
            for(int x = 0; x <= (dgv.Rows.Count-1); x++)
            {
                
                    switch (dgv.Rows[x].Cells[12].Value)
                    {
                        case "FAILED":
                            dgv.Rows[x].Cells[12].Style.BackColor = Color.Red;
                            break;
                        case "COMPLETED":
                            dgv.Rows[x].Cells[12].Style.BackColor = Color.Gray;
                            break;
                        case "PENDING":
                            dgv.Rows[x].Cells[12].Style.BackColor = Color.Yellow;
                            break;
                        default:
                            dgv.Rows[x].Cells[12].Style.BackColor = Color.Gray;
                            break;

                   
                }
              
            
            } 
        }
 
 
        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
         if(dgv.Rows[dgv.CurrentRow.Index].Cells[12].Value.ToString() == "PENDING")
            {
                GlobalVariable.transaction_id = dgv.Rows[dgv.CurrentRow.Index].Cells[1].Value.ToString();
                 Process pm = new Process();
                this.Hide();
                 pm.Show();
                
            }
            else
            {
                MessageBox.Show("No action needed");
            }


        }
    }
}
