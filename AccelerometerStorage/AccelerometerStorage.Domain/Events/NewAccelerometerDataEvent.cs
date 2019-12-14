namespace AccelerometerStorage.Domain.Events
{
    public class NewAccelerometerDataEvent : INotification
    {
        public string Message { get; set; }
    }
}