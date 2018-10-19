namespace FileDateCracker.Helpers
{
    public class DialogHelper
    {
        public static string OpenFolderDialog()
        {
            var openFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            var result = openFolderDialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                return openFolderDialog.SelectedPath;
            }

            return null;
        }

        public static string OpenFileDialog(string filter = "", string initialDirectory = "")
        {
            var openFileDialog = new System.Windows.Forms.OpenFileDialog();

            if (!string.IsNullOrEmpty(filter))
            {
                openFileDialog.Filter = filter;
            }

            if (!string.IsNullOrEmpty(initialDirectory))
            {
                openFileDialog.InitialDirectory = initialDirectory;
            }

            var result = openFileDialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                return openFileDialog.FileName;
            }

            return null;
        }
    }
}
