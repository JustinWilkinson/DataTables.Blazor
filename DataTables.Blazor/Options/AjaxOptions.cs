namespace DataTables.Blazor.Options
{
    public class AjaxOptions
    {
        public AjaxOptions(string source)
        {
            Url = source;
        }

        public string Url { get; set; }

        //public object Data { get; set; }

        //public object DataSrc { get; set; }
    }
}
