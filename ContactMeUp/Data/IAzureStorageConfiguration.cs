namespace ContactMeUp.Data
{
    public interface IAzureStorageConfiguration
    {
        string ConnectionStringName { get; }
        string TableName { get; }
        bool IsReadOnly { get; }
    }
}