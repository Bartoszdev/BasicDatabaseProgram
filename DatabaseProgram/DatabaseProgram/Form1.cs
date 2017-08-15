using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatabaseProgram
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrEmpty(txtSearch.Text))
                {
                    this.playersTableAdapter.Fill(this.appData.Players);
                    playersBindingSource.DataSource = this.appData.Players;
                   
                    dataGridView.DataSource = playersBindingSource;
                }

                else
                {
                    var query = from o in this.appData.Players
                                where o.FullName.Contains(txtSearch.Text) || o.Nationality == txtSearch.Text || o.Age == txtSearch.Text || o.Position == txtSearch.Text || o.Description.Contains(txtSearch.Text)
                                select o;
                   // dataGridView.DataSource = query.ToList();
                    playersBindingSource.DataSource = query.ToList();

                }
            }

        }

        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Are you sure want to delete this record ? ", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) ;
                playersBindingSource.RemoveCurrent();
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
                this.appData.Players.AddPlayersRow(this.appData.Players.NewPlayersRow());
                playersBindingSource.MoveLast();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                playersBindingSource.ResetBindings(false);
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
            playersBindingSource.ResetBindings(false);

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                playersBindingSource.EndEdit();
                playersTableAdapter.Update(this.appData.Players);
                panel.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                playersBindingSource.ResetBindings(false);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'appData.Players' table. You can move, or remove it, as needed.
            this.playersTableAdapter.Fill(this.appData.Players);
            playersBindingSource.DataSource = this.appData.Players;
        }
    }
}
