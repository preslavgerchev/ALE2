namespace AutomataLogicEngineering2.RegExParser
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Symbols;
    using Utils;

    /// <summary>
    /// A static class, responsible for creating a tree out of a given string input.
    /// </summary>
    public static class NodeTreeCreator
    {
        /// <summary>
        /// Parses, validates and converts the input into a node tree, returning the 
        /// root node of the tree.
        /// </summary>
        /// <param name="input">The string input.</param>
        /// <returns>The root node of the tree.</returns>
        public static Node Initialize(string input)
        {
            var symbols = NodeTreeCreator.ParseToSymbols(input);

            return NodeTreeCreator.CreateTree(symbols);
        }

        /// <summary>
        /// Creates a tree, given the parsed input as a list of symbols.
        /// </summary>
        /// <param name="input">The list of symbols.</param>
        /// <returns>The root node of the tree.</returns>
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

        /// <summary>
        /// Parses the given string input to a list of <see cref="Symbol"/> instances.
        /// </summary>
        /// <param name="input">The string input.</param>
        /// <returns>A list of <see cref="Symbol"/> instances.</returns>
        private static List<Symbol> ParseToSymbols(string input)
        {
            var allChars = new Regex("\\s+").Replace(input, string.Empty).ToCharArray();
            return allChars.Select(ToSymbol).ToList();
        }

        /// <summary>
        /// Parses a given char to its corresponding symbol.
        /// </summary>
        /// <param name="inputChar">The char to parse.</param>
        /// <returns>A symbol that corresponds to the given char.</returns>
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