namespace HtmlValidate.Net.Core;

public abstract class HandlerBase<T> : IHandler<T> where T : class
{
    private IHandler<T>? _nextHandler;

    public void Handle(T request)
    {
        HandleInternal(request);
        _nextHandler?.Handle(request);
    }

    public IHandler<T> SetNext(IHandler<T> nextHandler)
    {
        _nextHandler = nextHandler;
        return _nextHandler;
    }

    protected abstract void HandleInternal(T request);
}