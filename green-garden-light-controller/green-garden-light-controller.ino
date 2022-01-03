
#include <ESP8266HTTPClient.h>
#include <ArduinoJson.h>

unsigned long currentMillis;

namespace timing
{
  // 1 second
  const long oneSecond = 1000;
  // fifteen seconds
  const long fifteenSeconds = oneSecond * 15;
  const long oneMinute = oneSecond * 60;
  const long fiveMinutes = oneMinute * 5;
  const long oneHour = oneMinute * 60;

  // 6 hours
  const long sixHours = oneHour * 6;
  // 12 hours
  const long twelveHours = oneHour * 12;
  // 18 hours
  const long eighteenHours = oneHour * 18;
  // ptime since last change (light on/off)
  unsigned long upperLight = 0;
  // time since last change (light on/off)
  unsigned long lowerLight = 0;
  long lightMillis = 0;
  // Last run
  unsigned long lastRun = 0;

} // namespace timing

namespace config
{
  const char *deviceId = "green-garden-lights-controller";
  const char *ssid = "SlowWiFi";
  const char *password = "BrynnVan";
  const char fingerprint[] = "679db33f787f1ea853143b8ce01dd76296774bdd";

  const int upperLight = 12;
  const int lowerLight = 13;
  const int led = LED_BUILTIN;

  String hostApi = "https://192.168.86.28:32790/api/DeviceMessage";

  //  long lightsOnSeconds = timing::eighteenHours;
  //  long lightsOffSeconds = timing::sixHours;
  //  long updateDelay = timing::fiveMinutes;

  long lightsOnSeconds = timing::twelveHours;
  long lightsOffSeconds = timing::twelveHours;
  long updateDelay = timing::fifteenSeconds;

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
  pinMode(config::upperLight, OUTPUT);
  pinMode(config::lowerLight, OUTPUT);
  // sendMessage("update", "light", "Light is being initialized", statusString(lightStatus(config::upperLight)));
  // sendMessage("update", "light", "Light is being initialized", statusString(lightStatus(config::lowerLight)));
  //  Always try to avoid duplicate code.
  //  Instead of writing digitalWrite(pin, LOW) here,
  //  call the function off() which already does that
  lightOff(config::upperLight);
  lightOff(config::lowerLight);
}

bool lightStatus(long light)
{
  return digitalRead(light);
}

String statusString(bool status)
{
  if (status)
  {
    return "on";
  }
  return "off";
}

void lightOn(long light)
{
  digitalWrite(light, HIGH);
  // sendMessage("change", "light", "Light has been turned on", statusString(lightStatus(light)));
  Serial.println("\nLight has been turned on\n");
}

void lightOff(long light)
{
  digitalWrite(light, LOW);
  // sendMessage("change", "light", "Light has been turned off", statusString(lightStatus(light))));
  Serial.println("\nLight has been turned off\n");
}

void checkLights(long light)
{
  currentMillis = millis();
  Serial.print("Light Status: ");
  Serial.print(lightStatus(light));
  Serial.print("\n");
  Serial.print("current :");
  Serial.print(currentMillis);
  Serial.print("\n");
  Serial.print("on :");
  Serial.print(config::lightsOnSeconds);
  Serial.print("\n");
  Serial.print("on :");
  Serial.print(config::lightsOffSeconds);
  Serial.print("\n");
  Serial.print("light :");
  Serial.print(timing::lightMillis);
  Serial.print("\n");
  // check to see if the light is on
  Serial.print(currentMillis - timing::lightMillis >= config::lightsOnSeconds);
  Serial.print("\n");
  Serial.print((currentMillis - timing::lightMillis) >= config::lightsOffSeconds);
  Serial.print("\n");
  Serial.print(currentMillis - timing::lightMillis);
  Serial.print("\n");
  if (lightStatus(light))
  {
    if ((currentMillis - timing::lightMillis) >= config::lightsOnSeconds)
    {
      Serial.print("Calling light off");
      Serial.print("\n");
      lightOff(config::upperLight);
      lightOff(config::lowerLight);
      timing::lightMillis = currentMillis;
    }
    else
    {
      // sendMessage("update", "light", "Light is currently on", statusString(lightStatus()));
    }
  }
  else
  {
    if ((currentMillis - timing::lightMillis) >= config::lightsOffSeconds)
    {
      Serial.print("Calling light on");
      Serial.print("\n");
      lightOn(config::upperLight);
      lightOn(config::lowerLight);
      timing::lightMillis = currentMillis;
    }
    else
    {
      // sendMessage("update", "light", "Light is currently off", statusString(lightStatus(light))));
    }
  }
}

