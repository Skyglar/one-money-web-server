using OneMoney.Common.SeedWork;

namespace Common.UnitTests.SeedWork;

public class TestEntity : Entity {
    public void SetId(Guid id) => base.Id = id;
}

public class AnotherTestEntity : Entity { }