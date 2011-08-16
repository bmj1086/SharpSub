using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SharpSub.Data
{
    [Serializable]
    public class SubsonicException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public SubsonicException()
        {
        }

        public SubsonicException(SubsonicResponse response)
            : base(String.Format("An error returned from Subsonic server, Subsonic Message: {0}", response.ErrorMessage))
        {
        }

        public SubsonicException(string message) : base(message)
        {
        }

        public SubsonicException(string message, Exception inner) : base(message, inner)
        {
        }

        protected SubsonicException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
