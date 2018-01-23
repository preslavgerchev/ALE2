namespace AutomataLogicEngineering2.RegExParser
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Symbols;
    using Utils;

    public static class NodeTreeCreator
    {
       
        public static Node Initialize(string input)
        {
            var symbols = NodeTreeCreator.ParseToSymbols(input);

            return NodeTreeCreator.CreateTree(symbols);
        }

        private static Node CreateTree(List<Symbol> input)
        {
            Node parentNode = null;
            foreach (var symbol in input)
            {
                if (symbol is Operator)
                {
                    if (parentNode == null)
                    {
                        parentNode = new Node(symbol);
                    }
                    else
                    {
                        var node = new Node(symbol);
                        parentNode.AddChild(node);
                        parentNode = node;
                    }
                }
                else if (symbol is Predicate)
                {
                    if (parentNode == null)
                    {
                        parentNode = new Node(symbol);
                    }
                    else
                    {
                        var node = new Node(symbol);
                        parentNode.AddChild(node);
                    }
                }
                else if (symbol is Parenthesis parenthesis)
                {
                    if (parenthesis.Side == ParenthesisSide.Closing)
                    {
                        if (parentNode?.Parent != null)
                        {
                            parentNode = parentNode?.Parent;
                        }
                    }
                }
            }

            return parentNode;
        }

        private static List<Symbol> ParseToSymbols(string input)
        {
            var allChars = new Regex("\\s+").Replace(input, string.Empty).ToCharArray();
            return allChars.Select(ToSymbol).ToList();
        }

        private static Symbol ToSymbol(char inputChar)
        {
            switch (inputChar)
            {
                case '(':
                    return new Parenthesis(inputChar, ParenthesisSide.Opening);
                case ')':
                    return new Parenthesis(inputChar, ParenthesisSide.Closing);
                case '.':
                    return new Operator(inputChar, OperatorType.Concatenation);
                case '*':
                    return new Operator(inputChar, OperatorType.KleeneStar);
                case '|':
                    return new Operator(inputChar, OperatorType.Union);
                case ',':
                    return new Separator(inputChar);
                default:
                    return new Predicate(inputChar.ParseChar());
            }
        }
    }
}