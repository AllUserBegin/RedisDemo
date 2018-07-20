using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedisDemo.Models
{
    public class UserModel
    {
        public int Uid
        {
            get; set;
        } = 2;

        public string NickName
        {
            get; set;
        } = "NickName";

        public string UserName
        {
            get; set;
        } = "UserName";

        public bool Sex
        {
            set;
            get;
        } = false;

        public string HeadUrl
        {
            set;
            get;
        } = "HeadUrl";

        public string PhoneNo
        {
            set;
            get;
        } = "1379938";

        public string Address
        {
            set;
            get;
        } = "Address";
    }
}