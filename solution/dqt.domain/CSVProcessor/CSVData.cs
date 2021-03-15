using System;

namespace dqt.domain
{
    public class CSVData
    {
        public string trn { get; set; }
        public string name { get; set; }
        public DateTime dob { get; set; }
        public string niNumber { get; set; }
        public DateTime qtsAwardDate { get; set; }
        public string ITTSubject1Code { get; set; }
        public string ITTSubject2Code { get; set; }
        public string ITTSubject3Code { get; set; }
        public bool ActiveAlert { get; set; }
    }
}
