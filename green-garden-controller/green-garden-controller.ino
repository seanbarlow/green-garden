#include <ESP8266WiFi.h>
#include <ESP8266HTTPClient.h>
#include <WiFiClientSecure.h>
#include <ArduinoJson.h>

const char *ssid = "Barlow1977";
const char *password = "BrynnVan";
//const char *deviceId = "green-garden-controller-backup";
const char *deviceId = "veggie-controller";
// const char *deviceId = "green-garden-controller";
const String HOST_API = "https://192.168.86.36:32790/api/DeviceMessage";
namespace timing
{
  // 1 second
  const long oneSecond = 1000;
    // five seconds
  const long fiveSeconds = oneSecond * 5;
  // ten seconds
  const long tenSeconds = oneSecond * 10;
  // fiftenns seconds
  const long fifteenSeconds = oneSecond * 15;
  // thirty seconds
  const long thirtySeconds = oneSecond * 30;
  // one minute
  const long oneMinute = oneSecond * 60;
  // one minute
  const long fiveMinutes = oneMinute * 5;
    // one minute
  const long thirtyMinutes = oneMinute * 30;
  // 60 minutes
  const long oneHour = oneMinute * 60;
  // 4 hours
  const long fourHours = oneHour * 4;
  // 6 hours
  const long sixHours = oneHour * 6;
  // 6hours
  const long eightHours = oneHour * 6;
  // 16 hours
  const long sixteenHours = oneHour * 16;
  // 18 hours
  const long eighteenHours = oneHour * 18;
  // pump start millis
  unsigned long pumpMillis = 0;
  // time since last change (light on/off)
  unsigned long lightMillis = 0;
  // Last run
  unsigned long lastRun = 0;

} // namespace timing

namespace pin
{
  const int light = 12;
  const int pump = 13;
  const int led = LED_BUILTIN;
} // namespace pin

namespace sensor
{
  bool lightStatus = false;
  bool pumpStatus = false;
} // namespace sensor

namespace config
{
  long pumpOnSeconds = timing::tenSeconds;
  long pumpOffSeconds = timing::oneHour;
  long lightOnSeconds = timing::sixteenHours;
  long lightOffSeconds = timing::eightHours;
  long updateDelay = timing::fiveMinutes;
  long pumpDelay = timing::oneSecond;
} // namespace config

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

void setupLight()
{
  sendMessage("update", "light", "Light is being initialized", "on");
  pinMode(pin::light, OUTPUT);
  // Always try to avoid duplicate code.
  // Instead of writing digitalWrite(pin, LOW) here,
  // call the function off() which already does that
  lightOff();
}

void lightOn()
{
  digitalWrite(pin::light, HIGH);
  sensor::lightStatus = true;
  sendMessage("change", "light", "Light has been turned on", "on");
  Serial.println("Light has been turned on");
}

void lightOff()
{
  digitalWrite(pin::light, LOW);
  sensor::lightStatus = false;
  sendMessage("change", "light", "Light has been turned off", "off");
  Serial.println("Light has been turned off");
}

void checkLights(unsigned long currentMillis)
{
  // check to see if the light is on
  if (sensor::lightStatus)
  {
    if (currentMillis - timing::lightMillis >= config::lightOnSeconds)
    {
      lightOff();
      timing::lightMillis = currentMillis;
    }
    else
    {
      sendMessage("update", "light", "Light is currently on", "on");
    }
  }
  else if (!sensor::lightStatus)
  {
    if (currentMillis - timing::lightMillis >= config::lightOffSeconds)
    {
      lightOn();
      timing::lightMillis = currentMillis;
    }
    else
    {
      sendMessage("update", "light", "Light is currently off", "off");
    }
  }
}

void setupPump()
{
  sendMessage("update", "pump", "Pump is being initialized", "off");
  pinMode(pin::pump, OUTPUT);
  // Always try to avoid duplicate code.
  // Instead of writing digitalWrite(pin, LOW) here,
  // call the function off() which already does that
  pumpOff();
}

void pumpOn()
{
  digitalWrite(pin::pump, HIGH);
  sensor::pumpStatus = true;
  sendMessage("change", "pump", "Pump has been turned on", "on");
  Serial.println("Pump has been turned on");
}

void pumpOff()
{
  digitalWrite(pin::pump, LOW);
  sensor::pumpStatus = false;
  sendMessage("change", "pump", "Pump has been turned off", "off");
  Serial.println("Pump has been turned off");
}

