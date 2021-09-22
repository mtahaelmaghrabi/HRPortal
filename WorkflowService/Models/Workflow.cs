using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkflowService.Models
{
    public class Workflow
    {
        [Required]
        [Key]
        public string WorkflowID { get; set; }

        public string RequestTypeID { get; set; }
        public string WorkflowID { get; set; }


    }
}