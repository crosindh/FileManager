using System.ComponentModel.DataAnnotations;


namespace FileManager.Models
{
    public class Dakk_Data
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Given Dakk Number")]
        public string Number { get; set; }


        [Display(Name = "Upload Time")]
        public string UploadTime { get; set; }


        [Required]
        public string Status { get; set; }


        [Display(Name = "Forward To")]
        public string ForwardTo { get; set; }

        public string Comments { get; set; }


        [Display(Name = "Date Of Dakk")]
        public string DateOnLetter { get; set; }


        [Display(Name = "Received Date Of Dakk")]
        public string DateReceived { get; set; }

        public string Department { get; set; }

        [Required]
        public string Subject { get; set; }


        [Required]
        [Display(Name = "Issued Number")]
        public string Givennumber { get; set; }


        public int Pages { get; set; }


        [Required]
        public string Addressee { get; set; }

        [Required]
        [Display(Name = "Section Of Origin")]
        public string Sectionoforigin { get; set; }

        [Required]
        [Display(Name = "Received By")]
        public string Receivedby { get; set; }


        [Display(Name = "Upload File")]
        public string Pdfdirectory { get; set; }

        [Display(Name = "Current Location")]
        public string CurrentLocation { get; set; }

        [Display(Name = "Forward Time")]
        public string ForwardTime { get; set; }
    }
}
