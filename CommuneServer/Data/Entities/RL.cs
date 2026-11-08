using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommuneServer.Data.Entities
{
    public class RL   // ResultList
    {
        public string Id { get; set; } // id = IdBureau + idListeElectoral
        
        [ForeignKey("RB")]
        public string IdRB { get; set; } // IdResultatBureau

        public virtual RB RB { get; set; }  // ResultatBureau

        [ForeignKey("LE")]
        public long Lid { get; set; }  // ListeElectoralId

        public virtual LE LE { get; set; }  //liste

        public int NC { get; set; } //NbCandidat

        public int NO { get; set; }  //NBObtenu

        public bool IF { get; set; } = false;

        public string CB { get; set; }  // CreatedBy
        public DateTime CA { get; set; }  // CreatedAt
        public string UBy { get; set; }   // UpdatedBy
        public DateTime? UA { get; set; }  // UpdatedAt
        public string VB { get; set; }  // ValidatedBy
        public DateTime? VA { get; set; }  // ValidatedAt

        public ICollection<RC> RC { get; set; }
    }
}
