namespace Domain.Interfaces
{
    public interface IUser : IEntityBase
    {
        string UserName{get;set;}
        string EmailId{get;set;}
        string Location{get;set;}
        string AccessType{get;set;}
    }
}