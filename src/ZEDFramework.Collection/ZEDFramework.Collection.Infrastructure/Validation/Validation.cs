using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ZEDFramework.Collection.Infrastructure.Validation.Attributes;

namespace ZEDFramework.Collection.Infrastructure.Validation
{
    public static class Validation
    {
        internal static string ValidateFieldHtmlValidation(object obj, PropertyInfo property)
        {
            string responseCode = string.Empty;
            try
            {
                FieldHtmlValidation customAttribute = property.GetCustomAttribute<FieldHtmlValidation>();
                if (customAttribute == null)
                {
                    return responseCode;
                }
                if (property.PropertyType == typeof(string))
                {
                    string str2 = (string)property.GetValue(obj);
                    if (string.IsNullOrWhiteSpace(str2))
                    {
                        return responseCode;
                    }
                    if (Regex.IsMatch(str2, @"<(.|\n)*?>"))
                    {
                        responseCode = customAttribute.ResponseCode;
                    }
                }
            }
            catch
            {
                throw;
            }
            return responseCode;
        }

        internal static string ValidateFieldLength(object obj, PropertyInfo property)
        {
            string responseCode = string.Empty;
            try
            {
                int? nullable;
                FieldLength customAttribute = property.GetCustomAttribute<FieldLength>();
                if (customAttribute == null)
                {
                    return responseCode;
                }
                if (property.PropertyType != typeof(string))
                {
                    throw new ArgumentException("TypeOf Not String!");
                }
                string str2 = property.GetValue(obj) as string;
                if (string.IsNullOrEmpty(str2))
                {
                    return responseCode;
                }
                if ((!customAttribute.MaxLength.HasValue && customAttribute.MinLength.HasValue) && ((str2.Length < (nullable = customAttribute.MinLength).GetValueOrDefault()) && nullable.HasValue))
                {
                    responseCode = customAttribute.ResponseCode;
                }
                if ((!customAttribute.MinLength.HasValue && customAttribute.MaxLength.HasValue) && (str2.Length > customAttribute.MaxLength.Value))
                {
                    responseCode = customAttribute.ResponseCode;
                }
                if ((customAttribute.MinLength.HasValue && (str2.Length < customAttribute.MinLength.Value)) || (customAttribute.MaxLength.HasValue && (str2.Length > customAttribute.MaxLength.Value)))
                {
                    responseCode = customAttribute.ResponseCode;
                }
            }
            catch
            {
                throw;
            }
            return responseCode;
        }

        internal static string ValidateFieldNotEmptyGuid(object obj, PropertyInfo property)
        {
            string responseCode = string.Empty;
            try
            {
                FieldNotEmptyGuid customAttribute = property.GetCustomAttribute<FieldNotEmptyGuid>();
                if (customAttribute == null)
                {
                    return responseCode;
                }
                if (property.PropertyType == typeof(Guid))
                {
                    Guid guid2 = (Guid)property.GetValue(obj);
                    if (guid2 == Guid.Empty)
                    {
                        responseCode = customAttribute.ResponseCode;
                    }
                }
            }
            catch
            {
                throw;
            }
            return responseCode;
        }

        internal static string ValidateFieldNotZero(object obj, PropertyInfo property)
        {
            string responseCode = string.Empty;
            try
            {
                FieldNotZero customAttribute = property.GetCustomAttribute<FieldNotZero>();
                if (customAttribute == null)
                {
                    return responseCode;
                }
                if (property.PropertyType == typeof(int))
                {
                    int num = (int)property.GetValue(obj);
                    if (num <= 0)
                    {
                        responseCode = customAttribute.ResponseCode;
                    }
                    return responseCode;
                }
                if (property.PropertyType == typeof(decimal))
                {
                    decimal num2 = (decimal)property.GetValue(obj);
                    if (num2 <= 0M)
                    {
                        responseCode = customAttribute.ResponseCode;
                    }
                }
            }
            catch
            {
                throw;
            }
            return responseCode;
        }

        internal static string ValidateFieldRequired(object obj, PropertyInfo property)
        {
            string responseCode = string.Empty;
            try
            {
                FieldRequired customAttribute = property.GetCustomAttribute<FieldRequired>();
                if (customAttribute == null)
                {
                    return responseCode;
                }
                if (property.PropertyType != typeof(string))
                {
                    throw new ArgumentException("TypeOf Not String!");
                }
                string str2 = property.GetValue(obj) as string;
                if (string.IsNullOrWhiteSpace(str2))
                {
                    responseCode = customAttribute.ResponseCode;
                }
            }
            catch
            {
                throw;
            }
            return responseCode;
        }
    }
}
