using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedisDemo.RedisHelper
{
    /// <summary>
    /// RedisBase类，是redis操作的基类，继承自IDisposable接口，主要用于释放内存
    /// </summary>
    public abstract class RedisBase : IDisposable
    {
        public static IRedisClient Core { get; private set; }
        private bool _disposed = false;
        static RedisBase()
        {
            Core = RedisManager.GetClient();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    Core.Dispose();
                    Core = null;
                }
            }
            this._disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// 保存数据DB文件到硬盘
        /// </summary>
        public void Save()
        {
            Core.Save();
        }
        /// <summary>
        /// 异步保存数据DB文件到硬盘
        /// </summary>
        public void SaveAsync()
        {
            Core.SaveAsync();
        }

        /// <summary>
        /// 通过Redis管道执行多个命令
        /// </summary>
        /// <param name="actions"></param>
        public static void DoPipeline(List<Action<IRedisClient>> actions)
        {
            if (actions.Count <= 0)
                return;

            using (IRedisClient redis = RedisManager.GetClient())
            {
                if (redis == null)
                    throw new Exception("manager.GetClient为空");
                using (RedisAllPurposePipeline pipe = (RedisAllPurposePipeline)redis.CreatePipeline())
                {
                    foreach (Action<IRedisClient> action in actions)
                    {
                        pipe.QueueCommand(action);
                    }
                    pipe.Flush();
                }
            }
        }

        public  static string GetHashValue(Dictionary<string, string> dicHash, string key)
        {
            string value;
            if (dicHash.TryGetValue(key, out value))
            {
                return value;
            }
            return string.Empty;
        }
    }
}