using System;
using System.Collections.Generic;

namespace CompilerComponents
{
    enum TokenType
    {
        ID, NUMBER, ASSIGN, PLUS, MULT, EOF
    }

    class Token
    {
        public TokenType Type;
        public string Value;

        public Token(TokenType type, string value)
        {
            Type = type;
            Value = value;
        }

        public override string ToString()
        {
            return $"[{Type}, {Value}]";
        }
    }

    class Lexer
    {
        private string _text;
        private int _pos;

        public Lexer(string text)
        {
            _text = text;
            _pos = 0;
        }

        public List<Token> Tokenize()
        {
            List<Token> tokens = new List<Token>();

            while (_pos < _text.Length)
            {
                char current = _text[_pos];

                if (char.IsWhiteSpace(current))
                {
                    _pos++;
                }
                else if (char.IsLetter(current))
                {
                    string id = "";
                    while (_pos < _text.Length && (char.IsLetterOrDigit(_text[_pos]) || _text[_pos] == '_'))
                    {
                        id += _text[_pos++];
                    }
                    tokens.Add(new Token(TokenType.ID, id));
                }
                else if (char.IsDigit(current))
                {
                    string num = "";
                    while (_pos < _text.Length && char.IsDigit(_text[_pos]))
                    {
                        num += _text[_pos++];
                    }
                    tokens.Add(new Token(TokenType.NUMBER, num));
                }
                else if (current == '=')
                {
                    tokens.Add(new Token(TokenType.ASSIGN, "="));
                    _pos++;
                }
                else if (current == '+')
                {
                    tokens.Add(new Token(TokenType.PLUS, "+"));
                    _pos++;
                }
                else if (current == '*')
                {
                    tokens.Add(new Token(TokenType.MULT, "*"));
                    _pos++;
                }
                else
                {
                    throw new Exception("Unknown character: " + current);
                }
            }

            tokens.Add(new Token(TokenType.EOF, ""));
            return tokens;
        }
    }

    class Parser
    {
        private List<Token> _tokens;
        private int _pos;
        private Dictionary<string, int> _symbolTable;

        public Parser(List<Token> tokens, Dictionary<string, int> symbolTable)
        {
            _tokens = tokens;
            _pos = 0;
            _symbolTable = symbolTable;
        }

        private Token Current()
        {
            if (_pos < _tokens.Count)
                return _tokens[_pos];
            return new Token(TokenType.EOF, "");
        }

        private Token Eat(TokenType expected)
        {
            Token token = Current();
            if (token.Type == expected)
            {
                _pos++;
                return token;
            }
            throw new Exception("Expected " + expected + " but found " + token.Type);
        }

        public void Parse()
        {
            Statement();
        }

        private void Statement()
        {
            string varName = Eat(TokenType.ID).Value;
            Eat(TokenType.ASSIGN);
            int value = Expr();
            _symbolTable[varName] = value;
            Console.WriteLine(">> " + varName + " = " + value);
        }

        private int Expr()
        {
            int result = Term();
            while (Current().Type == TokenType.PLUS)
            {
                Eat(TokenType.PLUS);
                result += Term();
            }
            return result;
        }

        private int Term()
        {
            int result = Factor();
            while (Current().Type == TokenType.MULT)
            {
                Eat(TokenType.MULT);
                result *= Factor();
            }
            return result;
        }

        private int Factor()
        {
            Token token = Current();
            if (token.Type == TokenType.NUMBER)
            {
                return int.Parse(Eat(TokenType.NUMBER).Value);
            }
            else if (token.Type == TokenType.ID)
            {
                string name = Eat(TokenType.ID).Value;
                if (!_symbolTable.ContainsKey(name))
                {
                    throw new Exception("Semantic Error: Variable '" + name + "' is not defined.");
                }
                return _symbolTable[name];
            }
            else
            {
                throw new Exception("Unexpected token: " + token.Type);
            }
        }
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("Enter arithmetic assignment statements (e.g., x = 3 + 4 * 2).");
            Console.WriteLine("Type 'exit' to quit.\n");

            Dictionary<string, int> symbolTable = new Dictionary<string, int>();

            while (true)
            {
                Console.Write(">> ");
                string input = Console.ReadLine().TrimStart('>', ' ').Trim();

                if (string.IsNullOrWhiteSpace(input))
                    continue;

                if (input.ToLower() == "exit")
                    break;

                try
                {
                    Lexer lexer = new Lexer(input);
                    List<Token> tokens = lexer.Tokenize();

                    Parser parser = new Parser(tokens, symbolTable);
                    parser.Parse();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("❌ Error: " + ex.Message);
                }
            }
        }
    }
}
