using System;
using System.Collections.Generic;
using System.Text;

namespace PersonPosition.StaticService
{
    public static class Resource_Service
    {
        private static System.Resources.ResourceManager resManger = new System.Resources.ResourceManager("PersonPosition.Properties.Resources", typeof(Resource_Service).Assembly);

        /// <summary>
        /// 得到指定Key的图像
        /// </summary>
        /// <param name="strKey"></param>
        /// <returns></returns>
        public static System.Drawing.Image GetImage(string strKey)
        {
            return (System.Drawing.Image)Resource_Service.resManger.GetObject(strKey);
        }
    }
}
