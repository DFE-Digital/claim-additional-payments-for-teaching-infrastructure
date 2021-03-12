using Microsoft.Azure.WebJobs;

namespace dqt.domain.SFTPToBlob
{
    public interface ISFTPToBlobProcessor
    {
        void SaveCSVToBlob(ExecutionContext context);
    }
}
