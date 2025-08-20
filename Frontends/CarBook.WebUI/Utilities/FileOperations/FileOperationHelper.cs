
namespace CarBook.WebUI.Utilities.FileOperations
{
    public class FileOperationHelper : IFileOperationHelper
    {
        public async Task<string> CopyFileToFolder(FileProperty fileProperty)
        {
            bool isFolderExists = IsFolderExists(fileProperty.FilePath);
            string resource = Directory.GetCurrentDirectory();

            if (!isFolderExists)
            {
                Directory.CreateDirectory(resource + fileProperty.FilePath);
            }

            string extention = Path.GetExtension(fileProperty.LoadedFile.FileName);
            string userFileName = Guid.NewGuid().ToString() + extention;
            string saveLocation = resource + fileProperty.FilePath + userFileName;
            FileStream stream = new FileStream(saveLocation, FileMode.Create);

            await fileProperty.LoadedFile.CopyToAsync(stream);

            return userFileName;
        }

        public bool IsFolderExists(string pathName)
        {
            return Directory.Exists($@"{pathName}") ? true : false;
        }
    }
}
