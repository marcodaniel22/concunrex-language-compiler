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
        public Stack<string> tokens { get; }
        private readonly Dictionary<string, IEnumerable<string>> rules;

        private readonly List<string> knownTerminalWords = new List<string>()
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

        public AnalisadorSintatico(List<string> tokens)
        {
            var reverseTokens = tokens.ToList();
            reverseTokens.Reverse();
            this.tokens = new Stack<string>(reverseTokens);
            this.tokens.Push("S");
            this.rules = Regras.ObterDicionarioDeRegras();
        }

        public Node GetTree(Node node, bool assertTerminal = false, string splitedRuleError = "", string splitedRuleErrorParent = "")
        {
            Node found = null;
            var top = string.Empty;

            if(node.Value == "S")
                tokens.Pop();
            else if (isTerminal(node.Value))
            {
                if (tokens.Peek().Contains(node.Value))
                {
                    tokens.Pop();
                    return node;
                }
                else if (assertTerminal)
                    throw new Exception($"Esperado {splitedRuleError} próximo a {splitedRuleErrorParent.Replace("VAR_", string.Empty)}. Erro: Sintático");
                else
                    return null;
            }
            if (rules.ContainsKey(node.Value))
            {
                var tokenRules = rules[node.Value];
                foreach (var tokenRule in tokenRules)
                {
                    if (tokenRule == "$")
                    {
                        var child = new Node("$");
                        node.Children.Add(child);
                        child.Parent = node;
                        return child;
                    }
                    else if (found == null)
                    {
                        var splitedRules = tokenRule.Split('_').ToList();
                        foreach (var splitedRule in splitedRules)
                        {
                            var child = new Node(splitedRule);
                            if (found != null && found.Value == "$" && node.Value == splitedRule)
                            {
                                found = null;
                                break;
                            }
                            found = GetTree(child, splitedRules.FirstOrDefault() != splitedRule, splitedRule, tokens.Peek());
                            if (found == null)
                                break;
                            else
                            {
                                node.Children.Add(child);
                                child.Parent = node;
                            }
                        }
                    }
                }
            }
            return found;
        }

        private bool isTerminal(string token)
        {
            var regex = new Regex(@"[:=(){}[\]+\-\/*&?<>]");
            return knownTerminalWords.Contains(token) || regex.Match(token).Success;
        }
    }
}
