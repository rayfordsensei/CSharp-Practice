public partial class SimpleProjects
{
    readonly string _incorrectNumber = "Incorrect number, please try again.";
    readonly string _pressToContinue = "Press any key to continue...";
    readonly string _enterSecondNumber = "Please enter your second number: ";

    public void ConsoleCalculator()
    {
        float startingNumber;
        float secondNumber;
        bool validNumber;

        Console.Clear();
        Console.WriteLine("You're using a simple console calculator\nCurrently available features are:\n\tAddition\n\tSubtraction\n\tMultiplication\n\tDivision");

        do
        {
            Console.Write("Please enter your starting number: ");
            validNumber = float.TryParse(Console.ReadLine(), out startingNumber);

            if (!validNumber)
                Console.WriteLine(_incorrectNumber);
        } while (!validNumber);

        ConsoleKeyInfo keyPressed;

        do
        {
            Console.Clear();
            Console.WriteLine($"Select an operation:\n\t(1) Addition\n\t(2) Subtraction\n\t(3) Multiplication\n\t(4) Division\n\tPress enter to terminate the program\nYour current number: {startingNumber}");

            keyPressed = Console.ReadKey();
            Console.WriteLine();

            switch (keyPressed.Key)
            {
                case ConsoleKey.D1:
                    secondNumber = ReadValidNumber(_enterSecondNumber);
                    startingNumber = MakeEquation("+", startingNumber, secondNumber);
                    break;

                case ConsoleKey.D2:
                    secondNumber = ReadValidNumber(_enterSecondNumber);
                    startingNumber = MakeEquation("-", startingNumber, secondNumber);
                    break;

                case ConsoleKey.D3:
                    secondNumber = ReadValidNumber(_enterSecondNumber);
                    startingNumber = MakeEquation("*", startingNumber, secondNumber);
                    break;

                case ConsoleKey.D4:
                    secondNumber = ReadValidNumber(_enterSecondNumber);

                    if (keyPressed.Key == ConsoleKey.D4 && secondNumber == 0)
                    {
                        Console.WriteLine("Division by zero is not allowed.\nPress any key to continue...");
                        Console.ReadKey(true);
                        continue;
                    }

                    startingNumber = MakeEquation("/", startingNumber, secondNumber);
                    break;
            }

        } while (keyPressed.Key != ConsoleKey.Enter);
    }

    private float ReadValidNumber(string prompt)
    {
        float number;
        bool validNumber;
        do
        {
            Console.Write(prompt);
            validNumber = float.TryParse(Console.ReadLine(), out number);
            if (!validNumber)
                Console.WriteLine(_incorrectNumber);
        } while (!validNumber);
        return number;
    }

    private float MakeEquation(string equationOperator, float firstOperand, float secondOperand)
    {
        float answer = 0;
        switch (equationOperator)
        {
            case "+":
                answer = firstOperand + secondOperand;
                Console.WriteLine($"{firstOperand} + {secondOperand} = {answer:F2}.\n{answer:F2} is the new value for your starting number.");
                Console.WriteLine(_pressToContinue);
                Console.ReadKey(true);
                break;
            case "-":
                answer = firstOperand - secondOperand;
                Console.WriteLine($"{firstOperand} - {secondOperand} = {answer:F2}.\n{answer:F2} is the new value for your starting number.");
                Console.WriteLine(_pressToContinue);
                Console.ReadKey(true);
                break;
            case "*":
                answer = firstOperand * secondOperand;
                Console.WriteLine($"{firstOperand} * {secondOperand} = {answer:F2}.\n{answer:F2} is the new value for your starting number.");
                Console.WriteLine(_pressToContinue);
                Console.ReadKey(true);
                break;
            case "/":
                answer = firstOperand / secondOperand;
                Console.WriteLine($"{firstOperand} / {secondOperand} = {answer:F2}.\n{answer:F2} is the new value for your starting number.");
                Console.WriteLine(_pressToContinue);
                Console.ReadKey(true);
                break;
        }
        return answer;
    }
}
