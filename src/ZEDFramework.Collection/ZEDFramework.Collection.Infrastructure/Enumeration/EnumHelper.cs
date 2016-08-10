using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ZEDFramework.Collection.Infrastructure.Enumeration
{
    public static class EnumHelper
    {
        public static string GetEnumDescription(this Enum e)
        {
            string description = string.Empty;
            if (e != null)
            {
                MemberInfo[] member = e.GetType().GetMember(e.ToString());
                if ((member != null) && (member.Length > 0))
                {
                    object[] customAttributes = member[0].GetCustomAttributes(typeof(EnumDescription), false);
                    if ((customAttributes != null) && (customAttributes.Length > 0))
                    {
                        description = ((EnumDescription)customAttributes[0]).Description;
                    }
                }
            }
            return description;
        }

        public static EnumModel GetEnumDescription<T>(string value)
        {
            return (from x in GetEnumDescriptions<T>()
                    where x.Value == value
                    select x).FirstOrDefault<EnumModel>();
        }

        public static List<EnumModel> GetEnumDescriptions<T>()
        {
            List<EnumModel> list = new List<EnumModel>();
            foreach (FieldInfo info in typeof(T).GetFields(BindingFlags.GetField | BindingFlags.Public | BindingFlags.Static))
            {
                foreach (EnumDescription description in info.GetCustomAttributes<EnumDescription>())
                {
                    EnumModel item = new EnumModel
                    {
                        Name = info.GetValue(null).ToString(),
                        Value = Enum.Parse(typeof(T), info.GetValue(null).ToString()).GetHashCode().ToString(),
                        Description = description.Description
                    };
                    if (description.IsViewable)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }
    }
}
