namespace Api.Common.Core.Authentication
{
    public class TokenConfigurations
    {
        public string Audience => "Api.Template.Audience";
        public string Issuer => "Api.Template.Issuer";
        public int Seconds => 86399;
    }
}