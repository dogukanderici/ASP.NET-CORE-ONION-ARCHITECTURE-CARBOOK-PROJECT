using CarBook.Dto.FooterAddressDtos;
using CarBook.Dto.FooterAddressDtos;
using CarBook.WebUI.Utilities.Settings;

namespace CarBook.WebUI.Services.FooterAddressServices
{
    public interface IFooterAddressService
    {

        Task<UIServiceApiResponseSetting<ResultFooterAddressDto>> GetFooterAddressAsync();
        Task<UIServiceApiResponseSetting<ResultFooterAddressDto>> GetFooterAddressByIdAsync(int id);
        Task<HttpResponseMessage> CreateFooterAddressAsync(CreateFooterAddressDto createFooterAddressDto);
        Task<HttpResponseMessage> UpdateFooterAddressAsync(UpdateFooterAddressDto updateFooterAddressDto);
        Task<HttpResponseMessage> DeleteFooterAddressAsync(int id);
    }
}
