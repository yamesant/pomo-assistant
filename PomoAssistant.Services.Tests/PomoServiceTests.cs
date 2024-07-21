using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using PomoAssistant.Core;

namespace PomoAssistant.Services.Tests;

public sealed class PomoServiceTests
{
    private IPomoService _sut;

    [SetUp]
    public void Setup()
    {
        _sut = new PomoService();
    }

    [Test]
    public void GetById_ReturnsNull_WhenNotExists()
    {
        // Arrange
        Guid id = new();

        // Act
        var result = _sut.GetById(id);

        // Assert
        result.Should().BeNull();
    }

    [Test, AutoData]
    public void GetById_ReturnsNonNull_WhenExists(PomoModels.CreateRequest request)
    {
        // Arrange
        var expectedResult = _sut.Create(request);
        Guid id = expectedResult.Id;

        // Act
        var result = _sut.GetById(id);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Test, AutoData]
    public void Search_FindsFrom(PomoModels.CreateRequest request)
    {
        // Arrange
        var model = _sut.Create(request);
        var searchRequest = new PomoModels.SearchRequest { From = model.CompletedAt };

        // Act
        var result = _sut.Search(searchRequest);

        // Assert
        result.Should().ContainEquivalentOf(model).And.HaveCount(1);
    }

    [Test, AutoData]
    public void Search_FindsTo(PomoModels.CreateRequest request)
    {
        // Arrange
        var model = _sut.Create(request);
        var searchRequest = new PomoModels.SearchRequest { To = model.CompletedAt };

        // Act
        var result = _sut.Search(searchRequest);

        // Assert
        result.Should().ContainEquivalentOf(model).And.HaveCount(1);
    }

    [Test, AutoData]
    public void Search_NotFindsOutsideRange(PomoModels.CreateRequest request)
    {
        // Arrange
        _sut.Create(request);
        var searchRequest = new PomoModels.SearchRequest
        {
            From = request.CompletedAt.AddSeconds(-2),
            To = request.CompletedAt.AddSeconds(-1),
        };

        // Act
        var result = _sut.Search(searchRequest);

        // Assert
        result.Should().BeEmpty();
    }

    [Test, AutoData]
    public void Create_CreatesAllProperties(PomoModels.CreateRequest request)
    {
        // Act
        var result = _sut.Create(request);

        // Assert
        result.Should().BeEquivalentTo(request);
    }

    [Test, AutoData]
    public void Update_NothingIfNotExists(PomoModels.UpdateRequest updateRequest)
    {
        // Act
        var result = _sut.Update(updateRequest);

        // Assert
        result.Should().BeNull();
    }

    [Test, AutoData]
    public void Update_UpdatesAllPropertiesIfExists(
        PomoModels.CreateRequest createRequest,
        PomoModels.UpdateRequest updateRequest)
    {
        // Arrange
        var model = _sut.Create(createRequest);
        updateRequest.Id = model.Id;

        // Act
        var result = _sut.Update(updateRequest);

        // Assert
        result.Should().BeEquivalentTo(updateRequest);
    }
    
    [Test, AutoData]
    public void Patch_NothingIfNotExists(PomoModels.PatchRequest patchRequest)
    {
        // Act
        var result = _sut.Patch(patchRequest);

        // Assert
        result.Should().BeNull();
    }

    [Test, AutoData]
    public void Patch_UpdatesAllPropertiesIfExists(
        PomoModels.CreateRequest createRequest,
        PomoModels.PatchRequest patchRequest)
    {
        // Arrange
        var model = _sut.Create(createRequest);
        patchRequest.Id = model.Id;

        // Act
        var result = _sut.Patch(patchRequest);

        // Assert
        result.Should().BeEquivalentTo(patchRequest);
    }
    
    [Test, AutoData]
    public void Patch_UpdatesCompletedAt(
        PomoModels.CreateRequest createRequest,
        DateTime completedAt)
    {
        // Arrange
        var expectedResult = _sut.Create(createRequest);
        expectedResult.CompletedAt = completedAt;
        var patchRequest = new PomoModels.PatchRequest
        {
            Id = expectedResult.Id,
            CompletedAt = completedAt
        };
        
        // Act
        var result = _sut.Patch(patchRequest);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }
    
    [Test, AutoData]
    public void Patch_UpdatesIntent(
        PomoModels.CreateRequest createRequest,
        Intent intent)
    {
        // Arrange
        var expectedResult = _sut.Create(createRequest);
        expectedResult.Intent = intent;
        var patchRequest = new PomoModels.PatchRequest
        {
            Id = expectedResult.Id,
            Intent = intent
        };
        
        // Act
        var result = _sut.Patch(patchRequest);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }
    
    [Test, AutoData]
    public void Patch_UpdatesDescription(
        PomoModels.CreateRequest createRequest,
        string description)
    {
        // Arrange
        var expectedResult = _sut.Create(createRequest);
        expectedResult.Description = description;
        var patchRequest = new PomoModels.PatchRequest
        {
            Id = expectedResult.Id,
            Description = description
        };
        
        // Act
        var result = _sut.Patch(patchRequest);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }
}