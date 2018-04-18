using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace YourMillionaires.Model
{
    public class Answer
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }
        [XmlAttribute("Id")]
        public int Id { get; set; }
        [XmlAttribute("IsOk")]
        public bool IsOk { get; set; }
        [XmlIgnore]
        public bool Marked { get; set; }
    }

    [XmlRoot("TestXML")]
    public class XML
    {
        [XmlElement("Question")]
        public List<Question> QuestionsList = new List<Question>();
        
        static string path = Path.Combine(
            Path.GetTempPath(),
            string.Concat(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, ".xml")
            );

        public class Question
        {
            [XmlAttribute]
            public string Values { get; set; }

            [XmlAttribute]
            public int Id { get; set; }

            public List<Answer> Items { get; set; }
        }

        public static void Serialize(XML items)
        {
            XmlSerializer serializer2 = new XmlSerializer(typeof(XML));
            using (StreamWriter writer = new StreamWriter(path))
            {
                serializer2.Serialize(writer, items);
            }
        }

        public XML Deserialize()
        {
            if (!File.Exists(path))
                using (var stream = File.Create(path));

            if(File.ReadAllText(path) == string.Empty)
                return new XML();

            XML items = null;
            XmlSerializer serializer = new XmlSerializer(typeof(XML));
            using (StreamReader reader = new StreamReader(path))
            {
                items = (XML)serializer.Deserialize(reader);
            }

            return items;
        }
    }
}
