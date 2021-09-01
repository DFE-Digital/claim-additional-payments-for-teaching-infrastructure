using System;
using dqt.datalayer.Model;
using dqt.datalayer.Repository;
using dqt.domain.DTOs;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace dqt.domain.QTS
{
    public class QualifiedTeachersService : IQualifiedTeachersService
    {
        private static readonly DateTimeFormatInfo DATE_TIME_FORMAT_INFO = CultureInfo.CreateSpecificCulture("en-GB").DateTimeFormat;

        private readonly IRepository<QualifiedTeacher> _qualifiedTeachersRepository;

        public QualifiedTeachersService(IRepository<QualifiedTeacher> repo)
        {
            _qualifiedTeachersRepository = repo;
        }

        public async Task<IEnumerable<QualifiedTeacherDTO>> GetQualifiedTeacherRecords(string teacherReferenceNumber, string nationalInsuranceNumber)
        {
            var fullTeacherReferenceNumber = teacherReferenceNumber.PadLeft(7, '0');
            var trimmedTeacherReferenceNumber = teacherReferenceNumber.TrimStart('0');
            var qts = await _qualifiedTeachersRepository.FindAsync(x => x.Trn == fullTeacherReferenceNumber || x.Trn == trimmedTeacherReferenceNumber);

            if (!qts.Any())
            {
                if (!string.IsNullOrWhiteSpace(nationalInsuranceNumber))
                {
                    qts = await _qualifiedTeachersRepository.FindAsync(
                        x => EF.Functions.ILike(x.NINumber, nationalInsuranceNumber)
                        );
                }
            }

            if (qts == null)
            {
                return null;
            }

            var qtsList = new List<QualifiedTeacherDTO>();
            qts.ToList().ForEach(model=>
            {
                qtsList.Add(ConvertToDto(model));
            });

            return qtsList;
        }

        private QualifiedTeacherDTO ConvertToDto(QualifiedTeacher model)
        {
            if (model == null)
            {
                return null;
            }

            var result = new QualifiedTeacherDTO();

            result.Id = model.Id;
            result.Trn = model.Trn.PadLeft(7, '0');
            result.Name = model.Name;
            result.DoB = StringToDate(model.DoB, nameof(model.DoB));
            result.NINumber = model.NINumber;
            result.QTSAwardDate = StringToDate(model.QTSAwardDate, nameof(model.QTSAwardDate));
            result.ITTSubject1Code = model.ITTSubject1Code;
            result.ITTSubject2Code = model.ITTSubject2Code;
            result.ITTSubject3Code = model.ITTSubject3Code;
            result.ActiveAlert = model.ActiveAlert;
            result.QualificationName = model.QualificationName;
            result.ITTStartDate = StringToDate(model.ITTStartDate, nameof(model.ITTStartDate));
            result.TeacherStatus = model.TeacherStatus;

            return result;
        }

        private static DateTime? StringToDate(string date, string name = "")
        {
            if (string.IsNullOrEmpty(date) || string.Equals(date, "NULL", StringComparison.CurrentCultureIgnoreCase))
            {
                return null;
            }

            try
            {
                return Convert.ToDateTime(date, DATE_TIME_FORMAT_INFO);
            }
            catch(FormatException e)
            {
                throw new FormatException($"{name}: {date} threw FormatException converting to DateTime", e);
            }
        }

    }
}
