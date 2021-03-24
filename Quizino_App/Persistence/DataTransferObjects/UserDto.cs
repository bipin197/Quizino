using Domain.Interfaces;
using Domain.Models;

namespace Persistence.DataTransferObjects
{
    public class UserDto : EntityBase, IUser
    {
        public string UserName { get;set; }
        public string EmailId { get;set; }
        public string Location { get;set; }
        public string AccessType { get;set; }
    }
}
