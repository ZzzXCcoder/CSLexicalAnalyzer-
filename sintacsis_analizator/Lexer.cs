using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sintacsis_analizator
{
    public class Lexer
    {
        private readonly string _sourceCode;
        private int _position = 0;
        private int _supprotPosition = 0;
        public List<Token> _token;
        public Lexer(string sourceCode)
        {
            _sourceCode = sourceCode;
            _token = new List<Token>();   
        }
        private char PeekNextChar()
        {
            return _position + 1 < _sourceCode.Length ? _sourceCode[_position + 1] : '\0';
        }
        private void NextPosition() => _position++;
        private char CurrentChar => _position >= _sourceCode.Length ? '\0' : _sourceCode[_position];

        public void AnalizeCode()
        {
            while (_position < _sourceCode.Length)
            {
                char current = CurrentChar;
                if (IsSpace(current))
                {

                }
                else if (char.IsLetter(current) || current == '_')
                {
                    _token.Add(ParseIdentifierOrKeyword());
                }
                else if (char.IsNumber(current))
                {
                    _token.Add(ParseToNumber());
                }
                else if(IsComment())
                {
                    _token.Add(ParseToComment());
                }
                else if (IsOperator(current))
                {
                    _token.Add(ParseToOperator(current));
                }
                else if(IsDelimiter(current))
                {
                    _token.Add(ParseIsDelimiter(current));
                }

                NextPosition();
                    
            }
            

        }

        private Token ParseToUnknown(char current)
        {
            
           Console.WriteLine(current);
           return new Token(TokenType.Unknown, Convert.ToString(current));
            
        }
        private Token ParseIsDelimiter(char current)
        {
            return new Token(TokenType.Delimiter, Convert.ToString(current));
        }
        private Token ParseToOperator(char current)
        {
            return new Token(TokenType.Operator, Convert.ToString(current));
        }

        private bool IsOperator(char current)
        {
            return "+-*/=%<>!".Contains(current);
        }

        private bool IsDelimiter(char current)
        {
            return ";{}(),".Contains(current);
        }
        private bool IsComment()
        {
            if (_sourceCode[_position] == '/' && _sourceCode[_position+1] == '/')
            {
                return true;
            }
            if (_sourceCode[_position] == '/' && _sourceCode[_position + 1] == '*')
            {
                return true;
            }
            return false;
        }
        private Token ParseToComment()
        {
            string comment = "";
            if (_position + 1 == '/')
            {
                int endposition = _position + 2;
                
                while (_sourceCode[endposition] != '\n' || _sourceCode[endposition] < _sourceCode.Length - 1)
                {
                    comment += _sourceCode[endposition];
                }
            }
            else if (_position + 1 == '*')
            {
                int endposition = _position + 2;
                
                while ((_sourceCode[endposition] != '*' && _sourceCode[endposition+1] != '/') || endposition < _sourceCode.Length - 1)
                {
                    comment += _sourceCode[endposition];
                }
            }
            return new Token(TokenType.Comment, comment);
        }
        private Token ParseIdentifierOrKeyword()
        {
            int endposition = _position;
            while (char.IsLetterOrDigit(_sourceCode[endposition]) && endposition < _sourceCode.Length)
            {
                Console.WriteLine(_sourceCode[endposition]);
                endposition++;
            }
            string value = _sourceCode.Substring(_position, endposition - _position);
            _position = endposition - 1;
            if (IsKeyWord(value))
            {
                return new Token(TokenType.Keyword, value);
            }
            return new Token(TokenType.Identifier, value);

        }
        private Token ParseToNumber()
        {
            int endposition = _position;
            while (char.IsNumber(_sourceCode[endposition]))
            {
                endposition++;
            }
            string number = _sourceCode.Substring(_position, endposition - _position);
            _position = endposition;
            return new Token(TokenType.Number, number);
            
        }
        private bool IsKeyWord(string value)
        {
            string[] keywords = { "int", "return", "if", "else", "while", "for", "void", "Main"};
            return keywords.Contains(value);
        }

        private bool IsSpace(char current)
        {
            if (current == ' ')
            {
                return true;
            }
            return false;
            
        }
    }
}
