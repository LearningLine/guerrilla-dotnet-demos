using System.ServiceModel;

namespace Contracts
{
    [ServiceContract]
    public interface IAccumulate
    {
        [OperationContract(IsOneWay = true)]
        void Add(int val);
    }
}