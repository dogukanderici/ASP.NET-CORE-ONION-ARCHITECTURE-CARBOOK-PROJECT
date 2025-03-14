using CarBook.Dto.FooterAddressDtos;

namespace CarBook.WebUI.Areas.Admin.Models
{
    public class AdminUIFooterAddressViewModel
    {
        public List<ResultFooterAddressDto> ResultDatas { get; set; }
        public CreateFooterAddressDto CreateData { get; set; }
        public UpdateFooterAddressDto UpdateData { get; set; }
    }
}
