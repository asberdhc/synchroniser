using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System
{
    public class XMLFile
    {
        //constant wich define the path for serialization
        public const string PATH = @"C:\Users\Curso\Downloads\";

        /// <summary>
        /// Method that serialize and save an object to a xml file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="fileName"></param>
        public static void SerializeList<T>(T obj, string fileName)
        {
            Console.WriteLine("1s");
            XmlSerializer ser = new XmlSerializer(typeof(T));
            Console.WriteLine("2s");
            //Create a FileStream object connected to the target file
            FileStream fileStream = new FileStream(PATH + fileName, FileMode.Create);
            Console.WriteLine("3s");
            ser.Serialize(fileStream, obj);
            Console.WriteLine("4s");
            fileStream.Close();
        }

        /// <summary>
        /// Method that deserialize an object from a xml file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static T DeserializeList<T>(string fileName)
        {
            Console.WriteLine("1d");
            XmlDocument doc = new XmlDocument();
            Console.WriteLine("2d");
            Threading.Thread.Sleep(500);
            doc.Load(PATH + fileName);
            Console.WriteLine("3d");
            T result;
            Console.WriteLine("4d");
            XmlSerializer ser = new XmlSerializer(typeof(T));
            Console.WriteLine("5d");
            using (TextReader tr = new StringReader(doc.OuterXml))
            {
                Console.WriteLine("6d");
                result = (T)ser.Deserialize(tr);
            }
            Console.WriteLine("7d");
            return result;
        }

        public static void Delete(string fileName)
        {
            File.Delete(PATH + fileName);
        }
    }
}
