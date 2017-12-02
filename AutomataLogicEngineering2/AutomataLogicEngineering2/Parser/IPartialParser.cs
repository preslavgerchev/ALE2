namespace AutomataLogicEngineering2.Parser
{
    using System.Collections.Generic;

    public interface IPartialParser<T>
    {
        T Parse(List<string> lines);
    }
}
