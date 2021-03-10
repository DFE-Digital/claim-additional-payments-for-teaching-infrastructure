using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Moq;
using dqt.datalayer.Database;
using dqt.datalayer.Repository;
using dqt.datalayer.Model;

namespace dqt.unittests.datalayer
{
    public class QualifiedTeachersRepositoryTests
    {
        private readonly List<QualifiedTeacher> qualifiedTeachers = new List<QualifiedTeacher>()
        {
            new QualifiedTeacher(){ Trn="TRN", NINumber="NI"},
            new QualifiedTeacher(){ Trn="TRN1", NINumber="NI1"},
            new QualifiedTeacher(){ Trn="TRN2", NINumber="NI2"}
        };

        private readonly Mock<DbSet<QualifiedTeacher>> _dbSetMock;
        private readonly Mock<DQTDataContext> _dbContextMock;

        private readonly QualifiedTeachersRepository _repository;

        public QualifiedTeachersRepositoryTests()
        {
            _dbSetMock = new Mock<DbSet<QualifiedTeacher>>();

            _dbSetMock.As<IQueryable<QualifiedTeacher>>().Setup(x => x.Provider).Returns(qualifiedTeachers.AsQueryable().Provider);
            _dbSetMock.As<IQueryable<QualifiedTeacher>>().Setup(x => x.Expression).Returns(qualifiedTeachers.AsQueryable().Expression);
            _dbSetMock.As<IQueryable<QualifiedTeacher>>().Setup(x => x.ElementType).Returns(qualifiedTeachers.AsQueryable().ElementType);
            _dbSetMock.As<IQueryable<QualifiedTeacher>>().Setup(x => x.GetEnumerator()).Returns(qualifiedTeachers.AsQueryable().GetEnumerator());

            _dbContextMock = new Mock<DQTDataContext>();
            _dbContextMock.Setup(x => x.Set<QualifiedTeacher>()).Returns(_dbSetMock.Object);

            _repository = new QualifiedTeachersRepository(_dbContextMock.Object);
        }

        [Fact]
        public async void Returns_QualifiedTeacherRecords_WhenMatchFound()
        {
            var results = await _repository.FindAsync(x => x.Trn == "TRN");

            Assert.Single(results);
            Assert.Equal("TRN", results.Single().Trn);
        }

        [Fact]
        public async void Returns_NoRecords_WhenMatchNotFound()
        {
            var results = await _repository.FindAsync(x => x.Trn == "WRONG-TRN");

            Assert.Empty(results);
        }
    }
}
