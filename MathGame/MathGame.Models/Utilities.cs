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

                var enumValues = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();

                // Display options to the user
                var i = 0;
                foreach (TEnum value in enumValues)
                {
                    Console.WriteLine($"{i++} - {value}");
                }

                var userSelection = Console.ReadLine();

                // Attempt to parse user input
                var isOptionParsable = Enum.TryParse<TEnum>(userSelection, out TEnum result);
                var isOptionDefined = Enum.IsDefined(typeof(TEnum), result);
                if (isOptionParsable && isOptionDefined)
                {
                    return result;
                }

                // Handle invalid input
                Console.Clear();
                Console.WriteLine($"{result} is an invalid option. Please enter a valid option.");
            }
        }
    }
}
