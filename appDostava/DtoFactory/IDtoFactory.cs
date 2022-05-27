using Contracts.Dtos;

namespace appDostava.DtoFactory
{
    public interface IDtoFactory
    {
        T GetInstance<T>() where T : ICanBeValidated,new();
    }
}
