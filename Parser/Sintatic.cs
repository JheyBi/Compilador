using System;
using System.IO;

namespace Translator {
    public class Sintatic {
        private Token _LA; 
        private Lexer _lexer;
        private SymbolTable _symbolTable;

        public Sintatic(Lexer lexer, SymbolTable st){
            _lexer = lexer;
            _LA = lexer.GetNextToken();
            _symbolTable = st;
        }


        // Verifica se a entrada é válida
        public int Parse(){
            //Console.WriteLine("Entrou no PARSE");
            var res = E();
            
            // if(_LA.Type != ETokenType.EOF){
            //     throw new Exception("ERRO de sintaxe: " + _LA.Type + " não esperado, esperava EOF");
            // }
            // else {
            //     //Console.WriteLine("Entrada válida");
            // }
            //Console.WriteLine("Resultado: " + res);
            return res;
        }

        public void Match(ETokenType token){
            // Verificar se o token é o esperado
             //Console.WriteLine("LA: " + _LA);
             //Console.WriteLine("Token: " + token);
            if(_LA.Type == token){
                // //Console.WriteLine("Token: " + _LA + " reconhecido");
                _LA = _lexer.GetNextToken();
                // //Console.WriteLine("Proximo Token: " + _LA + " a ser analisado");
            }else{
                throw new Exception("Erro de sintaxe");
            }
        }

        // public void Teste(){
        //     if(_LA.Type == ETokenType.EOL){
        //         Match(ETokenType.EOL);
        //     }
        // }

        public void Prog(){ // Prog: Line A
            Line(); 
            //Stmt();
            A();

            if(_LA.Type != ETokenType.EOF){
                throw new Exception("ERRO de sintaxe: " + _LA.Type + " não esperado, esperava EOF");
            }
            else {
                //Console.WriteLine("Entrada válida");
            }
        }

        public void Line(){ // Line: stmt EOL | EOF
            Stmt();
            //Console.WriteLine("Enviando o match do EOL");
            if(_LA.Type == ETokenType.EOF){
                Match(ETokenType.EOF);
            }
            else{
                Match(ETokenType.EOL);
            }
        }

        public void A(){ //A: EOF | Prog
            //Console.WriteLine("----------------");
            //Console.WriteLine("LA no A: " + _LA);
            if(_LA.Type==ETokenType.EOF){
                Match(ETokenType.EOF);
            }
            // else if(_LA.Type==ETokenType.EOL){
            //     Match(ETokenType.EOL);
            //     A();
            // }
            else{
                Prog();
            }
        }

        public void Stmt(){ //Stmt: in | out | atrib
            if(_LA.Type==ETokenType.INPUT){
                In();
            }
            else if(_LA.Type==ETokenType.OUTPUT){
                Out();
            }
            else if(_LA.Type==ETokenType.VAR){
                Atrib();
            }
            else{
                throw new Exception("ERRO de sintaxe: " + _LA.Type + " não esperado, esperava INPUT, OUTPUT ou VAR");
            }
        }

        public void In(){ //in: INPUT VAR
            
            Match(ETokenType.INPUT);
            var refx = _LA.Value;
            Match(ETokenType.VAR);
            Console.WriteLine("Digite um valor: ");
            //Pegar o valor inteiro digitado
            var valor = Convert.ToInt32(Console.ReadLine());
            //Colocar a variavel na tabela
            if (refx != null){
                var entry =_symbolTable.GetEntry(refx);
                if (entry != null) {
                    entry.Value = valor;
                }
            }
        }

        public void Out(){ //out    : OUTPUT VAR
            Match(ETokenType.OUTPUT);
            var refx = _LA.Value;
            Match(ETokenType.VAR);
            Console.WriteLine("Resultado: " + _symbolTable.GetEntry(refx).Value);
        }

