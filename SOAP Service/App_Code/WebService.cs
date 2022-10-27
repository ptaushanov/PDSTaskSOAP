using System.Collections.Generic;
using System.IO;
using System.Web.Services;
using System.Text.Json;
using System.Linq;
using System.IdentityModel;
using System.ServiceModel.Description;
using System.Web;
using System;
using System.Reflection;

/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{
    private readonly string FILENAME = "C:\\Users\\pepi1\\source\\repos\\SOAP\\SOAP Service\\sample.json";

    public WebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    [WebMethod]
    public int Add(int a, int b)
    {
        return a + b;
    }

    [WebMethod]
    public double FahrenheitToCelsius(double tempFahrenheit)
    {
        return ((tempFahrenheit - 32) / 9.0) * 5.0;
    }

    [WebMethod]
    public double CelsiusToFahrenheit(double tempCelsius)
    {
        return ((tempCelsius * 9.0) / 5.0) + 32;
    }

    [WebMethod]
    public UserData ReadUserData()
    {
        string jsonText = File.ReadAllText(FILENAME);
        UserData userData = JsonSerializer.Deserialize<UserData>(jsonText);
        return userData;
    }

    [WebMethod]
    public UserData CreateUserData(UserData requestData)
    {
        JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
        string jsonText = JsonSerializer.Serialize(requestData, options);
        File.WriteAllText(FILENAME, jsonText);
        return requestData;
    }

    [WebMethod]
    public UserData UpdateUserData(UserData requestData)
    {
        string jsonText = File.ReadAllText(FILENAME);
        UserData userData = JsonSerializer.Deserialize<UserData>(jsonText);
        PropertyInfo[] properties = userData.GetType().GetProperties();

        foreach (PropertyInfo property in properties)
        {
            PropertyInfo requestProperty = requestData.GetType().GetProperty(property.Name);
            if (requestProperty != null)
            {
                object propertyValue = requestProperty.GetValue(requestData);
                property.SetValue(userData, propertyValue);
            }
        }

        JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
        string newJsonText = JsonSerializer.Serialize(userData, options);
        File.WriteAllText(FILENAME, newJsonText);
        return userData;
    }

}
