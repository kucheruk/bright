using System.Threading.Tasks;
using bright.Data;
using MongoDB.Driver;

namespace bright.Actors
{
    public class ProjectMetaSaveCommand
    {
        private readonly MongoStore _ms;

        public ProjectMetaSaveCommand(MongoStore ms)
        {
            _ms = ms;
        }

        public async Task SaveAsync(ProjectMetaData pmd)
        {
            await _ms.ProjectMeta.ReplaceOneAsync(Builders<ProjectMetaData>.Filter.Eq(a => a.Id, pmd.Id),
                pmd, new ReplaceOptions
                {
                    IsUpsert = true
                });
        }
    }
}