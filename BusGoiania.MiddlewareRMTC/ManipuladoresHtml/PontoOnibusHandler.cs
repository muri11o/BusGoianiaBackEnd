using BusGoiania.MiddlewareRMTC.DTOs;
using BusGoiania.MiddlewareRMTC.Extensions;

namespace BusGoiania.MiddlewareRMTC.ManipuladoresHtml
{
    public static class PontoOnibusHandler
    {
        private static string REF_NUMERO_LINHA = "<td height=\"20\" align=\"center\" valign=\"middle\" class=\"coluna\"><strong>";
        private static string REF_DESTINO = "<td width=\"230\" height=\"20\" class=\"coluna\">";
        private static string REF_PROXIMO = "<td height=\"20\" align=\"center\" class=\"coluna\">";
        private static string REF_SEGUINTE = "<td height=\"20\" align=\"center\" class=\"coluna ultima\">";
        private static int QUANTIDADE_COLUNAS = 4;
        private static List<HorarioOnibus> HorariosOnibus { get; set; }

        public static List<HorarioOnibus> Handle(string paginaWeb)
        {
            HorariosOnibus = new List<HorarioOnibus>();

            if (string.IsNullOrEmpty(paginaWeb))
                return HorariosOnibus;

            var vPaginaWeb = paginaWeb.Split("\n");

            var qtdHorarios = ObterQtdHorarios(vPaginaWeb);

            var vMatriz = new string[qtdHorarios, QUANTIDADE_COLUNAS];
           
            PreencherColunaNumeroLinha(qtdHorarios, vPaginaWeb, ref vMatriz);
            PreencherColunaDescricao(qtdHorarios, vPaginaWeb, ref vMatriz);
            PreencherColunaProximo(qtdHorarios, vPaginaWeb, ref vMatriz);
            PreencherColunaSeguinte(qtdHorarios, vPaginaWeb, ref vMatriz);

            HorariosOnibus.Build(vMatriz, QUANTIDADE_COLUNAS);

            return HorariosOnibus;
        }

        private static int ObterQtdHorarios(string[] vPaginaWeb)
        {
            int contador = 0;

            for (int i = 0; i < vPaginaWeb.Length; i++)
            {

                if (vPaginaWeb[i].Contains(REF_NUMERO_LINHA))
                    contador++;
            }

            return contador;
        }

        private static int ObterIndicePrimeiraOcorrencia(string valor, string[] vPaginaWeb)
        {
    
            for (int i = 0; i < vPaginaWeb.Length; i++)
            {
                if (vPaginaWeb[i].Contains(valor))
                    return i;
            }

            return -1;
        }

        private static void PreencherColunaNumeroLinha(int qtdHorarios, string[] vPaginaWeb, ref string[,] vMatriz)
        {
            var coluna = 0;
            var contador = 0;
            var indice = ObterIndicePrimeiraOcorrencia(REF_NUMERO_LINHA, vPaginaWeb);
                       
            for (int i = 0; i < qtdHorarios; i++)
            {
                int inicioIntervalo = REF_NUMERO_LINHA.Length;
                int fimIntervalo = vPaginaWeb[indice].IndexOf("</strong></td>");

                var resultado = vPaginaWeb[indice].Substring(inicioIntervalo, (fimIntervalo - inicioIntervalo));

                vMatriz[contador, coluna] = resultado;

                contador++;
                indice = indice + 6;
            }

        }

        private static void PreencherColunaDescricao(int qtdHorarios, string[] vPaginaWeb, ref string[,] vMatriz)
        {
            var coluna = 1;
            var contador = 0;
            var indice = ObterIndicePrimeiraOcorrencia(REF_DESTINO, vPaginaWeb);

            for (int i = 0; i < qtdHorarios; i++)
            {
                int inicioIntervalo = REF_DESTINO.Length;
                int fimIntervalo = vPaginaWeb[indice].IndexOf("</td>");

                var resultado = vPaginaWeb[indice].Substring(inicioIntervalo, (fimIntervalo - inicioIntervalo));

                vMatriz[contador, coluna] = resultado;

                contador++;
                indice = indice + 6;
            }
        }

        private static void PreencherColunaProximo(int qtdHorarios, string[] vPaginaWeb, ref string[,] vMatriz)
        {
            var coluna = 2;
            var contador = 0;
            var indice = ObterIndicePrimeiraOcorrencia(REF_PROXIMO, vPaginaWeb);

            for (int i = 0; i < qtdHorarios; i++)
            {
                int inicioIntervalo = REF_PROXIMO.Length;
                int fimIntervalo = vPaginaWeb[indice].IndexOf("</td>");

                var resultado = vPaginaWeb[indice].Substring(inicioIntervalo, (fimIntervalo - inicioIntervalo));

                vMatriz[contador, coluna] = resultado;

                contador++;
                indice = indice + 6;
            }
        }

        private static void PreencherColunaSeguinte(int qtdHorarios, string[] vPaginaWeb, ref string[,] vMatriz)
        {
            var coluna = 3;
            var contador = 0;
            var indice = ObterIndicePrimeiraOcorrencia(REF_SEGUINTE, vPaginaWeb);

            for (int i = 0; i < qtdHorarios; i++)
            {
                int inicioIntervalo = REF_SEGUINTE.Length;
                int fimIntervalo = vPaginaWeb[indice].IndexOf("</td>");

                var resultado = vPaginaWeb[indice].Substring(inicioIntervalo, (fimIntervalo - inicioIntervalo));

                vMatriz[contador, coluna] = resultado;

                contador++;
                indice = indice + 6;
            }
        }
    }

    
}