void checkPump(unsigned long currentMillis)
{
  // Pump is on
  if (sensor::pumpStatus)
  {
    // make sure the pump only runs for 4.5 minutes
    if (currentMillis - timing::pumpMillis >= config::pumpOnSeconds)
    {
      pumpOff();
      timing::pumpMillis = currentMillis;
    }
    else
    {
      sendMessage("update", "pump", "Pump is currently on", "on");
    }
  }
  // pump is off
  else if (!sensor::pumpStatus)
  {
    if (currentMillis - timing::pumpMillis >= config::pumpOffSeconds)
    {
      pumpOn();
      timing::pumpMillis = currentMillis;
    }
    else
    {
      sendMessage("update", "pump", "Pump is currently off", "off");
    }
  }
}

void sendMessage(String eventType, String sensorType, String data, String actionType)
{
  // wait for WiFi connection
  if ((WiFi.status() == WL_CONNECTED))
  {
    DynamicJsonDocument doc(2048);
    doc["deviceId"] = deviceId;
    doc["eventType"] = eventType;
    doc["sensorType"] = sensorType;
    doc["actionType"] = actionType;
    doc["data"] = data;

    String json;
    serializeJson(doc, json);

    WiFiClientSecure client; //Declare object of class WiFiClient
    HTTPClient http;

    const char fingerprint[] = "679db33f787f1ea853143b8ce01dd76296774bdd";
    client.setFingerprint(fingerprint);
    Serial.print("[HTTP] begin...\n");
    http.useHTTP10(true);
    // configure traged server and url
    http.begin(client, HOST_API); //HTTP

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
      bool action = resultDoc["action"].as<bool>();
      if (action)
      {
        String sensorType = resultDoc["sensorType"].as<String>();
        String actionType = resultDoc["actionType"].as<String>();
        if (sensorType == "light")
        {
          timing::lightMillis = 0;

          if (actionType == "on")
          {
            lightOn();
          }
          else if (actionType == "off")
          {
            lightOff();
          }
          else if (actionType == "lightonseconds")
          {
            int lightOnSeconds = resultDoc["value"].as<int>() * timing::oneSecond;
            config::lightOnSeconds = lightOnSeconds;
          }
          else if (actionType == "lightoffseconds")
          {
            int lightoffseconds = resultDoc["value"].as<int>() * timing::oneSecond;
            config::lightOffSeconds = lightoffseconds;
          }
        }
        else if (sensorType == "pump")
        {
          timing::pumpMillis = 0;

          if (actionType == "on")
          {
            pumpOn();
          }
          if (actionType == "off")
          {
            pumpOff();
          }
          if (actionType == "pumponseconds")
          {
            int pumpOnSeconds = resultDoc["value"].as<int>() * timing::oneSecond;
            config::pumpOnSeconds = pumpOnSeconds;
          }
          if (actionType == "pumpoffseconds")
          {
            int pumpOffSeconds = resultDoc["value"].as<int>() * timing::oneSecond;
            config::pumpOffSeconds = pumpOffSeconds;
          }
        }
      }
    }
    else
    {
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
  setupLed();
  setupLight();
  lightOn();
  setupPump();
  pumpOn();
}

void loop()
{
  Serial.print("Light Status: ");
  Serial.print(sensor::lightStatus);
  Serial.print("\n");
  Serial.print("Light On Seconds: ");
  Serial.print(config::lightOnSeconds);
  Serial.print("\n");
  Serial.print("Light Off Seconds: ");
  Serial.print(config::lightOffSeconds);
  Serial.print("\n");
  Serial.print("Pump Status: ");
  Serial.print(sensor::pumpStatus);
  Serial.print("\n");
  Serial.print("Pump On Seconds: ");
  Serial.print(config::pumpOnSeconds);
  Serial.print("\n");
  Serial.print("Pump Off Seconds: ");
  Serial.print(config::pumpOffSeconds);
  Serial.print("\n");

  unsigned long currentMillis = millis();
  checkPump(currentMillis);
  if (!sensor::pumpStatus)
  {
    ledOff();
    Serial.print("Pump Status: False\n");
    checkLights(currentMillis);
    delay(config::updateDelay);
  }
  else
  {
    ledOn();
    Serial.print("Pump Status: True\n");
    delay(config::pumpDelay);
  }
}
