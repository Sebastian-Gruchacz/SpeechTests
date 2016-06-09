using System.Collections.Generic;
using System.Diagnostics;


namespace IoDemo.WikiModel
{
        public class WikiList
    {
       public WikiList(int id, string title)
        {
            Id = id;
            Title = title;
        }

    public int Id { get; private set; }
    public string Title { get; private set; }

    }
}
