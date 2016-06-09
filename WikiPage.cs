using System;
using System.Collections.Generic;

namespace IoDemo.WikiModel
{
    public class WikiPage
    {
        public WikiPage(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(title));

            Title = title;
            Sections = new SortedDictionary<int, WikiSection>();
            Elements = new SortedDictionary<int, WikiList>();
        }

        public string Title { get; set; }

        public SortedDictionary<int, WikiSection> Sections { get; private set; }
        public SortedDictionary<int, WikiList> Elements { get; private set; }
    }
}
