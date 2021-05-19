using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dqt.datalayer.Model
{
    public class DQTFileTransfer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime LastRun { get; set; }
        public string Status { get; set; }
        public string Error { get; set; }
    }
}
