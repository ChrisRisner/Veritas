using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CookComputing.XmlRpc;

namespace Veritas.BusinessLayer.Media
{
    [XmlRpcMissingMapping(MappingAction.Ignore)]
    public struct MediaObject
    {
        public string name;
        public string type;
        public byte[] bits;
    }

    [Serializable]
    public struct MediaObjectInfo
    {
        public string url;
    }
}
