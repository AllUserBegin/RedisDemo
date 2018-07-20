using RedisDemo.Models;
using RedisDemo.RedisHelper;
using ServiceStack.Redis;
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

        [Route("HashGet")]
        public ApiResult HashGet(int id)
        {
            UserModel entity = null;
            Dictionary<string, string> dicHash;
            try
            {
                string key = $"{0}:{1}:" + 2;

                dicHash = RedisBase.Core.GetAllEntriesFromHash(key);

                if (dicHash != null && dicHash.Count > 0)
                {
                    entity = new UserModel
                    {
                        Uid = id,
                        NickName = RedisBase.GetHashValue(dicHash, $"{0}:"),
                        UserName = RedisBase.GetHashValue(dicHash, $"{1}:"),
                        Sex = RedisBase.GetHashValue(dicHash, $"{2}:") == "1",
                        HeadUrl = RedisBase.GetHashValue(dicHash, $"{3}:"),
                        PhoneNo = RedisBase.GetHashValue(dicHash, $"{4}:"),
                        Address = RedisBase.GetHashValue(dicHash, $"{5}:"),
                    };
                }
            }
            catch
            {
            }
            return ApiResult.ReturnWebResult(true, "", entity);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("HashIn")]
        public ApiResult HashIn(string key)
        {
            HashSet<string> setField = new HashSet<string>();
            List<Action<IRedisClient>> actions = new List<Action<IRedisClient>>();
      
            UserModel entity1 = new Models.UserModel();

            //if (setField == null || setField.Contains("UserName"))
                actions.Add(redis => redis.SetEntryInHash(key, $"{0}:", entity1.UserName ?? ""));

            //if (setField == null || setField.Contains("NickName"))
                actions.Add(redis => redis.SetEntryInHash(key, $"{1}:", entity1.NickName ?? ""));

            //if (setField == null || setField.Contains("Sex"))
                actions.Add(redis => redis.SetEntryInHash(key, $"{2}:", (entity1.Sex ? 1 : 0).ToString()));

            //if (setField == null || setField.Contains("HeadUrl"))
                actions.Add(redis => redis.SetEntryInHash(key, $"{3}:", entity1.HeadUrl ?? ""));

            //if (setField == null || setField.Contains("PhoneNo"))
                actions.Add(redis => redis.SetEntryInHash(key, $"{4}:", entity1.PhoneNo.ToString()));

            //if (setField == null || setField.Contains("Address"))
                actions.Add(redis => redis.SetEntryInHash(key, $"{5}:", entity1.Address ?? ""));

            // 开始执行
            RedisBase.DoPipeline(actions);

            return ApiResult.ReturnWebResult(true, "", entity1);
        }

        /// <summary>
        /// 测试
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
       public ApiResult Test(int id,string name)
        {
            RedisBase.Core.FlushAll();
            string key = "Users";

            //内部维护一个List<T>集合
            //RedisBase.Core.AddItemToList("蜀国", "刘备");
            //RedisBase.Core.AddItemToList("蜀国", "关羽");
            //RedisBase.Core.AddItemToList("蜀国", "张飞");
            //List<string> ListString = RedisBase.Core.GetAllItemsFromList("蜀国");
            //foreach (string str in ListString)
            //{
            //    Response.Write(str);    //输出 刘备 关羽 张飞
            //}

            RedisBase.Core.AddItemToSet("魏国", "曹操");
            RedisBase.Core.AddItemToSet("魏国", "曹操");
            RedisBase.Core.AddItemToSet("魏国", "典韦");
            HashSet<string> HashSetString = RedisBase.Core.GetAllItemsFromSet("魏国");
            //foreach (string str in HashSetString)
            //{
            //    Response.Write(str);    //输出 典韦 曹操
            //}

            RedisBase.Core.AddItemToSortedSet("蜀国", "刘备", 5);
            RedisBase.Core.AddItemToSortedSet("蜀国", "关羽", 2);
            RedisBase.Core.AddItemToSortedSet("蜀国", "张飞", 3);
            IDictionary<String, double> DicString = RedisBase.Core.GetRangeWithScoresFromSortedSet("蜀国", 0, 2);
            foreach (var r in DicString)
            {
                Console.WriteLine(r.Key + ":" + r.Value);    //输出 
            }


            return ApiResult.ReturnWebResult(false, $"{id}-{name}");
        }
    }
}
