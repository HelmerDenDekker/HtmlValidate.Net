namespace HtmlValidate.Net.Rules;

public class ChainBuilder<TRequest> where TRequest : class
{
    private HandlerBase<TRequest>? _currentHandler;
    private HandlerBase<TRequest>? _firstHandler;

    // Add Handler
    public ChainBuilder<TRequest> RegisterHandler<THandler>() where THandler : HandlerBase<TRequest>, new()
    {
        var handler = new THandler();

        if (_firstHandler == null)
        {
            _firstHandler = handler;
            _currentHandler = handler;
            return this;
        }

        _currentHandler.SetNext(handler);
        _currentHandler = handler;
        return this;
    }

    public HandlerBase<TRequest> Build()
    {
        return _firstHandler ?? throw new InvalidOperationException("There are no handlers registered.");
    }
}