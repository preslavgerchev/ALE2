namespace AutomataLogicEngineering2.Symbols
{
    // TODO documentation.
    /// <summary>
    /// An enumeration, representing all the possible connectives.
    /// </summary>
    public enum OperatorType
    {
        /// <summary>
        /// Represents the . connective.
        /// </summary>
        Concatenation = 0,

        /// <summary>
        /// Represents the | connective.
        /// </summary>
        Union = 1,

        /// <summary>
        /// Represents the * connective.
        /// </summary>
        KleeneStar = 2,
    }
}
