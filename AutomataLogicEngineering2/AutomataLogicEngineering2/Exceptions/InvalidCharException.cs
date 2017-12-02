namespace AutomataLogicEngineering2.Exceptions
{
    using System;

    public class InvalidCharException : Exception
    {
        public InvalidCharException(string message)
            : base(message)
        {
        }
    }
}
