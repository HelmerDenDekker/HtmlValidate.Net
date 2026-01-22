namespace HtmlValidate.Net.Rules;

public interface IHandler<T> where T : class
{
    void Handle(T request);

    IHandler<T> SetNext(IHandler<T> nextHandler);
}