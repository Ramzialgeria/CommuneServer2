using CommuneSecurity.Encryption;
using CommuneServer.Migrations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommuneServer.Data.Entities
{
    public class St
    {
        [Key]
        public string Id { get; set; }

        public string LSEncrypted { get; set; }

        [NotMapped]
        public string LS
        {
            get
            {
                return EncryptionService.Decrypt(LSEncrypted);
            }
            set
            {
                LSEncrypted = EncryptionService.Encrypt(value);
            }
        }
    }

    public class W : St
    {
        public long IdW { get; set; }

        [JsonIgnore]
        public virtual ICollection<Cm> Cms { get; set; }
    }

    public class Cm : St
    {
        [ForeignKey("W")]
        public  string CW { get; set; }

        public virtual W W { get; set; }

        public long IdC { get; set; }

        [InverseProperty("Cm")]
        public virtual ICollection<Cn> Cns { get; set; } = new List<Cn>(); // Centre

    }
    //public class St
    //{
    //    public string Id { get; set; }

    //    public  string LS { get; set; }
    //}


    //public class Cm : St // Commune
    //{
    //    public string CC { get; set; } // CodeCommune
        
    //    [ForeignKey("W")]
    //    public string CW { get; set; }  // CodeWilaya

    //    public virtual W W { get; set; }

    //    public string NCEncrypted { get; set; } // NameCommune

    //    public virtual ICollection<Cn> Cns { get; set; } = new List<Cn>(); // Centre


    //    [NotMapped]
    //    public string NC
    //    {
    //        get
    //        {
    //            return EncryptionService.Decrypt(NCEncrypted);
    //        }
    //        set
    //        {
    //            NCEncrypted = EncryptionService.Encrypt(value);
    //        }
    //    }

    //}

    //public class W : St // Wilaya
    //{
    //    public string CW { get; set; }  // CodeWilaya
    //    public string NWEncrypted { get; set; } // NameWilaya
    //    public virtual ICollection<Cm> Cns { get; set; } = new List<Cm>(); // Commune

    //    [NotMapped]
    //    public string NW
    //    {
    //        get
    //        {
    //            return EncryptionService.Decrypt(NWEncrypted);
    //        }
    //        set
    //        {
    //            NWEncrypted = EncryptionService.Encrypt(value);
    //        }
    //    }

    //}
}
