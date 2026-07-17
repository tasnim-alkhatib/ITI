Console.Write("Enter temperature in Celsius: ");
double celsius = Convert.ToDouble(Console.ReadLine());

double fahrenheit = (celsius * 9 / 5) + 32;

Console.WriteLine($"{celsius}°C = {fahrenheit}°F");

Console.WriteLine("\n-------------------------------------------------\n");

Console.Write("Enter a number: ");
int number = Convert.ToInt32(Console.ReadLine());

if (number % 2 == 0)
    Console.WriteLine($"{number} is Even");
else
    Console.WriteLine($"{number} is Odd");

Console.WriteLine("\n-------------------------------------------------\n");

Console.Write("Enter your score (0-100): ");
int score = Convert.ToInt32(Console.ReadLine());

char grade;

if (score >= 90)
    grade = 'A';
else if (score >= 80)
    grade = 'B';
else if (score >= 70)
    grade = 'C';
else if (score >= 60)
    grade = 'D';
else
    grade = 'F';

Console.WriteLine($"Your grade is: {grade}");

Console.WriteLine("\n-------------------------------------------------\n");


Console.Write("Enter first number: ");
double num1 = Convert.ToDouble(Console.ReadLine());

Console.Write("Enter second number: ");
double num2 = Convert.ToDouble(Console.ReadLine());

Console.Write("Choose an operation (+, -, *, /): ");
string op = Console.ReadLine();

double result;

switch (op)
{
    case "+":
        result = num1 + num2;
        break;
    case "-":
        result = num1 - num2;
        break;
    case "*":
        result = num1 * num2;
        break;
    case "/":
        if (num2 == 0)
        {
            Console.WriteLine("Error: Division by zero is not allowed.");
            return;
        }
        result = num1 / num2;
        break;
    default:
        Console.WriteLine("Invalid operation.");
        return;
}

Console.WriteLine($"Result: {num1} {op} {num2} = {result}");
  
