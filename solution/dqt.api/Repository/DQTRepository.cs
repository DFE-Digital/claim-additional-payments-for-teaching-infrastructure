using dqt.datalayer.Database;
using dqt.datalayer.Model;
using System.Collections.Generic;
using System.Linq;

namespace dqt.api.Repository
{
    public class DQTRepository : IRepository
    {
        public DQTDataContext _context;

        public DQTRepository(DQTDataContext context)
        {
            _context = context;
        }
        public List<QualifiedTeacher> GetQualifiedTeacherRecords()
        {
            return null;
        }
    }
}
