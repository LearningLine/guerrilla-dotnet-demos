using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Hypermedia
{
    [DataContract(Namespace = "", Name = "link")]
    public class Link
    {
        [DataMember(Name = "rel")]
        public Relationship Relationship { get; set; }

        [DataMember(Name = "uri")]
        public string Uri { get; set; }

        [DataMember(Name = "verb")]
        public string Verb { get; set; }
    }

    [DataContract(Namespace = "")]
    public enum Relationship
    {
        [EnumMember(Value = "self")] Self,
        [EnumMember(Value = "parent")] Parent,
        [EnumMember(Value = "create")] Create,
        [EnumMember(Value = "children")] Children,
        [EnumMember(Value = "target")] Target
    }

    [DataContract(Namespace = "")]
    public class Resource
    {
        public Resource()
        {
            Links = new List<Link>();
        }

        [DataMember(Name = "links")]
        public List<Link> Links { get; private set; }
    }

    [DataContract(Namespace = "", Name = "resourceCollection")]
    public class ResourceCollection<T> : Resource
    {
        [DataMember(Name = "members")]
        public List<T> Members { get; set; }
    }
}