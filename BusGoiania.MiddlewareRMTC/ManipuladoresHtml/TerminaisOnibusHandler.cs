using BusGoiania.MiddlewareRMTC.DTOs;

namespace BusGoiania.MiddlewareRMTC.ManipuladoresHtml
{
    public static class TerminaisOnibusHandler
    {
        private static List<TerminalOnibus> TerminaisOnibus { get; set; }
        public static List<TerminalOnibus> Handle(string paginaWeb)
        {
            TerminaisOnibus = new List<TerminalOnibus>();

            if (string.IsNullOrEmpty(paginaWeb))
                return TerminaisOnibus;

            var vPaginaWeb = paginaWeb.Split("\n");

            ProcessarListaTerminais(vPaginaWeb);

            return TerminaisOnibus;
        }

        private static void ProcessarListaTerminais(string[] vPaginaWeb)
        {
            for (int i = 0; i < vPaginaWeb.Length; i++)
            {
                if (vPaginaWeb[i].Contains(">Terminal"))
                {
                    int inicioIntervalo = vPaginaWeb[i].IndexOf(">Terminal") + 1;
                    int fimIntervalo = vPaginaWeb[i].IndexOf("</a></li>");

                    var resultado = vPaginaWeb[i].Substring(inicioIntervalo, (fimIntervalo - inicioIntervalo));

                    TerminaisOnibus.Add(new TerminalOnibus { Terminal = resultado });
                }
            }
        }
    }
}
