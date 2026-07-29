using System.Collections.Generic;
using System.Linq;
using Lab_2;

namespace Lab_2_1
{
    public class Program
    {
        static string Separator => new string('=',100);
        public static void Main()
        {
            var products = ListGenerators.ProductList;
            var customers = ListGenerators.CustomerList;

            string[] words = File.ReadAllLines("dictionary_english.txt");

            RestrictionOperators(products);            
            ElementOperators(products);
            SetOperators(products, customers);
            AggregateOperators(products, customers, words);
            OrderingOperators(products);
            PartitioningOperators(customers);
            ProjectionOperators(products, customers);
            Quantifiers(products, words);
            GroupingOperators(words);
        }

        static void RestrictionOperators(List<Product> products)
        {
            Console.WriteLine("==== Restriction Operators ====");
            Console.WriteLine("1. Find all products that are out of stock.");
            var OutOfStockProducts = products.Where(p => p.UnitsInStock == 0);
            foreach (var p in OutOfStockProducts)
                Console.WriteLine(p.ProductName);

            Console.WriteLine("\n2. Find all products that are in stock and cost more than 3.00 per unit.");
            var inStockExpensive = products.Where(p => p.UnitsInStock > 0 && p.UnitPrice > 3.00m);
            foreach (var p in inStockExpensive)
                Console.WriteLine(p.ProductName + " - " + p.UnitPrice);

            Console.WriteLine("\n3. Returns digits whose name is shorter than their value.");
            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            var shortDigits = digits.Where((digit, index) => digit.Length < index);
            foreach (var d in shortDigits)
                Console.WriteLine(d);
            Console.WriteLine(Separator);
        }
        static void ElementOperators(List<Product> products)
        {
            Console.WriteLine("\n==== Element Operators ====");
            Console.WriteLine("1. Get first Product out of Stock");
            var firstOut = products.First(p => p.UnitsInStock == 0);
            Console.WriteLine(firstOut.ProductName);

            Console.WriteLine("\n2. Return the first product whose Price > 1000, unless there is no match, in which case null is returned.");
            var firstExpensive = products.FirstOrDefault(p => p.UnitPrice > 1000);
            Console.WriteLine(firstExpensive == null ? "null" : firstExpensive.ProductName);

            Console.WriteLine("\n3. Retrieve the second number greater than 5.");
            int[] arr = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var second = arr.Where(n => n > 5).ElementAt(1);
            Console.WriteLine(second);
            Console.WriteLine(Separator);
        }
        static void SetOperators(List<Product> products, List<Customer> customers)
        {
            Console.WriteLine("\n==== Set Operators ====");
            Console.WriteLine("1. Find the unique Category names from Product List");
            var categories = products.Select(p => p.Category).Distinct();
            foreach (var c in categories)
                Console.WriteLine(c);

            var productFirstChars = products.Select(p => p.ProductName[0]);
            var customerFirstChars = customers.Select(c => c.CompanyName[0]);

            Console.WriteLine("\n2. Produce a Sequence containing the unique first letter from both product and customer names.");
            var unionChars = productFirstChars.Union(customerFirstChars);
            foreach (var c in unionChars)
                Console.Write(c + ", ");

            Console.WriteLine("\n\n3. Create one sequence that contains the common first letter from both product and customer names.");
            var commonChars = productFirstChars.Intersect(customerFirstChars);
            foreach (var c in commonChars)
                Console.Write(c + ", ");

            Console.WriteLine("\n\n4. Create one sequence that contains the first letters of product names that are not also first letters of customer names.");
            var exceptChars = productFirstChars.Except(customerFirstChars);
            foreach (var c in exceptChars)
                Console.Write(c + ", ");

            Console.WriteLine("\n\n5. Create one sequence that contains the last Three Characters in each names of all customers and products, including any duplicates");
            var customerLastThree = customers.Select(c => c.CompanyName.Substring(Math.Max(0, c.CompanyName.Length - 3)));
            var productLastThree = products.Select(p => p.ProductName.Substring(Math.Max(0, p.ProductName.Length - 3)));
            var allLastThree = customerLastThree.Concat(productLastThree);
            foreach (var s in allLastThree)
                Console.Write(s + ", ");
            Console.WriteLine(Separator);
        }
        static void AggregateOperators(List<Product> products, List<Customer> customers, string[] words)
        {
            Console.WriteLine("\n===== Aggregate Operators =====");
            Console.WriteLine("1. Uses Count to get the number of odd numbers in the array");
            int[] arr = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var oddCount = arr.Count(n => n % 2 == 1);
            Console.WriteLine(oddCount);

            Console.WriteLine("\n2. Return a list of customers and how many orders each has.");
            var customerOrderCounts = customers
                .Select(c => new 
                { 
                    c.CustomerID,
                    OrderCount = c.Orders.Count() 
                });
            foreach (var c in customerOrderCounts)
                Console.WriteLine(c.CustomerID + " - " + c.OrderCount);

            Console.WriteLine("\n3. Return a list of categories and how many products each has");
            var categoryCounts = products
                .GroupBy(p => p.Category)
                .Select(g => new 
                { 
                    Category = g.Key, 
                    Count = g.Count() 
                });
            foreach (var c in categoryCounts)
                Console.WriteLine(c.Category + " - " + c.Count);

            Console.WriteLine("\n4. Get the total of the numbers in an array.");
            var total = arr.Sum();
            Console.WriteLine(total);
            
            Console.WriteLine("\n5. Get the total number of characters of all words in dictionary_english.txt (Read dictionary_english.txt into Array of String First)");
            var totalChars = words.Sum(w => w.Length);
            Console.WriteLine(totalChars);

            Console.WriteLine("\n6. Get the total units in stock for each product category.");
            var stockPerCategory = products
                .GroupBy(p => p.Category)
                .Select(g => new 
                { 
                    Category = g.Key, 
                    Total = g.Sum(p => p.UnitsInStock) 
                });
            foreach (var s in stockPerCategory)
                Console.WriteLine(s.Category + " - " + s.Total);

            Console.WriteLine("\n7. Get the length of the shortest word in dictionary_english.txt (Read dictionary_english.txt into Array of String First).");
            var shortestLength = words.Min(w => w.Length);
            Console.WriteLine(shortestLength);

            Console.WriteLine("\n8. Get the cheapest price among each category's products");
            var cheapestPerCategory = products
                .GroupBy(p => p.Category)
                .Select(g => new 
                { 
                    Category = g.Key, 
                    CheapestPrice = g.Min(p => p.UnitPrice) 
                });
            foreach (var c in cheapestPerCategory)
                Console.WriteLine(c.Category + " - " + c.CheapestPrice);

            Console.WriteLine("\n9. Get the products with the cheapest price in each category (Use Let");
            var cheapestProducts =
                from p in products
                group p by p.Category into g
                let minPrice = g.Min(p => p.UnitPrice)
                select new { Category = g.Key, Products = g.Where(p => p.UnitPrice == minPrice) };
            foreach (var c in cheapestProducts)
            {
                Console.WriteLine(c.Category + ":");
                foreach (var p in c.Products)
                    Console.WriteLine("   " + p.ProductName + " - " + p.UnitPrice);
            }

            Console.WriteLine("\n10. Get the length of the longest word in dictionary_english.txt (Read dictionary_english.txt into Array of String First).");
            var longestLength = words.Max(w => w.Length);
            Console.WriteLine(longestLength);

            Console.WriteLine("\n11. Get the most expensive price among each category's products.");
            var mostExpensivePerCategory = products
                .GroupBy(p => p.Category)
                .Select(g => new 
                { 
                    Category = g.Key, 
                    MaxPrice = g.Max(p => p.UnitPrice) 
                });
            foreach (var c in mostExpensivePerCategory)
                Console.WriteLine(c.Category + " - " + c.MaxPrice);

            Console.WriteLine("\n12. Get the products with the most expensive price in each category.");
            var mostExpensiveProducts =
                from p in products
                group p by p.Category into g
                let maxPrice = g.Max(p => p.UnitPrice)
                select new { Category = g.Key, Products = g.Where(p => p.UnitPrice == maxPrice) };
            foreach (var c in mostExpensiveProducts)
            {
                Console.WriteLine(c.Category + ":");
                foreach (var p in c.Products)
                    Console.WriteLine("   " + p.ProductName + " - " + p.UnitPrice);
            }

            Console.WriteLine("\n13. Get the average length of the words in dictionary_english.txt (Read dictionary_english.txt into Array of String First).");
            var averageLength = words.Average(w => w.Length);
            Console.WriteLine(averageLength);

            Console.WriteLine("\n14. Get the average price of each category's products.");
            var averagePricePerCategory = products
                .GroupBy(p => p.Category)
                .Select(g => new 
                { 
                    Category = g.Key, 
                    AveragePrice = g.Average(p => p.UnitPrice) 
                });
            foreach (var a in averagePricePerCategory)
                Console.WriteLine(a.Category + " - " + a.AveragePrice);
            Console.WriteLine(Separator);
        }
        static void OrderingOperators(List<Product> products)
        {
            Console.WriteLine("\n===== Ordering Operators =====");
            Console.WriteLine("1. Sort a list of products by name");
            var sortedByName = products.OrderBy(p => p.ProductName);
            foreach (var p in sortedByName)
                Console.WriteLine(p.ProductName);

            Console.WriteLine("\n2. Uses a custom comparer to do a case-insensitive sort of the words in an array.");
            string[] words1 = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };
            var sortedWords = words1.OrderBy(w => w, StringComparer.OrdinalIgnoreCase);
            foreach (var w in sortedWords)
                Console.WriteLine(w);

