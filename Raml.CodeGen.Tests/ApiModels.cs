











using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Fstab.Models
{

    public partial class Storage

    {
        

    } // end class

    public partial class StoragediskDevice  : Storage

    {
        

		[JsonProperty("device")]
        public string Device { get; set; }

    } // end class

    public partial class StoragediskUUID  : Storage

    {
        

		[JsonProperty("label")]
        public string Label { get; set; }

    } // end class

    public partial class Storagenfs  : Storage

    {
        

		[JsonProperty("remotePath")]
        public string RemotePath { get; set; }

		[JsonProperty("server")]
        public string Server { get; set; }

    } // end class

    public partial class Storagetmpfs  : Storage

    {
        

		[JsonProperty("sizeInMB")]
        public int SizeInMB { get; set; }

    } // end class


    public partial class Entry

    {
        

		[JsonProperty("storage")]
        public Storage Storage { get; set; }

		[JsonProperty("options")]
        public IList<string> Options { get; set; }

		[JsonProperty("readonly")]
        public bool Readonly { get; set; }

    } // end class


} // end Objects namespace