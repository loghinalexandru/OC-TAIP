using Newtonsoft.Json;

namespace AccelerometerStorage.Domain.Events
{
    public class NewAccelerometerDataEvent : INotification
    {
        public string Message { get; set; }

        public NewAccelerometerDataEvent(string username, string email)
        {
            Message = JsonConvert.SerializeObject(new
            {
                Username = username,
                Email = email
            });
        }
    }
}