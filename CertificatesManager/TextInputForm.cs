﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace CertificatesManager
{
    public partial class TextInputForm : Form
    {
        public TextInputForm(string caption)
        {
            InitializeComponent();
            Text = caption;
        }

        public string InputText
        {
            get { return textBox1.Text; }
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
