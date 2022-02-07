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
  const long twentyFourHours = oneHour * 24;
  const long twentyThreeHoursFortyFiveMinutes = (oneHour * 24) + (oneMinute * 45);
  const long thirtySixHours = eighteenHours * 2;
  // pump start millis
  unsigned long pumpMillis = 0;

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
  const char *deviceId = "1";
  const char *ssid = "slowWiFi";
  const char *password = "BrynnVan";
  const char fingerprint[] = "679db33f787f1ea853143b8ce01dd76296774bdd";

  const int pump1 = d7;
  const int pump2 = d6;
  const int led = LED_BUILTIN;

  String hostApi = "https://192.168.86.28:32790/1";

  long pumpOnSeconds = timing::twentySeconds;
  long pumpOffSeconds = timing::twentyFourHours;
  
  long updateDelay = timing::thirtySeconds;
  long pumpDelay = timing::oneSecond;
  bool pumpStatus = false;
  bool ledStatus = false;

} // namespace config

void setupLed()
{
  pinMode(config::led, OUTPUT);
  ledOff();
}

void ledOn()
{
  digitalWrite(config::led, LOW);
  config::ledStatus = true;
}

void ledOff()
{
  digitalWrite(config::led, HIGH);
  config::ledStatus = false;
}

String pumpStatus(bool status)
{
  if (status)
  {
    return "on";
  }
  return "off";
}


bool shouldWater() {
    // wait for WiFi connection
  if ((WiFi.status() == WL_CONNECTED))
  {
    WiFiClientSecure client; //Declare object of class WiFiClient
    HTTPClient http;

    client.setFingerprint(config::fingerprint);
    Serial.print("[HTTP] begin...\n");
    http.useHTTP10(true);
    // configure traged server and url
    http.begin(client, config::hostApi); //HTTP

    http.addHeader("Content-Type", "application/json");

    Serial.print("[HTTP] GET BEGIN...\n");
    // start connection and send HTTP header and body
    int httpCode = http.GET();
    Serial.printf("[HTTP] GET... code: %d\n", httpCode);
    Serial.print("[HTTP] GET END\n");
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
      return resultDoc["action"].as<bool>();
    }
  }
}

void sendMessage(String eventType, String sensorType, String data, String actionType)
{
  // wait for WiFi connection
  if ((WiFi.status() == WL_CONNECTED))
  {
    DynamicJsonDocument doc(2048);
    doc["status"] = data;

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
void pumpOn(int pump)
{
  digitalWrite(pump, LOW);
  config::pumpStatus = true;
  sendMessage("update", "pump", "Pump " + String(pump) + " turned on", pumpStatus(pump));
}

void pumpOff(int pump)
{
  digitalWrite(pump, HIGH);
  config::pumpStatus = false;
  sendMessage("update", "pump", "Pump " + String(pump) + " turned off", pumpStatus(pump));
}

void setupPump(int pump)
{
  pinMode(pump, OUTPUT);
  pumpOff(config::pump1);
}

void updatePumpMillis(int pump)
{
  timing::pumpMillis = currentMillis;
}

void checkPump(int pump)
{
  unsigned long pumpMillis = timing::pumpMillis;
  unsigned long pumpOnSeconds = config::pumpOnSeconds;
  unsigned long pumpOffSeconds = config::pumpOffSeconds;
  
  Serial.print("Pump Status: ");
  Serial.print(pumpStatus(config::pumpStatus));
  Serial.print(" - " + String(currentMillis) + " - " + String(pumpMillis) + " " + String(timing::twentyThreeHoursFortyFiveMinutes));
  Serial.print("\n");
  Serial.print("On Seconds: " + String(pumpOnSeconds) + " Off Seconds: " + String(pumpOffSeconds));
  Serial.print("\n");
  Serial.print(currentMillis - pumpMillis);
  Serial.print("\n");
  
  // Pump is on
  if (config::pumpStatus)
  {
    Serial.print("Pump is on");
    Serial.print("\n");
    if (currentMillis - pumpMillis >= pumpOnSeconds)
    {
      Serial.print("Pump is turning off");
      Serial.print("\n");
      pumpOff(pump);
      updatePumpMillis(pump);
      return;
    }
    else
    {
      // sendMessage("update", "pump", "Pump " + String(pump) + " is currently on", statusString(pumpStatus(pump)));
    }
  }
  // pump is off
  else if(!config::pumpStatus)
  {
    Serial.print("Pump is off");
    Serial.print("\n");
    if (currentMillis - pumpMillis >= pumpOffSeconds)
    {
      Serial.print("Pump is turning on");
      Serial.print("\n");
      pumpOn(pump);
      updatePumpMillis(pump);
      return;
    }
    else
    {
      // sendMessage("update", "pump", "Pump " + String(pump) + " is currently off", statusString(pumpStatus(pump)));
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
  delay(2000);
  timing::pumpMillis = timing::twentyThreeHoursFortyFiveMinutes;
  // timing::pump1Millis = timing::thirtySeconds;
  Serial.println("Initializing..."); //Test the serial monitor
  setupLed();
  setupPump(config::pump1);
  Serial.print("Pump Status: ");
}

void loop()
{
  Serial.print("\n");
  currentMillis = millis();
  checkPump(config::pump1);
  delay(config::pumpDelay);
  if (config::pumpStatus)
  {
    Serial.print("Led On");
    Serial.print("\n");
    ledOn();
    delay(config::pumpDelay);
  }
  else if(!config::pumpStatus)
  {
    Serial.print("Led Off");
    Serial.print("\n");
    ledOff();
    //delay(config::updateDelay);
    delay(config::pumpDelay);
  }
}
