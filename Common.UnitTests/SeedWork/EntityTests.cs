using MediatR;

namespace Common.UnitTests.SeedWork;

public class EntityTests {
    // A simple mock event for testing
    private record TestDomainEvent : INotification;

    [Fact]
    public void DomainEvents_Should_BeAddedAndCleared()
    {
        // Arrange
        var entity = new TestEntity();
        var domainEvent = new TestDomainEvent();

        // Act
        entity.AddDomainEvent(domainEvent);

        // Assert
        Assert.Single(entity.DomainEvents!);
        
        // Act: Clear
        entity.ClearDomainEvents();
        
        // Assert
        Assert.Empty(entity.DomainEvents!);
    }

    [Fact]
    public void IsTransient_Should_BeTrue_WhenIdIsEmpty()
    {
        var entity = new TestEntity();
        Assert.True(entity.IsTransient());
    }

    [Fact]
    public void Equals_Should_ReturnTrue_WhenIdsMatch()
    {
        // Arrange
        var id = Guid.NewGuid();
        var entity1 = new TestEntity();
        entity1.SetId(id);
        
        var entity2 = new TestEntity();
        entity2.SetId(id);

        // Assert
        Assert.True(entity1.Equals(entity2));
        Assert.True(entity1 == entity2);
    }

    [Fact]
    public void Equals_Should_ReturnFalse_WhenTypesDiffer()
    {
        // Arrange
        var id = Guid.NewGuid();
        var entity1 = new TestEntity();
        entity1.SetId(id);
        
        var entity2 = new AnotherTestEntity();
        // Even if we forced the same ID, they are different classes
        // (In a real DB this shouldn't happen, but logic should hold)
        
        // Assert
        Assert.False(entity1.Equals(entity2));
    }

    [Fact]
    public void GetHashCode_Should_BeSame_ForSameId()
    {
        // Arrange
        var id = Guid.NewGuid();
        var entity1 = new TestEntity();
        entity1.SetId(id);
        
        var entity2 = new TestEntity();
        entity2.SetId(id);

        // Assert
        Assert.Equal(entity1.GetHashCode(), entity2.GetHashCode());
    }
}