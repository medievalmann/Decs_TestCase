using System.Collections.ObjectModel;

namespace ConfigurationManager.Core.Models
{
    public class Application
    {
        public Application()
        {
            Configurations = new Collection<Configuration>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<Configuration> Configurations { get; set; }
    }
}
