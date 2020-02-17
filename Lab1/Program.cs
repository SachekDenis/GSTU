using BusinessLogic;

namespace ImformLab1
{
    public class Program
    {
        static void Main(string[] args)
        {
            FileService fileService = new FileService();
            fileService.ConvertFile(args);
        }
    }
}
