using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using bright.Gitlab;
using GitLabApiClient.Internal.Paths;

namespace bright.Actors
{
    public class CsprojFileParser : IFileParser
    {
        private readonly GitlabInstanceGate _gitlab;

        public CsprojFileParser(GitlabInstanceGate gitlab)
        {
            _gitlab = gitlab;
        }
        public bool Match(string f)
        {
            return f.EndsWith(".csproj", StringComparison.InvariantCultureIgnoreCase);
        }

        public async Task<ProjectMetaData> RetrieveDataAsync(string path, ProjectId pid)
        {
            var c = await _gitlab.GetFileContentAsync(pid, path);
            await using var s = new MemoryStream(Encoding.UTF8.GetBytes(c.ContentDecoded));
            s.Seek(0,SeekOrigin.Begin);
            var doc = XDocument.Load(new XmlTextReader(s));
            var pmeta = new ProjectMetaData
            {
                Id = pid.ToString()
            };
            ExtractFramework(doc, pmeta);
            ExtractDependencies(pmeta, doc.XPathSelectElements("//PackageReference"));
            return pmeta;
        }

        private static void ExtractFramework(XDocument doc, ProjectMetaData pmeta)
        {
            var tf = doc.XPathSelectElement("//TargetFramework");
            var fwValue = tf?.Value;
            pmeta.Framework = fwValue;
        }

        private static void ExtractDependencies(ProjectMetaData pmeta, IEnumerable<XElement> deps)
        {
            pmeta.Deps = new List<ProjectDependencyInfo>();
            if (deps != null)
            {
                foreach (var dep in deps)
                {
                    var name = dep.Attribute("Include");
                    var ver = dep.Attribute("Version");
                    pmeta.Deps.Add(new ProjectDependencyInfo()
                    {
                        Id = name?.Value,
                        Version = ver?.Value
                    });
                }
            }
        }
    }
}