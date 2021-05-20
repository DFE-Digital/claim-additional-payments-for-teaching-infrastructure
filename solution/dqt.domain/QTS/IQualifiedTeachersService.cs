using dqt.domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dqt.domain.QTS
{
    public interface IQualifiedTeachersService
    {
        Task<IEnumerable<QualifiedTeacherDTO>> GetQualifiedTeacherRecords(string teacherReferenceNumber, string nationalInsurance);
    }
}
