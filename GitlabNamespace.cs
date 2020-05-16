using MongoDB.Bson.Serialization.Attributes;

[BsonIgnoreExtraElements]
public class GitlabNamespace
{
    [BsonId]
    public int Id { get; set; }

    public string Name { get; set; }

    public string FullPath { get; set; }

    public string Kind { get; set; }

    public string Path { get; set; }

}