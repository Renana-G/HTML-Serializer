using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HTML_serializer
{
    public class Selector
    {
        public string Id { get; set; }
       
        public string TagName { get; set; }

        public List<string> Classes = new();
        
        public Selector Parent { get; set; }

        public Selector Child = null;

        public static Selector convertStringToSelector(string query)
        {
            Selector root = new();
            Selector currentSelector = root;

            List<string> subQueries = query.Split(" ").ToList();
            foreach (var subQuery in subQueries)
            {
                List<string> values = Regex.Split(subQuery, @"(?=[#\.])").Where(s => s!="").ToList();
                
                foreach (var value in values)
                {
                    switch (value[0])
                    {
                        case '#':
                            currentSelector.Id = value.Substring(1);
                            break;

                        case '.':
                            currentSelector.Classes.Add(value.Substring(1));
                            break;

                        default:
                            if (HtmlHelper.Instance.HtmlTags.Contains(value))
                                currentSelector.TagName = value;
                            break;
                    }                      
                }

                currentSelector.Child = new();
                currentSelector.Child.Parent = currentSelector;
                currentSelector = currentSelector.Child;
            }
            return root;
        }
    }
}
