using System;
namespace dqt.domain.DTOs
{
    public class QualifiedTeacherDTO
    {
        public int Id { get; set; }

        public string Trn { get; set; }

        public string Name { get; set; }

        public DateTime? DoB { get; set; }

        public string NINumber { get; set; }

        public DateTime? QTSAwardDate { get; set; }

        public string ITTSubject1Code { get; set; }

        public string ITTSubject2Code { get; set; }

        public string ITTSubject3Code { get; set; }

        public bool ActiveAlert { get; set; }

        public string QualificationName { get; set; }

        public DateTime? ITTStartDate { get; set; }

        public string TeacherStatus { get; set; }
    }
}