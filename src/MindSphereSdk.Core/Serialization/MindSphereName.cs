using System;
using System.Collections.Generic;
using System.Text;

namespace MindSphereSdk.Core.Serialization
{
    /// <summary>
    /// Attribute to specify the corresponding object name in the MindSphere.
    /// </summary>
    public class MindSphereName : Attribute
    {
        /// <summary>
        /// MindSphere name of the object.
        /// </summary>
        public string Name { get; set; }

        public MindSphereName(string name)
        {
            Name = name;
        }
    }
}
