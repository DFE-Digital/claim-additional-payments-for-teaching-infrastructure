using System.Collections.Generic;
using Moq;
using Xunit;
using dqt.datalayer.Model;
using dqt.datalayer.Repository;
using dqt.domain;


namespace dqt.unittests.domain
{
    public class QualifiedTeachersServiceTests
    {
        private const string TRN = "TRN";
        private const string NI = "NI";

        private IQualifiedTeachersService _qualifiedTeachersService;
        private List<QualifiedTeacher> mockQualifiedTeachers;

        private Mock<IRepository<QualifiedTeacher>> _qualifiedTeachersRepositoryMock;

        public QualifiedTeachersServiceTests()
        {
            _qualifiedTeachersRepositoryMock = new Mock<IRepository<QualifiedTeacher>>();
            _qualifiedTeachersService = new QualifiedTeachersService(_qualifiedTeachersRepositoryMock.Object);
            mockQualifiedTeachers = new List<QualifiedTeacher>
            {
                new QualifiedTeacher() { Name = "TEST1", Trn = TRN, NINumber = NI }
            };
        }

        [Fact]
        public async void Returns_QualifiedTeacherRecords_WhenTrnMatchFound()
        {
            _qualifiedTeachersRepositoryMock
                .Setup(q => q.FindAsync(x => x.Trn == TRN))
                .ReturnsAsync(mockQualifiedTeachers);

            var results = await _qualifiedTeachersService.GetQualifiedTeacherRecords(TRN, NI);

            Assert.Equal(results, mockQualifiedTeachers);
        }

        [Fact]
        public async void Returns_QualifiedTeacherRecords_WhenNIMatchFound()
        {
            _qualifiedTeachersRepositoryMock
                .Setup(q => q.FindAsync(x => x.NINumber == NI))
                .ReturnsAsync(mockQualifiedTeachers);

            var results = await _qualifiedTeachersService.GetQualifiedTeacherRecords(TRN, NI);

            Assert.Equal(results, mockQualifiedTeachers);
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
    }
}
