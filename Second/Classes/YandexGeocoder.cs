using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SecondTask.Classes {
    class YandexGC {
        private static readonly String url = "https://geocode-maps.yandex.ru/1.x/?format=json&geocode=";

        public static void GetCoordinates(String cityName, ref Double longitude, ref Double latitude) {
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url + cityName);
            HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();

            String response;

            using (StreamReader reader = new StreamReader(httpResponse.GetResponseStream())) {
                response = reader.ReadToEnd();
            }

            GeoCoder cityGeoInfo = JsonConvert.DeserializeObject<GeoCoder>(response);

            if (cityGeoInfo.response.GeoObjectCollection.metaDataProperty.GeocoderResponseMetaData.found != "0") {
                String str = cityGeoInfo.response.GeoObjectCollection.featureMember[0].GeoObject.Point.pos;

                String[] coords = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                longitude = Convert.ToDouble(coords[0].Replace('.', ','));
                latitude = Convert.ToDouble(coords[1].Replace('.', ','));
            }
            else {
                longitude = -1;
                latitude = -1;
            }
        }
    }

    class GeoCoder {
        public Response response { get; set; }
    }

    class Response {
        public GeoObjectCollection GeoObjectCollection { get; set; }
    }        

    class GeoObjectCollection {
        public MetaDataProperty metaDataProperty { get; set; }
        public List<FeatureMember> featureMember { get; set; }
    }

    class MetaDataProperty {
        public GeocoderResponseMetaData GeocoderResponseMetaData { get; set; }
    }

    class GeocoderResponseMetaData {
        public string request { get; set; }
        public string found { get; set; }
        public string results { get; set; }
    }

    class FeatureMember {
        public GeoObject GeoObject { get; set; }
    }

    class GeoObject {
        public string description { get; set; }
        public string name { get; set; }
        public BoundedBy boundedBy { get; set; }
        public YPoint Point { get; set; }
    }   

    class BoundedBy {
        public Envelope Envelope { get; set; }
    }

    class Envelope {
        public string lowerCorner { get; set; }
        public string upperCorner { get; set; }
    }

    class YPoint {
        public string pos { get; set; }
    }    
}
