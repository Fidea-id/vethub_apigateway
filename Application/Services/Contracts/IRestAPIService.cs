﻿using Domain.Entities;

namespace Application.Services.Contracts
{
    public interface IRestAPIService
    {
        Task<T> GetResponse<T>(APIType type, string url, string auth = null)
            where T : class;
        Task<T> GetResponseFilter<T, TFilter>(APIType type, string url, string auth = null, TFilter queryParams = null)
            where T : class
            where TFilter : BaseEntityFilter;
        Task<T> PostResponse<T>(APIType type, string url, string obj, string auth = null)
            where T : class;
        Task<T> PutResponse<T>(APIType type, string url, int id, string obj, string auth = null)
            where T : class;
        Task<T> DeleteResponse<T>(APIType type, string url, int id, string auth = null)
            where T : class;
    }
}
