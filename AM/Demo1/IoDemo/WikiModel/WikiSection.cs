using System.Collections.Generic;
using System.Diagnostics;

namespace IoDemo.WikiModel
{
    [DebuggerDisplay("Id = {Id}, Title = {Title}")]
    public class WikiSection
    {
        public WikiSection(int id, string title)
        {
            Id = id;
            Title = title;
            Paragraphs = new List<Paragraph>();
        }

        public int Id { get; private set; }

        
        public string Title { get; private set; }

        public List<Paragraph> Paragraphs { get; private set; }

        public override string ToString()
        {
            return Title;
        }
    }
}