using System.Numerics;
using System.Text;

namespace FirstLab.Polynomial;

public class Polynomial<T> where T: INumber<T>
{
    private readonly Dictionary<int, T> _coefficients;
    
    public Polynomial() => _coefficients = new Dictionary<int, T>();

    public Polynomial(Dictionary<int, T> coefficients)
    {
        _coefficients = coefficients;
    }
    
    public Polynomial(IEnumerable<(int, T)> coefficients)
    {
        _coefficients = new Dictionary<int, T>();

        foreach ((int power, T coefficient) in coefficients)
        {
            if (!_coefficients.TryAdd(power, coefficient))
            {
                _coefficients[power] += coefficient;
            }
        }
    }
    
    public Polynomial<T> Add(Polynomial<T> polynomial)
    {
        List<KeyValuePair<int, T>> newCoefficients = [];
        
        foreach (var firstCoefficient in _coefficients)
        {
            if (polynomial._coefficients.TryGetValue(firstCoefficient.Key, out var newCoefficient))
            {
                newCoefficients.Add(new KeyValuePair<int, T>(firstCoefficient.Key,
                    firstCoefficient.Value + newCoefficient));
            }
        }
        
        var differentPowerCoefficients = _coefficients
            .ExceptBy(polynomial._coefficients.Keys, k => k.Key)
            .Concat(polynomial._coefficients.ExceptBy(_coefficients.Keys, k => k.Key)).ToDictionary();

        var newPolynomial = new Polynomial<T>(newCoefficients
            .Concat(differentPowerCoefficients)
            .ToDictionary());

        return NormalizePolynomial(newPolynomial);
    }
    
    public Polynomial<T> Subtract(Polynomial<T> polynomial)
    {
        List<KeyValuePair<int, T>> newCoefficients = [];
        
        foreach (var firstCoefficient in _coefficients)
        {
            if (polynomial._coefficients.TryGetValue(firstCoefficient.Key, out var newCoefficient))
            {
                newCoefficients.Add(new KeyValuePair<int, T>(firstCoefficient.Key,
                    firstCoefficient.Value - newCoefficient));
            }
        }
        
        var differentFirstCoefficients = _coefficients
            .ExceptBy(polynomial._coefficients.Keys, k => k.Key)
            .ToDictionary();
            
        var differentSecondCoefficients = polynomial._coefficients
            .ExceptBy(_coefficients.Keys, k => k.Key)
            .ToDictionary(x => x.Key, x => - x.Value);
        
        var newPolynomial = new Polynomial<T>(newCoefficients
            .Concat(differentFirstCoefficients)
            .Concat(differentSecondCoefficients)
            .ToDictionary());
        
        return NormalizePolynomial(newPolynomial);
    }
    
    public Polynomial<T> Multiply(Polynomial<T> polynomial)
    {
        Dictionary<int, T> newCoefficients = new();
        
        foreach (var firstCoefficient in _coefficients)
        {
            foreach (var secondCoefficient in polynomial._coefficients)
            {
                var newMemberPower = firstCoefficient.Key + secondCoefficient.Key;

                var newMemberCoefficient = firstCoefficient.Value * secondCoefficient.Value;

                if (!newCoefficients.TryAdd(newMemberPower, newMemberCoefficient))
                {
                    newCoefficients[newMemberPower] += newMemberCoefficient;
                }
            }
        }

        return NormalizePolynomial(new Polynomial<T>(newCoefficients));
    }

    private static Polynomial<T> NormalizePolynomial(Polynomial<T> polynomial)
    {
        Dictionary<int, T> normalizedCoefficients = polynomial._coefficients
            .Where(x => x.Value != T.Zero)
            .ToDictionary(x => x.Key, x => x.Value);

        return new Polynomial<T>(normalizedCoefficients);
    }

    public override int GetHashCode()
    {
        int value = 19;

        for (int i = 0; i < _coefficients.Count; i++)
        {
            value ^= _coefficients[i].GetHashCode();
        }

        return value;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Polynomial<T> polynomial)
        {
            return false;
        }
        
        if (_coefficients.Count != polynomial._coefficients.Count 
            || _coefficients.Except(polynomial._coefficients).Any())
        {
            return false;
        }
        
        return true;
    }

    public override string ToString()
    {
        if (_coefficients.Count == 0)
        {
            return string.Empty;
        }
        
        StringBuilder text = new();

        foreach (var coefficient in _coefficients)
        {
            if (coefficient.Value >= T.Zero)
            {
                text.Append('+');
            }
            
            text.Append($"{coefficient.Value}*x^{coefficient.Key} ");
        }

        text.Remove(text.Length - 1, 1);

        return text.ToString();
    }
}