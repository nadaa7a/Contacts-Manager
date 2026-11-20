using ContactsBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Contacts_Presentation
{
    public partial class ContactManager : Form
    {



        public ContactManager()
        {
            InitializeComponent();
        }

        private void _RefreshContactList()
        {
            dgvAllContacts.DataSource = clsContacts.GetAllContacts();
        }
     
        
     
    private void ContactManager_Load(object sender, EventArgs e)
        {
            _RefreshContactList();

            dgvAllContacts.BackgroundColor = Color.White;
            dgvAllContacts.BorderStyle = BorderStyle.None;

            dgvAllContacts.EnableHeadersVisualStyles = false;

            dgvAllContacts.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(18, 52, 102);

            dgvAllContacts.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;

            dgvAllContacts.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 11, FontStyle.Bold);

            dgvAllContacts.ColumnHeadersDefaultCellStyle.SelectionBackColor =
                Color.FromArgb(18, 52, 102);

            dgvAllContacts.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(234, 241, 252);

            dgvAllContacts.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(190, 215, 255);

            dgvAllContacts.DefaultCellStyle.SelectionForeColor =
                Color.Black;

            dgvAllContacts.RowHeadersVisible = false;

            dgvAllContacts.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvAllContacts.ReadOnly = true;

            dgvAllContacts.AllowUserToAddRows = false;

            dgvAllContacts.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvAllContacts.Columns["Address"].Visible = false;
            dgvAllContacts.Columns["CountryID"].Visible = false;
            dgvAllContacts.Columns["ImagePath"].Visible = true;

            dgvAllContacts.Columns["ContactID"].HeaderText = "ID";
            dgvAllContacts.Columns["FirstName"].HeaderText = "First Name";
            dgvAllContacts.Columns["LastName"].HeaderText = "Last Name";
            dgvAllContacts.Columns["DateOfBirth"].HeaderText = "Date Of Birth";
            dgvAllContacts.Columns["ImagePath"].HeaderText = "Image Path";

            dgvAllContacts.DefaultCellStyle.Font =
                new Font("Segoe UI", 10, FontStyle.Regular);

            dgvAllContacts.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            dgvAllContacts.ColumnHeadersHeight = 40;
            dgvAllContacts.RowTemplate.Height = 32;
        }


        private void editToolStripeMenuItem_Click(object sender, EventArgs e)
        {
            AddEditContact frm = new AddEditContact((int)dgvAllContacts.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshContactList();
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
           if ( MessageBox.Show("Are you sure that you want to delete contact [ " + dgvAllContacts.CurrentRow.Cells[0].Value 
                + " ]" , "Confirm Delete Message " , MessageBoxButtons.OKCancel) == DialogResult.OK)

           {
               if( clsContacts.DeleteContact((int)dgvAllContacts.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Contact Deleted Successfully");
                    _RefreshContactList();
                }
               else
                {
                    MessageBox.Show("Contact Is Not Deleted");
                }
           }
           
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddEditContact frm = new AddEditContact(-1);
            frm.ShowDialog();
            _RefreshContactList();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dgvAllContacts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
