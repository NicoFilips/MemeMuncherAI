using Google.Cloud.Vision.V1;
using MemeMuncherAI.DependencyInjection;
using MemeMuncherAI.Util.Abstraction;
using Microsoft.Extensions.DependencyInjection;

namespace MemeMuncherAI
{
    class Program
    {
        private const string CredentialPath = @"C:\Users\busin\Desktop\credentials.json";
        private const string ImagePath = @"C:\Users\busin\Desktop\GCP-AI-Detect-Memes";
        private static readonly string IsMemePath = Path.Combine(ImagePath, "IsMeme");
        private static readonly string NotMemePath = Path.Combine(ImagePath, "NotMeme");

        private static IDirectoryUtil? _directoryUtil = null;
        private static IFileUtil? _fileUtil = null;
        private static IImageAnalyzerUtil? _imageAnalyzerUtil = null;


        private static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddMemeMuncherServices()
                .BuildServiceProvider();

            _directoryUtil = serviceProvider.GetService<IDirectoryUtil>();
            _fileUtil = serviceProvider.GetService<IFileUtil>();
            _imageAnalyzerUtil = serviceProvider.GetService<IImageAnalyzerUtil>();

            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", CredentialPath);
            var client = ImageAnnotatorClient.Create();

            _directoryUtil?.CreateDirectories(IsMemePath, NotMemePath);

            foreach (var file in Directory.EnumerateFiles(ImagePath))
            {
                var image = Image.FromFile(file);
                bool isMeme = _imageAnalyzerUtil!.IsMemeImage(client, image, file);
                _fileUtil?.MoveFile(file, isMeme ? IsMemePath : NotMemePath, isMeme);
            }
        }
    }
}
