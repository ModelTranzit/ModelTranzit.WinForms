using Dizignit.Core;
using System.Drawing;

namespace Dizignit.DAL
{
    public class HttpApiRequest
    {
        private string _url { get; set; }
        private string _apiKey { get; set; }
        private string _latitude { get; set; }
        private string _longitude { get; set; }
        private int _zoom { get; set; }
        private int _size { get; set; }


        public HttpApiRequest(MapCordinate cordinate, int size, int zooom)
        {
            _apiKey = Environment.GetEnvironmentVariable("GoogleMapsAPIKey");

            if (string.IsNullOrEmpty(_apiKey))
                throw new Exception("GoogleMapsAPIKey variable not set.");

            _latitude = cordinate.Latitude.ToString();
            _longitude = cordinate.Longitude.ToString();
            _size = size;
            _zoom = zooom;

            _url = $"https://maps.googleapis.com/maps/api/staticmap?center={_latitude},{_longitude}&zoom={_zoom}&size={_size}x{_size}&maptype=roadmap&style=element:labels|visibility:off&key={_apiKey}";
        }

        public async Task<byte[]> GetImageAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(_url);
                if (response.IsSuccessStatusCode)
                {
                    return(await response.Content.ReadAsByteArrayAsync());
                }
                else
                {
                    throw new Exception($"Error fetching tile: {response.StatusCode}");
                }
            }
        }
    }
}
