namespace Translator {
    public class Lexer {

        private String _input;
        private Int32 _position;
        private Char? _currentChar;
        public Int32 Line{get;protected set;}
        public Int32 Column{get;protected set;}

        public SymbolTable SymbolTable {get; protected set;}

        public Lexer(String input, SymbolTable? st=null) {
            _input = input;
            if(st==null){
                st = new SymbolTable();
            }
            SymbolTable = st;
            _position = 0;
            _currentChar = _input[_position];
            Line = Column = 1;
        }

        private void Advance() {
            _position++;
            if (_position > _input.Length - 1) {
                _currentChar = '\0';
            } else {
                _currentChar = _input[_position];
            }
        }

        private Int32 Integer() {
            String result = "";
            while (_currentChar != '\0' && Char.IsDigit(_currentChar??' ')) {
                result += _currentChar;
                Advance();
            }
            return Int32.Parse(result);
        }
        
        private bool TestSufix(String sufix) {
            char [] sufixArray = sufix.ToCharArray();
            var test = true;
            Advance();
            foreach (char s in sufixArray){
                if (s != _currentChar){
                    test = false;
                }
                Advance();
            }
            return test;
                
        }

        public Token GetNextToken() {

                if (_currentChar == '\0') {
                    return new Token(ETokenType.EOF);
                }

                while(_currentChar == null | _currentChar == ' ' || _currentChar == '\t' || _currentChar == '\r') {
                    Advance();
                }

                //Tokens
                switch(_currentChar)
                {
                    case '+': _currentChar = null; return new Token(ETokenType.SUM);
                    case '-': _currentChar = null; return new Token(ETokenType.SUB);
                    case '*': _currentChar = null; return new Token(ETokenType.MUL);
                    case '/': _currentChar = null; return new Token(ETokenType.DIV);
                    case '(': _currentChar = null; return new Token(ETokenType.OPEN);
                    case ')': _currentChar = null; return new Token(ETokenType.CLOSE);
                    case '=': _currentChar = null; return new Token(ETokenType.AT);
                    case '\n':
                        _currentChar = null; 
                        Column = 1;
                        Line++;
                        return new Token(ETokenType.EOL);
                }

                // var $[a-z]+
                if (_currentChar == '$'){
                    var varName = "";
                    Advance();
                    while(Char.IsLetter(_currentChar.Value)){
                        varName += _currentChar;
                        Advance();
                    }
                    var key = SymbolTable.Put(varName);
                    return new Token(ETokenType.VAR, key);
                }

                // number
                if (Char.IsDigit(_currentChar??' ')) {
                    return new Token(ETokenType.NUMBER, Integer());
                }
                
                //write
                if (_currentChar == 'w') {
                    if (TestSufix("rite")) {
                        return new Token(ETokenType.OUTPUT);
                    }
                }

                //read
                if (_currentChar == 'r') {
                    if (TestSufix("ead")) {
                        return new Token(ETokenType.INPUT);
                    }
                }
                
                if (_currentChar == ';') {
                    Advance();
                    return new Token(ETokenType.EOL);
                }
                throw new Exception("Char Inválido"); // tratar adequadamente
            
        }
    }
}