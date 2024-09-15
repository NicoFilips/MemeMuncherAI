using MemeMuncherAI.Util.Abstraction;

namespace MemeMuncherAI.Util;

public class FileUtil : IFileUtil
{
    public void MoveFile(string sourcePath, string destinationDirectory, bool isMeme)
    {
        string destPath = Path.Combine(destinationDirectory, Path.GetFileName(sourcePath));

        try
        {
            File.Move(sourcePath, destPath);
            Console.WriteLine(isMeme ? "This image is likely a meme." : "This image is not identified as a meme.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to move file {sourcePath}: {ex.Message}");
        }
    }
}