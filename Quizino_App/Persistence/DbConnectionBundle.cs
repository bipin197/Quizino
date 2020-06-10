namespace Persistence
{
    public class DbConnectionBundle
    {
        public EndpointConnection EndpointConnection { get; set; }
        public string DatabaseId { get; set; }
        public string CollectionId { get; set; }
    }

    public class EndpointConnection
    {
        public string Endpoint { get; set; }
        public string Key { get; set; }
    }
}
