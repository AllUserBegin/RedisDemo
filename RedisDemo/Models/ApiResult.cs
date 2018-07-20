using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedisDemo.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ApiResult
    {/// <summary>
     /// 
     /// </summary>
        public ApiResult()
        {
            StateMsg = string.Empty;
        }
        /// <summary>
        /// 返回状态码(0：正常 1:失败)
        /// </summary>
        public int StateCode { get; set; }

        /// <summary>
        /// 返回提示信息
        /// </summary>
        public string StateMsg { get; set; }

        /// <summary>
        /// 返回结果数据
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 总记录数(用于分页)
        /// </summary>
        public int Cnt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public dynamic MoreData { get; set; }


        /// <summary>
        /// 统一接口返回规则
        /// </summary>
        /// <param name="isSuccess"></param>
        /// <param name="errorMsg"></param>
        /// <param name="obj"></param>
        /// <param name="cnt"></param>
        /// <param name="_moreData"></param>
        /// <param name="StateCode">1失败  2</param>
        /// <returns></returns>
        public static ApiResult ReturnWebResult(bool isSuccess, string errorMsg, object obj = null, int cnt = 0, dynamic _moreData = null)
        {
            ApiResult result = new ApiResult();
            if (isSuccess)
            {
                result.StateCode = 0;
                result.StateMsg = errorMsg;
                result.Data = obj != null ? obj : result.Data;
                result.Cnt = cnt;
                result.MoreData = _moreData;
            }
            else
            {
                result.StateCode = 1;
                result.StateMsg = errorMsg;
            }
            return result;
        }


        /// <summary>
        /// 重新载入
        /// </summary>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public static ApiResult ReLoad(string errorMsg)
        {
            ApiResult result = new ApiResult();
            result.StateCode = 2;
            result.StateMsg = errorMsg;
            return result;
        }


    }
}