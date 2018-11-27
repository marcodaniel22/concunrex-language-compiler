using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            try
            {
                var code = txtBoxCode.Text;
                var lexer = new AnalisadorLexico(code);
                var tokens = lexer.GetTokens();
                var analisadorSintatico = new AnalisadorSintatico(tokens);
                var node = new Node("S");
                var x = analisadorSintatico.GetTree(node);
                if (x == null || analisadorSintatico.tokens.Count > 0)
                    throw new Exception("Erro");
                else
                {
                    var analisarSemantico = new AnalisadorSemantico(tokens);
                    analisarSemantico.Analise();
                    lbTokens.DataSource = new List<string> { "Compilado com sucesso." };

                    //Fazer a conversão para C

                    string codeC = "int main(){" + code + "}";

                    File.WriteAllText("C:\\corcunrex\\code.c", codeC);
                }
            }
            catch (Exception ex)
            {
                lbTokens.DataSource = new List<string> { ex.Message };
            }
        }
    }
}
