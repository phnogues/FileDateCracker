namespace FileDateCracker
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string CultureInfo => "en-US";

        protected override void OnStartup(StartupEventArgs e)
        {
            //CultureInfo ci = System.Globalization.CultureInfo.CreateSpecificCulture(CultureInfo);
            //ci.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            //Thread.CurrentThread.CurrentCulture = ci;
            //Thread.CurrentThread.CurrentUICulture = ci;

            base.OnStartup(e);
        }
    }
}
