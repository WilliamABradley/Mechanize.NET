using ConsoleHelpers;

namespace Test_Mechanize
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Init();
            Helpers.PreventClose();
        }

        private static async void Init()
        {
            await TestGoogle.Test();
        }
    }
}