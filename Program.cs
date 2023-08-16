using System;

namespace Translator // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            var st = new SymbolTable();
            // Analisador lexico
            var lexer = new Lexer("docs/example.lang", st);

            // Analisador sintatico
            var sintatic = new Sintatic(lexer, st);
            
            // Inicia a analise sintatica
            sintatic.Prog();
            //Console.WriteLine(res);

            Console.WriteLine(st.ToString());
        
        }
    }
}