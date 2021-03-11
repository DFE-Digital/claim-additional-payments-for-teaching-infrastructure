using System;
using System.Collections.Generic;
using System.Text;

namespace dqt.domain
{
    public interface IRollbarService
    {
        void Info(string message);
        
        void Error(Exception exception);

        void Warning(string message);
    }
}
