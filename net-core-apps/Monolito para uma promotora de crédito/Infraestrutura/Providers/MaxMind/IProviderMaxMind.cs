namespace Infraestrutura
{
    public interface IProviderMaxMind
    {
        (double? latitude, double? longitude) ObterLatitudeLongitude();
    }
}