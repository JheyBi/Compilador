using System;

namespace Translator // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            Token l1 = new Token(ETokenType.NUMBER, 22);
            Token l2 = new Token(ETokenType.SUM, 0);

            String? input = Console.ReadLine();
            
            var sintatic = new Sintatic();
            var lexer = sintatic.le_entrada(input);
            sintatic.match(lexer, l1);
            sintatic.match(lexer, l2);
        }
    }
}