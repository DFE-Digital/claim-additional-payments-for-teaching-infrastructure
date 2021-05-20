using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dqt.datalayer.Model
{
    public class QualifiedTeacherBackup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Trn { get; set; }

        public string Name { get; set; }

        public string DoB { get; set; }

        public string NINumber { get; set; }

        public string QTSAwardDate { get; set; }

        public string ITTSubject1Code { get; set; }

        public string ITTSubject2Code { get; set; }

        public string ITTSubject3Code { get; set; }

        public bool ActiveAlert { get; set; }

        public string QualificationName { get; set; }

        public string ITTStartDate { get; set; }

        public string TeacherStatus { get; set; }
    }
}
