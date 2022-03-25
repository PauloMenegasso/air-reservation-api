namespace AirReservationApi.Infra.Logging
{
    //Although functional, this logger is not complite. It's only logging on the console. However, I want to keep the class, since it's easy to evolve to, for instance, 
    //loging with elastic search, since the interface can be kept. 
    public interface ILogger
    {
        void Error(Exception ex, string message);
        void Error(string messageTemplate, params object[] propertyValues);
        void Information(string message);
        void Information(string messageTemplate, params object[] propertyValues);
        void Warning(string messageTemplate, params object[] propertyValues);
    }
    public class Logger : ILogger
    {
        public void Error(Exception ex, string message)
        {
            Console.WriteLine(message, ex.Message);
        }

        public void Error(string messageTemplate, params object[] propertyValues)
        {
            var messageString = "";
            foreach (var property in propertyValues)
            {
                messageString += $"{property.ToString()} /n";
            }
            Console.WriteLine(messageTemplate, messageString);
        }

        public void Information(string message)
        {
            Console.WriteLine(message);
        }

        public void Information(string messageTemplate, params object[] propertyValues)
        {
            var messageString = "";
            foreach (var property in propertyValues)
            {
                messageString += $"{property.ToString()} /n";
            }
            Console.WriteLine(messageTemplate, messageString);
        }

        public void Warning(string messageTemplate, params object[] propertyValues)
        {
            var messageString = "";
            foreach (var property in propertyValues)
            {
                messageString += $"{property.ToString()} /n";
            }
            Console.WriteLine(messageTemplate, messageString);
        }
    }
}