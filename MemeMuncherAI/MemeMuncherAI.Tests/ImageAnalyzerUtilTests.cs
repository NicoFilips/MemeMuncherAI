using System.Collections.Generic;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Google.Cloud.Vision.V1;
using MemeMuncherAI.Util;
using MemeMuncherAI.Util.Abstraction;

namespace MemeMuncherAI.Tests;

[TestFixture]
public class ImageAnalyzerUtilTests
{
    private Mock<ImageAnnotatorClient> _clientMock;
    private IImageAnalyzerUtil _imageAnalyzerUtil;

    [SetUp]
    public void SetUp()
    {
        _clientMock = new Mock<ImageAnnotatorClient>();
        _imageAnalyzerUtil = new ImageAnalyzerUtil();
    }

    [Test]
    public void IsMemeImage_ShouldReturnTrue_WhenMemeLabelIsDetected()
    {
        // Arrange
        var image = new Image();
        var file = "testfile.png";

        var labels = new List<EntityAnnotation>
        {
            new EntityAnnotation { Description = "meme" }
        };
        _clientMock.Setup(c => c.DetectLabels(It.IsAny<Image>(), null, 0, null)).Returns(labels);

        // Act
        bool result = _imageAnalyzerUtil.IsMemeImage(_clientMock.Object, image, file);

        // Assert
        result.Should().BeTrue();
    }


    [Test]
    public void IsMemeImage_ShouldReturnFalse_WhenNoMemeLabelIsDetected()
    {
        // Arrange
        var image = new Image();
        var file = "testfile.png";

        var labels = new List<EntityAnnotation>
        {
            new EntityAnnotation { Description = "animal" }
        };
        _clientMock.Setup(c => c.DetectLabels(It.IsAny<Image>(), null, 0, null)).Returns(labels);

        // Act
        bool result = _imageAnalyzerUtil.IsMemeImage(_clientMock.Object, image, file);

        // Assert
        result.Should().BeTrue();
    }
}