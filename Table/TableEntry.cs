namespace Translator {
    public class TableEntry {
        public ETokenType Type{get;set;}
        public String Name{get;set;}
        public Double? Value{get;set;}

        public TableEntry(ETokenType type, String name, Double? value=null) {
            Type = type;
            Name = name;
            Value = value;
        }
    }
}