
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Lab12
{
    // Можно внести внутрь класса листа, но замчем
    [Serializable]
    public class Owner
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Organization { get; private set; }

        public Owner(string id, string name, string organization)
        {
            Id = id;
            Name = name;
            Organization = organization;
        }
    }
    [Serializable]
    public class Date
    {
        public int Day { get; private set; }
        public int Month { get; private set; }
        public int Year { get; private set; }

        public Date(int day, int month, int year)
        {
            Day = day;
            Month = month;
            Year = year;
        }
        public Date()
        {
            string[] s = System.DateTime.Now.ToString("dd MM yyyy").Split();
            Day = System.Convert.ToInt32(s[0]);
            Month = System.Convert.ToInt32(s[1]);
            Year = System.Convert.ToInt32(s[2]);
        }
    }
    [Serializable]
    public class Node<T>
    {
        public T Item { get; set; }

        public Node<T> Next { get; set; }


        public Node(T item)
        {
            Item = item;
        }

        public bool IsNull()
        {
            if (this == null)
                return true;
            else
                return false;
        }

        public override string ToString()
        {
            return Item.ToString();
        }
    }
    [Serializable]
    public class CList<T> where T: IComparable
    {
        private int itemCount;
        private Node<T> head;

        public Owner owner { get; }
        public Date date { get; }
        public Node<T> Head { get => head; }

        public CList()
        {
            itemCount = 0;
            owner = new Owner("65741796", "Artyom Orlov", "BSTU");
            date = new Date();
        }

        public bool IsEmpty()
        {
            if (itemCount == 0)
                return true;
            else
                return false;
        }
        public static CList<T> operator +(CList<T> a, CList<T> b)
        {
            CList<T> c = new CList<T>();

            for (int i = 0; i < a.getItemCount(); i++)
            {
                c.Add(a[i]);
            }

            for (int i = 0; i < b.getItemCount(); i++)
            {
                c.Add(b[i]);
            }

            return c;
        }

        public static CList<T> operator --(CList<T> a)
        {
            if (a.itemCount == 0) throw new Exception("List is empty");

            a.itemCount--;
            a.head = a.Head.Next;
            return a;
        }

        public static bool operator ==(CList<T> a, CList<T> b)
        {
            if (a.getItemCount() == b.getItemCount())
            {
                for (int i = 0; i < a.getItemCount(); i++)
                {
                    if (!Object.Equals(a[i], b[i])) return false;
                }
                return true;
            }
            else
                return false;
        }

        public static bool operator !=(CList<T> a, CList<T> b)
        {
            if (a.getItemCount() == b.getItemCount())
            {
                for (int i = 0; i < a.getItemCount(); i++)
                {
                    if (Object.Equals(a, b)) return true;
                }
                return false;
            }
            else
                return true;
        }

        public override string ToString()
        {
            Node<T> current = head;
            string rez = System.Convert.ToString(head.Item);
            for (int i = 1; i < getItemCount(); i++)
            {
                current = current.Next;
                rez += " " + System.Convert.ToString(current.Item);
            }
            return rez;
        }
        public int getItemCount() => itemCount;
       
        public T this[int i]
        {
            get
            {
                Node<T> current = Head;
                for (int j = 0; j < i; j++)
                {
                    current = current.Next;
                }
                return current.Item;
            }
            set
            {
                Node<T> current = Head;
                for (int j = 0; j < i; j++)
                {
                    current = current.Next;
                }
                current.Item = value;
            }

        }

        public void Add(T val)
        {
            if (head == null)
            {
                head = new Node<T>(val);
            }
            else
            {
                Node<T> current = Head;
                for (int i = 1; i < getItemCount(); i++)
                {
                    current = current.Next;
                }
                current.Next = new Node<T>(val);

            }
            itemCount++;
        }

        public void Remove()
        {
            if (itemCount == 0) throw new Exception("List is empty");

            itemCount--;
           
            head = Head.Next;

            
        }

        public void Print()
        {
            Console.Write(this.ToString());
        }


        public  void ToFile(string fileName)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, this);
            stream.Close();
        }

        public static void FromFile(out CList<T> list, string fileName)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            list = (CList<T>)formatter.Deserialize(stream);
        }

       /* public void ToJson(string fileName)
        {
            string json = JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(fileName, json);
        }

        public static void FromJson(out CList<T> list, string fileName)
        {

            list = JsonConvert.DeserializeObject<CList<T>>(File.ReadAllText(fileName), new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });
        }*/

        public void toXML(string fileName)
        {
            XmlSerializer ser = new XmlSerializer(typeof(XmlElement));
            XmlElement Document = new XmlDocument().CreateElement("CList");
            Node<T> current = Head;
            //var NodeList = new List<XmlElement>();
            String s;
            for (int j = 0; j < itemCount; j++)
            {
                //NodeList.Add(new XmlDocument().CreateElement(current.ToString(), "ns"));

                s = $"Node{j}";
                Console.WriteLine(s);
                XmlElement temp = new XmlDocument().CreateElement(s);
                temp.InnerText = current.ToString();
                XmlNode TValue = Document.OwnerDocument.ImportNode(temp,true);
                //XmlElement curNode = XmlDocument().CreateElement(current.Node)
                Document.AppendChild(TValue);
                current = current.Next;
            }
            
            TextWriter writer = new StreamWriter(fileName);
            ser.Serialize(writer, Document);
            writer.Close();
        }

    }
}

