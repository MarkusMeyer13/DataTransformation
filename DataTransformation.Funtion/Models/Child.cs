using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DataTransformation.Funtion.Models
{
    /// <summary>
    /// Child.
    /// </summary>
    public class Child
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [XmlElement("name")]
        public string Name { get; set; }
    }
}