            Console.WriteLine("\n3. Sort a list of products by units in stock from highest to lowest.");
            var sortedByStock = products.OrderByDescending(p => p.UnitsInStock);
            foreach (var p in sortedByStock)
                Console.WriteLine(p.ProductName + " - " + p.UnitsInStock);

            Console.WriteLine("\n4. Sort a list of digits, first by length of their name, and then alphabetically by the name itself.");
            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            var sortedDigits = digits.OrderBy(d => d.Length).ThenBy(d => d);
            foreach (var d in sortedDigits)
                Console.WriteLine(d);

            Console.WriteLine("\n5. Sort first by word length and then by a case-insensitive sort of the words in an array.");
            var sortedWords2 = words1.OrderBy(w => w.Length).ThenBy(w => w, StringComparer.OrdinalIgnoreCase);
            foreach (var w in sortedWords2)
                Console.WriteLine(w);

            Console.WriteLine("\n6. Sort a list of products, first by category, and then by unit price, from highest to lowest.");
            var sortedProducts = products.OrderBy(p => p.Category).ThenByDescending(p => p.UnitPrice);
            foreach (var p in sortedProducts)
                Console.WriteLine(p.Category + " - " + p.ProductName + " - " + p.UnitPrice);

            Console.WriteLine("\n7. Sort first by word length and then by a case-insensitive descending sort of the words in an array.");
            var sortedWords3 = words1.OrderBy(w => w.Length).ThenByDescending(w => w, StringComparer.OrdinalIgnoreCase);
            foreach (var w in sortedWords3)
                Console.WriteLine(w);

