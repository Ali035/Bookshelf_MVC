using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookshelf.Utility
{
    public enum ActionModeEnum
    {
        Create,
        Update,
        Delete
    }

    public class FilePath
    {
        public string WebRootPath { get; set; }
        public string NewFileName { get; set; }
        public string Url { get; set; }
        public string FileStreamPath { get; set; }
        private string EntityName { get; set; }

        public FilePath(string webRootPath, string entityName, string fileName)
        {
            EntityName = entityName;
            WebRootPath = webRootPath;
            NewFileName = Guid.NewGuid().ToString() + Path.GetExtension(fileName);
            Url = @$"\images\{EntityName}\{NewFileName}";
            FileStreamPath = Path.Combine(WebRootPath, Url.TrimStart('\\'));
        }
    }
}
