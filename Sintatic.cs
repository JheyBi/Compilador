using System;
using System.IO;

namespace Translator {
    public class Sintatic {
        private Token LA; 
        
        
        public Lexer le_entrada(String input){
            var lexer = new Lexer(input);
            LA = lexer.GetNextToken();
            return lexer;
        }

        // public void parser(Lexer lexer){
        //     var token = lexer.GetNextToken();
        //     while(token.Type != ETokenType.EOF){
        //         Console.WriteLine(token);
        //         token = lexer.GetNextToken();
        //     }
        // }

        public void match(Lexer lexer, Token token){
            // Verificar se o token é o esperado
            if(LA.Equals(token)){
                Console.WriteLine("Token: " + LA + " reconhecido");
                LA = lexer.GetNextToken();
            }else{
                throw new Exception("Erro de sintaxe");
            }
        }




    }
}

