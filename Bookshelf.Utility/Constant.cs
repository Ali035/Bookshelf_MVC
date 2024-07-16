namespace Bookshelf.Utility
{
    public enum ActionModeEnum
    {
        Create,
        Update,
        Delete
    }

    public class ResultMessage(bool success, string message)
    {
        public bool Success { get; set; } = success;
        public string Message { get; set; } = message;
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

    public class FileManager(string webRootPath)
    {
        public string WebRootPath { get; set; } = webRootPath;

        public void RemoveFileIfExists(string filePath)
        {
            string oldImagePath = Path.Combine(WebRootPath, filePath.TrimStart('\\'));
            if (File.Exists(oldImagePath))
            {
                File.Delete(oldImagePath);
            }
        }
    }
}
