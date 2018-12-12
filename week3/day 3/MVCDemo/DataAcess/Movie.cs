using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAcess
{
    public class Movie
    {
        //Mark Primary key as [Key]
        [Key] //but it does infer that everything named MovieId or Id etc will be the primary key, if not otherwise configured
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)] // max 50 chars
        public string Title { get; set; }

        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ReleaseDate { get; set; }

        public List<string> Cast { get; set; } = new List<string>();

        public virtual ICollection<CastMember> CasMenter { get; set; }
    }
}