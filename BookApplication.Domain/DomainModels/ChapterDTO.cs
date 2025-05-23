using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApplication.Domain.DomainModels
{
    public class ChapterDTO
    {
        public int ChapterId { get; set; }
        public int Id { get; set; }
        public string BookId { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public string KeyConcept { get; set; }
        public int TotalPages { get; set; }
        public bool IncludesExercises { get; set; }
        public string Level { get; set; }
        public DateTime LastUpdated { get; set; }
        public string EstimatedTime { get; set; }
        public bool Recommended { get; set; }

    }
}
