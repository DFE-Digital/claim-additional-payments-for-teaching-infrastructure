using System;
using System.ComponentModel.DataAnnotations;

namespace dqt.datalayer.Model
{
    public class QualifiedTeacher
    {
        [Key]
        public int Id { get; set; }
        public string TeacherReferenceNumber { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string NationalInsuranceNumber { get; set; }

        public DateTime QualifiedTeachingStatusAwardDate { get; set; }

        public string UndergraduateSubject { get; set; }

        public string HigherEducationSubjectCode { get; set; }

        public string InitialTeacherTrainingSubjectCode { get; set; }

        public string ClientReference { get; set; }
    }
}
