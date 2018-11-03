using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Web;

namespace SchoolSchedule.ViewModels
{
    public class NewsViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageUpload { get; set; }
        public bool IsDeleted { get; set; }
        public Image Image { get; set; }
    }
}