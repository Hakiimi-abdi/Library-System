namespace Library_Mangement_System
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Splasher splash = new Splasher();
            splash.ShowDialog();

            Application.Run(new Login());
        }
    }
}