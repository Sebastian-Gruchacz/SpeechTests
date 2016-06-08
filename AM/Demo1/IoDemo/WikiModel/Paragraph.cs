namespace IoDemo.WikiModel
{
    public class Paragraph
    {
        public Paragraph(string content)
        {
            Content = content;
        }

        public string Content { get; set; }

        public override string ToString()
        {
            return Content;
        }
    }
}