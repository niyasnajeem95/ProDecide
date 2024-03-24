using System.ComponentModel.DataAnnotations;

namespace ProDecide.Model
{
    public class SearchModel
    {
      public string model { get; set; }
       public List<ContentData> messages { get; set; }

    }
    public class ContentData
    {
        public string role { get; set; }
        public string content { get; set; }
    }
}
