namespace InputHandler;
static class Chooseer
{    
    /// <summary>
    /// Shows a list of alternatives on the console, and lets the user choose 
    /// one with the arrow keys and enter.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="message">Prompt message to display</param>
    /// <param name="options">An array with values to be chosen and corresponding messages to be displayed as alternatives</param>
    /// <returns></returns>
    public static T ChooseAlternative<T>(string message, (string message, T option)[] options)
    {
        int selectionIndex = 0;
        int linesToRedraw = options.Length + 1; // One line for each option and one for the prompt.
        ConsoleKeyInfo output;

        while (true)
        {
            Console.WriteLine(message);
            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine($"{(i == selectionIndex ? '>' : ' ')}{options[i].message}");
            }

            output = Console.ReadKey(true);
            if (output.Key == ConsoleKey.UpArrow)
            {
                selectionIndex = (selectionIndex + options.Length - 1) % options.Length;
            }
            else if (output.Key == ConsoleKey.DownArrow)
            {
                selectionIndex = (selectionIndex + 1) % options.Length;
            }
            else if (output.Key == ConsoleKey.Enter)
            {
                return (options[selectionIndex].option);
            }
            // // If we are dealing with ints, we can cancel by presing Q, returning -1
            // else if (char.ToUpper(output.KeyChar) == 'Q' && typeof(T) == typeof(int)) 
            // {
            //     if(typeof(T) == typeof(int))
            //         return (T)(object)-1;
            // }

            // If the options are chars, pressing one will return it
            if (typeof(T) == typeof(char) && char.IsLetterOrDigit(output.KeyChar))
            {
                var pressed = char.ToUpper((char)(object)output.KeyChar);
                if (options.Select(o => (char)(object)o.option).Contains(pressed))
                {
                    return (T)(object)pressed;
                }
            }

            Console.SetCursorPosition(0, Console.CursorTop - linesToRedraw);
        }
    }
    /// <summary>
    /// Shows a list of alternatives on the console, and lets the user choose 
    /// one with the arrow keys and enter.
    /// </summary>
    /// <param name="message">Prompt message to display</param>
    /// <param name="optionmessages">labels for the options</param>
    /// <returns>An index refering to an option</returns>
    public static int ChooseAlternative(string message, string[] optionmessages)
    {
        var options = optionmessages.Select((o, index) => (o, index)).ToArray();
        return ChooseAlternative(message, options);
    }

    /// <summary>
    /// Shows a list of alternatives on the console, and lets the user choose 
    /// one with the arrow keys and enter. 
    /// </summary>
    /// <param name="message">Prompt message to display </param>
    /// <param name="options">An array of tuples, representing the messages, and the corresponding int to return</param>
    /// <returns>The corresponding int for the option</returns>
    public static int ChooseAlternative(string message, (string message, char option)[] options) => 
        ChooseAlternative(message, options);

    /// <summary>
    /// Shows a list of alternatives on the console, and lets the user choose 
    /// one with the arrow keys and enter. The chars also act as fast options that can be pressed to choose. 
    /// </summary>
    /// <param name="message">Prompt message to display</param>
    /// <param name="options">An array of messages and chars to be returned if option is chosen</param>
    /// <returns>A char corresponding to the option</returns>
    public static int ChooseAlternative(string message, (string message, int option)[] options) => 
        ChooseAlternative(message, options);
}