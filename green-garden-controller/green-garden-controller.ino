#include <ESP8266WiFi.h>
#include <ESP8266HTTPClient.h>
#include <ArduinoJson.h>

const char *ssid = "Barlow1977";
const char *password = "BrynnVan";
const char *deviceId = "green-garden-controller";

namespace timing
{
  // 1 second
  const long oneSecond = 1000;
  // one minute
  const long oneMinute = oneSecond * 60;
  // 60 minutes
  const long oneHour = oneMinute * 60;
  // 6 hours
  const long sixHours = oneHour * 6;
  // 14 hours
  const long eighteenHours = oneHour * 19;

  // pump start millis
  unsigned long pumpMillis = 0;

  // time since last change (light on/off)
  unsigned long lightMillis = 0;

  // Last run
  unsigned long lastRun = 0;

} // namespace timing

namespace pin
{
  const int light = 13;
  const int pump = 12;
} // namespace pin

namespace sensor
{
  bool lightStatus = false;
  bool pumpStatus = false;
} // namespace sensor

void setupLight()
{
  pinMode(pin::light, OUTPUT);
  // Always try to avoid duplicate code.
  // Instead of writing digitalWrite(pin, LOW) here,
  // call the function off() which already does that
  lightOff();
}

void lightOn()
{
  digitalWrite(pin::light, LOW);
  sensor::lightStatus = true;
  Serial.println("Light has been turned on");
}

void lightOff()
{
  digitalWrite(pin::light, HIGH);
  sensor::lightStatus = false;
  Serial.println("Light has been turned off");
}

void checkLights(unsigned long currentMillis)
{
  // check to see if the light is on
  if (sensor::lightStatus)
  {
    if (currentMillis - timing::lightMillis >= timing::eighteenHours)
    {
      lightOff();
      timing::lightMillis = currentMillis;
    }
  }
  else if (!sensor::lightStatus)
  {
    if (currentMillis - timing::lightMillis >= timing::sixHours)
    {
      lightOn();
      timing::lightMillis = currentMillis;
    }
  }
}

void setupPump()
{
  pinMode(pin::pump, OUTPUT);
  // Always try to avoid duplicate code.
  // Instead of writing digitalWrite(pin, LOW) here,
  // call the function off() which already does that
  pumpOff();
}

void pumpOn()
{
  digitalWrite(pin::pump, LOW);
  sensor::pumpStatus = true;
  Serial.println("Pump has been turned on");
}

void pumpOff()
{
  digitalWrite(pin::pump, HIGH);
  sensor::pumpStatus = false;
  Serial.println("Pump has been turned off");
}

void checkPump(unsigned long currentMillis)
{
  // Pump is on
  if (sensor::pumpStatus)
  {
    // make sure the pump only runs for 4.5 minutes
    if (currentMillis - timing::pumpMillis >= timing::oneMinute)
    {
      pumpOff();
      timing::pumpMillis = currentMillis;
    }
  }
  // pump is off
  else if (!sensor::pumpStatus)
  {
    if (currentMillis - timing::pumpMillis >= timing::sixHours)
    {
      pumpOn();
      timing::pumpMillis = currentMillis;
    }
  }
}

void sendMessage(){
    // wait for WiFi connection
  if ((WiFi.status() == WL_CONNECTED)) {

    WiFiClient client;
    HTTPClient http;

    Serial.print("[HTTP] begin...\n");
    // configure traged server and url
    http.begin(client, "http://" SERVER_IP "/postplain/"); //HTTP
    http.addHeader("Content-Type", "application/json");

    Serial.print("[HTTP] POST...\n");
    // start connection and send HTTP header and body
    int httpCode = http.POST("{\"hello\":\"world\"}");

    // httpCode will be negative on error
    if (httpCode > 0) {
      // HTTP header has been send and Server response header has been handled
      Serial.printf("[HTTP] POST... code: %d\n", httpCode);

      // file found at server
      if (httpCode == HTTP_CODE_OK) {
        const String& payload = http.getString();
        Serial.println("received payload:\n<<");
        Serial.println(payload);
        Serial.println(">>");
      }
    } else {
      Serial.printf("[HTTP] POST... failed, error: %s\n", http.errorToString(httpCode).c_str());
    }

    http.end();
  }
}

void setup()
{

  Serial.begin(115200);
  WiFi.begin(ssid, password);

  while (WiFi.status() != WL_CONNECTED)
  {
    delay(1000);
    Serial.println("Connecting...");
  }

  Serial.println("Initializing..."); //Test the serial monitor
  setupLight();
  lightOn();
  setupPump();
  pumpOn();
}

void loop()
{
  unsigned long currentMillis = millis();
  checkLights(currentMillis);
  checkPump(currentMillis);

  if (WiFi.status() == WL_CONNECTED)
  {
    WiFiClient client;
    HTTPClient http; //Object of class HTTPClient
        if (http.begin(client, "http://jigsaw.w3.org/HTTP/connection.html")) {  // HTTP


      Serial.print("[HTTP] GET...\n");
      // start connection and send HTTP header
      int httpCode = http.GET();

      // httpCode will be negative on error
      if (httpCode > 0) {
        // HTTP header has been send and Server response header has been handled
        Serial.printf("[HTTP] GET... code: %d\n", httpCode);

        // file found at server
        if (httpCode == HTTP_CODE_OK || httpCode == HTTP_CODE_MOVED_PERMANENTLY) {
          String payload = http.getString();
          Serial.println(payload);
        }
      } else {
        Serial.printf("[HTTP] GET... failed, error: %s\n", http.errorToString(httpCode).c_str());
      }

      http.end();
    } else {
      Serial.printf("[HTTP} Unable to connect\n");
    }
    
  }
  delay(60000);
}
