using System;

namespace Translator // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            // Entrada de variavel
            Console.WriteLine("Digite a expressão a ser analisada: ");
            String? input = Console.ReadLine();

            // Analisador lexico
            var lexer = new Lexer(input+"\0");

            // Analisador sintatico
            var sintatic = new Sintatic(lexer);
            
            // Inicia a analise sintatica
            sintatic.Parse();
        }
    }
}