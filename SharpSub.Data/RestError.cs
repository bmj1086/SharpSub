using System;
using System.Reflection;

namespace SharpSub.Data
{
    public enum RestError
    {
        [ErrorAttribute("A generic error")]
        Generic = 0,

        [ErrorAttribute("Required parameter is missing")]
        MissingParamater = 10,

        [ErrorAttribute("Incompatible Subsonic REST protocol version. Client must upgrade")]
        IncompatibleClientRestVersion = 20,

        [ErrorAttribute("Incompatible Subsonic REST protocol version. Server must upgrade")]
        IncompatibleServerRestVersion = 30,

        [ErrorAttribute("Wrong username or password")]
        InvalidCredentials = 40,

        [ErrorAttribute("User is not authorized for the given operation")]
        NotAuthorized = 50,

        [ErrorAttribute("The trial period for the Subsonic server is over. Please donate to get a license key. Visit subsonic.org for details")]
        TrialEnded = 60,

        [ErrorAttribute("The requested data was not found")]
        NotFound = 70,

    }

    public class ErrorAttribute : Attribute
    {
        /// <summary>
        /// Holds the value for the attribute
        /// </summary>
        public string Value { get; protected set; }

        public ErrorAttribute(String value)
        {
            Value = value;
        }

        /// <summary>
        /// Will get the string value for a given enums value, this will
        /// only work if you assign the Value attribute to
        /// the items in your enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetValue(Enum value)
        {
            // Get the type
            Type type = value.GetType();

            // Get fieldinfo for this type
            FieldInfo fieldInfo = type.GetField(value.ToString());

            // Get the stringvalue attributes
            ErrorAttribute[] attribs = fieldInfo.GetCustomAttributes(
                typeof(ErrorAttribute), false) as ErrorAttribute[];

            // Return the first if there was a match.
            return attribs.Length > 0 ? attribs[0].Value : null;
        }
    


    }
}
