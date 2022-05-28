using BusGoiania.MiddlewareRMTC.DTOs;

namespace BusGoiania.MiddlewareRMTC.Extensions
{
    public static class HorariosOnibusExtensions
    {
        public static void Build(this List<HorarioOnibus> horariosOnibus, string[,] vMatriz, int quantidadeColunas)
        {

            for (int i = 0; i < (vMatriz.Length / quantidadeColunas); i++)
            {
                var numeroLinha = "";
                var destino = "";
                var proximo = "";
                var seguinte = "";

                for (int j = 0; j < quantidadeColunas; j++)
                {
                    switch (j)
                    {
                        case 0:
                            numeroLinha = vMatriz[i, j];
                            break;
                        case 1:
                            destino = vMatriz[i, j];
                            break;
                        case 2:
                            proximo = vMatriz[i, j];
                            break;
                        case 3:
                            seguinte = vMatriz[i, j];
                            break;
                    }
                }

                var horarioOnibus = new HorarioOnibus
                {
                    NumeroLinha = numeroLinha,
                    Destino = destino,
                    Proximo = proximo,
                    Seguinte = seguinte,
                };

                horariosOnibus.Add(horarioOnibus);
            }
        }
    }
}
