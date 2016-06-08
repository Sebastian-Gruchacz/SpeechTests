using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using IoDemo.WikiModel;

namespace IoDemo
{
    class WikiPageReader
    {
        public WikiPage ReadModel(Stream modelStream)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(modelStream);

            var xDocRoot = xDoc.DocumentElement;
            if (xDocRoot == null)
                throw new Exception("Not valid XML document");
            if (xDocRoot.Name != "wikipedia")
                throw new Exception("Not valid Wiki document");

            WikiPage page = new WikiPage(xDocRoot.GetAttribute("tytul"));

            foreach (var childNode in xDocRoot.ChildNodes)
            {
                XmlElement el = childNode as XmlElement;
                if (el == null)
                    continue;

                switch (el.Name.ToUpper())
                {
                    case "WPROWADZENIE":
                    {
                        var section = new WikiSection(0, null);

                        foreach (var par in el.ChildNodes)
                        {
                            XmlElement parel = par as XmlElement;
                            if (parel == null)
                                continue;

                            switch (parel.Name.ToUpper())
                            {
                                case "P":
                                {
                                    section.Paragraphs.Add(new Paragraph(parel.InnerXml));
                                    break;
                                }
                            }
                        }

                        page.Sections.Add(section.Id, section);

                        break;
                    }
                    case "SPIS":
                    {
// ...
                        break;
                    }
                    case "SEKCJE":
                    {
                        foreach (var schildNode in el.ChildNodes)
                        {
                            XmlElement sel = schildNode as XmlElement;
                            if (sel == null)
                                continue;
                            if (!sel.Name.Equals("sekcja", StringComparison.InvariantCulture))
                                    throw new Exception("Unexpected Wiki node");

                            var section = new WikiSection(int.Parse(sel.GetAttribute("id")), sel.GetAttribute("tytul"));

                            foreach (var par in sel.ChildNodes)
                            {
                                XmlElement parel = par as XmlElement;
                                if (parel == null)
                                    continue;

                                switch (parel.Name.ToUpper())
                                {
                                    case "P":
                                    {
                                        section.Paragraphs.Add(new Paragraph(parel.InnerXml));
                                        break;
                                    }
                                }
                            }

                            page.Sections.Add(section.Id, section);
                        }
                        break;
                    }
                    default:
                        throw new Exception("Unknow Wiki section element");
                }
            }
            


            return page;
        }
    }
}
