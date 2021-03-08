using dqt.datalayer.Model;
using dqt.datalayer.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dqt.domain
{
    public class QualifiedTeachersService : IQualifiedTeachersService
    {
        private readonly IRepository<QualifiedTeacher> _qualifiedTeachersRepository;

        public QualifiedTeachersService(IRepository<QualifiedTeacher> repo)
        {
            _qualifiedTeachersRepository = repo;
        }

        public async Task<IEnumerable<QualifiedTeacher>> GetQualifiedTeacherRecords(string teacherReferenceNumber, string nationalInsuranceNumber)
        {
            var qts = await _qualifiedTeachersRepository.FindAsync(x => x.TeacherReferenceNumber == teacherReferenceNumber);
            
            if (!qts.Any())
            {
                qts = await _qualifiedTeachersRepository.FindAsync(x => x.NationalInsuranceNumber == nationalInsuranceNumber);
            }

            return qts;
        }
    }
}
