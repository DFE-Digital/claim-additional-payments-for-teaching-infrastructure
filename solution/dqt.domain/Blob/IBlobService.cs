using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace dqt.domain.Blob
{
    public interface IBlobService
    {
        Task UploadFile(FileStream filestream, string fileName);
        Task DeleteFile(string fileName);
    }
}
