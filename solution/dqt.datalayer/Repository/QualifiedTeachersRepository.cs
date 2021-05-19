using dqt.datalayer.Database;
using dqt.datalayer.Model;

namespace dqt.datalayer.Repository
{
    public class QualifiedTeachersRepository : GenericRepository<QualifiedTeacher, DQTDataContext>
    {
        public QualifiedTeachersRepository(DQTDataContext context) : base(context) { }
    }
}
