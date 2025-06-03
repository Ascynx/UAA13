public class Optional<T>
{
    private readonly T value;
    private readonly bool empty;

    private Optional(T value)
    {
        this.value = value;
        empty = false;
    }

    private Optional()
    {
        empty = true;
    }

    public static Optional<T> Of(T value)
    {
        return new Optional<T>(value);
    }

    #nullable enable
    public static Optional<T> OfNullable(T? value)
    {
        return value == null ? new Optional<T>() : new Optional<T>(value);
    }

    public static Optional<T> Empty()
    {
        return new Optional<T>();
    }

    public bool IsPresent()
    {
        return value != null && !empty;
    }

    public bool IsEmpty()
    {
        return !IsPresent();
    }

    public T Get()
    {
        if (value == null || empty)
        {
            throw new System.Exception("No value present");
        }
        return value;
    }

    public T OrElse(T other)
    {
        return value == null || empty ? other : value;
    }

    public Optional<T> IfPresent(System.Action<T> action)
    {
        if (IsPresent())
        {
            action(value);
        }
        return this;
    }

    public Optional<T> Else(System.Action action)
    {
        if (IsEmpty())
        {
            action();
        }
        return this;
    }

    public override string ToString()
    {
        return $"Optional<{value.GetType()}>({value})";
    }
}
