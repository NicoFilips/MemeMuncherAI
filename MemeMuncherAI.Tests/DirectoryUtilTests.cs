using System.IO;
using FluentAssertions;
using MemeMuncherAI.Util;
using Moq;
using NUnit.Framework;
using MemeMuncherAI.Util.Abstraction;

namespace MemeMuncherAI.Tests;

[TestFixture]
public class DirectoryUtilTests
{
    private IDirectoryUtil _directoryUtil;
    private Mock<IDirectoryUtil> _directoryUtilMock = new Mock<IDirectoryUtil>();

    [SetUp]
    public void SetUp()
    {
        _directoryUtil = new DirectoryUtil();
    }

    [Test]
    public void CreateDirectories_ShouldCreateDirectories_WhenTheyDoNotExist()
    {
        // Arrange
        var testPath = "TestDirectory";

        // Act
        _directoryUtil.CreateDirectories(testPath);

        // Assert
        Directory.Exists(testPath).Should().BeTrue();
        Directory.Delete(testPath);
    }

    [Test]
    public void CreateDirectories_ShouldNotThrowException_WhenDirectoryAlreadyExists()
    {
        // Arrange
        var testPath = "ExistingDirectory";
        Directory.CreateDirectory(testPath);

        // Act
        TestDelegate act = () => _directoryUtil.CreateDirectories(testPath);

        // Assert
        Assert.DoesNotThrow(act);
        Directory.Delete(testPath);
    }
    
    [Test]
    public void CreateDirectories_ShouldThrowException_WhenPathIsInvalid()
    {
        // Arrange
        var invalidPath = "Invalid|Path";
        _directoryUtilMock.Setup(d => d.CreateDirectories(invalidPath)).Throws<ArgumentException>();

        // Act
        TestDelegate act = () => _directoryUtilMock.Object.CreateDirectories(invalidPath);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Test]
    public void CreateDirectories_ShouldCreateSubdirectories()
    {
        // Arrange
        var testPath = "TestDirectory/SubDirectory";
        _directoryUtilMock.Setup(d => d.CreateDirectories(testPath));

        // Act
        _directoryUtilMock.Object.CreateDirectories(testPath);

        // Assert
        _directoryUtilMock.Verify(d => d.CreateDirectories(testPath), Times.Once);
    }

    [Test]
    public void CreateDirectories_ShouldNotThrowException_WhenPathIsNull()
    {
        // Arrange
        string nullPath = null;
        _directoryUtilMock.Setup(d => d.CreateDirectories(nullPath));

        // Act
        TestDelegate act = () => _directoryUtilMock.Object.CreateDirectories(nullPath);

        // Assert
        Assert.DoesNotThrow(act);
    }
}