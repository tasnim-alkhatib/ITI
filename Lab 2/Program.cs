Random rand = new Random();
int secretNumber = rand.Next(1, 101);
int maxAttempts = 5;
bool won = false;

Console.WriteLine("Guess the number between 1 and 100!");
Console.WriteLine($"You have {maxAttempts} attempts.");

for (int attempt = 1; attempt <= maxAttempts; attempt++)
{
    Console.Write($"\nAttempt {attempt}/{maxAttempts} - Enter your guess: ");
    int guess = Convert.ToInt32(Console.ReadLine());

    if (guess == secretNumber)
    {
        Console.WriteLine($"Correct! You guessed it in {attempt} attempt(s)!");
        won = true;
        break;
    }
    else if (guess > secretNumber)
    {
        Console.WriteLine("Too high!");
    }
    else
    {
        Console.WriteLine("Too low!");
    }
}

if (!won)
    Console.WriteLine($"\nGame Over! The number was {secretNumber}.");


Console.WriteLine("\n----------------------------------------------\n");


Console.Write("Enter height: ");
int height = Convert.ToInt32(Console.ReadLine());

for (int row = 1; row <= height; row++)
{
    // Spaces for right alignment
    Console.Write(new string(' ', height - row));

    // Numbers count down from 'row' to 1
    for (int num = row; num >= 1; num--)
    {
        Console.Write(num);
    }
    Console.WriteLine("\n");
}


Console.WriteLine("\n----------------------------------------------\n");


Console.Write("Enter height: ");
int _height = Convert.ToInt32(Console.ReadLine());

for (int row = 1; row <= _height; row++)
{
    // spaces
    Console.Write(new string(' ', _height - row));

    // Stars 
    Console.Write(new string('*', 2 * row - 1));

    Console.WriteLine();
}


Console.WriteLine("\n----------------------------------------------\n");


Console.Write("Enter your age: ");
string ageInput = Console.ReadLine() ?? "0";

int age = Convert.ToInt32(ageInput);
int futureAge = age + 5;
Console.WriteLine($"In 5 years, you will be {futureAge} years old");

double ageAsDouble = Convert.ToDouble(ageInput);
double result = ageAsDouble / 3;
Console.WriteLine($"Your age divided by 3 = {result}");


Console.WriteLine("\n----------------------------------------------\n");


Console.Write("Enter annual salary: ");
double salary = Convert.ToDouble(Console.ReadLine());

double taxRate = salary switch
{
    <= 10000 => 0.0,
    > 10000 and <= 30000 => 0.10,
    > 30000 and <= 70000 => 0.20,
    > 70000 => 0.30,
    _ => 0.0
};

double taxAmount = salary * taxRate;

Console.WriteLine($"Tax Rate: {taxRate * 100}%");
Console.WriteLine($"Tax Amount: {taxAmount}");
Console.WriteLine($"Net Salary: {salary - taxAmount}");


Console.WriteLine("\n----------------------------------------------\n");


Console.Write("Enter a number: ");
int n = Convert.ToInt32(Console.ReadLine());

for (int i = 1; i <= 10; i++)
    Console.WriteLine($"{n} x {i} = {n * i}");