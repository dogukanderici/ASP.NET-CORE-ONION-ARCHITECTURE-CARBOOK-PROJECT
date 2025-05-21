namespace CarBook.WebUI.Models
{
    public class FooterUIViewModel
    {
        public FooterUIViewModel()
        {
            FooterAddressUIViewModel = new FooterAddressUIViewModel();
            SocialMediaUIViewModel = new SocialMediaUIViewModel();
        }

        public FooterAddressUIViewModel FooterAddressUIViewModel { get; set; }
        public SocialMediaUIViewModel SocialMediaUIViewModel { get; set; }

    }
}
