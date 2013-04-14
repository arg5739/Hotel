using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Net;
using System.Web.Services;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
public class Service : IService
{
    // variable declaration
	string key = null;

    //constructor
    public Service() 
    {
        // key is obtained from Expedia affiliate network
        key = "utf2pd8jsm9mr6mmfxyjqeq9";
    }

    /**
 * Provides the list of hotels in given city and state
 *
 * @param       city_name    (string) it is city name 
 * @param       state_name   (string) it is state name
 
 *
 * @return              json object 
 */
    public string getListOfHotel(string city_name ,string state_name) 
    {
          string output;
        using (WebClient client = new WebClient())
        {

            string input = "http://api.ean.com/ean-services/rs/hotel/v3/list?_type=json&locale=en_US&currencyCode=USD&numberOfResults=15&apiKey=" + key + "&stateProvinceCode=" + state_name + "&city=" + city_name;
            output = client.DownloadString(input);

            
            
            //JsonValue value = JsonValue.Parse(output);
            var jss = new JavaScriptSerializer();
            var dict = jss.Deserialize<Dictionary<string, dynamic>>(output);

            return (jss.Serialize(dict["HotelListResponse"]));
        }
    }

    /**
* Provides the room availability for given hotel
*
* @param       hotel_id    (string) hotel id 
* @param       arrival_date   (string) it is arrival date in form "mm/dd/yyyy"
* @param       departure_date   (string) it is departure date in form "mm/dd/yyyy"
* @return              json object 
*/

    public string getAvailOfHotel(string hotel_id, string arrival_date, string departure_date)
    {
        string output;
        using (WebClient client = new WebClient())
        {

            string input = "http://api.ean.com/ean-services/rs/hotel/v3/avail?_type=json&locale=en_US&currencyCode=USD&apiKey=" + key + "&hotelId=" + hotel_id + "&arrivalDate=" + arrival_date + "&departureDate=" + departure_date;
            output = client.DownloadString(input);



            //JsonValue value = JsonValue.Parse(output);
            var jss = new JavaScriptSerializer();
            var dict = jss.Deserialize<Dictionary<string, dynamic>>(output);
            return (jss.Serialize(dict));
        }
    } 
	
	
}
