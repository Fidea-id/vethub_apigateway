using DevExpress.Xpo;

namespace Domain.Entities.Models.Clients.XPO
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    [Persistent("SchemaVersion")]
    public class SchemaVersionXPO : XPLiteObject
    {
        public SchemaVersionXPO(Session session) : base(session) { }

        public int Version { get; set; }
        [DbType("LONGTEXT")]
        public string Note { get; set; }
    }
}
