

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AzureAppDbHosting.Models
{
    public class LeadingEntity
    {
        [System.ComponentModel.DataAnnotations.Key]  
        public int Id { get; set; } 
       [DataType(DataType.Date)]
            [DisplayName("Date")]

        public DateTime LeadDate { get; set; }
        [DisplayName("Lead Source")]

        public string LeadSource { get; set; } 
        public string Name { get; set; }
        public string Mobile { get; set; }  
        public string Email { get; set; }


        




    }
}
