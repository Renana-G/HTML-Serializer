using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTML_serializer
{
    public class HtmlElement
    {
        public string Id = "";

        public string Name = "";

        public Dictionary<string, string> Attributes = new();

        public List<string> Classes = new();

        public string InnerHTML;

        public HtmlElement Parent;

        public HashSet<HtmlElement> Children = new();

         
        public IEnumerable<HtmlElement> Descendants()
        {
            Queue<HtmlElement> queue = new Queue<HtmlElement>();
            queue.Enqueue(this);
            while (queue.Count > 0)
            {
                HtmlElement currentElement = queue.Dequeue();
                yield return currentElement;
                foreach (HtmlElement child in currentElement.Children) 
                { 
                    queue.Enqueue(child);
                }
            }

        }

        public IEnumerable<HtmlElement> Ancestors()
        {
            HtmlElement currentElement = this;
            while (currentElement != null)
            {
                yield return currentElement;
                currentElement = currentElement.Parent;
            }
        }

        public override string ToString()
        {
            return $"<{Name} id=\"{Id}\", classes=\"{string.Join(" ", Classes)}\">";
        }

    }
}
