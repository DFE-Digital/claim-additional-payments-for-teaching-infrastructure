using Microsoft.Azure.WebJobs;
using System.Threading.Tasks;

namespace dqt.domain.SFTPToBlob
{
    public interface ISFTPToBlobProcessor
    {
        Task SaveCSVToBlobAsync(ExecutionContext context);
    }
}
