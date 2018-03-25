using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lands2.Domain
{
    public class UserType
    {
        [Key] //Alfanumerico autoincrementable
        public int UserTypeId { get; set; } //la clave primaria de la tabla

        [Required(ErrorMessage = "The field {0} is required.")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contains a maximum of {1} characters length.")]
        [Index("UserType_Name_Index", IsUnique = true)] //para evitar que haya dos UserType iguales \\Nombre de tabla, Nombre de campo y sufijo index (es una estándar personal del profe)        
        public string Name { get; set; } //nombre del usuario

        [JsonIgnore]
        public virtual ICollection<User> Users { get; set; }
    }
}
