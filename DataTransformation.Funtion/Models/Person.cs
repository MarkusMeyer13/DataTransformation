using Newtonsoft.Json;
using System.Xml.Serialization;

namespace DataTransformation.Funtion.Models
{
    /// <summary>
    /// Person.
    /// </summary>
    [XmlRoot("person")]
    [JsonObject(Title = "Person")]
    public class Person
    {
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [XmlElement("firstname")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [XmlElement("lastname")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        [XmlAttribute("gender")]
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets the age.
        /// </summary>
        /// <value>
        /// The age.
        /// </value>
        [XmlElement("age")]
        public int Age { get; set; }
    }
}
