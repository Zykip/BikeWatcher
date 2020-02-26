using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BikeWatcher.Models
{
    public class SignVelo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int IdVelo { get; set; }
        public string Email { get; set; }
        public string Commentaire { get; set; }
    }
}
