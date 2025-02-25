namespace MathGame.Models
{
    public static class Utilities
    {
        public static TEnum GetUserSelection<TEnum>(string displayText) where TEnum : struct
        {
            Console.Clear();
            
            while (true)
            {
                Console.WriteLine(displayText);

                var enumOptions = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();

                // Display options to the user
                var i = 0;
                foreach (TEnum option in enumOptions)
                {
                    Console.WriteLine($"{i++} - {option}");
                }

                var userSelection = Console.ReadLine();

                // Attempt to parse user input
                var isOptionParsable = Enum.TryParse<TEnum>(userSelection, out TEnum selectedOption);
                var isOptionDefined = Enum.IsDefined(typeof(TEnum), selectedOption);
                if (isOptionParsable && isOptionDefined)
                {
                    return selectedOption;
                }

                // Handle invalid input
                Console.Clear();
                Console.WriteLine($"{selectedOption} is an invalid option. Please enter a valid option.");
            }
        }
    }
}
