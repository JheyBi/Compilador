namespace Translator {
        public class SyntaxError: Exception
    {
        public int LinhaDoErro { get; }

        public SyntaxError(int linha, String text, String _LA = "")
            : base("ERRO: " + _LA + " " + text + " - linha: "+ linha)
        {
            LinhaDoErro = linha;
        }
    }
}