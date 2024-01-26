using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace Countries.Services
{
    /// <summary>
    /// Helper for serialization/deserialization XML
    /// </summary>
    public static class ObjectSerializer
    {
        public static object Deserialize(Type type, String xml)
        {
            object obj;
            lock (typeof(ObjectSerializer))
            {
                XmlSerializer xs = new XmlSerializer(type);
                obj = xs.Deserialize(new StringReader(xml));                
            }
            return obj;
        }

        public static T Deserialize<T>(String xml)
        {
            return (T)Deserialize(typeof(T), xml);
        }

        public static String Serialize(object obj)
        {
            return Serialize(obj.GetType(), obj, new XmlWriterSettings() { Encoding = Encoding.Default });
        }

        public static String Serialize(object obj, XmlWriterSettings settings)
        {
            return Serialize(obj.GetType(), obj, settings);
        }

        public static String Serialize(Type type, object obj)
        {
            return Serialize(type, obj, new XmlWriterSettings() { Encoding = Encoding.Default });
        }

        public static String Serialize(Type type, object obj, XmlWriterSettings settings)
        {
            XmlSerializer xs = new XmlSerializer(type);
            if (settings != null && settings.Encoding == Encoding.UTF8)
            {
                using (var writer = new Utf8StringWriter())
                {
                    XmlSerializer xml = new XmlSerializer(type);
                    xml.Serialize(writer, obj);
                    return writer.ToString();
                }

            }
            else
            {
                StringBuilder sb = new StringBuilder();
                var w = XmlWriter.Create(sb, settings);
                xs.Serialize(w, obj);
                return sb.ToString();
            }
        }
        
    }

    public class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding
        {
            get { return new UTF8Encoding(false); } 
        }
    }
}