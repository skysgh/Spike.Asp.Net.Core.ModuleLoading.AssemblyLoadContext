using System.Collections.ObjectModel;

namespace App.Base.Shared.Models
{
    public class SomeBaseParentModel
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<SomeBaseChildModel> Addresses
        {
            get => _children;
            set => _children = value;
        }
        ICollection<SomeBaseChildModel> _children = new Collection<SomeBaseChildModel>();
    }

}
