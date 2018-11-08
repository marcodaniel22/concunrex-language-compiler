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
            "FLOAT",
            "VAR"
        };

        public AnalisadorSintatico(string code)
        {
            this.code = code;
            this.lexer = new AnalisadorLexico(code);
            var reverseTokens = lexer.GetTokens();
            reverseTokens.Reverse();
            this.tokens = new Stack<string>(reverseTokens);
            this.tokens.Push("S");
            this.consumedTokens = new List<string>();
            this.rules = Regras.ObterDicionarioDeRegras();
            this.tree = null;
        }

        public Node GetTree(Node node)
        {
            Node found = null;
            var top = string.Empty;

            if (tokens.Peek().Contains(node.Value))
                top = tokens.Pop();
            if (isTerminal(node.Value) && top.Contains(node.Value))
                return node;
            else if (rules.ContainsKey(node.Value))
            {
                var tokenRules = rules[node.Value];
                foreach (var tokenRule in tokenRules)
                {
                    if (tokenRule == "$")
                    {
                        var child = new Node("$", node);
                        node.Children.Add(child);
                        return child;
                    }
                    if (found == null)
                    {
                        var splitedRules = tokenRule.Split('_').ToList();
                        foreach (var splitedRule in splitedRules)
                        {
                            var child = new Node(splitedRule, node);
                            node.Children.Add(child);
                            found = GetTree(child);
                            if (found == null)
                                break;
                        }
                    }
                }
            }
            return found;
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
