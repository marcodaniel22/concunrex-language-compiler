using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corcunrex
{
    public class AnalisadorSemantico
    {
        private readonly List<string> tokens;

        public AnalisadorSemantico(List<string> tokens)
        {
            this.tokens = tokens;
        }

        public void Analise()
        {
            var state = "VARIABLES";
            var variables = new List<string>();
            foreach (var token in tokens)
            {
                switch (state)
                {
                    case "VARIABLES":
                        if (token.Contains("VAR") && token != "VARIABLES")
                        {
                            if (variables.Any(x => x.Equals(token)))
                                throw new Exception($"{token.Replace("VAR_", string.Empty)} não existe. Erro: Semântico");
                            else
                                variables.Add(token);
                        }
                        if (token == "INIT")
                            state = token;
                        break;
                    case "INIT":
                        if (token.Contains("VAR"))
                        {
                            if (!variables.Any(x => x.Equals(token)))
                                throw new Exception($"{token.Replace("VAR_", string.Empty)} não existe. Erro: Semântico");
                        }
                        break;
                }
            }
        }
    }
}
