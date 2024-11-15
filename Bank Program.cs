using System;

class BankApp
{
    static void Main()
    {
        // Starter med en balance på 100 DKK
        decimal balance = 100m;

        while (true)
        {
            try
            {
                Console.WriteLine("\nVælg en mulighed:");
                Console.WriteLine("1. Indsæt beløb");
                Console.WriteLine("2. Hæv beløb");
                Console.WriteLine("3. Afslut");
                Console.Write("Indtast dit valg (1-3): ");
                int valg = int.Parse(Console.ReadLine());

                if (valg == 1)
                {
                    // Indsættelse
                    Console.Write("Indtast beløb til indsættelse: ");
                    decimal indsætBeløb = decimal.Parse(Console.ReadLine());

                    if (indsætBeløb < 0)
                    {
                        throw new ArgumentException("Du kan ikke indsætte et negativt beløb.");
                    }

                    balance += indsætBeløb;
                    Console.WriteLine($"Beløbet er indsat. Ny balance: {balance} DKK");
                }
                else if (valg == 2)
                {
                    // Hævning
                    Console.Write("Indtast beløb til hævning: ");
                    decimal hævBeløb = decimal.Parse(Console.ReadLine());

                    if (hævBeløb < 0)
                    {
                        throw new ArgumentException("Du kan ikke hæve et negativt beløb.");
                    }
                    if (hævBeløb > balance)
                    {
                        throw new InvalidOperationException("Ikke nok penge på kontoen til hævningen.");
                    }

                    balance -= hævBeløb;
                    Console.WriteLine($"Beløbet er hævet. Ny balance: {balance} DKK");
                }
                else if (valg == 3)
                {
                    Console.WriteLine("Tak fordi du brugte bank appen. Farvel!");
                    break;
                }
                else
                {
                    Console.WriteLine("Ugyldigt valg. Prøv igen.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Fejl: Du indtastede ikke et gyldigt tal. Prøv igen.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Fejl: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Fejl: {ex.Message}");
            }
            finally
            {
                Console.WriteLine($"Din nuværende balance er: {balance} DKK");
            }
        }
    }
}
