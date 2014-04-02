using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuoWangDemo.Model
{
    public class SingleBookInforModel
    {
        public string ID { get; set; }
        public string Alt { get; set; }
        public RatingInfo Rating { get; set; }

        public List<NameInfo> AuthorList { get; set; }

        public string AltTitle { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string MobileLink { get; set; }
        public string Summary { get; set; }

        public AttrsInfo Attrs { get; set; }

        public List<TagsInfo> TagsList { get; set; }
    }

    public class TagsInfo
    {
        public int Count { get; set; }
        public string Name { get; set; }
    }
    public class RatingInfo
    {
        public int Max { get; set; }
        public string Average { get; set; }
        public int NumRaters { get; set; }
        public int Min { get; set; }
    }

    public class NameInfo
    {
        public string Name { get; set; }
    }

    public class AttrsInfo
    {
        public string Publisher { get; set; }
        public string Pubdate { get; set; }
        public string Author { get; set; }
        public string Price { get; set; }
        public string Title { get; set; }
        public string Binding { get; set; }
        public string Translator { get; set; }
        public string Pages { get; set; }
    }
}
