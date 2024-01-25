using AZTDinner.Infrastructure.Authentication;
public class JwtSettings{
    public const string SessionName="JwtSettings";
    public string Secret{get;init;}=null;
    public string Issuer{get;init;}=null;
    public string Audience{get;init;}=null;
    public double ExpiryMinutes{get;init;}

}