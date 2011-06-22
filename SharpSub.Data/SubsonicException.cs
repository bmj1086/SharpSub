using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpSub.Data
{
    class SubsonicException : Exception
    {
        public SubsonicException(SubsonicResponse response) : base(String.Format("An error returned from the Subsonic server: {0}", response.ErrorMessage))
        {
        }
    }
}
