namespace AutomataLogicEngineering2.Parser
{
    using System.Collections.Generic;

    public class CommentParser : IPartialParser<string>
    {
        public string Parse(List<string> lines)
        {
            var comment = string.Empty;
            foreach (var line in lines)
            {
                if (!line.StartsWith("#")) continue;

                comment = line.Split('#')[1];
                break;
            }

            return comment;
        }
    }
}
