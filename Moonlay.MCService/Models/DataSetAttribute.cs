using Moonlay.Core.Models;

namespace Moonlay.MasterData.WebApi.Models
{
    public class DataSetAttribute : IModel
    {
        public string Name { get; internal set; }
        public string Type { get; internal set; }
        public string Value { get; internal set; }
        public string PrimaryKey { get; internal set; }
        public string AutoIncrement { get; internal set; }
        public string Null { get; internal set; }
        public string DataSetName { get; internal set; }
        public string DomainName { get; internal set; }
    }
}
