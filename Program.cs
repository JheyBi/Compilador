using System;

namespace Translator // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            // Entrada de variavel
            Console.Write("Digite a expressão a ser analisada: ");
            String? input = Console.ReadLine();

            var st = new SymbolTable();
            // Analisador lexico
            var lexer = new Lexer(input+"\0", st);

            // Analisador sintatico
            var sintatic = new Sintatic(lexer);
            
            // Inicia a analise sintatica
            var res = sintatic.Parse();
            Console.WriteLine(res);


        // symbolTable.Put("a", 1);
        // symbolTable.Put("b", 2);
        // symbolTable.Put("c", 3);
        // symbolTable.Put("d", 4);
        // symbolTable.Put("e", 5);
        // symbolTable.Put("11");
        Console.WriteLine(st.ToString());
        
        }
    }
}