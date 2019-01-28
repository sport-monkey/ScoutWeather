using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherStore.Models
{
    public class Reading
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReadingId { get; set; }

        public int Device { get; set; }

        [Column(TypeName = "decimal(6,1)")]
        public decimal Temperature { get; set; }

        [Column(TypeName = "decimal(6,1)")]
        public decimal Humidity { get; set; }

        [Column(TypeName = "decimal(6,1)")]
        public decimal Pressure { get; set; }   
        
        public DateTime Created { get; set; }

    }
}
