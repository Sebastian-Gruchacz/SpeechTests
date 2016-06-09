using System.Diagnostics;

namespace IoDemo.WikiModel
{
    [DebuggerDisplay(@"{Title} ({Id})")]
    public class WikiIndexItem
    {
        public WikiIndexItem(int id, string title)
        {
            Id = id;
            Title = title;
        }

        public int Id { get; private set; }

        public string Title { get; private set; }
    }
}
