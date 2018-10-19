namespace FileDateCracker.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using File = FileDateCracker.Models.File;

    public static class FileHelper
    {
        public static void ChangeFilesDates(List<File> files, DateTime? createdDate, DateTime? createdTime, 
                                                              DateTime? lastWriteDate, DateTime? lastWriteTime,
                                                              DateTime? lastAccessDate, DateTime? lastAccessTime)
        {
            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file.Path);          

                System.IO.File.SetCreationTime(fileInfo.FullName, getDate(fileInfo.CreationTime, createdDate,createdTime));
                System.IO.File.SetLastWriteTime(fileInfo.FullName, getDate(fileInfo.LastWriteTime, lastWriteDate, lastWriteTime));
                System.IO.File.SetLastAccessTime(fileInfo.FullName, getDate(fileInfo.LastAccessTime, lastAccessDate, lastAccessTime));
            }
        }

        private static DateTime getDate(DateTime defaultDate, DateTime? date, DateTime? time)
        {
          return new DateTime(
              date?.Year ?? defaultDate.Year,
              date?.Month ?? defaultDate.Month,
              date?.Day ?? defaultDate.Day,
              time?.Hour ?? defaultDate.Hour,
              time?.Minute ?? defaultDate.Minute,
              time?.Second ?? defaultDate.Second);
        }
    }
}
