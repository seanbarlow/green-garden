#include <ESP8266WiFi.h>
#include <ESP8266HTTPClient.h>
#include <WiFiClientSecure.h>
#include <ArduinoJson.h>

unsigned long currentMillis;

namespace timing
{
  // 1 second
  const long oneSecond = 1000;
  // five seconds
  const long fiveSeconds = oneSecond * 5;
  // seven seconds
  const long sevenSeconds = oneSecond * 7;
  // ten seconds
  const long tenSeconds = oneSecond * 10;
  // fiftenns seconds
  const long fifteenSeconds = oneSecond * 15;
  // twenty seconds
  const long twentySeconds = oneSecond * 20;
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
  unsigned long pump1Millis = 0;
  unsigned long pump2Millis = 0;

} // namespace timing

#define d0 16
#define d1 5
#define d2 4
#define d3 0
#define d4 2
#define d5 14
#define d6 12
#define d7 13
#define d8 15

namespace config
{
  const char *deviceId = "green-garden-pumps-controller";
  const char *ssid = "Barlow1977";
  const char *password = "BrynnVan";
  const char fingerprint[] = "679db33f787f1ea853143b8ce01dd76296774bdd";

  const int pump1 = d7;
  const int pump2 = d6;
  const int led = LED_BUILTIN;

  String hostApi = "https://192.168.86.28:32790/api/DeviceMessage";

  long pumpOnSeconds = timing::thirtySeconds;
  long pumpOffSeconds = timing::fourHours;

  long updateDelay = timing::fiveMinutes;
  long pumpDelay = timing::oneSecond;

} // namespace config

void setupLed()
{
  pinMode(config::led, OUTPUT);
}

void ledOn()
{
  digitalWrite(config::led, LOW);
}

void ledOff()
{
  digitalWrite(config::led, HIGH);
}
String statusString(bool status)
{
  if (status)
  {
    return "on";
  }
  return "off";
}

void setupPump(int pump)
{
  pinMode(pump, OUTPUT);
  sendMessage("update", "pump", "Pump " + String(pump) + " is being initialized", statusString(pumpStatus(pump)));
  // Always try to avoid duplicate code.
  // Instead of writing digitalWrite(pin, LOW) here,
  // call the function off() which already does that
  pumpOff(pump);
}

bool pumpStatus(int pump)
{
  return digitalRead(pump);
}

void pumpOn(int pump)
{
  digitalWrite(pump, HIGH);
  sendMessage("update", "pump", "Pump " + String(pump) + " turned on", statusString(pumpStatus(pump)));
  Serial.println("Pump " + String(pump) + " has been turned on");
}

void pumpOff(int pump)
{
  digitalWrite(pump, LOW);
  sendMessage("update", "pump", "Pump " + String(pump) + " turned off", statusString(pumpStatus(pump)));
  Serial.println("Pump " + String(pump) + " has been turned off");
}

void updatePumpMillis(int pump)
{
  if (pump == d6)
  {
    timing::pump1Millis = currentMillis;
  }
  else
  {
    timing::pump2Millis = currentMillis;
  }
}

void checkPump(int pump)
{
  unsigned long pumpMillis;
  if (pump == d6)
  {
    pumpMillis = timing::pump1Millis;
  }
  else
  {
    pumpMillis = timing::pump2Millis;
  }
  // Pump is on
  if (pumpStatus(pump))
  {
    // make sure the pump only runs for 4.5 minutes
    if (currentMillis - pumpMillis >= config::pumpOnSeconds)
    {
      pumpOff(pump);
      updatePumpMillis(pump);
    }
    else
    {
      sendMessage("update", "pump", "Pump " + String(pump) + " is currently on", statusString(pumpStatus(pump)));
    }
  }
  // pump is off
  else
  {
    if (currentMillis - pumpMillis >= config::pumpOffSeconds)
    {
      pumpOn(pump);
      updatePumpMillis(pump);
    }
    else
    {
      sendMessage("update", "pump", "Pump " + String(pump) + " is currently off", statusString(pumpStatus(pump)));
    }
  }
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
    http.begin(client, config::hostApi); //HTTP

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
      bool action = resultDoc["action"].as<bool>();
      if (action)
      {
        String sensorType = resultDoc["sensorType"].as<String>();
        Serial.print("Sensor Type: ");
        Serial.print(sensorType);
        Serial.print("\n");
        String actionType = resultDoc["actionType"].as<String>();
        Serial.print("Action Type: ");
        Serial.print(actionType);
        Serial.print("\n");
        if (sensorType == "pump")
        {
          if (actionType == "on")
          {
          }
          if (actionType == "off")
          {
          }
          if (actionType == "pumponseconds")
          {
            int pumpOnSeconds = resultDoc["value"].as<int>() * timing::oneSecond;
            config::pumpOnSeconds = pumpOnSeconds;
            Serial.print("Updating pump on seconds to: ");
            Serial.print(config::pumpOnSeconds);
            Serial.print("\n");
          }
          if (actionType == "pumpoffseconds")
          {
            int pumpOffSeconds = resultDoc["value"].as<int>() * timing::oneSecond;
            config::pumpOffSeconds = pumpOffSeconds;
            Serial.print("Updating pump off seconds to: ");
            Serial.print(config::pumpOffSeconds);
            Serial.print("\n");
          }
        }
      }
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
  setupPump(config::pump1);
  setupPump(config::pump2);
  pumpOn(config::pump1);
  pumpOn(config::pump2);
}

void loop()
{
  Serial.print("Pump 1 Status: ");
  Serial.print(pumpStatus(config::pump1));
  Serial.print("\n");
  Serial.print("Pump 2 Status: ");
  Serial.print(pumpStatus(config::pump2));
  Serial.print("\n");
  Serial.print("Pump On Seconds: ");
  Serial.print(config::pumpOnSeconds);
  Serial.print("\n");
  Serial.print("Pump Off Seconds: ");
  Serial.print(config::pumpOffSeconds);
  Serial.print("\n");

  currentMillis = millis();
  checkPump(config::pump1);
  checkPump(config::pump2);
  if (!pumpStatus(config::pump1) && !pumpStatus(config::pump2))
  {
    ledOff();
    delay(config::updateDelay);
  }
  else
  {
    ledOn();
    delay(config::pumpDelay);
  }
}
