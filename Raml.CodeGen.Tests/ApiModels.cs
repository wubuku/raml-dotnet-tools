











using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Fstab.Models
{

    public partial class Storage

    {
        

    } // end class

    public partial class StoragediskDevice  : Storage

    {
        

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]

        public Type Type { get; set; }

        [JsonProperty("device")]

        public string Device { get; set; }

    } // end class

    public partial class StoragediskUUID  : Storage

    {
        

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]

        public Type Type { get; set; }

        [JsonProperty("label")]

        public string Label { get; set; }

    } // end class

    public partial class Storagenfs  : Storage

    {
        

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]

        public TypeA Type { get; set; }

        [JsonProperty("remotePath")]

        public string RemotePath { get; set; }

        [JsonProperty("server")]

        public string Server { get; set; }

    } // end class

    public partial class Storagetmpfs  : Storage

    {
        

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]

        public TypeB Type { get; set; }

        [JsonProperty("sizeInMB")]

        public int SizeInMB { get; set; }

    } // end class


    public partial class Entry

    {
        

        [JsonProperty("storage")]

        public Storage Storage { get; set; }

        [JsonProperty("fstype")]
        [JsonConverter(typeof(StringEnumConverter))]

        public Fstype Fstype { get; set; }

        [JsonProperty("options")]

        public IList<string> Options { get; set; }

        [JsonProperty("readonly")]

        public bool Readonly { get; set; }

    } // end class

    
    public enum Type
    {
        disk
    }

    
    public enum TypeA
    {
        nfs
    }

    
    public enum TypeB
    {
        tmpfs
    }

    
    public enum Fstype
    {
        ext3, ext4, btrfs
    }


} // end Objects namespace