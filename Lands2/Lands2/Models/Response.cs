namespace Lands2.Models
{
    public class Response
    {
        //"http://restcountries.eu/rest/v2/all"
        public bool IsSuccess
        {
            get;
            set;
        }
        public string Message
        {
            get;
            set;
        }
        public object Result
        {
            get;
            set;
        }
    }
}
