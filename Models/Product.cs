namespace webapi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationDate { get; set; }
        public string ImageUrl { get; set; }
        //public List<Comment> Comments { get; set; } = new List<Comment>();
    }

}
