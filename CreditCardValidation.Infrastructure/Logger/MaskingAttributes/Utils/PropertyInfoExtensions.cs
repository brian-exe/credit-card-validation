using System.Linq;
using System.Reflection;

namespace CreditCardValidation.Infrastructure.Logger.MaskingAttributes.Utils
{
    public static class PropertyInfoExtensions
    {
        public static T GetCustomAttribute<T>(this PropertyInfo propertyInfo)
        {
            return propertyInfo.GetCustomAttributes().OfType<T>().FirstOrDefault();
        }
    }
}
