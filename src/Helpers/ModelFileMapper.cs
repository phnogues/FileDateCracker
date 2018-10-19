namespace FileDateCracker.Helpers
{
    using FileDateCracker.Models;

    public static class ModelFileMapper
    {
        public static File ToModelFile(this System.IO.FileInfo fileInfo, bool isChecked = true)
        {
            return new File()
            {
                Name = fileInfo.Name,
                Path =  fileInfo.FullName,
                CreatedDate = fileInfo.CreationTime,
                ModifiedDate  = fileInfo.LastWriteTime,
                LastAccessDate = fileInfo.LastAccessTime,
                IsChecked = isChecked
            };
        }
    }
}
