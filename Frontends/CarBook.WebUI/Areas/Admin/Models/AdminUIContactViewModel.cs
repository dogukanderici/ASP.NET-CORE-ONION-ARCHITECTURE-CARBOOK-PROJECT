using CarBook.Dto.ContactDtos;

namespace CarBook.WebUI.Areas.Admin.Models
{
    public class AdminUIContactViewModel
    {
        public List<ResultContactDto> ResultDatas { get; set; }
        public ResultContactDto ResultData { get; set; }
        public CreateContactDto CreateData { get; set; }
    }
}
