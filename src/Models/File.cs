namespace FileDateCracker.Models
{
    using System;

    public class File
    {
        public string Name { get; set; }
        public string Path { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime LastAccessDate { get; set; }

        public bool IsChecked { get; set; }
    }
}
