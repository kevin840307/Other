using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RandomProgram
{
    public partial class ShowWordContent : Form
    {
        public ShowWordContent()
        {
            InitializeComponent();
        }

        public ShowWordContent(string content)
        {
            InitializeComponent();
            label1.Text = content;
        }
    }
}
