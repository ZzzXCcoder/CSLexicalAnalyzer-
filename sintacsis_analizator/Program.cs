using System;
using sintacsis_analizator;

class Program
{
    static void Main(string[] args)
    {
        string sourceCode = @"
            int main() {
                int a = 5;
                if (a > 0) {
                    return a;
                }
            }
        ";

        Lexer lexer = new Lexer(sourceCode);
        lexer.AnalizeCode(); // Вызов метода анализа кода
        var tok = lexer._token; // Теперь _token будет заполнен

        foreach (var token in tok)
        {
            Console.WriteLine(token);
        }

        // Ожидание ввода для предотвращения закрытия консоли
        Console.ReadKey();
    }
}