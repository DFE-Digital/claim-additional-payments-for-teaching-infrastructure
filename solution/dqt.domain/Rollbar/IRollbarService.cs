using System; 

namespace dqt.domain.Rollbar
{
    public interface IRollbarService
    {
        void Configure(string message);

        void Info(string message);

        void Error(Exception exception);

        void Warning(string message);
    }
}
