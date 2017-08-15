using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicDatabaseProgram
{
    public partial class BasicDatabaseProgram : Form
    {
        public BasicDatabaseProgram()
        {
            InitializeComponent();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrEmpty(txtSearch.Text))
                {
                    this.barcelonaPlayersTableAdapter.Fill(this.appData.BarcelonaPlayers);
                    barcelonaPlayersBindingSource.DataSource = this.appData.Employees;
                    dataGridView.DataSource = barcelonaPlayersBindingSource;
                }
                    
                else
                {
                    var query = from o in this.appData.BarcelonaPlayers
                                where o.Fullname.Contains(txtSearch.Text) || o.Nationality == txtSearch.Text || o.Age == txtSearch.Text || o.Position == txtSearch.Text || o.Description.Contains(txtSearch.Text)
                                select o;
                    dataGridView.DataSource = query.ToList();

                }
            }

        }

        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Are you sure want to delete this record ? ", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) ;
                barcelonaPlayersBindingSource.RemoveCurrent();
            }
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
                {
                    try
                    {
                        using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "JPEG|*.jpg", ValidateNames = true, Multiselect = false })
                        {
                            if (ofd.ShowDialog() == DialogResult.OK)
                                pictureBox.Image = Image.FromFile(ofd.FileName);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }


                }

        private void buttonNew_Click(object sender, EventArgs e)
                {
                    try
                    {
                        panel.Enabled = true;
                        txtFullName.Focus();
                        this.appData.BarcelonaPlayers.AddBarcelonaPlayersRow(this.appData.BarcelonaPlayers.NewBarcelonaPlayersRow());
                        barcelonaPlayersBindingSource.MoveLast();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        barcelonaPlayersBindingSource.ResetBindings(false);
                    }
                }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            panel.Enabled = true;
            txtFullName.Focus();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            panel.Enabled = false;
            barcelonaPlayersBindingSource.ResetBindings(false);
               
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                        barcelonaPlayersBindingSource.EndEdit();
                        barcelonaPlayersTableAdapter.Update(this.appData.BarcelonaPlayers);
                        panel.Enabled = false; 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                barcelonaPlayersBindingSource.ResetBindings(false);
            }
        }

        private void BasicDatabaseProgram_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'appData.BarcelonaPlayers' table. You can move, or remove it, as needed.
            this.barcelonaPlayersTableAdapter.Fill(this.appData.BarcelonaPlayers);
            barcelonaPlayersBindingSource.DataSource = this.appData.BarcelonaPlayers;

        }
    }
}
