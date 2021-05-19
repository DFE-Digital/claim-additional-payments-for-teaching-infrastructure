using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dqt.datalayer.Model
{
    public class QualifiedTeacher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Trn { get; set; }

        public string Name { get; set; }

        [Column(TypeName = "Date")]
        public DateTime DoB { get; set; }

        public string NINumber { get; set; }

        [Column(TypeName = "Date")]
        public DateTime QTSAwardDate { get; set; }

        public string ITTSubject1Code { get; set; }

        public string ITTSubject2Code { get; set; }

        public string ITTSubject3Code { get; set; }

        public bool ActiveAlert { get; set; }

        public string QualificationName { get; set; }

        [Column(TypeName = "Date")]
        public DateTime ITTStartDate { get; set; }

        public string TeacherStatus { get; set; }
    }
}
