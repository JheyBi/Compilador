using System.Text;
// tipagem dos dados (chave, nome, valor)



// CLASSE SYMBOLTABLE
// dicionario que vai armazenar os simbolos
// funções que armazenam e recuperam os simbolos
// funções que alterar o valor do simbolo
// funções que verificam se o simbolo existe

namespace Translator {
    public class SymbolTable {
        private int _key;
        private Dictionary<int, TableEntry> _data;

        public SymbolTable() {
            _data = new Dictionary<int, TableEntry>();
        }

         public Int32 Put(string name) {
            return this.Put(name, null);
        }

        public Int32 Put(string name, Double? value) {
            var (entry, id) = GetByName(name);
            if (entry != null)
                return id;

            _data.Add(++_key, new TableEntry(ETokenType.VAR, name, value));
            return _key;
        }

        public (TableEntry?, Int32) GetByName(String name) {
            foreach (var id in _data.Keys.ToList())
            {
                if (_data[id].Name == name)
                {
                    return (_data[id], id);
                }
            };
            return (null, 0);
        }

        public double? Get(Int32 id) {
            if (!_data.ContainsKey(id))
                return null;
            TableEntry entry = _data[id];
            return entry.Value;
        }


        public TableEntry? GetEntry(Int32 id) {
            if (!_data.ContainsKey(id))
                return null;
            TableEntry entry = _data[id];
            return entry;
        }

        public override String ToString() {
            var sb = new StringBuilder();
            sb.Append("ID".PadRight(5, ' '));
            sb.Append("Type".PadRight(10, ' '));
            sb.Append("Name".PadRight(15, ' '));
            sb.Append("Value".PadRight(5, ' '));
            sb.AppendLine();

            _data.Keys.ToList().ForEach(id =>
            {
                var entry = _data[id];
                sb.Append(id.ToString().PadRight(5, ' '));
                sb.Append(entry.Type.ToString().PadRight(10, ' '));
                sb.Append(entry.Name.ToString().PadRight(15, ' '));
                if (entry.Value.HasValue)
                    sb.Append(entry.Value.Value.ToString().PadRight(5, ' '));
                sb.AppendLine();
            });
            return sb.ToString();
        }
    }
}