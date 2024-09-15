using Google.Cloud.Vision.V1;

namespace MemeMuncherAI.Util.Abstraction;

public interface IImageAnalyzerUtil
{
    bool IsMemeImage(ImageAnnotatorClient client, Image image, string file);
}