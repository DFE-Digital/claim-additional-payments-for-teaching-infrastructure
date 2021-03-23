namespace dqt.api.Functions
{
    public class ResultDTO<T>
    {
        public T Data { get; set; }

        public string Message { get; set; }

        public ResultDTO(T data, string message = null)
        {
            this.Data = data;
            this.Message = message;
        }
    }
}