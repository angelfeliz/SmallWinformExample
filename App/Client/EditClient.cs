﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Services;


namespace App.Client
{
    public partial class EditClient : Form
    {
        private ListClient _listClient;
        public EditClient(DAL.Model.Client client, Form parent)
        {
            InitializeComponent();
            _listClient = parent as ListClient;
            idTxt.Text = client.Id.ToString();
            firstNameTxt.Text = client.FirstName;
            lastNameTxt.Text = client.LastName;
            phoneTxt.Text = client.Phone;
            categoryCbx.DataSource = new BindingSource(ClientServices.GetClientCategorys(), null);
            categoryCbx.DisplayMember = "Value";
            categoryCbx.ValueMember = "Key";
            categoryCbx.Text = ClientServices.GetClientCategorys().Where( x => x.Key == client.Category.ToString()).Select( x => x.Value).ToString();
        }       

        private void saveBtn_Click(object sender, EventArgs e)
        {
            saveBtn.Enabled = false;
            DAL.Model.Client client = new DAL.Model.Client();
            client.Id = int.Parse(idTxt.Text);
            client.FirstName = firstNameTxt.Text;
            client.LastName = lastNameTxt.Text;
            client.Phone = phoneTxt.Text;
            client.Category = int.Parse(categoryCbx.SelectedValue.ToString());

            try
            {
                ClientServices.UpdateClient(client);
                _listClient.clientListDgv.DataSource = Services.ClientServices.ListClient();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                saveBtn.Enabled = true;
            }
            
        }
     }
}
