using System.Globalization;
using System.Resources;

namespace quest5.Resources
{
    public static class ResourceHelper
    {
        private static readonly ResourceManager _rm =
            new ResourceManager("LibraryApi.Resources.Messages", typeof(ResourceHelper).Assembly);

        public static string Get(string key, params object[] args)
        {
            var value = _rm.GetString(key, CultureInfo.CurrentCulture);
            return args.Length > 0 ? string.Format(value ?? key, args) : value ?? key;
        }
    }
}
