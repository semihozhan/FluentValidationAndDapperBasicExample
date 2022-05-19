using System.ComponentModel.DataAnnotations;

namespace DapperTest.Models
{
    public class TodoItems
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
    }
}