            Console.WriteLine("\n8. Create a list of all digits in the array whose second letter is 'i' that is reversed from the order in the original array");
            var iDigits = digits.Where(d => d.Length > 1 && d[1] == 'i').Reverse();
            foreach (var d in iDigits)
                Console.WriteLine(d);
            Console.WriteLine(Separator);
        }
        static void PartitioningOperators(List<Customer> customers)
        {
            Console.WriteLine("\n===== Partitioning Operators =====");

            var washingtonOrders = customers.Where(c => c.Region == "Washington").SelectMany(c => c.Orders);

            Console.WriteLine("\n1. Get the first 3 orders from customers in Washington");
            var first3 = washingtonOrders.Take(3);
            foreach (var o in first3)
                Console.WriteLine(o.OrderID);

            Console.WriteLine("\n2. Get all but the first 2 orders from customers in Washington.");
            var skip2 = washingtonOrders.Skip(2);
            foreach (var o in skip2)
                Console.WriteLine(o.OrderID);

            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            Console.WriteLine("\n3. Return elements starting from the beginning of the array until a number is hit that is less than its position in the array");
            var takeWhileResult = numbers.TakeWhile((n, index) => n >= index);
            foreach (var n in takeWhileResult)
                Console.WriteLine(n);

            Console.WriteLine("\n4. Get the elements of the array starting from the first element divisible by 3.");
            var skipWhileDivisible = numbers.SkipWhile(n => n % 3 != 0);
            foreach (var n in skipWhileDivisible)
                Console.WriteLine(n);

            Console.WriteLine("\n5. Get the elements of the array starting from the first element less than its position.");
            var skipWhilePosition = numbers.SkipWhile((n, index) => n >= index);
            foreach (var n in skipWhilePosition)
                Console.WriteLine(n);
            Console.WriteLine(Separator);
        }
        static void ProjectionOperators(List<Product> products, List<Customer> customers)
        {
            Console.WriteLine("\n===== Projection Operators =====");

            Console.WriteLine("1. Return a sequence of just the names of a list of products");
            var productNames = products.Select(p => p.ProductName);
            foreach (var n in productNames)
                Console.WriteLine(n);

            Console.WriteLine("\n2. Produce a sequence of the uppercase and lowercase versions of each word in the original array (Anonymous Types).");
            string[] words1 = { "aPPLE", "BlUeBeRrY", "cHeRry" };
            var upperLower = words1.Select(w => new { Upper = w.ToUpper(), Lower = w.ToLower() });
            foreach (var w in upperLower)
                Console.WriteLine(w.Upper + " - " + w.Lower);

            Console.WriteLine("\n3. Produce a sequence containing some properties of Products, including UnitPrice which is renamed to Price in the resulting type.");
            var renamedProducts = products.Select(p => new { p.ProductName, p.Category, Price = p.UnitPrice });
            foreach (var p in renamedProducts)
                Console.WriteLine(p.ProductName + " - " + p.Category + " - " + p.Price);

            Console.WriteLine("\n4. Determine if the value of ints in an array match their position in the array.");
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var inPlace = numbers.Select((num, index) => new { Number = num, InPlace = num == index });
            foreach (var n in inPlace)
                Console.WriteLine(n.Number + ": " + n.InPlace);

            Console.WriteLine("\n5. Returns all pairs of numbers from both arrays such that the number from numbersA is less than the number from numbersB.");
            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };
            var pairs = numbersA.SelectMany(a => numbersB.Where(b => a < b), (a, b) => new { a, b });
            foreach (var pair in pairs)
                Console.WriteLine(pair.a + " is less than " + pair.b);

