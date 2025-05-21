using CarBook.Dto.ContactDtos;

namespace CarBook.WebUI.Models
{
    public class ContactUIViewModel
    {
        public ContactUIViewModel()
        {
            CreateData = new CreateContactDto();
        }

        public CreateContactDto CreateData { get; set; }
    }
}
