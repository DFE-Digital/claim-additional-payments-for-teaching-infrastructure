using System;
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

namespace dqt.unittests.domain
{
    public class QualifiedTeachersServiceTests
    {
        private const string TRN = "TRN";
        private const string NI = "NI";

        private IQualifiedTeachersService _qualifiedTeachersService;

        private Mock<IRepository<QualifiedTeacher>> _qualifiedTeachersRepositoryMock;

        public QualifiedTeachersServiceTests()
        {
            _qualifiedTeachersRepositoryMock = new Mock<IRepository<QualifiedTeacher>>();
            _qualifiedTeachersService = new QualifiedTeachersService(_qualifiedTeachersRepositoryMock.Object);
        }

        [Fact]
        public async void Returns_QualifiedTeacherRecords_WhenTrnMatchFound()
        {
            var record = new QualifiedTeacher {Name = "TEST1", Trn = TRN, NINumber = NI};

            _qualifiedTeachersRepositoryMock
                .Setup(q => q.FindAsync(x => x.Trn == TRN))
                .ReturnsAsync(new List<QualifiedTeacher>{ record });

            var results = await _qualifiedTeachersService.GetQualifiedTeacherRecords(TRN, NI);

            Assert.Equal(results.ToList().FirstOrDefault()?.Trn, record.Trn);
        }

        [Fact]
        public async void Returns_QualifiedTeacherRecords_WhenNIMatchFound()
        {
            var record = new QualifiedTeacher {Name = "TEST1", Trn = TRN, NINumber = NI};

            _qualifiedTeachersRepositoryMock
                .Setup(q => q.FindAsync(x => string.Equals(x.NINumber, NI, StringComparison.CurrentCultureIgnoreCase)))
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
                .Setup(q => q.FindAsync(x => string.Equals(x.NINumber, lowerNI, StringComparison.CurrentCultureIgnoreCase)))
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

            _qualifiedTeachersRepositoryMock
                .Setup(q => q.FindAsync(x => x.Trn == trn))
                .ReturnsAsync(new List<QualifiedTeacher>{ record });

            var result = (await _qualifiedTeachersService.GetQualifiedTeacherRecords(trn, string.Empty)).FirstOrDefault();

            Assert.Equal(trn, result?.Trn);
            Assert.Equal(date1 + " 00:00:00", result?.DoB.Value.ToString(CultureInfo.CreateSpecificCulture("en-GB").DateTimeFormat));
            Assert.Equal(date2 + " 00:00:00", result?.QTSAwardDate.Value.ToString(CultureInfo.CreateSpecificCulture("en-GB").DateTimeFormat));
            Assert.Equal(date3 + " 00:00:00", result?.ITTStartDate.Value.ToString(CultureInfo.CreateSpecificCulture("en-GB").DateTimeFormat));

            Thread.CurrentThread.CurrentCulture = originalCulture;
        }
    }
}
