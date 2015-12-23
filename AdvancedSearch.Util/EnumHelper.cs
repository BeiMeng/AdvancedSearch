using System;

namespace AdvancedSearch.Util
{
    public class EnumHelper
    {
        #region GetInstance(获取实例)

        /// <summary>
        /// 获取实例
        /// </summary>
        /// <typeparam name="T">枚举</typeparam>
        /// <param name="enumName">成员名
        /// 范例:Enum1枚举有成员A则传入"A"获取 Enum1.A</param>
        public static T GetInstance<T>(string enumName)
        {
            if (string.IsNullOrWhiteSpace(enumName))
                throw new ArgumentNullException("enumName");
            return (T)Enum.Parse(TypeHelper.GetType<T>(), enumName, true);
        }
        /// <summary>
        /// 获取实例
        /// </summary>
        /// <typeparam name="T">枚举</typeparam>
        /// <param name="enumValue">成员名
        /// 范例:Enum1枚举有成员A=0,则传入"0"获取 Enum1.A</param>
        public static T GetInstance<T>(int enumValue)
        {
            return (T)Enum.Parse(TypeHelper.GetType<T>(), enumValue.ToString(), true);
        }
        #endregion 

    }
}