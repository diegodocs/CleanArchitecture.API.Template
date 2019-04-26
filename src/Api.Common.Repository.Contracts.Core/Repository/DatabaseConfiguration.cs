namespace Api.Common.Repository.Contracts.Core.Repository
{
    public class DatabaseConfiguration : IDatabaseConfiguration
    {
        public string ConnectionString { get; set; }
    }
}