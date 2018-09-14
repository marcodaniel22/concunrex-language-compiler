using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Corcunrex
{
    public class Lexer
    {
        private List<string> terminal;
        private string word;
        private string number;
        private Regex regex;
        private readonly string code;
        private readonly List<string> knownTerminalWords = new List<string>()
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

        public Lexer(string code)
        {
            this.code = code + " ";
            this.terminal = new List<string>();
            this.word = string.Empty;
            this.number = string.Empty;
        }

        public List<string> GetTokens()
        {
            for (int i = 0; i < code.Length; i++)
            {
                var l = code[i].ToString();

                if (isKnownTerminal(l) || isBlackSpace(l))
                {
                    WordVerify();
                    NumVerify();

                    if (isKnownTerminal(l))
                        addTerminalToResult(l);
                }
                else if (isLetter(l))
                {
                    word += l;
                }
                else if (isDigit(l))
                {
                    number += l;
                }
            }

            return terminal;
        }

        private void NumVerify()
        {
            if (!string.IsNullOrEmpty(number))
            {
                if (number.Contains(".") && Decimal.TryParse(number, out decimal idF))
                {
                    addTerminalToResult(string.Format("FLOAT_{0}", idF));
                }
                else if (Int32.TryParse(number, out int idI))
                {
                    addTerminalToResult(string.Format("INT_{0}", idI));
                }
                number = string.Empty;
            }
        }

        private void WordVerify()
        {
            if (!string.IsNullOrEmpty(word))
            {
                if (knownTerminalWords.Contains(word.ToUpper()))
                {
                    addTerminalToResult(word);
                }
                else
                {
                    addTerminalToResult(string.Format("VAR_{0}", word));
                }
                word = string.Empty;
            }
        }

        public bool isLetter(string letter)
        {
            regex = new Regex("[a-zA-Z]");
            return regex.Match(letter).Success;
        }

        public bool isDigit(string digit)
        {
            regex = new Regex("[0-9.]");
            return regex.Match(digit).Success;
        }

        public bool isBlackSpace(string bs)
        {
            return bs == " ";
        }

        public bool isKnownTerminal(string kt)
        {
            regex = new Regex(@"[:=(){}[\]+\-\/*&?<>]");
            return regex.Match(kt).Success;
        }

        public void addTerminalToResult(string t)
        {
            if (!string.IsNullOrEmpty(t.Trim()))
                terminal.Add(t.ToUpper());
        }
    }
}
