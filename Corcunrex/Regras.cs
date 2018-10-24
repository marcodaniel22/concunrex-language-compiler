using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corcunrex.Sintatica
{
    public class Regras
    {
        private Dictionary<string, IEnumerable<string>> _regras = new Dictionary<string, IEnumerable<string>>();

        public Regras()
        {
            _regras.Add("S", new List<string>()
            {
                "variables{A}init{B}code{C}"
            });

            _regras.Add("A", new List<string>()
            {
                "D{E} A",
                "$"
            });

            _regras.Add("D", new List<string>()
            {
                "int",
                "float"
            });

            _regras.Add("E", new List<string>()
            {
                "var"
            });

            _regras.Add("B", new List<string>()
            {
                "E: F B ",
                "$",
                "H"
            });

            _regras.Add("F", new List<string>()
            {
                "idF",
                "E",
                "G"
            });

            _regras.Add("C", new List<string>()
            {
                "G",
                "H",
                "I",
                "J",
                "K",
                "$"
            });

            _regras.Add("G", new List<string>()
            {
                "L(M)"
            });

            _regras.Add("L", new List<string>()
            {
                "+",
                "-",
                "*",
                "/"
            });

            _regras.Add("M", new List<string>()
            {
                "F M",
                "$"
            });

            _regras.Add("H", new List<string>()
            {
                "N(M)"
            });

            _regras.Add("N", new List<string>()
            {
                "in",
                "out"
            });

            _regras.Add("I", new List<string>()
            {
                "?(K)O"
            });

            _regras.Add("K", new List<string>()
            {
                "Q",
                "R"
            });

            _regras.Add("Q", new List<string>()
            {
                "X(M)"
            });

            _regras.Add("X", new List<string>()
            {
                "&",
                "|"
            });

            _regras.Add("R", new List<string>()
            {
                "T(F F)"
            });

            _regras.Add("T", new List<string>()
            {
                ">",
                "<"
            });

            _regras.Add("O", new List<string>()
            {
                "P",
                "PP"
            });

            _regras.Add("P", new List<string>()
            {
                "[BC]"
            });

            _regras.Add("J", new List<string>()
            {
                "loop(K)P"
            });
        }

        public Dictionary<string, IEnumerable<string>> ObterDicionarioDeRegras()
        {
            return _regras;
        }
    }
}
