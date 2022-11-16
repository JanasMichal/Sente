namespace Commisions.Business.Contract
{
    public interface IProcessor
    {
        string Name { get; }
        object Process(object data, object conditionalData = null);
    }
}
