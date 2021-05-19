using System.IO;
using System.Threading.Tasks;

namespace dqt.domain
{
    public interface ICSVProcessor
    {
        Task SaveCSVDataToDatabase(Stream csvBLOB, string name);
    }
}
