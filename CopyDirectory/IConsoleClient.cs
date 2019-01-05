using System.Threading.Tasks;

namespace CopyDirectory
{
    public interface IConsoleClient
    {
        Task TriggerWithInitialValues(string initialSource, string initialDestination);

        Task Trigger();
    }
}
