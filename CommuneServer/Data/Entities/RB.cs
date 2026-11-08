using CommuneSecurity.Encryption;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommuneServer.Data.Entities
{
    public class RB   // ResultBureau
    {
        public string Id { get; set; } // id = codecommune + idBureau

        [ForeignKey("Bu")]
        public long Bid { get; set; } // BureauId 
        public virtual Bu Bu { get; set; }

        public int NP { get; set; } //NBParticipation

        public int NVE { get; set; }  //NBVoixExprime

        public int NVA { get; set; }   //NBVoixAnnule

        public int NVC { get; set; }  //NBVoixConteste

        public bool IS { get; set; } = false; //IsSaisie

        public bool IF { get; set; } = false;  //IsFinished

        public string FPEncrypted { get; set; } //FileEtatPath

        // Which app captured this bureau's result - see DataSource.
        public  DataSource Source { get; set; }

        public string CB { get; set; }  // CreatedBy
        public DateTime CA { get; set; }  // CreatedAt
        public string UBy{ get; set; }   // UpdatedBy
        public DateTime? UA { get; set; }  // UpdatedAt
        public string VB { get; set; }  // ValidatedBy
        public DateTime? VA { get; set; }  // ValidatedAt

        public ICollection<RL> RL { get; set; }

        [NotMapped]
        public string FP
        {
            get
            {
                return EncryptionService.Decrypt(FPEncrypted);
            }
            set
            {
                FPEncrypted = EncryptionService.Encrypt(value);
            }
        }
    }
}
