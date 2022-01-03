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
  // 50 sconds
  const long fiftySeconds = thirtySeconds + twentySeconds;
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
  // 8 hours
  const long eightHours = oneHour * 8;
  // 12 hours
  const long twelveHours = oneHour * 12;
  // 16 hours
  const long sixteenHours = oneHour * 16;
  // 18 hours
  const long eighteenHours = oneHour * 18;
  // 20 hours
  const long twentyHours = oneHour * 20;
  // pump start millis
  unsigned long pumpMillis = 0;
  // time since last change (light on/off)
  unsigned long lightMillis = 0;
  // Last run
  unsigned long lastRun = 0;

} // namespace timing

namespace config
{
  const char *deviceId = "green-garden-controller";
  const char *ssid = "SlowWiFi";
  const char *password = "BrynnVan";
  const char fingerprint[] = "679db33f787f1ea853143b8ce01dd76296774bdd";

  const int light = 12;
  const int pump = 13;
  const int led = LED_BUILTIN;

  String hostApi = "https://192.168.86.28:32790/api/DeviceMessage";

  long pumpOnSeconds = timing::fifteenSeconds;
  long pumpOffSeconds = timing::twentyHours;
  long lightOnSeconds = timing::sixteenHours;
  long lightOffSeconds = timing::eightHours;

  long updateDelay = timing::fiveMinutes;
  long pumpDelay = timing::oneSecond / 10;

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

void setupLight()
{
  pinMode(config::light, OUTPUT);
  sendMessage("update", "light", "Light is being initialized", statusString(lightStatus()));
  // Always try to avoid duplicate code.
  // Instead of writing digitalWrite(pin, LOW) here,
  // call the function off() which already does that
  lightOff();
}

bool lightStatus()
{
  return digitalRead(config::light);
}

String statusString(bool status)
{
  if (status)
  {
    return "on";
  }
  return "off";
}

void lightOn()
{
  digitalWrite(config::light, HIGH);
  sendMessage("change", "light", "Light has been turned on", statusString(lightStatus()));
  Serial.println("\nLight has been turned on\n");
}

void lightOff()
{
  digitalWrite(config::light, LOW);
  sendMessage("change", "light", "Light has been turned off", statusString(lightStatus()));
  Serial.println("\nLight has been turned off\n");
}

void checkLights()
{
  Serial.print("Light Status: ");
  Serial.print(lightStatus());
  Serial.print("\n");
  Serial.print("current :");
  Serial.print(currentMillis);
  Serial.print("\n");
  Serial.print("on :");
  Serial.print(config::lightOnSeconds);
  Serial.print("\n");
  Serial.print("on :");
  Serial.print(config::lightOffSeconds);
  Serial.print("\n");
  Serial.print("light :");
  Serial.print(timing::lightMillis);
  Serial.print("\n");
  // check to see if the light is on
  Serial.print(currentMillis - timing::lightMillis >= config::lightOnSeconds);
  Serial.print("\n");
  Serial.print((currentMillis - timing::lightMillis) >= config::lightOffSeconds);
  Serial.print("\n");
  Serial.print(currentMillis - timing::lightMillis);
  Serial.print("\n");
  if (lightStatus())
  {
    if ((currentMillis - timing::lightMillis) >= config::lightOnSeconds)
    {
      Serial.print("Calling light off");
      Serial.print("\n");
      lightOff();
      timing::lightMillis = currentMillis;
    }
    else
    {
      sendMessage("update", "light", "Light is currently on", statusString(lightStatus()));
    }
  }
  else
  {
    if ((currentMillis - timing::lightMillis) >= config::lightOffSeconds)
    {
      Serial.print("Calling light on");
      Serial.print("\n");
      lightOn();
      timing::lightMillis = currentMillis;
    }
    else
    {
      sendMessage("update", "light", "Light is currently off", statusString(lightStatus()));
    }
  }
}

void setupPump()
{
  pinMode(config::pump, OUTPUT);
  sendMessage("update", "pump", "Pump is being initialized", statusString(pumpStatus()));
  // Always try to avoid duplicate code.
  // Instead of writing digitalWrite(pin, LOW) here,
  // call the function off() which already does that
  pumpOff();
}

bool pumpStatus()
{
  return digitalRead(config::pump);
}

void pumpOn()
{
  digitalWrite(config::pump, HIGH);
  sendMessage("update", "pump", "Pump turned on", statusString(pumpStatus()));
  Serial.println("Pump has been turned on");
}

void pumpOff()
{
  digitalWrite(config::pump, LOW);
  sendMessage("update", "pump", "Pump turned off", statusString(pumpStatus()));
  Serial.println("Pump has been turned off");
}

void checkPump()
{
  // Pump is on
  if (pumpStatus())
  {
    // make sure the pump only runs for 4.5 minutes
    if (currentMillis - timing::pumpMillis >= config::pumpOnSeconds)
    {
      pumpOff();
      timing::pumpMillis = currentMillis;
    }
    else
    {
      sendMessage("update", "pump", "Pump is currently on", statusString(pumpStatus()));
    }
  }
  // pump is off
  else
  {
    if (currentMillis - timing::pumpMillis >= config::pumpOffSeconds)
    {
      pumpOn();
      timing::pumpMillis = currentMillis;
    }
    else
    {
      sendMessage("update", "pump", "Pump is currently off", statusString(pumpStatus()));
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
        if (sensorType == "light")
        {
          if (actionType == "on")
          {
            lightOn();
            timing::lightMillis = currentMillis;
          }
          else if (actionType == "off")
          {
            lightOff();
            timing::lightMillis = currentMillis;
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
          if (actionType == "on")
          {
            pumpOn();
            timing::lightMillis = currentMillis;
          }
          if (actionType == "off")
          {
            pumpOff();
            timing::lightMillis = currentMillis;
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
  setupLight();
  lightOn();
  setupPump();
  pumpOn();
}

void loop()
{
  Serial.print("Light Status: ");
  Serial.print(lightStatus());
  Serial.print("\n");
  Serial.print("Light On Seconds: ");
  Serial.print(config::lightOnSeconds);
  Serial.print("\n");
  Serial.print("Light Off Seconds: ");
  Serial.print(config::lightOffSeconds);
  Serial.print("\n");
  Serial.print("Pump Status: ");
  Serial.print(pumpStatus());
  Serial.print("\n");
  Serial.print("Pump On Seconds: ");
  Serial.print(config::pumpOnSeconds);
  Serial.print("\n");
  Serial.print("Pump Off Seconds: ");
  Serial.print(config::pumpOffSeconds);
  Serial.print("\n");

  currentMillis = millis();
  checkPump();
  Serial.print("Pump Status: ");
  Serial.print(pumpStatus());
  Serial.print("\n");
  if (!pumpStatus())
  {
    ledOff();
    checkLights();
    delay(config::updateDelay);
  }
  else
  {
    ledOn();
    delay(config::pumpDelay);
  }
}
