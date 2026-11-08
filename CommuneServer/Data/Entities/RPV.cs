using CommuneSecurity.Encryption;
using CommuneServer.Migrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommuneServer.Data.Entities
{
    public enum DataSource
    {
        Web = 1,
        Desktop = 2,
    }

    public class RPV
    {
        public Guid IdPV { get; set; } = Guid.NewGuid();

        [ForeignKey("St")]
        public string IdSt { get; set; } //IdStructure

        public virtual St St { get; set; }  // Structure

        public string TSEncrypted { get; set; } //TypeStructure

        public string PVPEncrypted { get; set; } //PVPath

        public string FEPEncrypted { get; set; } //FileEtatPath

        // Which app produced this PV - see DataSource.
        public DataSource Source { get; set; }

        public string CB { get; set; }  // CreatedBy
        public DateTime CA { get; set; }  // CreatedAt
        public string UBy { get; set; }   // UpdatedBy
        public DateTime? UA { get; set; }  // UpdatedAt
        public string VB { get; set; }  // ValidatedBy
        public DateTime? VA { get; set; }  // ValidatedAt

        [NotMapped]
        public string TS
        {
            get
            {
                return EncryptionService.Decrypt(TSEncrypted);
            }
            set
            {
                TSEncrypted = EncryptionService.Encrypt(value);
            }
        }

        [NotMapped]
        public string PVP
        {
            get
            {
                return EncryptionService.Decrypt(PVPEncrypted);
            }
            set
            {
                PVPEncrypted = EncryptionService.Encrypt(value);
            }
        }

        [NotMapped]
        public string FEP
        {
            get
            {
                return EncryptionService.Decrypt(FEPEncrypted);
            }
            set
            {
                FEPEncrypted = EncryptionService.Encrypt(value);
            }
        }
    }
}
