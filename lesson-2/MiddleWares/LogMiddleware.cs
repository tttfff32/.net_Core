using System.Diagnostics;

namespace Middlewares;

    public class LogMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger logger;
        private readonly string logFilePath;//="D:/שנה ב/זילברברג/שיעורי בית/פרויקט/net_Core-main/.net_Core-main/lesson-2/Log.txt";

        public LogMiddleware(RequestDelegate next, ILogger<LogMiddleware> logger, string logFilePath)
        {
            this.next = next;
            this.logger = logger;
            this.logFilePath = logFilePath;
        }

        public async Task Invoke(HttpContext c)
        {
            var sw = new Stopwatch();
            sw.Start();
            await next.Invoke(c);
            
            string logMessage =$"{c.Request.Path}.{c.Request.Method} took {sw.ElapsedMilliseconds}ms. User: {c.User?.FindFirst("userId")?.Value ?? "unknown"}";
            LogToFile(logMessage);
        }

        private void LogToFile(string message)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine($"{DateTime.Now}: {message}");
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Error writing to log file: {ex.Message}");
            }
        }
    }

    public static partial class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMyLogMiddleware(this IApplicationBuilder builder, string logFilePath)
        {
            return builder.UseMiddleware<LogMiddleware>(logFilePath);
        }
    }


