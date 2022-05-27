using Contracts.Dtos;

namespace appDostava.DtoFactory
{

    public delegate ICanBeValidated Make();

    public class DtoFactory : IDtoFactory
    {
        public T GetInstance<T>() where T : ICanBeValidated,new()
        {
            return new T();
        }
    }
}
