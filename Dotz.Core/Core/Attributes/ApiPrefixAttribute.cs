using System;
using System.Web.Http;

namespace TesteDotz.Core
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ApiPrefixAttribute : RoutePrefixAttribute
    {
        private readonly string Name;

        public ApiPrefixAttribute(string name)
        {
            this.Name = name;
        }

        public string Version { get; set; }

        /// <summary>
        /// Geração do prefixo de rota da api.
        /// </summary>
        public override string Prefix {
            get {
                string path = "api";

                return $"{path}/{this.Name}";
            }
        }
    }
}
