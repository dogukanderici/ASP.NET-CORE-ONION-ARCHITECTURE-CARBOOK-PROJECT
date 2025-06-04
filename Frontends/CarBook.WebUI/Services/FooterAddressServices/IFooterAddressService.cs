using CarBook.Dto.FooterAddressDtos;

namespace CarBook.WebUI.Services.FooterAddressServices
{
    public interface IFooterAddressService
    {

        Task<List<ResultFooterAddressDto>> GetFooterAddressAsync();
    }
}
