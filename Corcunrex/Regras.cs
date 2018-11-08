﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corcunrex.Sintatica
{
    public class Regras
    {
        public static Dictionary<string, IEnumerable<string>> ObterDicionarioDeRegras()
        {
            var _regras = new Dictionary<string, IEnumerable<string>>();

            _regras.Add("S", new List<string>()
            {
                "VARIABLES_{_A_}_INIT_{_B_}_CODE_{_C_}"
            });

            _regras.Add("A", new List<string>()
            {
                "D_{_E_}_A",
                "$"
            });

            _regras.Add("D", new List<string>()
            {
                "INT",
                "FLOAT"
            });

            _regras.Add("E", new List<string>()
            {
                "VAR"
            });

            _regras.Add("B", new List<string>()
            {
                "E_:_F_B",
                "$"
                //"H"
            });

            _regras.Add("F", new List<string>()
            {
                "INT",
                "FLOAT",
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
                "L_(_M_)"
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
                "F_M",
                "$"
            });

            _regras.Add("H", new List<string>()
            {
                "N_(_M_)"
            });

            _regras.Add("N", new List<string>()
            {
                "IN",
                "OUT"
            });

            _regras.Add("I", new List<string>()
            {
                "?_(_K_)_O"
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
                "T_(_F_F_)"
            });

            _regras.Add("T", new List<string>()
            {
                ">",
                "<"
            });

            _regras.Add("O", new List<string>()
            {
                "P",
                "P_P"
            });

            _regras.Add("P", new List<string>()
            {
                "[_B_C_]"
            });

            _regras.Add("J", new List<string>()
            {
                "loop_(_K_)_P"
            });

            return _regras;
        }
    }
}
