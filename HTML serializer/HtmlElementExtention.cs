using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTML_serializer
{
    internal class HtmlElementExtention
    {
        public static void findElementBySelector(HtmlElement currentElement, Selector selector, HashSet<HtmlElement> res)
        {
            HashSet<HtmlElement> descendants = new (currentElement.Descendants());
            foreach (var d in descendants)
            {
                if (!isMatch(d, selector))
                    descendants.Remove(d);
            }


            if (selector.Child == null)
            {
                res.Add(currentElement);
                return;
            }

            //descendants.Where(d => isMatch(d, selector));
            foreach (HtmlElement descendant in descendants)
            {
                findElementBySelector(descendant, selector.Child, res);
            }
        }

        public static HashSet<HtmlElement> findElementBySelector(HtmlElement currentElement, Selector selector)
        {
            HashSet<HtmlElement> res = new();
            findElementBySelector(currentElement, selector, res);
            return res;
        }

        public static bool isMatch(HtmlElement element, Selector selector)
        {
            if(selector.Id != null && !selector.Id.Equals(element.Id))
                return false;
            if(selector.TagName != null && !selector.TagName.Equals(element.Name))
                return false;
            if (selector.Classes.Count() > 0 && !selector.Classes.All(item => element.Classes.Contains(item)))
                return false;

            return true;
        }
    }
}
