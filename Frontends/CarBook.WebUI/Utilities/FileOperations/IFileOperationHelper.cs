namespace CarBook.WebUI.Utilities.FileOperations
{
    public interface IFileOperationHelper
    {
        Task<string> CopyFileToFolder(FileProperty fileProperty);
        bool IsFolderExists(string pathName);
    }
}
