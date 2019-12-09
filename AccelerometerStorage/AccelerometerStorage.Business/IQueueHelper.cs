using System;
using System.Collections.Generic;
using System.Text;

namespace AccelerometerStorage.Business
{
    public interface IQueueHelper
    {
        public void EnqueueMessage(string message);
    }
}
