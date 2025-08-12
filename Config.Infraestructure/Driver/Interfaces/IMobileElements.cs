namespace Config.Infraestructure.Driver.Interfaces;
public interface IMobileElements
{
    string Name { get; set; }
    By By { get; set; }
    int Count { get; }
    MobileElement this[int index] { get; }
    IList<MobileElement> Items { get; }
}
