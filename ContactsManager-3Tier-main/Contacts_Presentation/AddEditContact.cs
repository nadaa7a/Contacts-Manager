
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
    public partial class AddEditContact : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };

        private enMode _Mode;
        int _ContactID;
        clsContacts _Contact;


        public AddEditContact(int ContactID)
        {
            InitializeComponent();

            _ContactID = ContactID;

            if (_ContactID != -1)
                _Mode = enMode.Update;
            else
                _Mode = enMode.AddNew;


        }

        private void _Load()
        {
            _FillCountriesInComboBox();
            cbCountry.SelectedIndex = 0;

            if (_Mode == enMode.AddNew)
            {
                lblMode.Text = "📞Add New Contact";
                lblRemoveImage.Visible = false;
                return;
            }

            _Contact = clsContacts.Find(_ContactID);

            if (_Contact == null )
            {
                MessageBox.Show("This form will be closed because No Contact with ID = " + _ContactID);
                this.Close();

                return;
            }

            lblMode.Text = "📞Edit Contact ID = " + _ContactID ;

            lblID.Text = _ContactID.ToString();
            txtFirstName.Text = _Contact.FirstName;
            txtLastName.Text = _Contact.LastName;
            txtEmail.Text = _Contact.Email;
            txtboxPhone.Text = _Contact.Phone;
            txtAddress.Text = _Contact.Address;
            dateTimePicker1.Value = _Contact.DateOfBirth;


            if (_Contact.ImagePath != "")
            {
                pictureBox1.Load(_Contact.ImagePath);
            }

            lblRemoveImage.Visible = (_Contact.ImagePath != "");
            cbCountry.SelectedIndex = cbCountry.FindString(clsCountries.FindByCountryID(_Contact.CountryID).CountryName);


            if (!string.IsNullOrEmpty(_Contact.ImagePath))
            {
                string path = _Contact.ImagePath.Trim('"');

                if (System.IO.File.Exists(path))
                {
                    try
                    {
                        pictureBox1.Load(path);
                        lblRemoveImage.Visible = true;
                    }
                    catch (Exception)
                    {

                        pictureBox1.ImageLocation = null;
                        lblRemoveImage.Visible = false;
                    }
                }
                else
                {
                    lblRemoveImage.Visible = false;
                }
            }
            else
            {
                lblRemoveImage.Visible = false;
            }
        }
        private void _FillCountriesInComboBox()
        {
            DataTable dtCountries = clsCountries.GetAllCountries();

            foreach (DataRow row in dtCountries.Rows)
            {
                cbCountry.Items.Add(row["CountryName"]);

            }
        }

        private void AddEditContact_Load(object sender, EventArgs e)
        {
            _Load();
        }

        

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnChooseImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            

        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void lblContactInfo_Click(object sender, EventArgs e)
        {

        }

        private void lblSubtitle_Click(object sender, EventArgs e)
        {

        }

        private void lblSetImage_Click(object sender, EventArgs e)
        {

            try
            {
                openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.ImageLocation = openFileDialog1.FileName;
                    lblRemoveImage.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Set Image Error");
            }
        }    
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {

            int CountryID = clsCountries.FindByCountryName(cbCountry.Text).CountryID;

            _Contact.FirstName = txtFirstName.Text;
            _Contact.LastName = txtLastName.Text;
            _Contact.Email = txtEmail.Text;
            _Contact.Phone = txtboxPhone.Text;
            _Contact.Address = txtAddress.Text;
            _Contact.DateOfBirth = dateTimePicker1.Value;
            _Contact.CountryID = CountryID;

            if (pictureBox1.ImageLocation != null)
                _Contact.ImagePath = pictureBox1.ImageLocation;
            else
                _Contact.ImagePath = "";

            if (_Contact.Save())
            {
                MessageBox.Show("Data Saved Successfully ! ");

            }
            else
            {
                MessageBox.Show("Error : Data Is Not Saved Successfully !");
            }

            _Mode = enMode.Update;
            lblMode.Text = "📞Edit Contact ID = " + _Contact.ID;
            lblContactID.Text = _Contact.ID.ToString();
        }

        private void lblRemoveImage_Click(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = null;
            lblRemoveImage.Visible = false;
            MessageBox.Show("Image Removed Successfully:)", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
