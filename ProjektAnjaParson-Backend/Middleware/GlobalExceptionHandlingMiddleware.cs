using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;
using System.Text.Json;

namespace ProjektAnjaParson_Backend.Middleware
{
    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;
        public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }


        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                // Log "true" error to devs, e.g. "object reference..."
                _logger.LogError(ex, ex.Message);

                // Write log to .log file, showing error message and method that threw exception.
                WriteLog(ex.Message + " in the method " + ex.TargetSite + "\n");
                context.Response.StatusCode =
                    (int)HttpStatusCode.InternalServerError;

                // Create a readable message to show the consumer.
                // This could be manipulated based on the status code returned.
                ProblemDetails problem = new()
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Type = "Server error",
                    Title = "Server error",
                    Detail = "An internal server error has occurred"
                };

                string json = JsonSerializer.Serialize(problem);

                // Must be set before WriteAsync.
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(json);
            }
        }
        /// <summary>
        /// Writes logs to a log file in the working directory.
        /// </summary>
        /// <param name="strLog"></param>
        public static void WriteLog(string strLog)
        {
            string logFilePath = Directory.GetCurrentDirectory() + "\\logs\\" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + "." + "log";
            FileInfo logFileInfo = new FileInfo(logFilePath);
            DirectoryInfo logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
            if (!logDirInfo.Exists) logDirInfo.Create();
            StringBuilder sb = new StringBuilder();
            using (FileStream fileStream = new FileStream(logFilePath, FileMode.Append))
            {
                using (StreamWriter log = new StreamWriter(fileStream))
                {
                    sb.Append(DateTime.UtcNow.ToString());
                    sb.Append(" : ");
                    sb.Append(strLog);
                    sb.Append('\n');
                    log.WriteLine(sb.ToString());
                    sb.Clear();
                }
            }
        }
    }
}
