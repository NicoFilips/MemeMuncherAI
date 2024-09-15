using MemeMuncherAI.Util.Abstraction;

namespace MemeMuncherAI.Util;

public class DirectoryUtil : IDirectoryUtil
{
    public void CreateDirectories(params string[] paths)
    {
        foreach (var path in paths)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}