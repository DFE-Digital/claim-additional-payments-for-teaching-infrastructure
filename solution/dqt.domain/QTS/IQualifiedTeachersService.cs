using dqt.datalayer.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dqt.domain.QTS
{
    public interface IQualifiedTeachersService
    {
        Task<IEnumerable<QualifiedTeacher>> GetQualifiedTeacherRecords(string teacherReferenceNumber, string nationalInsurance);
    }
}
