using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sintacsis_analizator
{
    public enum TokenType
    {
        Keyword,        // Ключевые слова, например, int, return
        Identifier,     // Идентификаторы (переменные, функции)
        Number,         // Числовые литералы
        Operator,       // Операторы (+, -, *, / и т.д.)
        Delimiter,      // Разделители (;, {, }, (, ))
        Comment,        // Комментарии
        Whitespace,     // Пробелы (их можно игнорировать)
        Unknown         // Неопознанные символы
    }

    public class Token
    {
        public TokenType Type { get; }
        public string Value { get; }

        public Token(TokenType type, string value)
        {
            Type = type;
            Value = value;
        }

        public override string ToString()
        {
            return $"Token({Type}, '{Value}')";
        }
    }
}
