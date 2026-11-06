public static class Locations
{
    public const string RUSSIA = "Россия";

    public const string KAZAKHSTAN = "Казахстан";

    public const string GEOGRIA = "Грузия";

    public static HashSet<string> FriendlyCountries = new() { RUSSIA, KAZAKHSTAN };

    public static HashSet<string> HostileCountries = new() { GEOGRIA };

}