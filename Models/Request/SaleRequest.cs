using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace MavickBackend.Models.Request
{
    public class SaleRequest
    {
        [Range(1, double.MaxValue, ErrorMessage = "El valor del IdUser debe ser mayor a 0")]
        [isExistUser(ErrorMessage = "El Usuario no existe")]
        public long IdUser { get; set; }

        [MinLength(1, ErrorMessage = "Deben existir conceptos")]
        public List<Concept> Concepts { get; set; }

        public SaleRequest()
        {
            Concepts = new List<Concept>();
        }
    }

    public class Concept
    {
        public int Amount { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Import { get; set; }
        public int IdProduct { get; set; }
    }

    #region Validaciones
    public class isExistUserAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            long idUser = (long)value;
            using (var db = new MavickDBContext())
            {
                if (db.Users.Find(idUser) == null)
                {
                    return false;
                }
            }
            return true;
        }
    }
    #endregion
}
