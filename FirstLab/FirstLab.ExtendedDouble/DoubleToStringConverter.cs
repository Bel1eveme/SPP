namespace FistLab.ExtendedString
{
    public static class DoubleToBitStringConverter
    {
        private const int BitsInDoubleCount = 64;
        public static string ToBinarySting(this double number)
        {
            long bits = BitConverter.DoubleToInt64Bits(number);

            var bitString = Convert.ToString(bits, 2);

            if (bitString.Length != BitsInDoubleCount)
            {
                return string.Concat(new string('0', BitsInDoubleCount - bitString.Length), bitString);
            }

            return bitString;
        }
    }
}
