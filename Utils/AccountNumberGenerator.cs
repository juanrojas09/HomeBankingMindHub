namespace HomeBankingNetMvc.Utils
{
    public class AccountNumberGenerator
    {
        private static readonly Random random = new Random();

        public static string GenerarNumeroCuenta()
        {
            const string prefijo = "VIN-";
            const int longitudNumeros = 8;

            
            int numeroAleatorio = random.Next((int)Math.Pow(10, longitudNumeros));

           
            string numeroCuenta = numeroAleatorio.ToString().PadLeft(longitudNumeros, '0');

           
            return prefijo + numeroCuenta;
        }
    }
}
