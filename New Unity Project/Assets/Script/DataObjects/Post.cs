using System.Collections.Generic;

namespace Assets.Script.DataObjects
{
    [System.Serializable]
    public class Post
    {
        public string date;
        public string author;
        public string text;
        public List<string> tags;
        public Message[] replies;
    }
}
