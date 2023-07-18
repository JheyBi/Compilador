using System;
using System.IO;

namespace Translator {
    public class Sintatic {
        private Token _LA; 
        private Lexer _lexer;

        public Sintatic(Lexer lexer){
            _lexer = lexer;
            _LA = lexer.GetNextToken();
        }


        // Verifica se a entrada é válida
        public void Parse(){
            E();
            if(_LA.Type != ETokenType.EOF){
                throw new Exception("ERRO de sintaxe: " + _LA.Type + " não esperado, esperava EOF");
            }
            else {
                Console.WriteLine("Entrada válida");
            }
        }

        public void match(ETokenType token){
            // Verificar se o token é o esperado
            if(_LA.Type == token){
                //Console.WriteLine("Token: " + _LA + " reconhecido");
                _LA = _lexer.GetNextToken();
                //Console.WriteLine("Proximo Token: " + _LA + " a ser analisado");
            }else{
                throw new Exception("Erro de sintaxe");
            }
        }

        public void E(){
            //Console.WriteLine("Estado: E ->  LA:" + _LA.Value);
            T();
            X();
        }

        public void X(){
            //Console.WriteLine("Estado: X ->  LA:" + _LA.Value);
            if(_LA.Type==ETokenType.SUM){
                match(ETokenType.SUM);
                E();
            }
            else if(_LA.Type==ETokenType.SUB){
                match(ETokenType.SUB);
                E();
            }
            
            else{
                if(_LA.Type!=ETokenType.EOF && _LA.Type!=ETokenType.CLOSE){
                    throw new Exception("ERRO de sintaxe: " + _LA.Type + " não esperado, esperava + ou -");
                }
            }
        }

        public void T(){
            //Console.WriteLine("Estado: T ->  LA:" + _LA.Value);
            F();
            Y();
        }

        public void Y(){
            //Console.WriteLine("Estado: Y ->  LA:" + _LA.Value);
            if(_LA.Type==ETokenType.MUL){
                match(ETokenType.MUL);
                E();
            }
            else if(_LA.Type==ETokenType.DIV){
                match(ETokenType.DIV);
                E();
            }
            else{
                if(_LA.Type!=ETokenType.EOF && _LA.Type!=ETokenType.CLOSE && _LA.Type!=ETokenType.SUM && _LA.Type!=ETokenType.SUB){
                    throw new Exception("ERRO de sintaxe: " + _LA.Type + " não esperado, esperava * ou /");
                }
            }
        }

        public void F(){
            //Console.WriteLine("Estado: F ->  LA:" + _LA.Value);
            if(_LA.Type==ETokenType.NUMBER){
                match(ETokenType.NUMBER);
            }
            else if(_LA.Type==ETokenType.OPEN){
                match(ETokenType.OPEN);
                E();
                match(ETokenType.CLOSE);
            }
            else{
                if(_LA.Type!=ETokenType.EOF && _LA.Type!=ETokenType.CLOSE && _LA.Type!=ETokenType.SUM && _LA.Type!=ETokenType.SUB && _LA.Type!=ETokenType.MUL && _LA.Type!=ETokenType.DIV){
                    throw new Exception("ERRO de sintaxe: " + _LA.Type + " não esperado, esperava número ou (");
                }
            }
        }


    }
}

