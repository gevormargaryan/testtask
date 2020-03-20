using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace TestApp.Models.MemCahedModels
{

    [Serializable()]
    public class Employee
    {
        [Required(ErrorMessage = "FullName is required")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; }

        public void Returns<T>(Func<object, object> p)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            return (((Employee)obj).FullName == this.FullName) 
                && (((Employee)obj).Role == this.Role);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
