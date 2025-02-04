﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using System;
using System.IO;
using todoanderson.Common.Models;
using todoanderson.Functions.Entities;

namespace todoanderson.Test.Helpers{
    public class TestFactory{
        public static TodoEntity GetTodoEntity(){
            return new TodoEntity{
                ETag = "*",
                PartitionKey = "TODO",
                RowKey = Guid.NewGuid().ToString(),
                CreatedTime = DateTime.UtcNow,
                IsCompleted = false,
                TaskDescription = "Task: kill the humans."
            };
        }
        public static DefaultHttpRequest CreatedHttpRequest(Guid todoId, Todo todoRequest){
            string request = JsonConvert.SerializeObject(todoRequest);
            return new DefaultHttpRequest( new DefaultHttpContext() ){
                Body = GenerateStreamFromString(request),
                Path = $"/{todoId}"
            };
        }
        public static DefaultHttpRequest CreatedHttpRequest(Guid todoId){
            return new DefaultHttpRequest(new DefaultHttpContext()){
                Path = $"/{todoId}"
            };
        }
        public static DefaultHttpRequest CreatedHttpRequest(Todo todoRequest){
            string request = JsonConvert.SerializeObject(todoRequest);
            return new DefaultHttpRequest(new DefaultHttpContext()){
                Body = GenerateStreamFromString(request)
            };
        }
        public static DefaultHttpRequest CreatedHttpRequest(){
            return new DefaultHttpRequest(new DefaultHttpContext());
        }
        public static Todo GetTodoRequest(){
            return new Todo{
                CreatedTime = DateTime.UtcNow,
                IsCompleted = false,
                TaskDescription = "Try to conquer the world"
            };
        }
        public static Stream GenerateStreamFromString(string stringToConvert){
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(stringToConvert);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
        public static ILogger CreatedLogger(LoggerTypes type = LoggerTypes.Null){
            ILogger logger;
            if (type == LoggerTypes.List){
                logger = new ListLogger();
            } else {
                logger = NullLoggerFactory.Instance.CreateLogger("Null Logger");
            }
            return logger;
        }
    }
}
