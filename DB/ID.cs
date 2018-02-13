using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public struct ID
    {
        internal string id;

        internal ID(string id)
        {
            this.id = id;
        }

        public static implicit operator bool(ID _id)
        {
            return _id.id != null;
        }

        public static implicit operator string(ID _id)
        {
            return _id.id;
        }
    }
}
