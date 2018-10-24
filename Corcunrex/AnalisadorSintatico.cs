using Corcunrex.Sintatica;
using System.Collections.Generic;
using System.Linq;

namespace Corcunrex
{
    public class AnalisadorSintatico
    {
        public string code { get; }
        public Stack<string> tokens { get; }
        public Dictionary<string, IEnumerable<string>> rules { get; }
        public AnalisadorLexico lexer { get; }
        public Node tree { get; set; }

        public AnalisadorSintatico(string code)
        {
            this.code = code;
            this.lexer = new AnalisadorLexico(code);
            this.tokens = new Stack<string>(lexer.GetTokens());
            this.rules = Regras.ObterDicionarioDeRegras();
            this.tree = null;
        }

        public Node GetTree()
        {
            tree = new Node("S", null);
            while(tokens.Count > 0)
            {

            }
        }
    }
}
