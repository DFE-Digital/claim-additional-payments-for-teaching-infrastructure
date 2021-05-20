using System.Runtime.CompilerServices;
using System;
using dqt.datalayer.Model;
using dqt.datalayer.Repository;
using dqt.domain.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dqt.domain.QTS
{
    public class QualifiedTeachersService : IQualifiedTeachersService
    {
        private readonly IRepository<QualifiedTeacher> _qualifiedTeachersRepository;

        public QualifiedTeachersService(IRepository<QualifiedTeacher> repo)
        {
            _qualifiedTeachersRepository = repo;
        }

        public async Task<IEnumerable<QualifiedTeacherDTO>> GetQualifiedTeacherRecords(string teacherReferenceNumber, string nationalInsuranceNumber)
        {
            var qts = await _qualifiedTeachersRepository.FindAsync(x => x.Trn == teacherReferenceNumber);
            
            if (!qts.Any())
            {
                if (!string.IsNullOrWhiteSpace(nationalInsuranceNumber))
                {
                    qts = await _qualifiedTeachersRepository.FindAsync(x => x.NINumber == nationalInsuranceNumber);
                }
            }
            if(qts == null)
                return null;
            
            var qtsList = new List<QualifiedTeacherDTO>();
            qts.ToList().ForEach(model=> {
                qtsList.Add(ConvertToDto(model));
            });

            return qtsList;
        }

        private QualifiedTeacherDTO ConvertToDto(QualifiedTeacher model)
        {
            if(model == null)
                return null;
            return new QualifiedTeacherDTO
            {
                Id = model.Id,
                Trn = model.Trn,
                Name = model.Name,
                DoB = model.DoB,
                NINumber = model.NINumber,
                QTSAwardDate = model.QTSAwardDate,
                ITTSubject1Code = model.ITTSubject1Code,
                ITTSubject2Code = model.ITTSubject2Code,
                ITTSubject3Code = model.ITTSubject3Code,
                ActiveAlert = model.ActiveAlert,
                QualificationName = model.QualificationName,
                ITTStartDate = StringToDate(model.ITTStartDate),
                TeacherStatus = model.TeacherStatus
            };
        }
        private DateTime? StringToDate(string date) {
            if(string.IsNullOrEmpty(date)|| date == "NULL")
                return null;
            return Convert.ToDateTime(date);
        }

    }
}
