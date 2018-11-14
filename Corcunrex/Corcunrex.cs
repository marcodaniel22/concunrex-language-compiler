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
            var tokens = lexer.GetTokens();
            lbTokens.DataSource = tokens;
            
            try
            {
                var analisadorSintatico = new AnalisadorSintatico(tokens);
                var node = new Node("S");
                if (analisadorSintatico.GetTree(node) == null || analisadorSintatico.tokens.Count > 0)
                    throw new Exception("Erro");
                else
                {
                    var analisarSemantico = new AnalisadorSemantico(tokens);
                    analisarSemantico.Analise();

                }
            }
            catch (Exception ex)
            {
                lbTokens.DataSource = new List<string> { "ERRO" };
            }
        }
    }
}
