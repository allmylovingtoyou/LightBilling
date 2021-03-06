﻿#define DEBUG
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using static LightBilling.Extensions.InternalExceptions;

namespace LightBilling
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            if (exception is NotFoundException)
            {
                context.Response.StatusCode = (int) HttpStatusCode.NotFound;
                return context.Response.WriteAsync(JsonConvert.SerializeObject(new MessageAndTrace(exception)));
            }

            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(JsonConvert.SerializeObject(new MessageAndTrace(exception)));
        }

        [SuppressMessage("ReSharper", "MemberCanBePrivate.Local")]
        [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
        private class MessageAndTrace
        {
            internal MessageAndTrace(Exception exception)
            {
                Type = exception.GetType().ToString();
                Message = exception.Message;

                InnerMessage = exception.InnerException?.Message;


                if (exception is NotFoundException)
                {
                    Message = $"Not found {exception.Message}";
                }

//                Trace = exception.StackTrace.Substring(0, 80);
                Trace = exception.StackTrace;
            }

            public string Type { get; set; }
            public string Message { get; set; }

            public string InnerMessage { get; set; }
            public string Trace { get; set; }
        }
    }
}