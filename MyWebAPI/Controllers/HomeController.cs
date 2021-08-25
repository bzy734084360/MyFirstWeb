using MyWebAPI.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyWebAPI.Controllers
{
    /// <summary>
    /// 首页测试
    /// </summary>
    [RoutePrefix("api/Home")]
    public class HomeController : ApiController
    {
        /// <summary>
        /// 数据源
        /// </summary>
        /// <returns></returns>
        private List<DeviceStatus> GetDeviceStatus()
        {
            var devi = new List<DeviceStatus> {
             new DeviceStatus{ID=1,Power="开",Mode="制冷",Fan="低",TempSet=10,UpdateTime="2019-08-01 17:00:00"},
             new DeviceStatus{ID=2,Power="关",Mode="制热",Fan="低",TempSet=20,UpdateTime="2019-08-02 17:00:00"},
             new DeviceStatus{ID=3,Power="开",Mode="除湿",Fan="高",TempSet=30,UpdateTime="2019-08-03 17:00:00"},
             new DeviceStatus{ID=4,Power="关",Mode="制热",Fan="中",TempSet=40,UpdateTime="2019-08-04 17:00:00"},
             new DeviceStatus{ID=4,Power="关",Mode="制热",Fan="高",TempSet=50,UpdateTime="2019-08-05 17:00:00"},
            };
            return devi;
        }
        /// <summary>
        /// 首次测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<string> InputString()
        {
            return new string[] { "hellow world" };
        }

        #region Get请求示例

        /// <summary>
        /// 无参数Get请求
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public IEnumerable<DeviceStatus> GetData()
        {
            return GetDeviceStatus();
        }
        /// <summary>
        /// 一个参数Get请求
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<DeviceStatus> GetOneParameter([FromUri] int ID)
        {
            return GetDeviceStatus().Where(t => t.ID == ID);
        }
        /// <summary>
        /// 多个参数的Get请求
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Power"></param>
        /// <param name="Mode"></param>
        /// <param name="Fan"></param>
        /// <param name="TempSet"></param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<string> GetManyParameter([FromUri] int ID, string Power, string Mode, string Fan, int TempSet)
        {
            return new string[] { "编号:" + ID.ToString(), "开机状态:" + Power, "工作模式:" + Mode, "风量:" + Fan, "温度:" + TempSet.ToString() };
        }
        /// <summary>
        /// 实体参数请求
        /// </summary>
        /// <param name="devi"></param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<string> GetManyParameterModel([FromUri] DeviceStatus devi)
        {
            return new string[] { "编号:" + devi.ID.ToString(), "开机状态:" + devi.Power, "工作模式:" + devi.Mode, "风量:" + devi.Fan, "温度:" + devi.TempSet.ToString() };
        }

        #endregion

        #region Post请求示例

        /// <summary>
        /// 键值对请求
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        public string PostNoKeyValueParameter([FromBody] int ID)
        {
            return "接收的编号是:" + ID;
        }
        /// <summary>
        /// dynamic动态类型请求
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        public string PostDynamicValueParameter([FromBody] dynamic ID)
        {
            return "接受的编号是:" + ID;
        }
        /// <summary>
        /// JObject参数请求
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public string PostJObjectValueParameter([FromBody] JObject data)
        {
            return "接收编号是：" + data["ID"] + "|开机状态:" + data["Power"] + "|工作模式:" + data["Mode"];
        }
        /// <summary>
        /// 实体请求
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public string PostModelValueParameter([FromBody] DeviceStatus data)
        {
            return "接收编号是：" + data.ID + ";开机状态:" + data.Power + ";工作模式:" + data.Mode + ";风量:" + data.Fan + "；温度:" + data.TempSet.ToString();
        }


        #endregion
    }
}
