using System;
using Slalom.Stacks.Logging;

namespace Slalom.Stacks.Messaging
{
    public class Logger : ILogger
    {
        public void Dispose()
        {
        }

        public void Debug(Exception exception, string template, params object[] properties)
        {
            Console.WriteLine(template);
        }

        public void Debug(string template, params object[] properties)
        {
            Console.WriteLine(template);
        }

        public void Error(Exception exception, string template, params object[] properties)
        {
            Console.WriteLine(template);
        }

        public void Error(string template, params object[] properties)
        {
            Console.WriteLine(template);
        }

        public void Fatal(Exception exception, string template, params object[] properties)
        {
            Console.WriteLine(template);
        }

        public void Fatal(string template, params object[] properties)
        {
            Console.WriteLine(template);
        }

        public void Information(Exception exception, string template, params object[] properties)
        {
            Console.WriteLine(template);
        }

        public void Information(string template, params object[] properties)
        {
            Console.WriteLine(template);
        }

        public void Verbose(Exception exception, string template, params object[] properties)
        {
            Console.WriteLine(template);
        }

        public void Verbose(string template, params object[] properties)
        {
            Console.WriteLine(template);
        }

        public void Warning(Exception exception, string template, params object[] properties)
        {
            Console.WriteLine(template);
        }

        public void Warning(string template, params object[] properties)
        {
            Console.WriteLine(template);
        }
    }
}