using FirstLab.Polynomial;

namespace FirstLab.PolynomialTests;

public class PolynomialTest
{
    
    [Fact]
    public void Equals_Polynomial_ReturnsTrueWhenEqual()
    {
        Polynomial<int> firstPolynomial = new ([(1, 2), (2, 6)]);
        Polynomial<int> secondPolynomial = new ([(1, 2), (2, 6)]);

        var actual = firstPolynomial.Equals(secondPolynomial);

        Assert.True(actual);
    }
    
    [Fact]
    public void Add_Polynomial_ReturnsNewPolynomial()
    {
        Polynomial<int> firstPolynomial = new ([(1, 2), (2, -5), (4, 10)]);
        Polynomial<int> secondPolynomial = new ([(1, 3), (2, 6), (3, 15)]);

        var actual = firstPolynomial.Add(secondPolynomial);
        
        Assert.Equal(new Polynomial<int>([(1, 5), (2, 1), (3, 15), (4, 10)]), actual);
    }
    
    [Fact]
    public void Subtract_Polynomial_ReturnsNewPolynomial()
    {
        Polynomial<int> firstPolynomial = new ([(1, 2), (2, -5), (4, 10)]);
        Polynomial<int> secondPolynomial = new ([(1, 3), (2, 6), (3, 15)]);

        var actual = firstPolynomial.Subtract(secondPolynomial);
        
        Assert.Equal(new Polynomial<int>([(1, -1), (2, -11), (3, -15), (4, 10)]), actual);
    }
    
    [Fact]
    public void Multiply_Polynomial_ReturnsNewPolynomial()
    {
        Polynomial<int> firstPolynomial = new ([(1, 2), (4, 10)]);
        Polynomial<int> secondPolynomial = new ([(1, 3), (2, 6)]);

        var actual = firstPolynomial.Multiply(secondPolynomial);
        
        Assert.Equal(new Polynomial<int>([(2, 6), (3, 12), (5, 30), (6, 60)]), actual);
    }
    
    [Fact]
    public void ToString_Polynomial_ReturnsPolynomialStringRepresentation()
    {
        Polynomial<int> firstPolynomial = new ([(1, 2), (2, -5), (4, 10)]);

        var actual = firstPolynomial.ToString();
        
        Assert.Equal("+2*x^1 -5*x^2 +10*x^4", actual);
    }
}