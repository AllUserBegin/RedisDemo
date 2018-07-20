using RedisDemo.Models;
using RedisDemo.RedisHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RedisDemo.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class RedisController : ApiController
    {

        /// <summary>
        /// 测试
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
       public ApiResult Test(int id,string name)
        {

            string key = "Users";
            RedisBase.Core.FlushAll();
            RedisBase.Core.AddItemToList(key, "cuiyanwei");
            RedisBase.Core.AddItemToList(key, "xiaoming");
            RedisBase.Core.Add<string>("mykey", "123456");
            RedisString.Set("mykey1", "abcdef");


            return ApiResult.ReturnWebResult(false, $"{id}-{name}");
        }
    }
}