// void sendMessage(String eventType, String sensorType, String data, String actionType)
//{
//   // wait for WiFi connection
//   if ((WiFi.status() == WL_CONNECTED))
//   {
//     DynamicJsonDocument doc(2048);
//     doc["deviceId"] = config::deviceId;
//     doc["eventType"] = eventType;
//     doc["sensorType"] = sensorType;
//     doc["actionType"] = actionType;
//     doc["data"] = data;
//
//     String json;
//     serializeJson(doc, json);
//
//     WiFiClientSecure client; //Declare object of class WiFiClient
//     HTTPClient http;
//
//     client.setFingerprint(config::fingerprint);
//     Serial.print("[HTTP] begin...\n");
//     http.useHTTP10(true);
//     // configure traged server and url
//     http.begin(client, config::hostApi); //HTTP
//
//     http.addHeader("Content-Type", "application/json");
//
//     Serial.print("[HTTP] POST BEGIN...\n");
//     Serial.print(json);
//     Serial.print("\n");
//     // start connection and send HTTP header and body
//     int httpCode = http.POST(json);
//     Serial.printf("[HTTP] POST... code: %d\n", httpCode);
//     Serial.print("[HTTP] POST END\n");
//     // file found at server
//     if (httpCode == HTTP_CODE_OK)
//     {
//       DynamicJsonDocument resultDoc(2048);
//       Serial.print("[HTTP] RESPONSE BEGIN\n");
//       deserializeJson(resultDoc, http.getStream());
//       String output;
//       serializeJson(resultDoc, output);
//       Serial.print(output);
//       Serial.print("\n");
//       Serial.print("[HTTP] RESPONSE END\n");
//       http.end();
//       bool action = resultDoc["action"].as<bool>();
//       if (action)
//       {
//         String sensorType = resultDoc["sensorType"].as<String>();
//         Serial.print("Sensor Type: ");
//         Serial.print(sensorType);
//         Serial.print("\n");
//         String actionType = resultDoc["actionType"].as<String>();
//         Serial.print("Action Type: ");
//         Serial.print(actionType);
//         Serial.print("\n");
//
//         if (actionType == "on")
//         {
//           lightOn();
//           timing::lightMillis = currentMillis;
//         }
//         else if (actionType == "off")
//         {
//           lightOff();
//           timing::lightMillis = currentMillis;
//         }
//         else if (actionType == "lightonseconds")
//         {
//           int lightOnSeconds = resultDoc["value"].as<int>() * timing::oneSecond;
//           config::lightOnSeconds = lightOnSeconds;
//         }
//         else if (actionType == "lightoffseconds")
//         {
//           int lightoffseconds = resultDoc["value"].as<int>() * timing::oneSecond;
//           config::lightOffSeconds = lightoffseconds;
//         }
//       }
//     }
//     else
//     {
//       Serial.printf("[HTTP] POST... failed, error: %s\n", http.errorToString(httpCode).c_str());
//     }
//   }
// }

void setup()
{

  Serial.begin(115200);
  // WiFi.begin(config::ssid, config::password);

  // while (WiFi.status() != WL_CONNECTED)
  // {
  //   delay(1000);
  //   Serial.println("Connecting...");
  // }

  Serial.println("Initializing..."); // Test the serial monitor
  setupLed();
  setupLight();
  lightOn(config::upperLight);
  lightOn(config::lowerLight);
}

void lightStatus(long light, String description)
{
  Serial.print(description);
  Serial.print(" Light Status Upper: ");
  Serial.print(lightStatus(config::upperLight));
  Serial.print("\n");
  Serial.print(" Light Status Lower: ");
  Serial.print(lightStatus(config::lowerLight));
  Serial.print("\n");
  Serial.print("Light On Seconds: ");
  Serial.print(config::lightsOnSeconds);
  Serial.print("\n");
  Serial.print("Light Off Seconds: ");
  Serial.print(config::lightsOffSeconds);
  Serial.print("\n");
}

void loop()
{

  lightStatus(config::upperLight);
  lightStatus(config::lowerLight);
  ledOff();
  checkLights(config::upperLight);
  delay(config::updateDelay);
}
