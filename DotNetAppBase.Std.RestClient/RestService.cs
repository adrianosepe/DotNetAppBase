using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DotNetAppBase.Std.Library.ComponentModel.Model.Svc;
using DotNetAppBase.Std.RestClient.Contracts;
using Flurl;
using Flurl.Http;

namespace DotNetAppBase.Std.RestClient
{
    public class RestService
    {
        private readonly HttpClient _client;
        private readonly IRestController _controller;

        public RestService(IRestController controller)
        {
            _controller = controller;

            _client = new HttpClient();
        }

        public async Task<bool> Get<T>(Action<List<T>> onSuccess, Action<Exception> onError = null)
        {
            try
            {
                var result = await _controller.BaseUrl().GetJsonAsync<List<T>>();

                onSuccess?.Invoke(result);

                return true;
            }
            catch (FlurlHttpException ex)
            {
                onError?.Invoke(ex);
            }

            return false;
        }

        public async Task<bool> GetById<T>(int id, Action<T> onSuccess, Action<Exception> onError = null)
        {
            try
            {
                var result = await _controller
                    .BaseUrl()
                    .AppendPathSegment(segment: new {id})
                    .GetJsonAsync<T>();

                onSuccess?.Invoke(result);

                return true;
            }
            catch (FlurlHttpException ex)
            {
                onError?.Invoke(ex);
            }

            return false;
        }

        public async Task<Result<T>> Execute<T>(Func<IFlurlRequest, Task<Result<T>>> customizeAction)
        {
            try
            {
                var url = new Url(baseUrl: _controller.BaseUrl());

                return await customizeAction(
                    _controller.Intercept(
                        url.WithTimeout(_controller.DefaultTimeout)));
            }
            catch (FlurlHttpException ex)
            {
                return Result<T>.Exception(ex);
            }
        }
    }
}