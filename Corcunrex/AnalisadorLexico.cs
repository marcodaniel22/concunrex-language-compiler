using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Corcunrex
{
    public class AnalisadorLexico
    {
        public List<string> terminal { get; }
        public string word { get; set; }
        public string number { get; set; }
        public Regex regex { get; set; }
        public string code { get; }
        public List<string> knownTerminalWords { get; }

        public AnalisadorLexico(string code)
        {
            this.code = code + " ";
            this.terminal = new List<string>();
            this.word = string.Empty;
            this.number = string.Empty;
            this.knownTerminalWords = new List<string>()
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
        }

        public List<string> GetTokens()
        {
            for (int i = 0; i < code.Length; i++)
            {
                var l = code[i].ToString();

                if (isKnownTerminal(l) || isBlackSpace(l) || isBreakLine(l))
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
                decimal idF;
                int idI;
                if (number.Contains(".") && Decimal.TryParse(number, out idF))
                {
                    addTerminalToResult(string.Format("FLOAT_{0}", idF));
                }
                else if (Int32.TryParse(number, out idI))
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

        private bool isLetter(string letter)
        {
            regex = new Regex("[a-zA-Z]");
            return regex.Match(letter).Success;
        }

        private bool isDigit(string digit)
        {
            regex = new Regex("[0-9.]");
            return regex.Match(digit).Success;
        }

        private bool isBlackSpace(string bs)
        {
            return bs == " ";
        }

        private bool isBreakLine(string bl)
        {
            return bl == "\n";
        }

        private bool isKnownTerminal(string kt)
        {
            regex = new Regex(@"[:=(){}[\]+\-\/*&?<>]");
            return regex.Match(kt).Success;
        }

        private void addTerminalToResult(string t)
        {
            if (!string.IsNullOrEmpty(t.Trim()))
                terminal.Add(t.ToUpper());
        }
    }
}
