using MediatR;

namespace OneMoney.Common.SeedWork;

public abstract class Entity {
    private Guid _id;

    // We use a list to store events that happened to this entity
    private List<INotification>? _domainEvents;

    public virtual Guid Id {
        get => _id;
        protected set => _id = value;
    }

    public IReadOnlyCollection<INotification>? DomainEvents => _domainEvents?.AsReadOnly();

    public void AddDomainEvent(INotification eventItem) {
        _domainEvents ??= new List<INotification>();
        _domainEvents.Add(eventItem);
    }

    public void ClearDomainEvents() => _domainEvents?.Clear();

    public bool IsTransient() => Id == Guid.Empty;

    public override bool Equals(object? obj) {
        if (obj is not Entity item) return false;
        if (ReferenceEquals(this, item)) return true;
        if (GetType() != item.GetType()) return false;
        if (item.IsTransient() || IsTransient()) return false;

        return item.Id == Id;
    }

    public override int GetHashCode() => (GetType().ToString() + Id).GetHashCode();

    public static bool operator ==(Entity left, Entity right) {
        if (Object.Equals(left, null))
            return (Object.Equals(right, null)) ? true : false;
        else
            return left.Equals(right);
    }

    public static bool operator !=(Entity left, Entity right) {
        return !(left == right);
    }
}