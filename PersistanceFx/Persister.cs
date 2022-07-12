using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PersistanceFx
{
    public enum PersistanceType
    {
        XML,JSON,BINARY,CSV,RSS
    }
    public enum XmlTransformType
    {
        Attribute,Element
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class XmlPersisterAttribute :System.Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Class,AllowMultiple =false)]
    public class TargetPersistaneTypeAttribute:System.Attribute
    {
        public PersistanceType format;
        public TargetPersistaneTypeAttribute(PersistanceType format)
        {
            this.format = format;
        }

    }

    public class XmlAttributeAttibute : XmlPersisterAttribute 
    {
        

    }
    public class XmlElementAttibute: XmlPersisterAttribute
    {

    }

    public class IgnoreAttribute : XmlPersisterAttribute
    {

    }
    internal class XMLPersister
    {

        public void WriteObject(object source)
        {
            //Property List (public)
            // How to transform each Property (xml attribute ,xml element)

            var properyList = source.GetType().GetProperties();

            foreach (var property in properyList)
            {
                var xmlAttributes =
                    property.GetCustomAttributes(typeof(XmlPersisterAttribute), true) as XmlPersisterAttribute[];
                foreach (var xmlattribute in xmlAttributes)
                {
                    if (xmlattribute is XmlAttributeAttibute)
                    {
                        Console.WriteLine($"Target XML transform {property.Name}format is : {xmlattribute} ");
                    }
                    if (xmlattribute is XmlPersisterAttribute)
                    {
                        Console.WriteLine($"Target XML transform {property.Name}format is : {xmlattribute} ");
                    }
                    if (xmlattribute is XmlIgnoreAttribute)
                    {
                        Console.WriteLine($"Target XML transform {property.Name}format is : {xmlattribute} ");
                    }
                }
                
            }
            
        }
    }
    public class Persister
    {
        
        public bool Persist(object source)
        {
            var targetTypeAttribute = source.GetType().GetCustomAttributes(typeof(TargetPersistaneTypeAttribute), true).FirstOrDefault() as TargetPersistaneTypeAttribute;
            PersistanceType _targetFormat = targetTypeAttribute.format;
            Console.WriteLine($"Target format is : {_targetFormat} ");
            switch (_targetFormat)
            {
                case PersistanceType.XML: XMLPersister _persister = new XMLPersister();
                                          _persister.WriteObject(source);
                                           break;
            }

            return false;

        }
    }
}
