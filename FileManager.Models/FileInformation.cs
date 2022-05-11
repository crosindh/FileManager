using System;
using System.ComponentModel.DataAnnotations;

namespace FileManager.Models
{
    public class FileInformation
    {
        public int Id { get; set; }

        [Display(Name = "Upload Date")]
        public string UploadDate { get; set; }

        [Required]
        [Display(Name ="File Name")]
        public string Filename { get; set; }
        [Required]
        [Display(Name = "File Number")]
        public string Filenumber { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Type { get; set; }
        
        public int Pages { get; set; }

      
        public string Addressee { get; set; }

        [Display(Name = "Section Of Origin")]
        public string Sectionoforigin { get; set; }

       
        public string Department { get; set; }

       
        [Display(Name = "Received By")]
        public string Receivedby { get; set; }

        public string Status { get; set; }

      
        [Display(Name = "Upload File")]
        public string Pdfdirectory { get; set; }
    }
}
