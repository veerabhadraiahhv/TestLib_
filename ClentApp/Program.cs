using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClentApp
{
    //AnNotate -> Context Information
    [PersistanceFx.TargetPersistaneType(PersistanceFx.PersistanceType.XML)]
    //[PersistanceFx.TargetXmlTransformType(PersistanceFx.XmlTransformType.Attribute)]
    public class PatientDataModel
    {
        [PersistanceFx.XmlAttributeAttibute]
        public string MRN { get; set; }
        [PersistanceFx.XmlElementAttibute]
        public string Name { get; set; }
        [PersistanceFx.Ignore]
        public int  Age { get; set; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            PatientDataModel model = new PatientDataModel() { MRN = "M100", Name = "Tom", Age = 33 };
            PersistanceFx.Persister _persister = new PersistanceFx.Persister();
            _persister.Persist(model);
        }
    }
}
