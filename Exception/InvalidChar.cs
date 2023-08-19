namespace Translator {
        public class InvalidChar : Exception
    {
        public int LinhaDoErro { get; }

        public InvalidChar(int linha, Char c)
            : base("ERRO: Caractere (" + c + ") inválido encontrado na linha " + linha)
        {
            LinhaDoErro = linha;
        }
    }
}




