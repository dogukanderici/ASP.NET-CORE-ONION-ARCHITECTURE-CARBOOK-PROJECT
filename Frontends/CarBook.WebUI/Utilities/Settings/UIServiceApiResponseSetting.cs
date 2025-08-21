namespace CarBook.WebUI.Utilities.Settings
{
    public class UIServiceApiResponseSetting<TEntity>
    {
        public TEntity ResponseData { get; set; }
        public List<TEntity> ResponseDatas { get; set; }
        public HttpResponseMessage HttpResponseMessage { get; set; }
    }
}
