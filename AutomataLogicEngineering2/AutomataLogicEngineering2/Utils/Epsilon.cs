namespace AutomataLogicEngineering2.Utils
{
    public static class Epsilon
    {
        public const char Letter = 'ε';

        public static char ParseChar(this char ch) => ch == '_' ? Letter : ch;
    }
}
