using System.Collections.Generic;

namespace Gomi.Dados.Versoes
{
    public abstract class ScriptVersao
    {
        public abstract int Versao { get; }

        public abstract IEnumerable<string> Scripts { get; }
    }
}