using System.Collections.Generic;

namespace RabitMQTask.Model.Commons
{
    public class ResultViewModel<T>
    {
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; }=new List<string>();
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
