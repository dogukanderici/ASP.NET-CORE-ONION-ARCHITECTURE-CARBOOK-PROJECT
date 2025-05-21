using CarBook.Dto.FooterAddressDtos;

namespace CarBook.WebUI.Models
{
    public class FooterAddressUIViewModel
    {
        public FooterAddressUIViewModel()
        {
            FooterAddressDatas = new List<ResultFooterAddressDto>();
        }

        public List<ResultFooterAddressDto> FooterAddressDatas { get; set; }
    }
}
