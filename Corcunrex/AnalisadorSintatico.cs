using Corcunrex.Sintatica;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Text.RegularExpressions;
using System.Collections;

namespace Corcunrex
{
    public class AnalisadorSintatico
    {
        public string code { get; }
        public Stack<string> tokens { get; }
        public List<string> consumedTokens { get; }
        public Dictionary<string, IEnumerable<string>> rules { get; }
        public AnalisadorLexico lexer { get; }
        public Node tree { get; set; }

        private static List<string> knownTerminalWords = new List<string>()
        {
            "VARIABLES",
            "INIT",
            "CODE",
            "LOOP",
            "IN",
            "OUT",
            "INT",
            "FLOAT"
        };

        public AnalisadorSintatico(string code)
        {
            this.code = code;
            this.lexer = new AnalisadorLexico(code);
            this.tokens = new Stack<string>(lexer.GetTokens());
            this.tokens.Push("S");
            this.consumedTokens = new List<string>();
            this.rules = Regras.ObterDicionarioDeRegras();
            this.tree = null;
        }

        public Node GetTree(Node node)
        {
            if (isTerminal(node.Value))
            {
                var top = tokens.Pop();
                if (top == node.Value)
                    return node;
                else
                    throw new Exception("Erro");
            }
            else if(rules.ContainsKey(node.Value))
            {
                var tokenRules = rules[node.Value];
                foreach (var tokenRule in tokenRules)
                {
                    var splitedRules = tokenRule.Split('_').ToList();
                    foreach (var splitedRule in splitedRules)
                    {
                        var child = new Node(splitedRule, node);
                        node.Children.Add(child);
                        GetTree(child);
                    }
                }
            }
            return null;
        }

        public void FindLeaves(Node node, ref List<string> leaves)
        {
            if (node != null)
            {
                if (node.Children.Count == 0)
                {
                    leaves.Add(node.Value);
                }
                else
                {
                    foreach (var child in node.Children)
                    {
                        FindLeaves(child, ref leaves);
                    }
                }
            }
        }

        private bool isTerminal(string token)
        {
            var regex = new Regex(@"[:=(){}[\]+\-\/*&?<>]");
            return knownTerminalWords.Contains(token) || regex.Match(token).Success;
        }
    }
}
