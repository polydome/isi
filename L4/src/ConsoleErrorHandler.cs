using System;
using L4.Service;

namespace L4
{
    public class ConsoleErrorHandler : IErrorHandler
    {
        public void OnError(string message)
        {
            Console.WriteLine(message);
        }
    }
}