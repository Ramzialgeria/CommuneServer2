using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommuneServer.Data.Entities
{
    public class RC  // ResultCandidat
    {
        public string Id { get; set; } // id = IdListeElectoral + idCandidat
        

        [ForeignKey("Cd")]
        public long Cid { get; set; }  // CandidatId

        public virtual Cd Cd { get; set; }

        [ForeignKey("RL")]
        public string IdRL { get; set; }

        public virtual RL RL { get; set; }


        public int NO { get; set; }  // NBObtenu

        public string CB { get; set; }  // CreatedBy
        public DateTime CA { get; set; }  // CreatedAt
        public string UBy { get; set; }   // UpdatedBy
        public DateTime? UA { get; set; }  // UpdatedAt
        public string VB { get; set; }  // ValidatedBy
        public DateTime? VA { get; set; }  // ValidatedAt
    }
}
