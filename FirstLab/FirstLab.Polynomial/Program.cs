using FirstLab.Polynomial;


var first = new Polynomial<int>(new List<(int, int)> { (1, 2), (2, 4), (3, 6)});

var second = new Polynomial<int>(new List<(int, int)> { (1, 3), (2, 4)});


var sum = first.Add(second);

var difference = first.Subtract(second);

var multiplication = first.Multiply(second);


Console.WriteLine($"Sum: { sum }");
Console.WriteLine($"Difference: { difference }");
Console.WriteLine($"Multiplication: { multiplication }");