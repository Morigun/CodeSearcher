namespace CodeSearcher.Commands.Arguments.Parse
{
    public class ArgumentPosition
    {
        public ArgumentPosition()
        {
            StrIndex = -1;
            StrNumber = -1;
        }
        public ArgumentPosition(int strIndex, int strNumber)
        {
            StrIndex = strIndex;
            StrNumber = strNumber;
        }

        public int StrIndex { get; set; }
        public int StrNumber { get; set; }
    }
}
