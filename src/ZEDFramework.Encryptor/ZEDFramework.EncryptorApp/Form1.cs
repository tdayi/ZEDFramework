using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZEDFramework.Collection.Infrastructure.Security.Encryption;

namespace ZEDFramework.EncryptorApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(txtInput.Text))
                {
                    MessageBox.Show("Lütfen giriş parametresini boş bırakmayınız!");
                    return;
                }

                if (rdbEncrypt.Checked)
                {
                    txtOuput.Text = new CryptoHelper().Encrypt(txtInput.Text);
                }
                else
                {
                    txtOuput.Text = new CryptoHelper().Decrypt(txtInput.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
