using LinqTraining.Data;
using LinqTraining.Models;

List <Product> products = SampleData.GetProducts(); 
List <Book> books = SampleData.GetBooks();
List<Order> orders = SampleData.GetOrders();
List<Employee> employees = SampleData.GetEmployees();


var bestBooksByGenre = books
.GroupBy(b => b.Genre)
.Select(g => new
{
    Genre = g.Key,
    TopBooks = g.OrderByDescending(b => b.UnitsSold).Take(5)
});
foreach(var group in bestBooksByGenre)
{
    Console.WriteLine($"Genre: {group.Genre}");
    foreach (var book  in group.TopBooks)
    {
        Console.WriteLine($"Title: {book.Title} - Author: {book.Author} - Units Sold: {book.UnitsSold}");
    }
}


var bestCustomers = orders
.GroupBy(o => o.CustomerName)
.Select(g => new
{
    CustomerName = g.Key,
    OrdersCustomer = g.Count(),

    
})
.OrderByDescending(g => g.OrdersCustomer)
.Skip(2)
.Take(3);

foreach (var group in bestCustomers)
{
    Console.WriteLine($"Customer: {group.CustomerName}, Number of purchases: {group.OrdersCustomer}");
    Console.WriteLine();

}


Console.Write("Name of the book: ");

string? entry = Console.ReadLine();

if (!string.IsNullOrWhiteSpace(entry))
{
    Book? book = books.FirstOrDefault(b => b.Title.Contains(entry, StringComparison.OrdinalIgnoreCase));

    if (book is not null)
    {
        Console.WriteLine($"\nBook found: [{book.Id}] {book.Title} - {book.Author}");
    }
    else
    {
        Console.WriteLine($"\nBook not found whit '{entry}'.");
    }
}
else
{
    Console.WriteLine($"\nEmpty space.");
}





var customersNotOrders = orders
.GroupBy(o => o.CustomerName)
.Where(g => !g.Any(o => o.Status == "Completed"));
    
foreach (var group in customersNotOrders)
{
    Console.WriteLine($"customers Not Orders Completed: {group.Key}");
    foreach (var order in group)
    {
        Console.WriteLine($" - {order.Status}");
    }
}


var today = DateTime.Today;
var lastMonthDate = today.AddDays(-30);
var statusOrder = orders
.Where(o => o.Date >= lastMonthDate)
.GroupBy(o => o.Status);

foreach (var group in statusOrder)
{
    Console.WriteLine($"Status: {group.Key}");

    foreach (var order in group)
    {
        Console.WriteLine($"Customer: {order.CustomerName} | Date: {order.Date:yyyy-MM-dd} | Amount: ${order.Amount}");
    }
}
