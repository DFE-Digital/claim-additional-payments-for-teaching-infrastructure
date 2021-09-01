using System.Collections.Generic;
using System.Globalization;
using Moq;
using Xunit;
using dqt.datalayer.Model;
using dqt.datalayer.Repository;
using dqt.domain.QTS;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using System;

namespace dqt.unittests.domain
{
    public class QualifiedTeachersServiceTests
    {
        private const string TRN = "TRN";
        private const string NI = "NI";

        private readonly IQualifiedTeachersService _qualifiedTeachersService;

        private readonly Mock<IRepository<QualifiedTeacher>> _qualifiedTeachersRepositoryMock;

        public QualifiedTeachersServiceTests()
        {
            _qualifiedTeachersRepositoryMock = new Mock<IRepository<QualifiedTeacher>>();
            _qualifiedTeachersService = new QualifiedTeachersService(_qualifiedTeachersRepositoryMock.Object);
        }

        [Fact]
        public async void Returns_QualifiedTeacherRecords_WhenTrnMatchFound()
        {
            var record = new QualifiedTeacher {Name = "TEST1", Trn = TRN, NINumber = NI};

            var fullTeacherReferenceNumber = record.Trn.PadLeft(7, '0');
            var trimmedTeacherReferenceNumber = record.Trn.TrimStart('0');
            _qualifiedTeachersRepositoryMock
                .Setup(q => q.FindAsync(x => x.Trn == fullTeacherReferenceNumber || x.Trn == trimmedTeacherReferenceNumber))
                .ReturnsAsync(new List<QualifiedTeacher>{ record });

            var results = await _qualifiedTeachersService.GetQualifiedTeacherRecords(TRN, NI);

            Assert.Equal(results.ToList().FirstOrDefault()?.Trn, fullTeacherReferenceNumber);
        }

        [Fact]
        public async void Returns_FullTrn_WhenTrnMatchFoundWithFullTrn()
        {
            var record = new QualifiedTeacher {Name = "TEST1", Trn = TRN, NINumber = NI};

            var fullTeacherReferenceNumber = record.Trn.PadLeft(7, '0');
            var trimmedTeacherReferenceNumber = record.Trn.TrimStart('0');
            _qualifiedTeachersRepositoryMock
                .Setup(q => q.FindAsync(x => x.Trn == fullTeacherReferenceNumber || x.Trn == trimmedTeacherReferenceNumber))
                .ReturnsAsync(new List<QualifiedTeacher>{ record });

            var results = await _qualifiedTeachersService.GetQualifiedTeacherRecords(fullTeacherReferenceNumber, NI);

            Assert.Equal(results.ToList().FirstOrDefault()?.Trn, fullTeacherReferenceNumber);
        }

        [Fact]
        public async void Returns_FullTrn_WhenTrnMatchFoundWithTrimmedTrn()
        {
            var record = new QualifiedTeacher {Name = "TEST1", Trn = TRN, NINumber = NI};

            var fullTeacherReferenceNumber = record.Trn.PadLeft(7, '0');
            var trimmedTeacherReferenceNumber = record.Trn.TrimStart('0');
            _qualifiedTeachersRepositoryMock
                .Setup(q => q.FindAsync(x => x.Trn == fullTeacherReferenceNumber || x.Trn == trimmedTeacherReferenceNumber))
                .ReturnsAsync(new List<QualifiedTeacher>{ record });

            var results = await _qualifiedTeachersService.GetQualifiedTeacherRecords(trimmedTeacherReferenceNumber, NI);

            Assert.Equal(results.ToList().FirstOrDefault()?.Trn, fullTeacherReferenceNumber);
        }

        [Fact]
        public async void Returns_QualifiedTeacherRecords_WhenNIMatchFound()
        {
            var record = new QualifiedTeacher {Name = "TEST1", Trn = TRN, NINumber = NI};

            _qualifiedTeachersRepositoryMock
                .Setup(q => q.FindAsync(x => EF.Functions.ILike(x.NINumber, NI)))
                .ReturnsAsync(new List<QualifiedTeacher>{ record });

            var results = await _qualifiedTeachersService.GetQualifiedTeacherRecords(TRN, NI);

            Assert.Equal(record.NINumber, results.ToList().FirstOrDefault()?.NINumber);
        }

        [Fact]
        public async void Returns_QualifiedTeacherRecords_WhenCaseInsensitiveNIMatchFound()
        {
            var record = new QualifiedTeacher {Name = "TEST1", Trn = TRN, NINumber = NI};
            var lowerNI = NI.ToLower();

            _qualifiedTeachersRepositoryMock
                .Setup(q => q.FindAsync(x => EF.Functions.ILike(x.NINumber, lowerNI)))
                .ReturnsAsync(new List<QualifiedTeacher>{ record });

            var results = await _qualifiedTeachersService.GetQualifiedTeacherRecords(TRN, lowerNI);

            Assert.Equal(record.NINumber, results.ToList().FirstOrDefault()?.NINumber);
        }

