namespace HomeBankingNetMvc.Utils
{
    public class CardDataGenerator
    {
        private static readonly Random random = new Random();

        public static string GenerarNumeroTarjeta()
        {
            string numeroTarjeta = "";

           
            for (int i = 0; i < 16; i++)
            {
                if (i > 0 && i % 4 == 0)
                {
                    
                    numeroTarjeta += "-";
                }
                numeroTarjeta += random.Next(10).ToString();
            }

            return numeroTarjeta;
        }

        public static int GenerarCVV()
        {

            return random.Next(100, 1000);
        }

    }
}
