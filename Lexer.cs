namespace Translator {
    public class Lexer {
        private String _input;
        private Int32 _position;
        private Char _currentChar;

        public Lexer(String input) {
            _input = input;
            _position = 0;
            _currentChar = _input[_position];
        }

        private void Advance() {
            _position++;
            if (_position > _input.Length - 1) {
                _currentChar = '\0';
            } else {
                _currentChar = _input[_position];
            }
        }

        private void SkipWhitespace() {
            while (_currentChar != '\0' && Char.IsWhiteSpace(_currentChar)) {
                Advance();
            }
        }

        private Int32 Integer() {
            String result = "";
            while (_currentChar != '\0' && Char.IsDigit(_currentChar)) {
                result += _currentChar;
                Advance();
            }
            return Int32.Parse(result);
        }

        public Token GetNextToken() {
            while (_currentChar != '\0') {
                if (Char.IsWhiteSpace(_currentChar)) {
                    SkipWhitespace();
                    continue;
                }
                if (Char.IsDigit(_currentChar)) {
                    return new Token(ETokenType.NUMBER, Integer());
                }
                if (_currentChar == '+') {
                    Advance();
                    return new Token(ETokenType.SUM);
                }
                if (_currentChar == '-') {
                    Advance();
                    return new Token(ETokenType.SUB);
                }
                if (_currentChar == '*') {
                    Advance();
                    return new Token(ETokenType.MUL);
                }
                if (_currentChar == '/') {
                    Advance();
                    return new Token(ETokenType.DIV);
                }
                if (_currentChar == '(') {
                    Advance();
                    return new Token(ETokenType.OPEN);
                }
                if (_currentChar == ')') {
                    Advance();
                    return new Token(ETokenType.CLOSE);
                }
                throw new Exception("Char Inválido"); // tratar adequadamente
            }
            return new Token(ETokenType.EOF);
        }
    }
}