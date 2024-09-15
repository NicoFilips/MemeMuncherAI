namespace MemeMuncherAI.Util.Abstraction;

public interface IFileUtil
{
    void MoveFile(string sourcePath, string destinationDirectory, bool isMeme);
}