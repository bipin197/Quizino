using Domain.Interfaces;
namespace Domain.Models
{
    public class User : EntityBase, IUser
    {
        public string UserName { get; set; }
        public string EmailId  { get; set; }      
        public string Location { get; set; }
        public string AccessType  { get; set; }
    }
}