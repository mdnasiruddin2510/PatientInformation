using System.Collections;

namespace PatientInformation.Models.ViewModels
{
    public class VmSelectList
    {
        public VmSelectList(IEnumerable items, string dataValueField, string dataTextField)
        {
            Items = items;
            DataValueField = dataValueField;
            DataTextField = dataTextField;
        }

        public string DataTextField { get; }
        public string DataValueField { get; }
        public IEnumerable Items { get; }
    }
}
