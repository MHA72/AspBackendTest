namespace AspBackendTest.Infrastructure.Model;

public class Currency : BaseEntity
{
     public Currency(string name, string englishName, string code)
     {
          Name = name;
          EnglishName = englishName;
          Code = code;
     }

     public string Name { get; set; }
     public string EnglishName { get; set; }
     public string Code { get; set; }
}