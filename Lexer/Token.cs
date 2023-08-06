namespace Translator {
    public struct Token{
            public ETokenType Type;
            public Int32 Value;

            public Token(ETokenType type, Int32 value = 0) {
                Type = type;
                Value = value;
            }   
            public override String ToString() {
                return "<"+Type+","+Value+">";
            } 
    }

        
}