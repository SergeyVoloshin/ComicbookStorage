﻿
namespace ComicbookStorage.Application.Services.Configuration
{
    public interface IAppConfiguration
    {
        string BaseUrl { get; set; }
        string ConfirmEmailUrl { get; set; }
        string LogInUrl { get; set; }
        string ComicbookImagePath { get; set; }
        string SmallCoverName { get; set; }
    }

    public class AppConfiguration : IAppConfiguration
    {
        public string BaseUrl { get; set; }
        public string ConfirmEmailUrl { get; set; }
        public string LogInUrl { get; set; }
        public string ComicbookImagePath { get; set; }
        public string SmallCoverName { get; set; }
    }
}
