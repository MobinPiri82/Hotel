namespace Hotel.Result;

public readonly record struct Error(string Code, string Description)
{
    public static readonly Error None = new ("", "");
    public bool IsNone => string.IsNullOrEmpty(Code);
}

public readonly struct Result
{
    public bool IsSuccess { get; }

    public Error[] Errors { get; }

    private Result(bool isSuccess, Error[] errors) 
        => (IsSuccess, Errors) = (isSuccess,errors);

    public static Result Success() => new(true, Array.Empty<Error>());

    public static Result Failuar(params Error[] errors) => new (false,errors);

    public static Result Combine(params Result[] results)
        => results.Any(r => !r.IsSuccess)
        ? Failuar(results.Where(r => !r.IsSuccess).SelectMany(r => r.Errors).ToArray())
        : Success();

}

public readonly record struct Result<T>
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public Error[] Errors { get; }

    private Result(bool isSuccess, T? value,Error[] errors)
        => (IsSuccess, Value ,Errors) = (isSuccess, value ,errors);

    public static Result<T> Success(T value) => new(true, value ,Array.Empty<Error>());

    public static Result<T> Failuar(params Error[] errors) => new(false, default ,errors);
    public static Result<T> NotFound(params Error[] errors) => new(false, default ,[]);

    public Result<K> Map<K>(Func<T, K> map)
        => IsSuccess ? Result<K>.Success(map(Value)) : Result<K>.Failuar(Errors);
    public Result<K> Bind<K> (Func<T, Result<K>> next)
        => IsSuccess ? next(Value) : Result<K>.Failuar (Errors); 
    public Result<T> Ensure(Func<T ,bool> predicate, Error error)
        => IsSuccess && !predicate(Value) ? Failuar(Errors) : this;

}



























