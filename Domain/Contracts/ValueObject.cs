namespace Domain.Contracts;

public abstract class SimpleValueObject<T>
{
    public T Value { get; }

    public SimpleValueObject(T value) => Value = Validate(value);

    public abstract T Validate(T value);
}