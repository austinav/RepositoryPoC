namespace RepositoryPoC.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Entity1
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int? Entity2_Id { get; set; }

        public virtual Entity2? Entity2 { get; set; }
    }
}
