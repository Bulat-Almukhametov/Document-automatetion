using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CertificatesManager.Properties;

namespace CertificatesManager
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs ea)
        {
            TextInputForm cerOwnerInput = new TextInputForm("имя владельца сертификата");
            var result = cerOwnerInput.ShowDialog();
            if (result != DialogResult.OK) return;

            FolderBrowserDialog choosePath = new FolderBrowserDialog();
            choosePath.Description = "Выберите путь для сохранения сертификата";
            result = choosePath.ShowDialog();
            if (result != DialogResult.OK) return;

            Process myProcess = new Process();

            try
            {
                myProcess.StartInfo.UseShellExecute = false;
                myProcess.StartInfo.Arguments =  String.Format("-r -pe -n \"CN={0}\" -sky exchange -sv", cerOwnerInput.InputText) +
                                                String.Format(" \"{0}\\{1}.pvk\" \"{0}\\{1}(открытый ключ).cer\"", choosePath.SelectedPath, cerOwnerInput.InputText);

                myProcess.StartInfo.FileName = Environment.CurrentDirectory +@"\makecert.exe";
                myProcess.StartInfo.CreateNoWindow = true;
                myProcess.StartInfo.RedirectStandardInput = true;
                myProcess.Start();

                myProcess.WaitForExit();
                myProcess.StartInfo.FileName = Environment.CurrentDirectory + @"\pvk2pfx.exe";
                myProcess.StartInfo.Arguments = String.Format("-pvk \"{0}\\{1}.pvk\" -spc \"{0}\\{1}(открытый ключ).cer\" -pfx \"{0}\\{1}(закрытый ключ).pfx\" -f", choosePath.SelectedPath, cerOwnerInput.InputText);
                myProcess.Start();
                myProcess.WaitForExit();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
