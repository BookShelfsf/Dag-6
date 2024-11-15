using System;
using System.IO;

// Custom exceptions
public class InvalidNameException : Exception
{
    public InvalidNameException(string message) : base(message) { }
}

public class InvalidAgeException : Exception
{
    public InvalidAgeException(string message) : base(message) { }
}

public class InvalidEmailException : Exception
{
    public InvalidEmailException(string message, Exception innerException)
        : base(message, innerException) { }
}

public class FileHandler
{
    static string filePath = @"Files\Users.txt";

    static void Main()
    {
        try
        {
            // Sørg for, at Files-mappen og Users.txt findes
            Directory.CreateDirectory("Files");
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }

            // Brugeren indtaster data
            Console.WriteLine("Indtast fornavn:");
            string firstName = Console.ReadLine();
            ValidateName(firstName);

            Console.WriteLine("Indtast efternavn:");
            string lastName = Console.ReadLine();
            ValidateName(lastName);

            Console.WriteLine("Indtast alder (18-50):");
            int age = int.Parse(Console.ReadLine());
            ValidateAge(age, firstName, lastName);

            Console.WriteLine("Indtast e-mail:");
            string email = Console.ReadLine();
            ValidateEmail(email);

            // Gem brugerdata i tekstfilen
            SaveUserToFile(firstName, lastName, age, email);

            // Vis registrerede brugere
            Console.WriteLine("\nRegistrerede brugere:");
            DisplayUsers();
        }
        catch (InvalidNameException ex)
        {
            Console.WriteLine($"Navnefejl: {ex.Message}");
        }
        catch (InvalidAgeException ex)
        {
            Console.WriteLine($"Aldersfejl: {ex.Message}");
        }
        catch (InvalidEmailException ex)
        {
            Console.WriteLine($"E-mail fejl: {ex.Message}. Inner Exception: {ex.InnerException?.Message}");
        }
        catch (FileLoadException ex)
        {
            Console.WriteLine($"Filfejl: {ex.Message}");
        }
        catch (FormatException)
        {
            Console.WriteLine("Fejl: Du indtastede ikke et gyldigt tal.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Uventet fejl: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Programmet er afsluttet.");
        }
    }

    // Validering af navn
    static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new InvalidNameException("Navnet må ikke være tomt.");
        }
    }

    // Opdateret validering af alder
    static void ValidateAge(int age, string firstName, string lastName)
    {
        string fullName = $"{firstName} {lastName}";
        if (fullName != "Niels Olesen" && (age < 18 || age > 50))
        {
            throw new InvalidAgeException("Alderen skal være mellem 18 og 50, medmindre du hedder Niels Olesen.");
        }
    }

    // Validering af e-mail
    static void ValidateEmail(string email)
    {
        if (!email.Contains("@") || !email.Contains("."))
        {
            throw new InvalidEmailException("E-mailen er ugyldig.",
                new Exception("E-mail skal indeholde '@' og '.'"));
        }
    }

    // Gem brugerdata i tekstfilen
    static void SaveUserToFile(string firstName, string lastName, int age, string email)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine($"{firstName} {lastName}, {age}, {email}");
                Console.WriteLine("Brugerdata er gemt.");
            }
        }
        catch (Exception ex)
        {
            throw new FileLoadException("Kunne ikke skrive til filen.", ex);
        }
    }

    // Vis registrerede brugere
    static void DisplayUsers()
    {
        try
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
        }
        catch (Exception ex)
        {
            throw new FileLoadException("Kunne ikke læse fra filen.", ex);
        }
    }
}
