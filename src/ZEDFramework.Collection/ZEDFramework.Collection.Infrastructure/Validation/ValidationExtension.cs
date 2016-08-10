using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ZEDFramework.Collection.Infrastructure.Validation
{
    public static class ValidationExtension
    {
        public static List<string> Validate<TClass>(this TClass tClass) where TClass : class
        {
            List<string> list = new List<string>();
            try
            {
                if (tClass == null)
                {
                    return list;
                }

                PropertyInfo[] properties = tClass.GetType().GetProperties();
                if ((properties == null) || (properties.Length == 0))
                {
                    return list;
                }

                foreach (PropertyInfo info in properties)
                {
                    string validateFieldRequiredRc = ZEDFramework.Collection.Infrastructure.Validation.Validation.ValidateFieldRequired(tClass, info);
                    if (!string.IsNullOrEmpty(validateFieldRequiredRc))
                    {
                        list.Add(validateFieldRequiredRc);
                    }

                    string validateFieldLengthRc = ZEDFramework.Collection.Infrastructure.Validation.Validation.ValidateFieldLength(tClass, info);
                    if (!string.IsNullOrEmpty(validateFieldLengthRc))
                    {
                        list.Add(validateFieldLengthRc);
                    }

                    string validateFieldNotZeroRc = ZEDFramework.Collection.Infrastructure.Validation.Validation.ValidateFieldNotZero(tClass, info);
                    if (!string.IsNullOrEmpty(validateFieldNotZeroRc))
                    {
                        list.Add(validateFieldNotZeroRc);
                    }

                    string validateFieldNotEmptyGuidRc = ZEDFramework.Collection.Infrastructure.Validation.Validation.ValidateFieldNotEmptyGuid(tClass, info);
                    if (!string.IsNullOrEmpty(validateFieldNotEmptyGuidRc))
                    {
                        list.Add(validateFieldNotEmptyGuidRc);
                    }

                    string validateFieldHtmlValidationRc = ZEDFramework.Collection.Infrastructure.Validation.Validation.ValidateFieldHtmlValidation(tClass, info);
                    if (!string.IsNullOrEmpty(validateFieldHtmlValidationRc))
                    {
                        list.Add(validateFieldHtmlValidationRc);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return list;
        }
    }
}
