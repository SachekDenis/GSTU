using BusinessLogic;

namespace ImformLab1
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var fileService = new FileService();
            fileService.ConvertFile(args);
        }
    }
}
