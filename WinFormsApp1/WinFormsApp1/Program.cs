namespace WinFormsApp1
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Contains("--port"))
            {
                Form1.Port = Int32.Parse(args[Array.IndexOf(args, "--port") + 1]);
            }
            else if (args.Contains("-p"))
                Form1.Port = Int32.Parse(args[Array.IndexOf(args, "-p") + 1]);

            if (args.Contains("--server"))
            {
                Form1.ServerIP = args[Array.IndexOf(args, "--server") + 1];
            }
            else if (args.Contains("-s"))
                Form1.ServerIP = args[Array.IndexOf(args, "-s") + 1];



            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}