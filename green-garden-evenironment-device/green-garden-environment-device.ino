#include <DHTesp.h>

#include <ESP8266WiFi.h>
#include <ESP8266HTTPClient.h>
#include <WiFiClientSecure.h>
#include <ArduinoJson.h>

#define d0 16
#define d1 5
#define d2 4
#define d3 0
#define d4 2
#define d5 14
#define d6 12
#define d7 13
#define d8 15

DHTesp dht;

namespace config
{
  const char *ssid = "Barlow1977";
  const char *password = "BrynnVan";
  const char fingerprint[] = "679db33f787f1ea853143b8ce01dd76296774bdd";
  const char *deviceId = "green-garden-environment-ground-device";
  const String HOST_API = "https://192.168.86.28:32790/api/DeviceMessage";
} // namespace config

float Tc, Tf, RH, analogVolt;

namespace pin
{
  const int dht22 = d7;
  const int led = LED_BUILTIN;
} // namespace pin

void setupLed()
{
  pinMode(pin::led, OUTPUT);
}

void ledOn()
{
  digitalWrite(pin::led, LOW);
}

void ledOff()
{
  digitalWrite(pin::led, HIGH);
}

void sendMessage(String eventType, String sensorType, String data, String actionType)
{
  // wait for WiFi connection
  if ((WiFi.status() == WL_CONNECTED))
  {
    DynamicJsonDocument doc(2048);
    doc["deviceId"] = config::deviceId;
    doc["eventType"] = eventType;
    doc["sensorType"] = sensorType;
    doc["actionType"] = actionType;
    doc["data"] = data;

    String json;
    serializeJson(doc, json);

    WiFiClientSecure client; //Declare object of class WiFiClient
    HTTPClient http;

    client.setFingerprint(config::fingerprint);
    Serial.print("[HTTP] begin...\n");
    http.useHTTP10(true);
    // configure traged server and url
    http.begin(client, config::HOST_API); //HTTP

    http.addHeader("Content-Type", "application/json");

    Serial.print("[HTTP] POST BEGIN...\n");
    Serial.print(json);
    Serial.print("\n");
    // start connection and send HTTP header and body
    int httpCode = http.POST(json);
    Serial.printf("[HTTP] POST... code: %d\n", httpCode);
    Serial.print("[HTTP] POST END\n");
    // file found at server
    if (httpCode == HTTP_CODE_OK)
    {
      DynamicJsonDocument resultDoc(2048);
      Serial.print("[HTTP] RESPONSE BEGIN\n");
      deserializeJson(resultDoc, http.getStream());
      String output;
      serializeJson(resultDoc, output);
      Serial.print(output);
      Serial.print("\n");
      Serial.print("[HTTP] RESPONSE END\n");
      http.end();
    }
    else
    {
      Serial.printf("[HTTP] POST... failed, error: %s\n", http.errorToString(httpCode).c_str());
    }
  }
}

void setup()
{

  Serial.begin(115200);
  WiFi.begin(config::ssid, config::password);

  while (WiFi.status() != WL_CONNECTED)
  {
    delay(1000);
    Serial.println("Connecting...");
  }

  Serial.println("Initializing..."); //Test the serial monitor
  setupLed();

  // Autodetect is not working reliable, don't use the following line
  // dht.setup(17);
  // use this instead:
  dht.setup(pin::dht22, DHTesp::DHT22); // Connect DHT sensor to GPIO 17
}

int timeSinceLastRead = 0;
void loop()
{

  // Report every 2 seconds.
  if (timeSinceLastRead > 2000)
  {
    // Reading temperature or humidity takes about 250 milliseconds!
    // Sensor readings may also be up to 2 seconds 'old' (its a very slow sensor)
    float h = dht.getHumidity();
    // Read temperature as Celsius (the default)
    float t = dht.getTemperature();
    // Read temperature as Fahrenheit (isFahrenheit = true)
    float f = dht.toFahrenheit(t);
    // Check if any reads failed and exit early (to try again).
    if (isnan(h) || isnan(t) || isnan(f))
    {
      Serial.println("Failed to read from DHT sensor!");
      timeSinceLastRead = 0;
      return;
    }

    // Compute heat index in Fahrenheit (the default)
    float hif = dht.computeHeatIndex(f, h, true);
    // Compute heat index in Celsius (isFahreheit = false)
    float hic = dht.computeHeatIndex(t, h, false);

    Serial.print("Humidity: ");
    Serial.print(h);
    Serial.print(" %\t");
    sendMessage("update", "humidity", String(h), "humidity");
    Serial.print("Temperature: ");
    Serial.print(t);
    Serial.print(" *C ");
    Serial.print(f);
    Serial.print(" *F\t");
    sendMessage("update", "temperature", String(f), "temperature");
    Serial.print("Heat index: ");
    Serial.print(hic);
    Serial.print(" *C ");
    Serial.print(hif);
    Serial.println(" *F");
    sendMessage("update", "temperature", String(hif), "heatindex");
    timeSinceLastRead = 0;
  }
  delay(100);
  timeSinceLastRead += 100;
}
