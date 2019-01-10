using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Script.Serialization;

namespace AuthnAuthz.Extensions
{
    public class GeneralSerializer
    {
        public int ID { get; set; }
        public string AuthorName { get; set; }
    }
    public static class Extensions
    {
        public static string ToJSONWithIdnName<T>(this IEnumerable<T> input)
        {
            List<GeneralSerializer> temp = new List<GeneralSerializer>();
            foreach (var item in input)
            {
                Type type = item.GetType();
                PropertyInfo[] properties = type.GetProperties();
                string[] tempy = new string[3];
                foreach (PropertyInfo p in properties)
                {
                    if (p.Name == "ID")
                    {
                        tempy[1] = p.GetValue(item, null).ToString();
                    }
                    if (p.Name == "AuthorName")
                    {
                        tempy[2] = p.GetValue(item, null).ToString();
                    }
                }
                temp.Add(new GeneralSerializer() { ID = int.Parse(tempy[1]), AuthorName = tempy[2] });
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(temp);
        }
    }
}