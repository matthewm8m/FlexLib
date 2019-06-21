//using System;
//using System.Collections.Generic;

//namespace MathemediaConsole
//{
//    public enum ParseAssociativity
//    {
//        LEFT,
//        RIGHT
//    }

//    public class ParseRule
//    {
//        public readonly Type[] Arguments;
//        public readonly ParseAssociativity Associativity;
//        public readonly Func<object[], object> Parse;

//        public ParseRule(Type[] args, ParseAssociativity associate, Func<object[], object> parse)
//        {
//            Arguments = args;
//            Associativity = associate;
//            Parse = parse;
//        }
//    }

//    public class Parser
//    {
//        private readonly List<List<ParseRule>> Rules;

//        public Parser(List<List<ParseRule>> rules)
//        {
//            Rules = rules;
//        }

//        public object Parse(List<Token> tokens)
//        {
//            foreach (List<ParseRule> precedenceRules in Rules)
//            {
//                foreach (ParseRule rule in precedenceRules)
//                {
//                    switch (rule.Associativity)
//                    {
//                        case ParseAssociativity.LEFT:
//                            return null;
//                            break;
//                        case ParseAssociativity.RIGHT:
//                            return null;
//                            break;
//                    }
//                }
//            }
//        }
//    }
//}
