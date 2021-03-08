using dqt.datalayer.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dqt.domain
{
    public interface IQualifiedTeachersService
    {
        Task<IEnumerable<QualifiedTeacher>> GetQualifiedTeacherRecords(string teacherReferenceNumber, string nationalInsurance);
    }
}
