using FistLab.ExtendedString;

namespace FirstLab.ExtendedDoubleTests;

public class DoubleToBitStringTest
{
    [Fact]
    public void ToBinarySting_NegativeNumber_ReturnsSign()
    {
        double number = -1.1;
        
        var actual = number.ToBinarySting().Substring(0, 1);

        Assert.Equal("1", actual);
    }
    
    [Fact]
    public void ToBinarySting_BigPositiveNumber_ReturnsMantissa()
    {
        double number = 4134138419.3414313;
        
        var actual = number.ToBinarySting().Substring(12, 52);

        Assert.Equal("1110110011010011111001000110011010101110110100000001", actual);
    }
    
    [Fact]
    public void ToBinarySting_SmallNegativeNumber_ReturnsExponent()
    {
        double number = -0.000001234;
        
        var actual = number.ToBinarySting().Substring(1, 11);

        Assert.Equal("01111101011", actual);
    }
    
    [Fact]
    public void ToBinarySting_BigNegativeNumber_ReturnsExponent1()
    {
        double number = -33414134.201234;
        
        var actual = number.ToBinarySting();

        Assert.Equal("1100000101111111110111011011111101100011001110000100000100100101", actual);
    }
}