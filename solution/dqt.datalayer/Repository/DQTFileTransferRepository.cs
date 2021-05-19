using dqt.datalayer.Database;
using dqt.datalayer.Model;

namespace dqt.datalayer.Repository
{
    public class DQTFileTransferRepository : GenericRepository<DQTFileTransfer, DQTDataContext>
    {
        public DQTFileTransferRepository(DQTDataContext context) : base(context) { }
    }
}
