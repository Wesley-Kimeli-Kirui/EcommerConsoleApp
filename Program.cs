
using EcommerConsoleApp.Controller;


class Program
{
    public async static Task Main(string[] args)
    {
        await ProductController.Init();
    }
}