        [Fact]
        public async void Returns_Empty_WhenNoMatchFound()
        {
            _qualifiedTeachersRepositoryMock
                .Setup(q => q.FindAsync(x => x.Trn == TRN))
                .ReturnsAsync(new List<QualifiedTeacher>());

            _qualifiedTeachersRepositoryMock
                .Setup(q => q.FindAsync(x => x.NINumber == NI))
                .ReturnsAsync(new List<QualifiedTeacher>());

            var results = await _qualifiedTeachersService.GetQualifiedTeacherRecords(TRN, NI);

            Assert.Empty(results);
        }

        [Theory]
        [InlineData("0012390", "12/03/1990", "18/07/2019", "12/09/2017")]
        [InlineData("1643404", "17/02/1991", "07/07/2016", "14/09/2015")]
        [InlineData("3968203", "02/10/1995", "31/07/2020", "16/09/2019")]
        [InlineData("1643434", "23/03/1992", "14/07/2017", "07/09/2015")]
        [InlineData("1643432", "12/07/1985", "14/07/2016", "07/09/2015")]
        public async void Returns_QualifiedTeacherRecords_WithEnGBFormattedDates(string trn, string date1, string date2, string date3)
        {
            var originalCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");

            var record = new QualifiedTeacher
            {
                Trn = trn,
                DoB = date1,
                QTSAwardDate = date2,
                ITTStartDate = date3
            };

            var fullTeacherReferenceNumber = record.Trn.PadLeft(7, '0');
            var trimmedTeacherReferenceNumber = record.Trn.TrimStart('0');
            _qualifiedTeachersRepositoryMock
                .Setup(q => q.FindAsync(x => x.Trn == fullTeacherReferenceNumber || x.Trn == trimmedTeacherReferenceNumber))
                .ReturnsAsync(new List<QualifiedTeacher>{ record });

            var result = (await _qualifiedTeachersService.GetQualifiedTeacherRecords(trn, string.Empty)).FirstOrDefault();

            Assert.Equal(trn, result?.Trn);
            Assert.Equal(date1 + " 00:00:00", result?.DoB?.ToString(CultureInfo.CreateSpecificCulture("en-GB").DateTimeFormat));
            Assert.Equal(date2 + " 00:00:00", result?.QTSAwardDate?.ToString(CultureInfo.CreateSpecificCulture("en-GB").DateTimeFormat));
            Assert.Equal(date3 + " 00:00:00", result?.ITTStartDate?.ToString(CultureInfo.CreateSpecificCulture("en-GB").DateTimeFormat));

            Thread.CurrentThread.CurrentCulture = originalCulture;
        }

        [Fact]
        public async void Fails_WithIncorrectDoB()
        {
            var trn = "0012390";

            var record = new QualifiedTeacher
            {
                Trn = trn,
                DoB = "00:00.0",
            };

            var fullTeacherReferenceNumber = record.Trn.PadLeft(7, '0');
            var trimmedTeacherReferenceNumber = record.Trn.TrimStart('0');
            _qualifiedTeachersRepositoryMock
                .Setup(q => q.FindAsync(x => x.Trn == fullTeacherReferenceNumber || x.Trn == trimmedTeacherReferenceNumber))
                .ReturnsAsync(new List<QualifiedTeacher> { record });

            var e = await Assert.ThrowsAsync<FormatException>(() =>
                _qualifiedTeachersService.GetQualifiedTeacherRecords(trn, string.Empty)
            );

            Assert.StartsWith("DoB", e.Message);
        }

        [Fact]
        public async void Fails_WithIncorrectQTSDate()
        {
            var trn = "0012390";

            var record = new QualifiedTeacher
            {
                Trn = trn,
                QTSAwardDate = "00:00.0",
            };

            var fullTeacherReferenceNumber = record.Trn.PadLeft(7, '0');
            var trimmedTeacherReferenceNumber = record.Trn.TrimStart('0');
            _qualifiedTeachersRepositoryMock
                .Setup(q => q.FindAsync(x => x.Trn == fullTeacherReferenceNumber || x.Trn == trimmedTeacherReferenceNumber))
                .ReturnsAsync(new List<QualifiedTeacher> { record });

            var e = await Assert.ThrowsAsync<FormatException>(() =>
                _qualifiedTeachersService.GetQualifiedTeacherRecords(trn, string.Empty)
            );

            Assert.StartsWith("QTSAwardDate", e.Message);
        }

        [Fact]
        public async void Fails_WithIncorrectITTDate()
        {
            var trn = "0012390";

            var record = new QualifiedTeacher
            {
                Trn = trn,
                ITTStartDate = "00:00.0"
            };

            var fullTeacherReferenceNumber = record.Trn.PadLeft(7, '0');
            var trimmedTeacherReferenceNumber = record.Trn.TrimStart('0');
            _qualifiedTeachersRepositoryMock
                .Setup(q => q.FindAsync(x => x.Trn == fullTeacherReferenceNumber || x.Trn == trimmedTeacherReferenceNumber))
                .ReturnsAsync(new List<QualifiedTeacher> { record });

            var e = await Assert.ThrowsAsync<FormatException>(() =>
                _qualifiedTeachersService.GetQualifiedTeacherRecords(trn, string.Empty)
            );

            Assert.StartsWith("ITTStartDate", e.Message);
        }
    }
}