            Console.WriteLine("\n6. Select all orders where the order total is less than 500.00.");
            var smallOrders = customers.SelectMany(c => c.Orders).Where(o => o.Total < 500.00m);
            foreach (var o in smallOrders)
                Console.WriteLine(o.OrderID + " - " + o.Total);

            Console.WriteLine("\n7. Select all orders where the order was made in 1998 or later.");
            var recentOrders = customers.SelectMany(c => c.Orders).Where(o => o.OrderDate.Year >= 1998);
            foreach (var o in recentOrders)
                Console.WriteLine(o.OrderID + " - " + o.OrderDate);
            Console.WriteLine(Separator);
        }
        static void Quantifiers(List<Product> products, string[] words)
        {
            Console.WriteLine("\n===== Quantifiers =====");

            Console.WriteLine("1. Determine if any of the words in dictionary_english.txt (Read dictionary_english.txt into Array of String First) contain the substring 'ei'.");
            var containsEi = words.Any(w => w.Contains("ei"));
            Console.WriteLine(containsEi);

            Console.WriteLine("\n2. Return a grouped a list of products only for categories that have at least one product that is out of stock.");
            var categoriesWithOutOfStock = products.GroupBy(p => p.Category).Where(g => g.Any(p => p.UnitsInStock == 0));
            foreach (var g in categoriesWithOutOfStock)
            {
                Console.WriteLine(g.Key + ":");
                foreach (var p in g)
                    Console.WriteLine("   " + p.ProductName);
            }

            Console.WriteLine("\n3. Return a grouped a list of products only for categories that have all of their products in stock.");
            var categoriesAllInStock = products.GroupBy(p => p.Category).Where(g => g.All(p => p.UnitsInStock > 0));
            foreach (var g in categoriesAllInStock)
            {
                Console.WriteLine(g.Key + ":");
                foreach (var p in g)
                    Console.WriteLine("   " + p.ProductName);
            }
            Console.WriteLine(Separator);
        }
        static void GroupingOperators(string[] words)
        {
            Console.WriteLine("===== Grouping Operators =====");

            Console.WriteLine("1.");
            int[] numbers = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
            var numberGroups = numbers.GroupBy(n => n % 5);
            foreach (var g in numberGroups)
            {
                Console.WriteLine("Numbers with a remainder of " + g.Key + " when divided by 5:");
                foreach (var n in g)
                    Console.WriteLine(n);
            }

            //Console.WriteLine("\n2.");
            //var wordGroups = words.GroupBy(w => w[0]);
            //foreach (var g in wordGroups)
            //{
            //    Console.WriteLine(g.Key + ":");
            //    foreach (var w in g)
            //        Console.WriteLine("   " + w);
            //}

            Console.WriteLine("\n3.");
            string[] anagramWords = { "from ", " salt", " earn ", " last ", " near ", " form " };
            var anagramGroups = anagramWords.GroupBy(w => w, new AnagramEqualityComparer());
            foreach (var g in anagramGroups)
            {
                foreach (var w in g)
                    Console.WriteLine(w.Trim());
                Console.WriteLine();
            }
            Console.WriteLine(Separator);
        }
        public class AnagramEqualityComparer : IEqualityComparer<string>
        {
            public bool Equals(string x, string y)
            {
                return GetCanonicalString(x) == GetCanonicalString(y);
            }

            public int GetHashCode(string obj)
            {
                return GetCanonicalString(obj).GetHashCode();
            }

            private string GetCanonicalString(string word)
            {
                char[] wordChars = word.Trim().ToCharArray();
                Array.Sort(wordChars);
                return new string(wordChars);
            }
        }
    }
}