using System;
using System.Collections.Generic;

namespace API1.Models
{
    public partial class MasterBook
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double? Rating { get; set; }
        public string Image { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
