using CarBook.Dto.AuthorDtos;

namespace CarBook.WebUI.Areas.Admin.Models
{
    public class AdminUIAuthorViewModel
    {
        public List<ResultAuthorDto> ResultDatas { get; set; }
        public CreateAuthorDto CreateData { get; set; }
        public UpdateAuthorDto UpdateData { get; set; }
    }
}
