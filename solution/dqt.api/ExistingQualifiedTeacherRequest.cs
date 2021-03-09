using System;

namespace dqt.api
{
    internal class ExistingQualifiedTeacherRequest
    {
        public string TeacherReferenceNumber { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string NINumber { get; set; }
    }
}