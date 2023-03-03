using System.ComponentModel.DataAnnotations;

namespace PultApp.Models
{
    public class ReviewChartResult
    {
        public int Heard { get; set; }
        public int HeardCount { get; set; }
        public int Rating { get; set; }
        public int RatingCount { get; set; }
    }
}
