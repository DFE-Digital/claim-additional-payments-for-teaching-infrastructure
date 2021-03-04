using dqt.datalayer;
using System.Collections.Generic;

namespace dqt.api.Repository
{
    public interface IRepository
    {
        List<QualifiedTeacher> GetQualifiedTeacherRecords();
    }
}
