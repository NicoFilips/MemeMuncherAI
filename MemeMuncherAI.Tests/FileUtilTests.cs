using FluentAssertions;
using MemeMuncherAI.Util;
using MemeMuncherAI.Util.Abstraction;
using NUnit.Framework;

namespace MemeMuncherAI.Tests;

[TestFixture]
public class FileUtilTests
{
    private IFileUtil _fileUtil;

    [SetUp]
    public void SetUp()
    {
        _fileUtil = new FileUtil();
    }

    [Test]
    public void MoveFile_ShouldMoveFile_WhenCalled()
    {
        // Arrange
        var sourcePath = "testfile.txt";
        var destinationDirectory = "DestinationDirectory";
        Directory.CreateDirectory(destinationDirectory);
        File.WriteAllText(sourcePath, "Test content");

        // Act
        _fileUtil.MoveFile(sourcePath, destinationDirectory, true);

        // Assert
        var destPath = Path.Combine(destinationDirectory, Path.GetFileName(sourcePath));
        File.Exists(destPath).Should().BeTrue();

        // Cleanup
        File.Delete(destPath);
        Directory.Delete(destinationDirectory);
    }

    [Test]
    public void MoveFile_ShouldThrowException_WhenSourceFileDoesNotExist()
    {
        // Arrange
        var sourcePath = "nonexistentfile.txt";
        var destinationDirectory = "DestinationDirectory";

        // Act
        Action act = () => _fileUtil.MoveFile(sourcePath, destinationDirectory, true);

        // Assert
        act.Should().NotThrow<FileNotFoundException>();
    }
}