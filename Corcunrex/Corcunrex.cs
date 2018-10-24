using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Corcunrex
{
    public partial class Corcunrex : Form
    {
        public Corcunrex()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var code = txtBoxCode.Text;
            var lexer = new AnalisadorLexico(code);
            lbTokens.DataSource = lexer.GetTokens();

            var analisadorSintatico = new AnalisadorSintatico(code);
        }
    }
}