        public void Atrib(){//atrib  : VAR AT expr
            var refx = _LA.Value;
            Console.WriteLine("LA do atrib: " + _LA.Value);
            Match(ETokenType.VAR);
            Match(ETokenType.AT);
            var res = E();
            //Colocar a variavel na tabela
            if (refx != null){
                var entry =_symbolTable.GetEntry(refx);
                if (entry != null) {
                    entry.Value = res;
                }
            }
            //Console.WriteLine("Resultado: " + res);
        }

        public int E(){ //expr   : term Y
            ////Console.WriteLine("Estado: E ->  LA:" + _LA.Value);
            var res1 = T();
            ////Console.WriteLine("Estou no E e T é: " + res1);
            return X(res1);
        }

        public int X(int t){ //X      : vazio | + expr | - expr
            ////Console.WriteLine("Estado: X ->  LA:" + _LA.Value);
            if(_LA.Type==ETokenType.SUM){
                Match(ETokenType.SUM);
                var res = E();
                ////Console.WriteLine("Estou no X(+) e t é: " + t + " e E é: " + res);
                return t + res;
            }
            else if(_LA.Type==ETokenType.SUB){
                Match(ETokenType.SUB);
                var res = E();
                ////Console.WriteLine("Estou no X(-) e t é: " + t + " e E é: " + res);
                return t - res;
            }
            
            else{
                if(_LA.Type!=ETokenType.EOF && _LA.Type!=ETokenType.CLOSE && _LA.Type!=ETokenType.EOL){
                    throw new Exception("ERRO de sintaxe: " + _LA.Type + " não esperado, esperava + ou -");
                }
            }
            return t;
        }

        public int T(){ //term   : factZ
            ////Console.WriteLine("Estado: T ->  LA:" + _LA.Value);
            var res1 = F();
            
            ////Console.WriteLine("Estou no T e F é: " + res1);
            return Y(res1);
        }

        public int Y(int t){ // Y     : vazio | * term | / term
            ////Console.WriteLine("Estado: Y ->  LA:" + _LA.Value);
            if(_LA.Type==ETokenType.MUL){
                
                Match(ETokenType.MUL);
                var res = T();
                ////Console.WriteLine("Estou no Y(*) e t é: " + t + " e E é: " + res);
                return t * res;

            }
            else if(_LA.Type==ETokenType.DIV){
                Match(ETokenType.DIV);
                var res = T();
                ////Console.WriteLine("Estou no X(/) e t é: " + t + " e E é: " + res);
                return t / res;
            }
            else{
                if(_LA.Type!=ETokenType.EOF && _LA.Type!=ETokenType.CLOSE && _LA.Type!=ETokenType.SUM && _LA.Type!=ETokenType.SUB && _LA.Type!=ETokenType.EOL){
                    throw new Exception("ERRO de sintaxe: " + _LA.Type + " não esperado, esperava * ou /");
                }
            }
            return t;
        }

        public int F(){ //fact   : NUM | VAR | OE expr CE
            //Console.WriteLine("Estado: F ->  LA:" + _LA.Value);
            if(_LA.Type==ETokenType.NUMBER){
                var res = _LA.Value;
                Match(ETokenType.NUMBER);
                ////Console.WriteLine("Estou no F e LA é: " + res);
                return res;
            }
            else if(_LA.Type==ETokenType.OPEN){
                Match(ETokenType.OPEN);
                var res = E();
                Match(ETokenType.CLOSE);
                ////Console.WriteLine("Estou no F e E é: " + res);
                return res;
            }
            else{
                if(_LA.Type!=ETokenType.EOF && _LA.Type!=ETokenType.CLOSE && _LA.Type!=ETokenType.SUM && _LA.Type!=ETokenType.SUB && _LA.Type!=ETokenType.MUL && _LA.Type!=ETokenType.DIV){
                    throw new Exception("ERRO de sintaxe: " + _LA.Type + " não esperado, esperava número ou (");
                }
            }
            return 0;
        }
    }
}

